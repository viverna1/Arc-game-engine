using SFML.System;
using Engine.Core;

namespace Engine.Components;

public class Transform : Component
{
    private Vector2f _position = new(0, 0);
    
    public Vector2f Position
    {
        get => new(_position.X, -_position.Y);
        set => _position = new(value.X, -value.Y);
    }
    
    public Vector2f Size { get; set; } = new(50, 50);
    public float Rotation { get; set; }
}