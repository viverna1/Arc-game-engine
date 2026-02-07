using System;
using Arc.Core;
using SFML.Graphics;
using SFML.System;

namespace Arc.Components;

public class SpriteRenderer : Renderer
{
    public Color FillColor = Color.White;
    public override short ZLayer { get; set; } = 0;
    public string SpritePath;
    public Texture Texture;

    private RectangleShape _shape = new();
    
    public override void Start()
    {
        if (gameObject.GetComponent<Transform>() == null)
        {
            throw new InvalidOperationException(
                $"SpriteRenderer requires Transform on GameObject '{gameObject.Name}'");
        }

        var transform = gameObject.transform!; // Получаем Transform
        _shape.Position = transform.Position;
        _shape.Size = transform.Size;
        _shape.FillColor = FillColor;
    }
    
    public override void Update(float deltaTime)
    {
        // Обновляем позицию, если Transform изменился
        var transform = gameObject.transform!;
        _shape.Position = transform.Position;
        _shape.Rotation = transform.Rotation;
        _shape.Size = transform.Size;
        _shape.FillColor = FillColor;
    }
    
    public override void Draw(RenderWindow window)
    {
        window.Draw(_shape);
    }

    public void SetOrigin(Vector2f pos)
    {
        _shape.Origin = pos;
    }
}