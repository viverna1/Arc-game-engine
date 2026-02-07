using Arc.Core;
using Arc.System;
using SFML.System;

class Temp : Component
{
    private Vector2f pos = new(0, 0);
    private float speed = 500;
    
    public override void Update(float deltaTime)
    {
        if (Input.IsKeyPressed(SFML.Window.Keyboard.Key.A))
            pos.X -= deltaTime * speed;
        if (Input.IsKeyPressed(SFML.Window.Keyboard.Key.D))
            pos.X += deltaTime * speed;
        if (Input.IsKeyPressed(SFML.Window.Keyboard.Key.S))
            pos.Y += deltaTime * speed;
        if (Input.IsKeyPressed(SFML.Window.Keyboard.Key.W))
            pos.Y -= deltaTime * speed;
        Camera.SetPosition(pos);
    }
}