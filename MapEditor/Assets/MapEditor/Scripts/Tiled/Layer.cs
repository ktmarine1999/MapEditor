using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MapEditor.Tiled
{
	public class Layer
	{
		/// <summary>
		/// Gets the name of the layer.
		/// </summary>
		public string Name;
		
		/// <summary>
		/// Gets the width (in tiles) of the layer.
		/// </summary>
		public int Width;
		
		/// <summary>
		/// Gets the height (in tiles) of the layer.
		/// </summary>
		public int Height;
		
		/// <summary>
		/// Gets a collection of all of the tiles in this layer.
		/// </summary>
		public List<Tile> Tiles = new List<Tile>(); 
	}
}