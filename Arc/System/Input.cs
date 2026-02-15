using SFML.Window;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace Arc.System;

public static class Input
{
    private static HashSet<Keyboard.Key> _keysPressed = new();
    private static HashSet<Keyboard.Key> _keysDown = new();
    private static HashSet<Keyboard.Key> _keysReleased = new();

    private static HashSet<Mouse.Button> _mouseButtonsPressed = new();
    private static HashSet<Mouse.Button> _mouseButtonsDown = new();
    private static HashSet<Mouse.Button> _mouseButtonsReleased = new();

    private static Vector2i _mousePosition;
    private static Vector2i _mouseDelta;
    private static Vector2i _previousMousePosition;
    private static float _mouseWheelDelta;

    // Вызывается в начале каждого кадра
    internal static void Update()
    {
        _keysDown.Clear();
        _keysReleased.Clear();
        
        _mouseButtonsDown.Clear();
        _mouseButtonsReleased.Clear();
        
        _mouseDelta = _mousePosition - _previousMousePosition;
        _previousMousePosition = _mousePosition;
        
        _mouseWheelDelta = 0;
    }

    // Клавиатура
    internal static void OnKeyPressed(Keyboard.Key key)
    {
        if (!_keysPressed.Contains(key))
        {
            _keysPressed.Add(key);
            _keysDown.Add(key);
        }
    }

    internal static void OnKeyReleased(Keyboard.Key key)
    {
        _keysPressed.Remove(key);
        _keysReleased.Add(key);
    }

    // Публичные методы для клавиатуры
    public static bool IsKeyPressed(Keyboard.Key key) => _keysPressed.Contains(key);
    public static bool IsKeyDown(Keyboard.Key key) => _keysDown.Contains(key);
    public static bool IsKeyReleased(Keyboard.Key key) => _keysReleased.Contains(key);

    // Мышь - движение
    internal static void OnMouseMoved(Vector2i position)
    {
        _mousePosition = position;
    }
    
    // Мышь - кнопки
    internal static void OnMouseButtonPressed(Mouse.Button button)
    {
        if (!_mouseButtonsPressed.Contains(button))
        {
            _mouseButtonsPressed.Add(button);
            _mouseButtonsDown.Add(button);
        }
    }

    internal static void OnMouseButtonReleased(Mouse.Button button)
    {
        _mouseButtonsPressed.Remove(button);
        _mouseButtonsReleased.Add(button);
    }

    // Мышь - колесико
    internal static void OnMouseWheelScrolled(float delta)
    {
        _mouseWheelDelta += delta;
    }

    // Публичные методы для мыши
    public static bool IsMouseButtonPressed(Mouse.Button button) => _mouseButtonsPressed.Contains(button);
    public static bool IsMouseButtonDown(Mouse.Button button) => _mouseButtonsDown.Contains(button);
    public static bool IsMouseButtonReleased(Mouse.Button button) => _mouseButtonsReleased.Contains(button);

    public static Vector2i GetMouseDelta() => _mouseDelta;
    public static float GetMouseWheelDelta() => _mouseWheelDelta;

    public static Vector2f GetMousePosition()
    {
        Vector2u screenSize = Window.Size;
        Vector2f center = new Vector2f(screenSize.X / 2f, screenSize.Y / 2f);
        return new Vector2f(_mousePosition.X, _mousePosition.Y) - center;
    }

    public static Vector2i GetMousePositionRaw()
    {
        return _mousePosition;
    }
    
    // Позиция мыши в мировых координатах (с учетом камеры)
    public static Vector2f GetMouseWorldPosition() => Window.Instance.MapPixelToCoords(_mousePosition, Camera.View);
}