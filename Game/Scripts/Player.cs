using Engine.Core;
using Engine.System;
using SFML.Window;
using SFML.System;


public class Player : Component
{
    public float Speed = 3f;
    public GameObject? RayObject;

    private Vector2f targetPosition;

    public override void Update(float deltaTime)
    {
        if (transform!.Position != targetPosition)
        {
            transform!.SetPosition(VectorMath.Lerp(transform!.Position, targetPosition, Speed * deltaTime));
        }

        if (Input.IsMouseButtonPressed(Mouse.Button.Left))
        {
            var mousePos = Input.GetMouseWorldPosition();
            if (mousePos.X != 0 || mousePos.Y != 0)
            {
                float angle = VectorMath.AngleBetweenDegrees(transform!.Position, mousePos);
                transform!.Rotation = angle - 45f;
            }

            RayObject!.SetActive(true);
            float distance = VectorMath.Distance(transform!.Position, mousePos);
            RayObject!.transform!.SetSize(distance, RayObject!.transform!.Size.Y);
            targetPosition = mousePos;
        }
        else
        {
            RayObject!.SetActive(false);
        }
    }
}