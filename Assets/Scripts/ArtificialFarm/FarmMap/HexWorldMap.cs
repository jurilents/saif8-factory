using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ArtificialFarm.FarmMap
{
    public class HexWorldMap : WorldMap
    {
        public HexWorldMap(Tilemap tilemap, TileBase tile, Size size)
            : base(tilemap, tile, size)
        {
        }


        public override IEnumerable<Vector3Int> GetNeighbors(in Vector3Int pos)
        {
            return pos.y % 2 == 0
                ? new List<Vector3Int>
                {
                    new Vector3Int(pos.x - 1, pos.y + 1, BASE_Z),
                    new Vector3Int(pos.x, pos.y + 1, BASE_Z),
                    new Vector3Int(pos.x + 1, pos.y, BASE_Z),
                    new Vector3Int(pos.x, pos.y - 1, BASE_Z),
                    new Vector3Int(pos.x - 1, pos.y - 1, BASE_Z),
                    new Vector3Int(pos.x - 1, pos.y, BASE_Z)
                }
                : new List<Vector3Int>
                {
                    new Vector3Int(pos.x, pos.y + 1, BASE_Z),
                    new Vector3Int(pos.x + 1, pos.y + 1, BASE_Z),
                    new Vector3Int(pos.x + 1, pos.y, BASE_Z),
                    new Vector3Int(pos.x + 1, pos.y - 1, BASE_Z),
                    new Vector3Int(pos.x, pos.y - 1, BASE_Z),
                    new Vector3Int(pos.x - 1, pos.y, BASE_Z)
                };
        }


        public override Cell GetCellByMove(Cell from, in Turn turn)
        {
            var pos = from.Pos;
            return GetCell(pos.y % 2 == 0
                ? turn.Side switch
                {
                    0 => new Vector3Int(pos.x - 1, pos.y + 1, BASE_Z),
                    1 => new Vector3Int(pos.x, pos.y + 1, BASE_Z),
                    2 => new Vector3Int(pos.x + 1, pos.y, BASE_Z),
                    3 => new Vector3Int(pos.x, pos.y - 1, BASE_Z),
                    4 => new Vector3Int(pos.x - 1, pos.y - 1, BASE_Z),
                    5 => new Vector3Int(pos.x - 1, pos.y, BASE_Z),
                    _ => throw new IndexOutOfRangeException("Turn #" + turn + " was unexpected")
                }
                : turn.Side switch
                {
                    0 => new Vector3Int(pos.x, pos.y + 1, BASE_Z),
                    1 => new Vector3Int(pos.x + 1, pos.y + 1, BASE_Z),
                    2 => new Vector3Int(pos.x + 1, pos.y, BASE_Z),
                    3 => new Vector3Int(pos.x + 1, pos.y - 1, BASE_Z),
                    4 => new Vector3Int(pos.x, pos.y - 1, BASE_Z),
                    5 => new Vector3Int(pos.x - 1, pos.y, BASE_Z),
                    _ => throw new IndexOutOfRangeException("Turn #" + turn + " was unexpected")
                });
        }
    }
}