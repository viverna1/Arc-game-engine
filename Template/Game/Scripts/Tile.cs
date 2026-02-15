using Arc.Core;
using Arc.Components;
using SFML.Graphics;
using System;

class Tile : Component, ITickable
{
    public Tile? topTile;
    public Tile? rightTile;
    public Tile? bottomTile;
    public Tile? leftTile;

    public TileType tileType;
    public float temperature = 0;
    public float heatCapacity = 1f;
    private SpriteRenderer sprite = null!;

    public override void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public override void Update(float deltaTime)
    {
        sprite.FillColor = TemperatureToColor(temperature);
    }

    public void OnTick()
    {
        float transferRate = 0.1f;
        
        TransferHeatTo(topTile, transferRate);
        TransferHeatTo(rightTile, transferRate);
        TransferHeatTo(bottomTile, transferRate);
        TransferHeatTo(leftTile, transferRate);
    }

    private void TransferHeatTo(Tile? neighbour, float rate)
    {
        if (neighbour == null) return;
        
        // Разница температур
        float diff = temperature - neighbour.temperature;
        
        // Количество передаваемого тепла
        float transfer = diff * rate;
        
        // Учитываем теплоемкость
        float tempChange = transfer / heatCapacity;
        float neighbourChange = transfer / neighbour.heatCapacity;
        
        temperature -= tempChange;
        neighbour.temperature += neighbourChange;
    }

private Color TemperatureToColor(float temp)
{
    // Нормализуем температуру в диапазон 0-1
    // -273°C (светло-голубой) -> 25°C (зелёный) -> 1000°C+ (белый)
    float normalizedTemp = Math.Clamp((temp + 273f) / 1273f, 0f, 1f);
    
    byte r, g, b;
    
    if (normalizedTemp < 0.234f) // -273°C до 25°C (светло-голубой -> зелёный)
    {
        float t = normalizedTemp / 0.234f;
        r = (byte)(173 + (0 - 173) * t);
        g = (byte)(216 + (255 - 216) * t);
        b = (byte)(255 + (100 - 255) * t);
    }
    else if (normalizedTemp < 0.393f) // 25°C до 200°C (зелёный -> жёлто-зелёный)
    {
        float t = (normalizedTemp - 0.234f) / 0.159f;
        r = (byte)(0 + (128 - 0) * t);
        g = 255;
        b = (byte)(100 + (0 - 100) * t);
    }
    else if (normalizedTemp < 0.589f) // 200°C до 500°C (жёлто-зелёный -> жёлтый)
    {
        float t = (normalizedTemp - 0.393f) / 0.196f;
        r = (byte)(128 + (255 - 128) * t);
        g = 255;
        b = 0;
    }
    else if (normalizedTemp < 0.746f) // 500°C до 750°C (жёлтый -> оранжевый)
    {
        float t = (normalizedTemp - 0.589f) / 0.157f;
        r = 255;
        g = (byte)(255 + (140 - 255) * t);
        b = 0;
    }
    else if (normalizedTemp < 0.863f) // 750°C до 900°C (оранжевый -> красный)
    {
        float t = (normalizedTemp - 0.746f) / 0.117f;
        r = 255;
        g = (byte)(140 + (0 - 140) * t);
        b = 0;
    }
    else // 900°C до 1000°C+ (красный -> белый)
    {
        float t = (normalizedTemp - 0.863f) / 0.137f;
        r = 255;
        g = (byte)(0 + (255 - 0) * t);
        b = (byte)(0 + (255 - 0) * t);
    }
    
    return new Color(r, g, b);
}
}

public enum TileType
{
    Solid,
    Liquid,
    Laseous
}