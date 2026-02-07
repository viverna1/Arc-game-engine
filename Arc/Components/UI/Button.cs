using System;
using SFML.Graphics;

namespace Arc.Components.UI;

public class Button : CursorEvents
{
    private Image _image;

    public override void Start()
    {
        base.Start();
        _image = gameObject.GetComponent<Image>();
        if (_image == null)
        {
            throw new InvalidOperationException(
                $"Button requires Image on GameObject '{gameObject.Name}'");
        }
    }

    public override void OnMouseExit()
    {
        _image.FillColor = Color.White;
        global::System.Console.WriteLine("exit");
    }
    public override void OnMouseHover()
    {
        _image.FillColor = Color.Blue;
    }
    public override void OnMousePressed()
    {
        global::System.Console.WriteLine("press");
    }

    

    public override void OnMouseEnter()
    {
        global::System.Console.WriteLine("enter");
    }
    public override void OnMouseReleased()
    {
        global::System.Console.WriteLine("released");
    }
    public override void OnMouseClick()
    {
       global:: System.Console.WriteLine("click");
    }
}