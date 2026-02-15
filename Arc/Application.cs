using Arc.Core;
using Arc.System;
using System;

namespace Arc;

public sealed class Application
{
    private static Application _instance;
    public static Application Instance => _instance ?? throw new InvalidOperationException("Application not initialized");
    
    private Application(uint width, uint height, string title, string iconPath)
    {
        // Инициализируем окно
        Window.Initialize(width, height, title, iconPath);
        
        // Инициализируем Input
        var window = Window.Instance;
        window.KeyPressed += (s, e) => Input.OnKeyPressed(e.Code);
        window.KeyReleased += (s, e) => Input.OnKeyReleased(e.Code);
        window.MouseButtonPressed += (s, e) => Input.OnMouseButtonPressed(e.Button);
        window.MouseButtonReleased += (s, e) => Input.OnMouseButtonReleased(e.Button);
        window.MouseMoved += (s, e) => Input.OnMouseMoved(e.Position);
        window.MouseWheelScrolled += (s, e) => Input.OnMouseWheelScrolled(e.Delta);
        
        // Инициализируем камеру
        Camera.Initialize(window);
    }
    
    public static void Initialize(uint width, uint height, string title = "Game", string iconPath = "icon.png")
    {
        if (_instance != null) 
            throw new InvalidOperationException("Application already initialized");
        
        _instance = new Application(width, height, title, iconPath);
    }
    
    public void Run()
    {
        SFML.System.Clock clock = new();
        
        while (Window.IsOpen)
        {
            Input.Update();
            Window.DispatchEvents();
            
            float deltaTime = clock.Restart().AsSeconds();
            Scene.Instance.Update(deltaTime);
            Scene.Instance.Render(Window.Instance);
            
            Window.Display();
        }
    }
}