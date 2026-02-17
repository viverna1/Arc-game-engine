using SFML.System;
using SFML.Graphics;
using Arc.Core;
using System;

namespace Arc.Components.UI;

public class Text : IRenderable
{
    public override short ZLayer { get; set; } = 1000;
    public override bool IsUI { get; set; } = true;
    
    private Font _font = new("Game/SNPro.ttf");
    private string _text = "Text";
    private uint _size = 36;
    private SFML.Graphics.Text.Styles _style;
    private Color _fillColor = Color.White;

    private SFML.Graphics.Text _sfmlText = new(new Font("Game/SNPro.ttf"));
    private RectTransform _rect;
    

    public string text
    {
        get => _text;
        set { _text = value; _sfmlText = new(_font, value, _size); }
    }
    
    public Font Font
    {
        get => _font;
        set { _font = value; _sfmlText.Font = value; }
    }
    
    public uint Size
    {
        get => _size;
        set { _size = value; _sfmlText.CharacterSize = value; }
    }
    
    public Color FillColor
    {
        get => _fillColor;
        set { _fillColor = value; _sfmlText.FillColor = value; }
    }
    
    public SFML.Graphics.Text.Styles Style
    {
        get => _style;
        set { _style = value; _sfmlText.Style = value; }
    }


    public override void Start()
    {
        _rect = gameObject.GetComponent<RectTransform>();
        if (_rect == null)
        {
            throw new InvalidOperationException(
                $"Text requires RectTransform on GameObject '{gameObject.Name}'");
        }

        _sfmlText = new(_font, _text, _size);
        
        _sfmlText.FillColor = _fillColor;
    }
    
    public override void Update(float deltaTime)
    {
        _sfmlText.Position = _rect.GetCenter();
        _sfmlText.Rotation = _rect.Rotation;
        
        Vector2f rectSize = _rect.GetSize();
        _sfmlText.Origin = new Vector2f(
            _rect.Pivot.X * rectSize.X,
            _rect.Pivot.Y * rectSize.Y
        );
    }
    
    public override void Draw(RenderWindow window)
    {
        window.Draw(_sfmlText);
    }
}