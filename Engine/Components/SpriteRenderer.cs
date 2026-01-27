using System;
using Engine.Core;
using SFML.Graphics;

namespace Engine.Components;

public class SpriteRenderer : Component
{
    public Color FillColor = Color.White;
    public short ZLayer = 0;

    private RectangleShape _shape = new();
    
    public override void Start()
    {
        if (GameObject.GetComponent<Transform>() == null)
        {
            throw new InvalidOperationException(
                $"SpriteRenderer requires Transform on GameObject '{GameObject.Name}'");
        }

        var transform = GameObject.Transform!; // Получаем Transform
        _shape.Position = transform.Position;
        _shape.Size = transform.Size;
        _shape.FillColor = FillColor;
    }
    
    public override void Update(float deltaTime)
    {
        // Обновляем позицию, если Transform изменился
        var transform = GameObject.Transform!;
        _shape.Position = transform.Position;
        _shape.Rotation = transform.Rotation;
        _shape.FillColor = FillColor;
    }
    
    public void Draw(RenderWindow window)
    {
        window.Draw(_shape);
    }
}