using Arc.Core;
using Arc.System;
using SFML.System;
using SFML.Window;
using System;

namespace Arc.Components.UI;

public class CursorEvents : Component
{
    private RectTransform _rect;
    private bool _isHover = false;
    private bool _isMouseDown = false;
    private bool _isPressedOn = false;

    // Объявляем события как делегаты
    public event Action OnMouseEnter;
    public event Action OnMouseExit;
    public event Action OnMouseHover;
    public event Action OnMousePressed;
    public event Action OnMouseReleased;
    public event Action OnClick;

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
            OnMouseEnter?.Invoke();
        }
        else if (!isOver && _isHover)
        {
            _isHover = false;
            OnMouseExit?.Invoke();
        }
        
        if (isOver) OnMouseHover?.Invoke();

        // Click логика
        if (mouseDown)
        {
            if (_isPressedOn)
            {
                OnMousePressed?.Invoke();
            }
            if (isOver && !_isMouseDown)
            {
                _isPressedOn = true;
                _isMouseDown = true;
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
                    OnMouseReleased?.Invoke();
                    
                    if (isOver)
                    {
                        OnClick?.Invoke();
                    }
                }
                _isPressedOn = false;
            }
        }
    }
}