using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MapEditor.Tiled
{
	public class Tiled3D : MonoBehaviour
	{ 
		[SerializeField]
		string
			_filePath = @"Tiled/Maps";
		[SerializeField]
		string
			_mapName = "test.tmx";
		[SerializeField]
		Vector3
			_floorTileSize = new Vector3(4, 1.2f, 4);

		[SerializeField]
		List<GameObject>
			_tilePrefabs;

		public Map map;

		public string filePath { get { return _filePath; } }
		public string mapName { get { return _mapName; } }
		public List<GameObject> tilePrefabs { get { return _tilePrefabs; } }
		public Vector3 floorTileSize { get { return _floorTileSize; } }
	}//class
}//namespace