using Engine.Core;
using SFML.Graphics;
using SFML.System;

namespace Engine.Components;

public class Camera : Component
{
    public View View { get; private set; } = null!;
    public static Camera? Main { get; private set; } // Главная камера
    
    public override void Start()
    {
        View = Application.Instance.Window.GetView();
        if (Main == null) Main = this; // Первая камера становится главной
    }
    
    public void SetPosition(Vector2f position)
    {
        View.Center = position;
        Application.Instance.Window.SetView(View);
    }
    
    public void Zoom(float factor)
    {
        View.Size = new Vector2f(View.Size.X * factor, View.Size.Y * factor);
        Application.Instance.Window.SetView(View);
    }
}