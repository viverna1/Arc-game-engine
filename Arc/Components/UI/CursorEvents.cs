using Arc.Core;
using Arc.System;
using SFML.System;
using SFML.Window;
using System;

namespace Arc.Components.UI;

public abstract class CursorEvents : Component
{
    private RectTransform _rect;
    private bool _isHover = false;
    private bool _isMouseDown = false;
    private bool _isPressedOn = false;

    public override void Start()
    {
        _rect = gameObject.GetComponent<RectTransform>();
        if (_rect == null)
        {
            throw new InvalidOperationException(
                $"CursorEvents requires RectTransform on GameObject '{gameObject.Name}'");
        }
    }
    
    public override void Update(float deltaTime)
    {
        Vector2f mousePos = (Vector2f)Input.GetMousePositionRaw();
        bool isOver = _rect.Contains(mousePos);
        bool mouseDown = Input.IsMouseButtonPressed(Mouse.Button.Left);

        // Hover логика
        if (isOver && !_isHover)
        {
            _isHover = true;
            OnMouseEnter();
        }
        else if (!isOver && _isHover)
        {
            _isHover = false;
            OnMouseExit();
        }
        
        if (isOver) OnMouseHover();

        // Click логика
        if (mouseDown)
        {
            if (isOver && !_isMouseDown)
            {
                _isPressedOn = true;
                _isMouseDown = true;
                OnMousePressed();
            }
            else if (!isOver && !_isMouseDown)
            {
                _isPressedOn = false;
                _isMouseDown = true;
            }
        }
        else
        {
            if (_isMouseDown)
            {
                _isMouseDown = false;
                
                if (_isPressedOn)
                {
                    OnMouseReleased();
                    
                    if (isOver)
                    {
                        OnMouseClick();
                    }
                }
                _isPressedOn = false;
            }
        }
    }

    public virtual void OnMouseEnter() { }
    public virtual void OnMouseExit() { }
    public virtual void OnMouseHover() { }
    public virtual void OnMousePressed() { }
    public virtual void OnMouseReleased() { }
    public virtual void OnMouseClick() { }
}