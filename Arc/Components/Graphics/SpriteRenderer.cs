using System;
using Arc.Core;
using SFML.Graphics;

namespace Arc.Components;

public class SpriteRenderer : IRenderable
{
    public override short ZLayer { get; set; } = 0;
    public override bool IsUI { get; set; } = false;
    public Color FillColor = Color.White;
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

        _shape.Position = transform.Position;
        _shape.Size = transform.Size;
        _shape.FillColor = FillColor;
    }
    
    public override void Update(float deltaTime)
    {
        _shape.Position = transform.Position;
        _shape.Rotation = transform.Rotation;
        _shape.Size = transform.Size;
        _shape.FillColor = FillColor;
    }
    
    public override void Draw(RenderWindow window)
    {
        window.Draw(_shape);
    }
}