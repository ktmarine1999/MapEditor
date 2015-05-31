using UnityEngine;
using System.Collections;
using UnityEditor;

namespace MapEditor.Tiled
{
	[CustomEditor(typeof(Tiled3D))]
	public class Tiled3DEditor : Editor
	{
		Tiled3D map;
		string xmlFiletoLoad;

		/// <summary>
		/// When the <see cref="GameObject"/> is selected set the current tool to the view tool.
		/// </summary>
		private void OnEnable()
		{
			Tools.current = Tool.View;
			Tools.viewTool = ViewTool.FPS;

			map = (Tiled3D)target;

			// Create the string to load the xmlFile
			xmlFiletoLoad = Application.dataPath + @"/" + map.filePath + @"/" + map.mapName;

			//Load the map
			map.map = TiledParser.LoadMap(xmlFiletoLoad);
		}//OnEnable()

		/// <summary>
		/// Lets the Editor handle an event in the scene view.
		/// </summary>
		private void OnSceneGUI()
		{
			DrawPrefabs();

			//DrawGUI();
		}//OnSceneGUI

		void DrawPrefabs()
		{
			int tileIndex = 0;
			foreach(Layer layer in map.map.Layers)
			{
				for(int z = 0; z < map.map.Height; z++)
				{
					for(int x = 0; x < map.map.Width; x++)
					{

						if(layer.Tiles.Count - 1 < tileIndex)
						{
							Debug.Log("Tile index " + tileIndex.ToString() + "outside the range of " + layer.Name + " tiles " + layer.Tiles.Count);
							return;
						}//if tile.count
						if(layer.Tiles[tileIndex].gid >= map.tilePrefabs.Count)
						{
							Debug.Log("Tile gid " + layer.Tiles[tileIndex].gid.ToString() + " outside the range of map tile prefabs " + map.tilePrefabs.Count);
							return;
						}//if tile.count
						if(layer.Tiles[tileIndex].gid >= 0)
							CreateTile(layer.Tiles[tileIndex].gid, x, z);

						tileIndex++;
					}//width
				}//height

				tileIndex = 0;
			}//for layer
		}//DrawPrefabs

		void CreateTile(int gid, int x, int z)
		{
			// Given the tile position check to see if a tile has already been created at that location
			var cube = GameObject.Find(string.Format("Tile_{0}_{1}_{2}", x, z, gid));
			
			// if there is already a tile present and it is not a child of the game object we can just exit.
			if(cube != null && cube.transform.parent != map.transform)
				return;
			
			// if no game object was found we will create a cube
			if(cube == null)
				cube = GameObject.Instantiate(map.tilePrefabs[gid]);
			
			// set the cubes position on the tile map
			Vector3 tilePositionInLocalSpace = new Vector3((x * map.floorTileSize.x) + (map.floorTileSize.x / 2), 
			                                               (0 * map.floorTileSize.y),
			                                               (z * -map.floorTileSize.z) + (map.floorTileSize.z / 2));
			
			cube.transform.position = map.transform.position + tilePositionInLocalSpace;
			
			// we scale the cube to the tile size defined by the TileMap.TileWidth and TileMap.TileHeight fields 
			//cube.transform.localScale = new Vector3(map.floorTileSize.x, map.floorTileSize.y, map.floorTileSize.z);
			
			// set the cubes parent to the game object for organizational purposes
			cube.transform.parent = map.transform;
			
			// give the cube a name that represents it's location within the tile map
			cube.name = string.Format("Tile_{0}_{1}_{2}", x, z, gid);
		}

		void DrawGUI()
		{
			// draw a UI tip in scene view informing user about the map
			Handles.BeginGUI();
			GUILayout.BeginVertical("Box", GUILayout.ExpandWidth(false));
			GUILayout.Label("xml file: " + xmlFiletoLoad, GUILayout.ExpandWidth(false));
			GUILayout.BeginHorizontal();
			GUILayout.Label("Map Width: " + map.map.Width.ToString(), GUILayout.ExpandWidth(false));
			GUILayout.Label("Map Length: " + map.map.Height.ToString(), GUILayout.ExpandWidth(false));
			GUILayout.EndHorizontal();
			foreach(Layer l in map.map.Layers)
			{
				GUILayout.Label(l.Name + " has " + l.Tiles.Count.ToString());
				int index = 0;
				GUILayout.BeginHorizontal();
				for(int z = 0; z < map.map.Height; z++)
				{
					GUILayout.BeginVertical();
					for(int x = 0; x < map.map.Width; x++)
					{
						GUILayout.Label(x.ToString() + ", " + z.ToString() + ": " + l.Tiles[index].gid.ToString(), GUILayout.ExpandWidth(false));
						index++;
					}
					GUILayout.EndVertical();
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndVertical();
			Handles.EndGUI();
		}//DrawGUI
	}//Class
}//namespace