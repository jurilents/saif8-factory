using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
<<<<<<< HEAD
using ArtificialFarm.Core;
=======
>>>>>>> parent of 7347170... v0.1.0
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace ArtificialFarm.FarmMap
{
    public abstract class WorldMap : IEnumerable<Cell>
    {
        protected const int BASE_Z = 0;

        protected readonly Cell[,] _cells;

        private readonly Tilemap _tilemap;
        // private readonly Tile _tilePrefab;

        private readonly Size _size;

        public Size Size => _size;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
<<<<<<< HEAD
        public abstract IEnumerable<Vector3Int> GetNeighbors(in Vector3Int pos);
=======
        public abstract IReadOnlyList<Vector3Int> GetNeighbors(in Vector3Int pos);
>>>>>>> parent of 7347170... v0.1.0


        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="turn"></param>
        /// <returns></returns>
        public abstract Cell Move(in Cell from, in Turn turn);


        protected WorldMap(Tilemap tilemap, TileBase tile, Size size)
        {
<<<<<<< HEAD
            if (tile is null) throw new ArgumentNullException();
=======
            if (tile == null) throw new ArgumentNullException();
>>>>>>> parent of 7347170... v0.1.0
            _tilemap = tilemap ? tilemap : throw new ArgumentNullException();

            _size = size;
            _cells = new Cell[_size.Height, _size.Width];

            for (int y = 0; y < _size.Height; y++)
            for (int x = 0; x < _size.Width; x++)
            {
                var cell = new Cell(new Vector3Int(x, y, BASE_Z));
                // Define tile in global array
                _cells[y, x] = cell;
                // Display current time on the tilemap
                _tilemap.SetTile(cell.Pos, tile);
                // Enable ability to change tile color
                _tilemap.SetTileFlags(cell.Pos, TileFlags.None);
            }
        }

        // To use in FOREACH loop
        public IEnumerator<Cell> GetEnumerator() => _cells.Cast<Cell>().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        /// <summary>
        /// 
        /// </summary>
        public void Refresh()
        {
            foreach (var cell in _cells)
            {
                var color = cell.GetDisplayColor(FarmSettings.DisplayMode);
                _tilemap.SetColor(cell.Pos, color);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Cell GetCell(int x, int y) => GetCell(new Vector3Int(x, y, 0));


        /// <summary>
        /// Get cell by tilemap coords
        /// </summary>
        /// <param name="pos">Tile coords</param>
        /// <returns>Tile from position or throw error  if tile not found</returns>
        public Cell GetCell(in Vector3Int pos)
        {
            if (IsInside(pos)) return _cells[pos.y, pos.x];

            //TODO: Remove logging
            // Debug.LogError($"Cannot raise cell: {pos}");

            // ReSharper disable once SuggestVarOrType_SimpleTypes
            Vector3Int posCopy = pos;
            MoveInside(ref posCopy);
            return _cells[posCopy.y, posCopy.x];
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Cell GetRandomEmptyCell()
        {
            Cell cell;
            do
            {
                cell = GetCell(new Vector3Int(
                    Random.Range(0, Size.Width - 1),
                    Random.Range(0, Size.Height - 1),
                    BASE_Z
                ));
            } while (cell.Bot != null);

            return cell;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool IsInside(in Vector3Int pos) => IsInside(pos.x, pos.y);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsInside(int x, int y) =>
            0 <= x && x < Size.Width && 0 <= y && y < Size.Height;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        public void MoveInside(ref Vector3Int pos)
        {
            int i = 100;
            while (i-- > 0)
            {
                int newX = Size.LoopByX
                    ? pos.x < 0
                        ? pos.x + Size.Width
                        : pos.x >= Size.Width
                            ? pos.x - Size.Width
                            : pos.x
                    : pos.x < 0
                        ? 0
                        : pos.x >= Size.Width
                            ? Size.Width - 1
                            : pos.x;

                int newY = Size.LoopByY
                    ? pos.y < 0
                        ? pos.y + Size.Height
                        : pos.y >= Size.Height
                            ? pos.y - Size.Height
                            : pos.y
                    : pos.y < 0
                        ? 0
                        : pos.y >= Size.Height
                            ? Size.Height - 1
                            : pos.y;

                pos = new Vector3Int(newY, newX, pos.z);
                if (IsInside(pos)) break;
            }
        }
    }
}