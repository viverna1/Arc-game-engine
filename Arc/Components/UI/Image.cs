using System;
using Arc.Core;
using Arc.System;
using SFML.Graphics;
using SFML.System;

namespace Arc.Components.UI;

public class Image : Renderer
{
    public Color FillColor = Color.White;
    public override short ZLayer { get; set; } = 1000;
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
        
        // Origin
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
        var oldView = window.GetView();
        
        // Используем готовый UI View из Window
        window.SetView(Window.UIView);
        
        window.Draw(_shape);
        window.SetView(oldView);
    }
}