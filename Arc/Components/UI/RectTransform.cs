using SFML.System;
using Arc.Core;
using Arc.System;

namespace Arc.Components.UI;

public class RectTransform : Component
{
    // Якоря (где прикрепляемся к родителю, 0-1)
    public Vector2f AnchorMin { get; set; } = new(0f, 0f);
    public Vector2f AnchorMax { get; set; } = new(0f, 0f);
    
    // Отступы от якорей
    public float Left { get; set; } = 0f;
    public float Top { get; set; } = 0f;
    public float Right { get; set; } = 0f;
    public float Bottom { get; set; } = 0f;

    public Vector2f Position { get; set; } = new(0, 0);
    public Vector2f Size { get; set; } = new(0, 0);
    
    public float Rotation { get; set; } = 0f;
    public Vector2f Pivot { get; set; } = new(0.5f, 0.5f);
    
    public Vector2f GetPosition()
    {
        Vector2f parentSize = GetParentSize();
        Vector2f parentPos = GetParentPosition();

        float x, y;
        if (AnchorMin.X == AnchorMax.X)
            x = parentPos.X + parentSize.X * AnchorMin.X + Position.X;
        else
            x = parentPos.X + AnchorMin.X * parentSize.X + Left;
            
        if (AnchorMin.Y == AnchorMax.Y)
            y = parentPos.Y + parentSize.Y * AnchorMin.Y + Position.Y;
        else
            y = parentPos.Y + AnchorMin.Y * parentSize.Y + Top;
        
        return new Vector2f(x, y);
    }

    private Vector2f GetParentPosition()
    {
        var parentRect = gameObject.Parent?.GetComponent<RectTransform>();
        
        if (parentRect != null)
        {
            return parentRect.GetPosition();
        }
        else
        {
            return new Vector2f(0, 0);
        }
    }

    public Vector2f GetCenter()
    {
        Vector2f pos = GetPosition();
        Vector2f size = GetSize();
        
        return new Vector2f(
            pos.X + size.X * Pivot.X,
            pos.Y + size.Y * Pivot.Y
        );
    }

    public Vector2f GetParentSize()
    {
        var parentRect = gameObject.Parent?.GetComponent<RectTransform>();
        
        if (parentRect != null)
        {
            return parentRect.GetSize();
        }
        else
        {
            return Window.UIView.Size;
        }
    }

    public Vector2f GetSize()
    {
        Vector2f parentSize = GetParentSize();
        
        float width, height;
        
        if (AnchorMin.X == AnchorMax.X)
            width = Size.X;
        else
            width = (AnchorMax.X - AnchorMin.X) * parentSize.X + (-Right - Left);
        
        if (AnchorMin.Y == AnchorMax.Y)
            height = Size.Y;
        else
            height = (AnchorMax.Y - AnchorMin.Y) * parentSize.Y + (-Bottom - Top);
        
        return new Vector2f(width, height);
    }

    public bool Contains(Vector2f point)
    {
        Vector2f pos = GetPosition();
        Vector2f size = GetSize();
        return point.X >= pos.X && point.X <= pos.X + size.X &&
               point.Y >= pos.Y && point.Y <= pos.Y + size.Y;
    }
}