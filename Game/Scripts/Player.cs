using SFML.System;
using SFML.Window;
using Engine.Core;

class Player : Component
{
    private float speed = 10;

    public override void Start() {
        var transform = GameObject.Transform;
        if (transform == null) return;

        transform.Position = new Vector2f(600, 400);
    }

    public override void OnTick()
    {
        Move();
    }

    public void Move()
    {
        Vector2f direction = new();
        if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            direction.X -= 1;
        if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            direction.X += 1;
        if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
            direction.Y += 1;
        if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
            direction.Y -= 1;

        var transform = GameObject.Transform;
        if (transform == null) return;
        
        transform.Position += direction * speed;
    }
}