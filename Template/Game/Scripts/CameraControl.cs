using Arc.Core;
using Arc.System;
using SFML.System;
using SFML.Window;

class CameraControl : Component
{
    private Vector2f? startPos;
    private Vector2f pos = new(0, 0);
    private float speed = 500;

    private float zoomSpeed = 0.1f;

    public override void Start()
    {
    }

    public override void Update(float deltaTime)
    {
        if (Input.IsKeyPressed(Keyboard.Key.A))
            pos.X -= deltaTime * speed;
        if (Input.IsKeyPressed(Keyboard.Key.D))
            pos.X += deltaTime * speed;
        if (Input.IsKeyPressed(Keyboard.Key.S))
            pos.Y += deltaTime * speed;
        if (Input.IsKeyPressed(Keyboard.Key.W))
            pos.Y -= deltaTime * speed;
        Camera.Position = pos;

        if (Input.IsMouseButtonDown(Mouse.Button.Right))
        {
            startPos = Input.GetMouseWorldPosition();
        }

        if (Input.IsMouseButtonPressed(Mouse.Button.Right))
        {
            Camera.Position += (Vector2f)startPos! - Input.GetMouseWorldPosition();
        }

        if (Input.IsMouseButtonReleased(Mouse.Button.Right))
        {
            startPos = null;
        }

        float zoomDelta = Input.GetMouseWheelDelta();
        if (zoomDelta != 0)
        {
            Camera.Zoom += zoomDelta * zoomSpeed * Camera.Zoom;
        }
    }

    float Lerp(float a, float b, float t) 
    {
        return a + (b - a) * t;
    }
}