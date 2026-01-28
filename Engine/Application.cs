using Engine.Core;
using Engine.System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Engine;

public sealed class Application
{
    private static Application? _instance;
    public static Application Instance => _instance ?? throw new InvalidOperationException("Application not initialized");
    
    public RenderWindow Window { get; }
    private Scene? _currentScene;
    
    private Application(uint width, uint height, string title)
    {
        Vector2u windowSize = new Vector2u(width, height);
        Window = new RenderWindow(new VideoMode(windowSize), title);
        Window.Closed += (s, e) => Window.Close();
        Input._window = Window;
        
        // Подписываемся на события клавиатуры
        Window.KeyPressed += (s, e) => Input.OnKeyPressed(e.Code);
        Window.KeyReleased += (s, e) => Input.OnKeyReleased(e.Code);
        
        // Подписываемся на события мыши
        Window.MouseButtonPressed += (s, e) => Input.OnMouseButtonPressed(e.Button);
        Window.MouseButtonReleased += (s, e) => Input.OnMouseButtonReleased(e.Button);
        Window.MouseMoved += (s, e) => Input.OnMouseMoved(e.Position);
        Window.MouseWheelScrolled += (s, e) => Input.OnMouseWheelScrolled(e.Delta);
        
        Window.SetFramerateLimit(60);
    }
    
    public static void Initialize(uint width, uint height, string title = "Game")
    {
        if (_instance != null) 
            throw new InvalidOperationException("Application already initialized");
        _instance = new Application(width, height, title);
    }
    
    public void LoadScene(Scene scene)
    {
        _currentScene = scene;
        // scene.Start();
    }
    
    public void Run()
    {
        Clock clock = new Clock();
        
        while (Window.IsOpen)
        {
            Input.Update();
            Window.DispatchEvents();
            Window.Clear(new Color(0x0f, 0x12, 0x2a));
            
            float deltaTime = clock.Restart().AsSeconds();
            _currentScene?.Update(deltaTime);
            _currentScene?.Render(Window);
            
            Window.Display();
        }
    }
}