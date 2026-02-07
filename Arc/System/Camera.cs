using SFML.Graphics;
using SFML.System;

namespace Arc.System;

public static class Camera
{
    public static View View { get; private set; }
    
    private static Vector2f _defaultSize;
    private static float _currentZoom = 1f;
    
    public static float CurrentZoom => _currentZoom;
    
    public static void Initialize(RenderWindow window)
    {
        View = window.GetView();
        _defaultSize = View.Size;
        View.Center = new Vector2f(0, 0);  // Устанавливаем камеру в центр
        window.SetView(View);
    }
    
    public static void UpdateSize(uint width, uint height)
    {
        View = new View(new FloatRect(new Vector2f(0, 0), new Vector2f(width, height)));
        _defaultSize = new Vector2f(width, height);
        View.Size = _defaultSize / _currentZoom;
        Window.SetView(View);
    }
    
    public static void SetPosition(Vector2f position)
    {
        View.Center = position;
        Window.SetView(View);
    }
    
    public static void SetZoom(float zoom)
    {
        _currentZoom = zoom;
        View.Size = _defaultSize / zoom;
        Window.SetView(View);
    }
    
    public static void AddZoom(float delta)
    {
        SetZoom(_currentZoom + delta);
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