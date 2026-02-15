using SFML.Graphics;
using SFML.System;

namespace Arc.System;

public static class Camera
{
    public static View View { get; private set; }
    
    private static Vector2f _defaultSize;
    private static float _currentZoom = 1f;
    
    public static void Initialize(RenderWindow window)
    {
        _defaultSize = new Vector2f(window.Size.X, window.Size.Y);
        View = new View(new FloatRect(new Vector2f(0, 0), new Vector2f(window.Size.X, window.Size.Y)));
        window.SetView(View);
    }
    
    public static void UpdateSize(uint width, uint height)
    {
        _defaultSize = new Vector2f(width, height);
        // View = new View(new FloatRect(new Vector2f(0, 0), new Vector2f(width, height)));

        View.Size = _defaultSize / _currentZoom;
        // View.Center = new Vector2f(width / 2f, height / 2f);
        Window.SetView(View);
    }

    public static Vector2f Position
    {
        get
        {
            return View.Center;
        }
        set
        {
            View.Center = value;
            Window.SetView(View);
        }
    }

    public static float Zoom
    {
        get
        {
            return _currentZoom;
        }
        set
        {
            _currentZoom = value;
            View.Size = _defaultSize / value;
            Window.SetView(View);
        }
    }
    
    public static Vector2f ScreenToWorld(Vector2f screenPos)
    {
        return Window.Instance.MapPixelToCoords(
            new Vector2i((int)screenPos.X, (int)screenPos.Y), View);
    }
    
    public static Vector2f WorldToScreen(Vector2f worldPos)
    {
        var pixel = Window.Instance.MapCoordsToPixel(worldPos, View);
        return new Vector2f(pixel.X, pixel.Y);
    }
}