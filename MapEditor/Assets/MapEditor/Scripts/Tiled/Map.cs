using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MapEditor.Tiled
{
	/// <summary>
	/// Defines the possible orientations for a Map.
	/// </summary>
	public enum Orientation
	{
		/// <summary>
		/// The tiles of the map are orthogonal.
		/// </summary>
		Orthogonal,
		
		/// <summary>
		/// The tiles of the map are isometric.
		/// </summary>
		Isometric,
		
		/// <summary>
		/// The tiles of the map are isometric (staggered).
		/// </summary>
		Staggered
	}
	
	/// <summary>
	/// Defines the possible Rendering orders for the tiles in a Map
	/// </summary>
	public enum RenderOrder
	{
		/// <summary>
		/// Tiles are rendered bottom-top, right-left
		/// </summary>
		Right_Down,
		/// <summary>
		/// Tiles are rendered top-bottom, right-left
		/// </summary>
		Right_Up,
		/// <summary>
		/// Tiles are rendered bottom-top, left-right
		/// </summary>
		Left_Down,
		/// <summary>
		/// Tiles are rendered top-bottom, left-right
		/// </summary>
		Left_Up
	}

	public class Map
	{		
		/// <summary>
		/// Gets the version of Tiled used to create the Map.
		/// </summary>
		public string Version;

		/// <summary>
		/// Gets the orientation of the map.
		/// </summary>
		public Orientation Orientation;

		/// <summary>
		/// Gets this Map's RenderOrder.
		/// </summary>
		public RenderOrder MapRenderOrder;
		
		/// <summary>
		/// Gets the width (in tiles) of the map.
		/// </summary>
		public int Width;
		
		/// <summary>
		/// Gets the height (in tiles) of the map.
		/// </summary>
		public int Height;
		
		/// <summary>
		/// Gets the width of a tile in the map.
		/// </summary>
		public int TileWidth;
		
		/// <summary>
		/// Gets the height of a tile in the map.
		/// </summary>
		public int TileHeight;

		public int NextObjectID;

		/// <summary>
		/// Gets a collection of all of the tile sets in the map.
		/// </summary>
		public List<TileSet> TileSets = new List<TileSet>();

		/// <summary>
		/// Gets a collection of all of the layers in the map.
		/// </summary>
		public List<Layer> Layers = new List<Layer>();
	}
}