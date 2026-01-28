using Engine.Components;
using Engine.Core;
using Engine.System;
using SFML.System;

class CameraControl : Component
{
    public Camera? camera;
    public GameObject? player;
    private Vector2f? _mouseStartPos;
    
    public override void Update(float deltaTime) 
    {
        float mouseScroll = Input.GetMouseWheelDelta();
        if (mouseScroll != 0)
        {
            // Используем _mousePosition напрямую (от левого верхнего угла)
            Vector2i mouseScreenPos = Input.GetMousePositionRaw();
            
            // Конвертируем в мировые координаты ДО зума
            Vector2f worldPosBeforeZoom = camera!.ScreenToWorld(
                new Vector2f(mouseScreenPos.X, mouseScreenPos.Y));
            
            // Применяем зум
            camera.AddZoom(mouseScroll * 0.07f * camera.CurrentZoom);
            
            // Конвертируем ПОСЛЕ зума
            Vector2f worldPosAfterZoom = camera.ScreenToWorld(
                new Vector2f(mouseScreenPos.X, mouseScreenPos.Y));
            
            // Компенсируем смещение
            Vector2f offset = worldPosBeforeZoom - worldPosAfterZoom;
            camera.SetPosition(camera.View.Center + offset);
        }
    
        
        if (Input.IsMouseButtonPressed(SFML.Window.Mouse.Button.Right))
        {
            Vector2f mousePos = Input.GetMousePosition();
            Vector2f worldMousePos = camera!.ScreenToWorld(mousePos);
            if (_mouseStartPos == null)
            {
                _mouseStartPos = worldMousePos;
            }
            Vector2f delta = (Vector2f)_mouseStartPos - worldMousePos;
            camera!.SetPosition(camera!.View.Center + delta);
        }
        else
        {
            _mouseStartPos = null;
        }
    }
}