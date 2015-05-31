using UnityEngine;
using System.Collections;

namespace MapEditor.Tiled
{
	public class TileSet
	{
		/// <summary>
		/// This TileSet's First ID
		/// </summary>
		public int FirstId;
		
		/// <summary>
		/// This TileSet's Name
		/// </summary>
		public string Name;
		
		/// <summary>
		/// Width of the Tile in this set
		/// </summary>
		public int TileWidth;
		
		/// <summary>
		/// Height of the Tile in this set
		/// </summary>
		public int TileHeight;
		
		/// <summary>
		/// Spacing in pixels between Tiles
		/// </summary>
		public int Spacing = 0;
		
		/// <summary>
		/// Margin in pixels from Tiles to border of Texture
		/// </summary>
		public int Margin = 0;

		/// <summary>
		/// Offset in pixels in X Axis to draw the tiles from this tileset
		/// </summary>
		public int TileOffsetX = 0;
		
		/// <summary>
		/// Offset in pixels in Y Axis to draw the tiles from this tileset
		/// </summary>
		public int TileOffsetY = 0;
		
		/// <summary>
		/// This TileSet's image (sprite) path
		/// </summary>
		public string Image;
		
		/// <summary>
		/// This TileSet's loaded Texture
		/// </summary>
		public Texture2D Texture;
	}
}