using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ArtificialFarm.Core;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace ArtificialFarm.FarmMap
{
	public abstract class WorldMap : IEnumerable<ICell>
	{
		protected const int BASE_Z = 0;

		protected readonly ICell[,] _cells;

		private readonly Tilemap _tilemap;
		// private readonly Tile _tilePrefab;

		private readonly Size _size;

		/// <summary>
		/// Map size info (width and height)
		/// </summary>
		public Size Size => _size;


		/// <summary>
		/// To get all neighbored positions inside world map bounds
		/// </summary>
		/// <param name="pos">Base position</param>
		/// <returns>Collection of all neighbored existing positions</returns>
		public abstract IEnumerable<Vector3Int> GetNeighbors(in Vector3Int pos);


		/// <summary>
		/// 
		/// </summary>
		/// <param name="from"></param>
		/// <param name="turn"></param>
		/// <returns></returns>
		public abstract ICell GetCellByMove(ICell from, in Turn turn);


		protected WorldMap(Tilemap tilemap, TileBase tile, Size size)
		{
			if (tile is null) throw new ArgumentNullException();
			_tilemap = tilemap ? tilemap : throw new ArgumentNullException();

			_size = size;
			_cells = new ICell[_size.Height, _size.Width];

			for (int y = 0; y < _size.Height; y++)
			for (int x = 0; x < _size.Width; x++)
			{
				var cell = new Cell(new Vector3Int(x, y, BASE_Z), _size);
				// Define tile in global array
				_cells[y, x] = cell;
				// Display current time on the tilemap
				_tilemap.SetTile(cell.Pos, tile);
				// Enable ability to change tile color
				_tilemap.SetTileFlags(cell.Pos, TileFlags.None);
			}
		}

		// To use in FOREACH loop
		public IEnumerator<ICell> GetEnumerator() => _cells.Cast<ICell>().GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


		/// <summary>
		/// Display all actually cell colors on the farm tilemap
		/// </summary>
		public void Refresh()
		{
			foreach (ICell cell in _cells)
			{
				Color color = cell.Content?.OnDisplay(FarmSettings.DisplayMode)
				              ?? Defaults.Transparent;
				_tilemap.SetColor(cell.Pos, color);
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public ICell GetCell(int x, int y) => GetCell(new Vector3Int(x, y, 0));


		/// <summary>
		/// Get cell by tilemap coords
		/// </summary>
		/// <param name="pos">Tile coords</param>
		/// <returns>Tile from position or throw error  if tile not found</returns>
		public ICell GetCell(in Vector3Int pos)
		{
			if (IsInside(pos)) return _cells[pos.y, pos.x];

			// ReSharper disable once SuggestVarOrType_SimpleTypes
			Vector3Int posCopy = pos;
			MoveInside(ref posCopy);
			return _cells[posCopy.y, posCopy.x];
		}


		/// <summary>
		/// To randomly get any farm grid cell
		/// </summary>
		/// <returns>Any existing cell without content</returns>
		public ICell GetRandomEmptyCell()
		{
			ICell cell;
			do
			{
				cell = GetCell(new Vector3Int(
					Random.Range(0, Size.Width - 1),
					Random.Range(0, Size.Height - 1),
					BASE_Z
				));
			} while (cell.Content != null);

			return cell;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		public bool IsInside(in Vector3Int pos) => IsInsideX(pos.x) && IsInsideY(pos.y);


		/// <summary>
		/// 
		/// </summary>
		/// <param name="pos"></param>
		public void MoveInside(ref Vector3Int pos) =>
				pos = new Vector3Int(MoveInsideX(pos.x), MoveInsideY(pos.y), BASE_Z);


		private bool IsInsideX(in int x) => 0 <= x && x < Size.Width;

		private bool IsInsideY(in int y) => 0 <= y && y < Size.Height;


		public int MoveInsideX(int x)
		{
			int i = 100;
			while (i-- > 0)
			{
				x = Size.LoopByX
						? x < 0
								? x + Size.Width // x [ ... dx ]
								: x >= Size.Width
										? x - Size.Width // [ dx ... ] x
										: x // [ ... x == dx ... ]
						: x < 0
								? 0 // x [ dx ... ]
								: x >= Size.Width
										? Size.Width - 1 // [ ... dx ] x
										: x; // [ ... x == dx ... ]

				if (IsInsideX(x)) return x;
			}

			throw new TimeoutException("Too many iterations exception!");
		}

		public int MoveInsideY(int y)
		{
			int i = 100;
			while (i-- > 0)
			{
				y = Size.LoopByY
						? y < 0
								? y + Size.Height
								: y >= Size.Height
										? y - Size.Height
										: y
						: y < 0
								? 0
								: y >= Size.Height
										? Size.Height - 1
										: y;

				if (IsInsideY(y)) return y;
			}

			throw new TimeoutException("Too many iterations exception!");
		}
	}
}