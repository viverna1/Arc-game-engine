using Engine.Core;
using SFML.Graphics;
using SFML.System;

namespace Engine.Components;

public class Camera : Component
{
    public View View { get; private set; } = null!;
    public static Camera? Main { get; private set; }
    
    private Vector2f _defaultSize;
    private float _currentZoom = 1f;
    public float CurrentZoom => _currentZoom;
    public Vector2f InitialPosition { get; private set; } = new Vector2f(0, 0);

    public override void Start()
    {
        View = Application.Instance.Window.GetView();
        _defaultSize = View.Size;
        
        if (Main == null)
        {
            Main = this;
        }
        
        // Apply the view initially
        Application.Instance.Window.SetView(View);
    }

    public void SetPosition(Vector2f position)
    {
        View.Center = position;
        // Application.Instance.Window.SetView(View);
    }

    public void SetZoom(float zoom)
    {
        _currentZoom = zoom;
        View.Size = _defaultSize / zoom;
        Application.Instance.Window.SetView(View);
    }

    public void AddZoom(float delta)
    {
        SetZoom(_currentZoom + delta);
    }

    public void ResetZoom()
    {
        SetZoom(1f);
    }

    public Vector2f ScreenToWorld(Vector2f screenPos)
    {
        return Application.Instance.Window.MapPixelToCoords(
            new Vector2i((int)screenPos.X, (int)screenPos.Y), View);
    }

    public Vector2f WorldToScreen(Vector2f worldPos)
    {
        var pixel = Application.Instance.Window.MapCoordsToPixel(worldPos, View);
        return new Vector2f(pixel.X, pixel.Y);
    }
}