using UnityEngine;
using System.Collections;

namespace MapEditor
{
	public class TileMap3D : MonoBehaviour
	{
		/// <summary>
		/// The size of the map.
		/// </summary>
		public Vector3 mapSize;

		/// <summary>
		/// The size of the  of one floor tile.
		/// </summary>
		public Vector3 tileSize = new Vector3(4, 1.2f, 4);

		/// <summary>
		/// Used by editor components or game logic to indicate a tile location.
		/// </summary>
		/// <remarks>This will be hidden from the inspector window. See <see cref="HideInInspector"/></remarks>
		[HideInInspector]
		public Vector3
			MarkerPosition;


		/// <summary>
		/// Initializes a new instance of the <see cref="TileMap3D"/> class.
		/// </summary>
//		public TileMap3D ()
//		{
//			if (!mapSize || mapSize == Vector3.zero)
//				mapSize = new Vector3 (20, 1, 10);
//		}//TileMap3D()

		/// <summary>
		/// When the game object is selected this will draw the grid
		/// </summary>
		/// <remarks>Only called when in the Unity editor.</remarks>
		private void OnDrawGizmosSelected()
		{
			// store map width, height, length and position
			Vector3 size = new Vector3(mapSize.x * tileSize.x, mapSize.y * tileSize.y, mapSize.z * tileSize.z);
			var position = this.transform.position;
			
			// draw layer border
			Gizmos.color = Color.white;
			Gizmos.DrawWireCube(position + size / 2, size);

			// draw tile cells
			Gizmos.color = Color.grey;
			for(int x = 1; x < mapSize.x; x++)
			{
				for(int y = 1; y < mapSize.y; y++)
				{
					for(int z = 1; z < mapSize.z; z++)
					{
						float xPos = x * tileSize.x;
						float yPos = y * tileSize.y;
						float zPos = z * tileSize.z;
						Gizmos.DrawLine(position + new Vector3(xPos, 0, 0), position + new Vector3(xPos, size.y, 0));
						Gizmos.DrawLine(position + new Vector3(xPos, 0, 0), position + new Vector3(xPos, 0, size.z));
						Gizmos.DrawLine(position + new Vector3(xPos, size.y, 0), position + new Vector3(xPos, size.y, size.z));
						Gizmos.DrawLine(position + new Vector3(xPos, 0, size.z), position + new Vector3(xPos, size.y, size.z));
						Gizmos.DrawLine(position + new Vector3(0, yPos, 0), position + new Vector3(size.x, yPos, 0));
						Gizmos.DrawLine(position + new Vector3(0, yPos, 0), position + new Vector3(0, yPos, size.z));
						Gizmos.DrawLine(position + new Vector3(0, yPos, size.z), position + new Vector3(size.x, yPos, size.z));
						Gizmos.DrawLine(position + new Vector3(xPos, yPos, 0), position + new Vector3(xPos, yPos, size.z));
						Gizmos.DrawLine(position + new Vector3(size.x, yPos, 0), position + new Vector3(size.x, yPos, size.z));
						Gizmos.DrawLine(position + new Vector3(0, 0, zPos), position + new Vector3(size.x, 0, zPos));
						Gizmos.DrawLine(position + new Vector3(0, 0, zPos), position + new Vector3(0, size.y, zPos));
						Gizmos.DrawLine(position + new Vector3(0, yPos, zPos), position + new Vector3(size.x, yPos, zPos));
						Gizmos.DrawLine(position + new Vector3(0, size.y, zPos), position + new Vector3(size.x, size.y, zPos));
						Gizmos.DrawLine(position + new Vector3(xPos, 0, zPos), position + new Vector3(xPos, size.y, zPos));
						Gizmos.DrawLine(position + new Vector3(xPos, yPos, zPos), position + new Vector3(xPos, size.y, zPos));
						Gizmos.DrawLine(position + new Vector3(size.x, 0, zPos), position + new Vector3(size.x, size.y, zPos));
					}//z
				}//y
			}//x

//			for(int x = 1; x < mapSize.x; x++)
//			{
//				for(int y = 1; y < mapSize.y; y++)
//				{
//					for(int z = 1; z < mapSize.z; z++)
//					{
//						float xPos = x * tileSize.x;
//						float yPos = y * tileSize.y;
//						float zPos = z * tileSize.z;
//						Vector3 center = (position + new Vector3(xPos, yPos, zPos)) / 2;
//						Gizmos.DrawWireCube(center, tileSize);
//						center.y += y * tileSize.y;
//						Gizmos.DrawWireCube(center, tileSize);
//						center.x += x * tileSize.x;
//						Gizmos.DrawWireCube(center, tileSize);
//						center.y -= y * tileSize.y;
//						Gizmos.DrawWireCube(center, tileSize);
//						center.z += z * tileSize.z;
//						Gizmos.DrawWireCube(center, tileSize);
//						center.y += y * tileSize.y;
//						Gizmos.DrawWireCube(center, tileSize);
//						center.x -= x * tileSize.x;
//						Gizmos.DrawWireCube(center, tileSize);
//						center.y -= y * tileSize.y;
//						Gizmos.DrawWireCube(center, tileSize);
//					}//z
//				}//y
//			}//x
			
			// Draw marker position
			Gizmos.color = Color.red;    
			Gizmos.DrawWireCube(this.MarkerPosition, new Vector3(tileSize.x, tileSize.y, tileSize.z) * 1.1f);
		}
	}//class
}//namespace
