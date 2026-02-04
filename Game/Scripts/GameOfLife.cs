using Arc.Core;
using Arc.Components;
using SFML.System;
using System.Collections.Generic;
using System;
using System.Linq;

class GameOfLife : Component, ITickable
{
    public TileMap tileMap = null!;
    private Dictionary<Vector2i, bool> nextState = new();
    
    public override void Start()
    {
        Random random = new();
        for (int x = 0; x < tileMap.Width; x++)
        {
            for (int y = 0; y < tileMap.Height; y++)
            {
                if (random.Next(1, 4) == 1)
                {
                    tileMap.AddTile(new Vector2i(x, y), CreateCell());
                }
            }
        }
    }

    public void OnTick()
    {
        NextFrame();
    }

    private void NextFrame()
    {
        nextState.Clear();
        
        for (int x = 0; x < tileMap.Width; x++)
        {
            for (int y = 0; y < tileMap.Height; y++)
            {
                byte nCount = NeiborsCount(x, y);
                Vector2i position = new Vector2i(x, y);
                bool isAlive = tileMap.Tiles.ContainsKey(position);
                
                if (isAlive)
                {
                    if (nCount == 2 || nCount == 3)
                    {
                        nextState[position] = true;
                    }
                }
                else
                {
                    if (nCount == 3)
                    {
                        nextState[position] = true;
                    }
                }
            }
        }

        var positionsToRemove = new List<Vector2i>();
        foreach (var pos in tileMap.Tiles.Keys)
        {
            if (!nextState.ContainsKey(pos))
            {
                positionsToRemove.Add(pos);
            }
        }
        
        foreach (var pos in positionsToRemove)
        {
            tileMap.RemoveTile(pos);
        }

        foreach (var kvp in nextState)
        {
            if (!tileMap.Tiles.ContainsKey(kvp.Key))
            {
                tileMap.AddTile(kvp.Key, CreateCell());
            }
        }
    }

    private byte NeiborsCount(int x, int y)
    {
        byte result = 0;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;

                int nx = (x + i + tileMap.Width) % tileMap.Width;
                int ny = (y + j + tileMap.Height) % tileMap.Height;

                if (tileMap.Tiles.ContainsKey(new Vector2i(nx, ny)))
                    result++;
            }
        }

        return result;
    }

    private GameObject CreateCell()
    {
        GameObject cell = new("cell_" + Guid.NewGuid());
        cell.AddComponent<Transform>();
        cell.AddComponent<SpriteRenderer>().FillColor = SFML.Graphics.Color.White;
        return cell;
    }
}