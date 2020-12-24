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


<<<<<<< HEAD
        public override IEnumerable<Vector3Int> GetNeighbors(in Vector3Int pos)
=======
        public override IReadOnlyList<Vector3Int> GetNeighbors(in Vector3Int pos)
>>>>>>> parent of 7347170... v0.1.0
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


        public override Cell Move(in Cell from, in Turn turn)
        {
            var pos = from.Pos;
            Vector3Int newPos;

            if (pos.y % 2 == 0)
            {
                switch (turn.side)
                {
                    case 0:
                        newPos = new Vector3Int(pos.x - 1, pos.y + 1, BASE_Z);
                        break;
                    case 1:
                        newPos = new Vector3Int(pos.x, pos.y + 1, BASE_Z);
                        break;
                    case 2:
                        newPos = new Vector3Int(pos.x + 1, pos.y, BASE_Z);
                        break;
                    case 3:
                        newPos = new Vector3Int(pos.x, pos.y - 1, BASE_Z);
                        break;
                    case 4:
                        newPos = new Vector3Int(pos.x - 1, pos.y - 1, BASE_Z);
                        break;
                    case 5:
                        newPos = new Vector3Int(pos.x - 1, pos.y, BASE_Z);
                        break;
                    default:
                        throw new IndexOutOfRangeException(
                            "Turn #" + turn + " was unexpected");
                }
            }
            else
            {
                switch (turn.side)
                {
                    case 0:
                        newPos = new Vector3Int(pos.x, pos.y + 1, BASE_Z);
                        break;
                    case 1:
                        newPos = new Vector3Int(pos.x + 1, pos.y + 1, BASE_Z);
                        break;
                    case 2:
                        newPos = new Vector3Int(pos.x + 1, pos.y, BASE_Z);
                        break;
                    case 3:
                        newPos = new Vector3Int(pos.x + 1, pos.y - 1, BASE_Z);
                        break;
                    case 4:
                        newPos = new Vector3Int(pos.x, pos.y - 1, BASE_Z);
                        break;
                    case 5:
                        newPos = new Vector3Int(pos.x - 1, pos.y, BASE_Z);
                        break;
                    default:
                        throw new IndexOutOfRangeException(
                            "Turn #" + turn + " was unexpected");
                }
            }

            return GetCell(newPos);
        }
    }
}