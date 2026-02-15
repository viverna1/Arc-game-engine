using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Arc.System;

public static class Window
{
    public static RenderWindow Instance;
    private static View _uiView;
    
    public static Vector2u Size => Instance.Size;
    public static bool IsOpen => Instance.IsOpen;
    public static View UIView => _uiView;
    
    public static void Initialize(uint width, uint height, string title, string iconPath)
    {
        Vector2u windowSize = new(width, height);
        Instance = new RenderWindow(new VideoMode(windowSize), title);
        
        Instance.Closed += (s, e) => Instance.Close();
        Instance.Resized += OnWindowResized;
        Instance.SetFramerateLimit(60);
        
        // Создаём UI View
        _uiView = new View(new FloatRect(new Vector2f(0, 0), new Vector2f(width, height)));
        
        // Иконка
        try
        {
            using var icon = new Image(iconPath);
            Instance.SetIcon(new Vector2u(icon.Size.X, icon.Size.Y), icon.Pixels);
        }
        catch { }
    }
    
    private static void OnWindowResized(object sender, SizeEventArgs e)
    {
        var size = Instance.Size;

        // Создание нового Viev
        // _uiView = new View(new FloatRect(new Vector2f(0, 0), new Vector2f(size.X, size.Y)));

        // Изменение параметров оригинального Viev
        _uiView.Size = new Vector2f(size.X, size.Y);
        _uiView.Center = new Vector2f(size.X / 2f, size.Y / 2f);

        Camera.UpdateSize(size.X, size.Y);
    }
    
    public static void Clear(Color color) => Instance.Clear(color);
    public static void Display() => Instance.Display();
    public static void DispatchEvents() => Instance.DispatchEvents();
    public static void SetView(View view) => Instance.SetView(view);
    public static View GetView() => Instance.GetView();
    public static void SetFramerateLimit(uint fps) => Instance.SetFramerateLimit(fps);
}