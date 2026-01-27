using System;
using System.Collections.Generic;
using SFML.Window;

namespace Engine.System;

public static class Input
{
    private static HashSet<Keyboard.Key> _pressedKeys = new();
    
    public static event Action<Keyboard.Key>? KeyPressed;
    
    public static bool IsKeyPressed(Keyboard.Key key) => _pressedKeys.Contains(key);
    
    internal static void Update(KeyEventArgs e)
    {
        _pressedKeys.Add(e.Code);
        KeyPressed?.Invoke(e.Code);
    }
    
    internal static void ClearFrame() => _pressedKeys.Clear();
}