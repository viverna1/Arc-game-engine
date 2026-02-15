using SFML.System;
using Arc.Core;

namespace Arc.Components;

public class Transform : Component
{
    private Vector2f _localPosition = new(0, 0);
    
    // Локальная позиция (относительно родителя)
    public Vector2f LocalPosition
    {
        get => new Vector2f(_localPosition.X, -_localPosition.Y);
        set => _localPosition = new Vector2f(value.X, -value.Y);
    }
    
    // Мировая позиция (с учетом родителя)
    public Vector2f Position
    {
        get
        {
            if (gameObject.Parent?.transform != null)
            {
                return gameObject.Parent.transform.Position + LocalPosition;
            }
            return LocalPosition;
        }
        set
        {
            if (gameObject.Parent?.transform != null)
            {
                LocalPosition = value - gameObject.Parent.transform.Position;
            }
            else
            {
                LocalPosition = value;
            }
        }
    }
    
    private float _localRotation;
    
    // Локальный поворот
    public float LocalRotation
    {
        get => _localRotation;
        set => _localRotation = value;
    }
    
    // Мировой поворот (с учетом родителя)
    public float Rotation
    {
        get
        {
            if (gameObject.Parent?.transform != null)
            {
                return gameObject.Parent.transform.Rotation + LocalRotation;
            }
            return LocalRotation;
        }
        set
        {
            if (gameObject.Parent?.transform != null)
            {
                LocalRotation = value - gameObject.Parent.transform.Rotation;
            }
            else
            {
                LocalRotation = value;
            }
        }
    }
    
    public Vector2f Size { get; set; } = new(50, 50);
    
    public void SetLocalPosition(float x, float y)
    {
        LocalPosition = new Vector2f(x, y);
    }
    
    public void SetSize(Vector2f newSize)
    {
        Size = newSize;
    }
}