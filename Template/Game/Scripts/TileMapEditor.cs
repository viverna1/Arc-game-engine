using Arc.Core;
using Arc.Components;
using Arc.Components.UI;
using Arc.System;
using SFML.System;
using System;

class TileMapEditor : Component
{
    public TileMap tileMap = null!;
    public Image indicatorImage = null!;
    public TickManager tickManager = null!;

    private Vector2f mousePos;
    private GameObject cursorObj = null!;

    private string mode = "place";

    public override void Start()
    {
        cursorObj = new("TileMapCursor");
        cursorObj.AddComponent<Transform>();
        cursorObj.transform.Size = new Vector2f(tileMap.TileSize, tileMap.TileSize);
        var cursorObjSprite = cursorObj.AddComponent<SpriteRenderer>();
        cursorObjSprite.FillColor = new SFML.Graphics.Color(255, 255, 255, 100);
        cursorObjSprite.ZLayer = 10;
        Scene.Instance.AddGameObject(cursorObj);
    }
    
    public override void Update(float deltaTime)
    {
        mousePos = Input.GetMouseWorldPosition();
        cursorObj.transform.Position = new Vector2f(
            (float)Math.Floor(mousePos.X / tileMap.TileSize) * tileMap.TileSize,
            (float)Math.Floor(mousePos.Y / tileMap.TileSize) * tileMap.TileSize
        );

        Vector2i tileMousePos = new Vector2i(
            (int)Math.Floor(mousePos.X / tileMap.TileSize),
            (int)Math.Floor(mousePos.Y / tileMap.TileSize)
        );

        if (Input.IsMouseButtonPressed(SFML.Window.Mouse.Button.Left))
        {
            switch (mode)
            {
                case "place":
                    if (!tileMap.isTileBusy(tileMousePos))
                        CreateTile(tileMousePos);
                    break;
                    
                case "delete":
                    if (tileMap.isTileBusy(tileMousePos))
                        RemoveTile(tileMousePos);
                    break;
                    
                case "heat":
                    if (tileMap.isTileBusy(tileMousePos)) {
                        var tile = tileMap.Get(tileMousePos);
                        if (Input.IsKeyPressed(SFML.Window.Keyboard.Key.LShift))
                            tile?.GetComponent<Tile>().temperature += 200;
                        else
                            tile?.GetComponent<Tile>().temperature += 20;
                    }
                    break;
                    
                case "cool down":
                    if (tileMap.isTileBusy(tileMousePos)) {
                        var tile = tileMap.Get(tileMousePos);
                        if (Input.IsKeyPressed(SFML.Window.Keyboard.Key.LShift))
                            tile?.GetComponent<Tile>().temperature -= 200;
                        else
                            tile?.GetComponent<Tile>().temperature -= 20;
                    }
                    break;
            }
        }

        // Смена режима
        if (Input.IsKeyDown(SFML.Window.Keyboard.Key.Num1))
        {
            indicatorImage.FillColor = new SFML.Graphics.Color(100, 255, 100); // Зеленый
            mode = "place";
        }
        if (Input.IsKeyDown(SFML.Window.Keyboard.Key.Num2))
        {
            indicatorImage.FillColor = new SFML.Graphics.Color(255, 100, 100); // Красный
            mode = "delete";
        }
        if (Input.IsKeyDown(SFML.Window.Keyboard.Key.Num3))
        {
            indicatorImage.FillColor = new SFML.Graphics.Color(255, 255, 100); // Желтый
            mode = "heat";
        }
        if (Input.IsKeyDown(SFML.Window.Keyboard.Key.Num4))
        {
            indicatorImage.FillColor = new SFML.Graphics.Color(100, 100, 255); // Синий
            mode = "cool down";
        }
    }

    private GameObject CreateTile(Vector2i pos)
    {
        GameObject tile = new("TileMapCursor");
        tile.AddComponent<Transform>();
        tile.AddComponent<SpriteRenderer>();
        tile.AddComponent<Tile>();
        
        tileMap.AddTile(pos, tile);  // Здесь tile добавляется на сцену
        
        // Теперь можно получать компоненты
        var tileComp = tile.GetComponent<Tile>();
        
        
        // Связываем с соседями
        var rightTileObj = tileMap.Get(pos + new Vector2i(1, 0));
        if (rightTileObj != null)
        {
            var rightTile = rightTileObj.GetComponent<Tile>();
            tileComp.rightTile = rightTile;
            rightTile.leftTile = tileComp;
        }
        
        var leftTileObj = tileMap.Get(pos + new Vector2i(-1, 0));
        if (leftTileObj != null)
        {
            var leftTile = leftTileObj.GetComponent<Tile>();
            tileComp.leftTile = leftTile;
            leftTile.rightTile = tileComp;
        }
        
        var topTileObj = tileMap.Get(pos + new Vector2i(0, 1));
        if (topTileObj != null)
        {
            var topTile = topTileObj.GetComponent<Tile>();
            tileComp.topTile = topTile;
            topTile.bottomTile = tileComp;
        }
        
        var bottomTileObj = tileMap.Get(pos + new Vector2i(0, -1));
        if (bottomTileObj != null)
        {
            var bottomTile = bottomTileObj.GetComponent<Tile>();
            tileComp.bottomTile = bottomTile;
            bottomTile.topTile = tileComp;
        }


        tickManager.AddComponent(tileComp);
        return tile;
    }

    private void RemoveTile(Vector2i pos)
    {
        var tile = tileMap.Get(pos);
        var tileComp = tile.GetComponent<Tile>();
        tickManager.RemoveComponent(tileComp);

        tileComp.topTile?.bottomTile = null;
        tileComp.rightTile?.leftTile = null;
        tileComp.bottomTile?.topTile = null;
        tileComp.leftTile?.rightTile = null;
        tileMap.RemoveTile(pos);
    }
}