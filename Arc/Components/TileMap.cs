using Arc.Core;
using System.Collections.Generic;
using SFML.System;

namespace Arc.Components;

public class TileMap : Component
{
    public Dictionary<Vector2i, GameObject> Tiles = new();
    public int Width { get; set; } = 10;
    public int Height { get; set; } = 10;

    public float TileSize = 64;

    public void AddTile(Vector2i position, GameObject tile)
    {
        if (Tiles.ContainsKey(position)) return;
        Tiles.Add(position, tile);
        gameObject.AddChild(tile);
        tile.transform.Position = new Vector2f(position.X * TileSize, position.Y * TileSize);
        tile.transform.SetSize(new Vector2f(TileSize, TileSize));
        Scene.Instance.AddGameObject(tile);
    }

    public void RemoveTile(Vector2i position)
    {
        if (Tiles.TryGetValue(position, out GameObject tile))
        {
            gameObject.RemoveChild(tile);
            Scene.Instance.RemoveGameObject(tile);
            Tiles.Remove(position);
        }
    }

    public GameObject Get(Vector2i pos)
    {
        if (isTileBusy(pos))
            return Tiles[pos];
        return null;
    }

    public bool isTileBusy(Vector2i pos)
    {
        return Tiles.ContainsKey(pos);
    }
}