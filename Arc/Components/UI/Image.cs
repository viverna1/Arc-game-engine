using System;
using Arc.Core;
using SFML.Graphics;
using SFML.System;

namespace Arc.Components.UI;

public class Image : IRenderable
{
    public override short ZLayer { get; set; } = 1000;
    public override bool IsUI { get; set; } = true;
    public Color FillColor = Color.White;
    public Texture Texture;
    
    private RectTransform rect;
    private RectangleShape _shape = new();
    
    public override void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
        if (rect == null)
        {
            throw new InvalidOperationException(
                $"Image requires RectTransform on GameObject '{gameObject.Name}'");
        }
    }
    
    public override void Update(float deltaTime)
    {
        _shape.Position = rect.GetCenter();
        Vector2f rectSize = rect.GetSize();
        _shape.Size = rectSize;
        _shape.Rotation = rect.Rotation;
        
        _shape.Origin = new Vector2f(
            rect.Pivot.X * rectSize.X,
            rect.Pivot.Y * rectSize.Y
        );
        
        _shape.FillColor = FillColor;
        
        if (Texture != null)
        {
            _shape.Texture = Texture;
        }
    }
    
    public override void Draw(RenderWindow window)
    {
        window.Draw(_shape);
    }
}