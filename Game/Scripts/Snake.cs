using Engine.Core;
using Engine.Components;
using SFML.System;
using System.Collections.Generic;
using Engine.System;
using SFML.Window;

class Snake : Component
{
    public Vector2i Position { get; set; } = new();
    public Vector2i Direction { get; set; } = new Vector2i(1, 0);
    public List<Vector2i> Body { get; set; } = new();
    public TileMap tileMap = null!;
    

    public override void Start()
    {
        TickRate = 1f;
        tileMap.AddTile(Position, gameObject);
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

    public override void OnTick()
    {
        // Удаляем старую позицию
        if (Body.Count > 0)
        {
            var tail = Body[0];
            tileMap.RemoveTile(tail);
            Body.RemoveAt(0);
        }
        
        // Двигаемся
        Position += Direction;
        Body.Add(Position);
        
        // Создаём новую голову
        var head = new GameObject("SnakeHead");
        head.AddComponent<Transform>();
        head.AddComponent<SpriteRenderer>().FillColor = SFML.Graphics.Color.Green;
        tileMap.AddTile(Position, head);

        
        if (Body.Count > 1)
        {
            
        }
    }
}