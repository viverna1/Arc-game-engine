using Arc.Core;
using Arc.Components;
using SFML.System;
using System.Collections.Generic;
using Arc.System;
using SFML.Window;
using System;

class Snake : Component, ITickable
{
    public Vector2i Position { get; set; } = new();
    public Vector2i Direction { get; set; } = new Vector2i(1, 0);
    public List<Vector2i> Body { get; set; } = new();
    public TileMap tileMap = null!;

    private GameObject apple = new("apple");
    private Vector2i applePos = new(5, 5);

    private bool game = true;
     
 
    public override void Start()
    {
        apple.AddComponent<Transform>();
        apple.AddComponent<SpriteRenderer>().FillColor = SFML.Graphics.Color.Red;
        SpawnApple();
        
        Body.Add(new Vector2i(0, 0));
        Body.Add(new Vector2i(-1, 0));
        Body.Add(new Vector2i(-2, 0));
        Body.Add(new Vector2i(-3, 0));
        Body.Add(new Vector2i(-4, 0));
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        if (Input.IsKeyDown(Keyboard.Key.Up) && Direction.Y != 1)
            Direction = new Vector2i(0, -1);
        else if (Input.IsKeyDown(Keyboard.Key.Down) && Direction.Y != -1)
            Direction = new Vector2i(0, 1);
        else if (Input.IsKeyDown(Keyboard.Key.Left) && Direction.X != 1)
            Direction = new Vector2i(-1, 0);
        else if (Input.IsKeyDown(Keyboard.Key.Right) && Direction.X != -1)
            Direction = new Vector2i(1, 0);
    }

    public void OnTick()
    {
        if (!game) return;

        // Двигаемся
        Position = new Vector2i(
            ((Position.X + Direction.X) % 20 + 20) % 20,
            ((Position.Y + Direction.Y) % 11 + 11) % 11
        );

        if (IsPositionOccupied(Position))
        {
            Console.WriteLine("Game over");
            game = false;
            return;
        }

        Body.Add(Position);

        if (Position == applePos)
        {
            tileMap.RemoveTile(applePos);
            SpawnApple();
        } else if (Body.Count > 0)
        {
            var tile = Body[0];
            tileMap.RemoveTile(tile);
            Body.RemoveAt(0);
        }
        
        var head = new GameObject("SnakeHead");
        head.AddComponent<Transform>();
        head.AddComponent<SpriteRenderer>().FillColor = SFML.Graphics.Color.Green;
        tileMap.AddTile(Position, head);
    }

    public void SpawnApple()
    {
        Random random = new();
        Vector2i newPos;
        int attempts = 0;
        int maxAttempts = 1000; // На всякий случай
        
        do
        {
            // Случайная позиция в пределах карты
            newPos = new Vector2i(
                random.Next(0, 20),
                random.Next(0, 11)
            );
            
            attempts++;
            
            if (attempts >= maxAttempts)
            {
                // Если не нашли свободное место, выходим
                return;
            }
        } 
        while (IsPositionOccupied(newPos)); // Пока позиция занята
        
        applePos = newPos;
        tileMap.AddTile(applePos, apple);
    }

    private bool IsPositionOccupied(Vector2i pos)
    {
        // Проверяем, не попадает ли в змею
        foreach (var bodyPart in Body)
        {
            if (bodyPart == pos)
                return true;
        }
            
        return false;
    }
}