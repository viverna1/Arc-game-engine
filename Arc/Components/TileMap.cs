using Arc.Core;
using System.Collections.Generic;
using SFML.System;

namespace Arc.Components;

class TileMap : Component
{
    public Dictionary<Vector2i, GameObject> Tiles = new();
    public int Width;
    public int Height;

    public float TileSize = 64;

    public void AddTile(Vector2i position, GameObject tile)
    {
        Tiles.Add(position, tile);
        gameObject.AddChild(tile);
        tile.transform!.SetPosition(position.X * TileSize, position.Y * TileSize);
        tile.transform!.SetSize(TileSize, TileSize);
        Scene.Instance!.AddGameObject(tile);
    }

    public void RemoveTile(Vector2i position)
    {
        if (Tiles.TryGetValue(position, out GameObject? tile))
        {
            gameObject.RemoveChild(tile);
            Scene.Instance.RemoveGameObject(tile);
            Tiles.Remove(position);
        }
    }
}