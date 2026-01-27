using Engine.Core;
using Engine.Components;
using Engine.System;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

using System.Collections.Generic;
using System;

class Application
{
    public RenderWindow Window { get; }

    private List<GameObject> _objects { get; } = [];
    private Dictionary<Keyboard.Key, Action> _keyActions = [];
    
    public Application(uint width, uint height, string title = "Game")
    {
        Vector2u windowSize = new(width, height);
        Window = new RenderWindow(new VideoMode(windowSize), title);
        Window.Closed += (s, e) => Window.Close();
        Window.SetFramerateLimit(60); // Лимит фпс

        Window.KeyPressed += (s, e) => 
        {
            Input.Update(e);
            if (_keyActions.TryGetValue(e.Code, out var action))
                action();
        };
    }

    public void Run()
    {
        // Вызов Start у всех обьектов
        foreach (GameObject obj in _objects)
        {
            obj.Start();
        }

        // Главный цикл
        Clock clock = new Clock();
        while (Window.IsOpen)
        {
            Window.DispatchEvents(); // Обработка событий
            Window.Clear(new Color(0x0f, 0x12, 0x2a)); // Очистка экрана (цвет #0f122a)
            float deltaTime = clock.Restart().AsSeconds();

            // Вызов Update у всех обьектов
            foreach (GameObject obj in _objects)
            {
                obj.Update(deltaTime);
                obj.GetComponent<SpriteRenderer>()?.Draw(Window);
            }
            
            Window.Display(); // Отображение на экране
        }
    }

    public void BindKey(Keyboard.Key key, Action action)
    {
        _keyActions[key] = action;
    }

    public void AddGameObject(GameObject gameObject)
    {
        _objects.Add(gameObject);
    }
}
