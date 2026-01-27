using Engine.Core;
using Engine.System;
using SFML.Window;
using SFML.System;
using System;


public class Player : Component
{
    public float Speed = 200f;
    public gameObject? RayObject;

    public override void Start()
    {
        Console.WriteLine("start");
    }

    public override void Update(float deltaTime)
    {
        Vector2f movement = new(0, 0);
        
        if (Input.IsKeyPressed(Keyboard.Key.Up)) movement.Y -= 1;
        if (Input.IsKeyPressed(Keyboard.Key.Down)) movement.Y += 1;
        if (Input.IsKeyPressed(Keyboard.Key.Left)) movement.X -= 1;
        if (Input.IsKeyPressed(Keyboard.Key.Right)) movement.X += 1;
        
        transform!.Position += movement * Speed * deltaTime;

        // Движение мыши (для камеры, прицела)
        var mousePos = Input.GetMousePosition();
        if (mousePos.X != 0 || mousePos.Y != 0)
        {
            float angle = VectorMath.AngleBetweenDegrees(transform!.Position, mousePos);
            transform!.Rotation = angle - 45f;
        }

        if (Input.IsMouseButtonPressed(Mouse.Button.Left))
        {
            RayObject!.SetActive(true);
            float distance = VectorMath.Distance(transform!.Position, mousePos);
            RayObject!.transform!.SetSize(distance, RayObject!.transform!.Size.Y);
        }
        else
        {
            RayObject!.SetActive(false);
        }
    }
}