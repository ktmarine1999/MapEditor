/*
 * Bassed on Dean Lunz (aka Created by: X) 2D Tilemap Starter Kit 
 * found at http://wiki.unity3d.com/index.php/2D_Tilemap_Starter_Kit
 * 
 * Modified for 3D tile creation
 * it uses a vector3 for the size of the map but don't think i am going to use the height
 * 
 * update draw method to create a floor tile instead of a cube
*/
using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

namespace MapEditor
{
/// <summary>
/// Provides a editor for the <see cref="TileMap"/> component
/// </summary>
	[CustomEditor(typeof(TileMap3D))]
	public class TileMapEditor : Editor
	{
		/// <summary>
		/// Holds the location of the mouse hit location
		/// </summary>
		private Vector3 mouseHitPos;
		
		/// <summary>
		/// Lets the Editor handle an event in the scene view.
		/// </summary>
		private void OnSceneGUI()
		{
			// if UpdateHitPosition return true we should update the scene views so that the marker will update in real time
			if(this.UpdateHitPosition())
			{
				SceneView.RepaintAll();
			}//if UpdateHitPosition
			
			// Calculate the location of the marker based on the location of the mouse
			this.RecalculateMarkerPosition();
			
			// get a reference to the current event
			Event current = Event.current;
			
			// if the mouse is positioned over the layer allow drawing actions to occur
			//if(this.IsMouseOnLayer())
			{
				Debug.Log("If true");
				// if mouse down or mouse drag event occurred
				if(current.type == EventType.MouseDown || current.type == EventType.MouseDrag)
				{
					if(current.button == 1)
					{
						// if right mouse button is pressed then we erase blocks
						this.Erase();
						current.Use();
					}// button == 1
					else if(current.button == 0)
					{
						// if left mouse button is pressed then we draw blocks
						this.Draw();
						current.Use();
					}//button == 2
				}// mouse down or drag
			}//if MouseOnLayer()
			
			// draw a UI tip in scene view informing user how to draw & erase tiles
			Handles.BeginGUI();
			GUI.Label(new Rect(10, Screen.height - 90, 100, 100), "LMB: Draw");
			GUI.Label(new Rect(10, Screen.height - 105, 100, 100), "RMB: Erase");
			Handles.EndGUI();
		}//OnSceneGUI()
		
		/// <summary>
		/// When the <see cref="GameObject"/> is selected set the current tool to the view tool.
		/// </summary>
		private void OnEnable()
		{
			Tools.current = Tool.View;
			Tools.viewTool = ViewTool.FPS;
		}//OnEnable()
		
		/// <summary>
		/// Draws a block at the pre-calculated mouse hit position
		/// </summary>
		private void Draw()
		{
			// get reference to the TileMap component
			TileMap3D map = (TileMap3D)this.target;
			
			// Calculate the position of the mouse over the tile layer
			Vector3 tilePos = this.GetTilePositionFromMouseLocation();

			Debug.Log(tilePos);
			
			// Given the tile position check to see if a tile has already been created at that location
			var cube = GameObject.Find(string.Format("Tile_{0}_{1}_{2}", tilePos.x, tilePos.y, tilePos.z));
			
			// if there is already a tile present and it is not a child of the game object we can just exit.
			if(cube != null && cube.transform.parent != map.transform)
				return;
			
			// if no game object was found we will create a cube
			if(cube == null)
				cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
			
			// set the cubes position on the tile map
			Vector3 tilePositionInLocalSpace = new Vector3((tilePos.x * map.tileSize.x) + (map.tileSize.x / 2), 
			                                               (tilePos.y * map.tileSize.y) + (map.tileSize.y / 2),
			                                               (tilePos.z * map.tileSize.z) + (map.tileSize.z / 2));

			cube.transform.position = map.transform.position + tilePositionInLocalSpace;
			
			// we scale the cube to the tile size defined by the TileMap.TileWidth and TileMap.TileHeight fields 
			cube.transform.localScale = new Vector3(map.tileSize.x, map.tileSize.y, map.tileSize.z);
			
			// set the cubes parent to the game object for organizational purposes
			cube.transform.parent = map.transform;
			
			// give the cube a name that represents it's location within the tile map
			cube.name = string.Format("Tile_{0}_{1}_{2}", tilePos.x, tilePos.y, tilePos.z);
		}//OnDraw()
		
		/// <summary>
		/// Erases a block at the pre-calculated mouse hit position
		/// </summary>
		private void Erase()
		{
			// get reference to the TileMap component
			TileMap3D map = (TileMap3D)this.target;
			
			// Calculate the position of the mouse over the tile layer
			Vector3 tilePos = this.GetTilePositionFromMouseLocation();
			
			// Given the tile position check to see if a tile has already been created at that location
			var cube = GameObject.Find(string.Format("Tile_{0}_{1}_{2}", tilePos.x, tilePos.y, tilePos.z));
			
			// if a game object was found with the same name and it is a child we just destroy it immediately
			if(cube != null && cube.transform.parent == map.transform)
				UnityEngine.Object.DestroyImmediate(cube);
		}//Erase()
		
		/// <summary>
		/// Calculates the location in tile coordinates (Column/Row) of the mouse position
		/// </summary>
		/// <returns>Returns a <see cref="Vector2"/> type representing the Column and Row where the mouse of positioned over.</returns>
		private Vector3 GetTilePositionFromMouseLocation()
		{
			// get reference to the tile map component
			TileMap3D map = (TileMap3D)this.target;
			
			// calculate column and row location from mouse hit location
			Vector3 pos = new Vector3(this.mouseHitPos.x / map.tileSize.x, this.mouseHitPos.y / map.tileSize.y, map.transform.position.z);
			
			// round the numbers to the nearest whole number using 5 decimal place precision
			pos = new Vector3((int)Math.Round(pos.x, 5, MidpointRounding.ToEven), (int)Math.Round(pos.y, 5, MidpointRounding.ToEven), 0);
			
			// do a check to ensure that the row and column are with the bounds of the tile map
			var col = pos.z;
			var row = pos.x;
			var height = pos.y;
			if(row < 0)
				row = 0;
			
			if(row > map.mapSize.x - 1)
				row = map.mapSize.x - 1;
			
			if(col < 0)
				col = 0;
			
			if(col > map.mapSize.z - 1)
				col = map.mapSize.z - 1;

			if(height < 0)
				height = 0;
			
			if(height > map.mapSize.y - 1)
				height = map.mapSize.y - 1;
			
			// return the column and row values
			return new Vector3(row, height, col);
		}//GetTilePositionFromMouseLocation()
		
		/// <summary>
		/// Returns true or false depending if the mouse is positioned over the tile map.
		/// </summary>
		/// <returns>Will return true if the mouse is positioned over the tile map.</returns>
		private bool IsMouseOnLayer()
		{
			// get reference to the tile map component
			TileMap3D map = (TileMap3D)this.target;
			
			// return true or false depending if the mouse is positioned over the map
			return this.mouseHitPos.x > 0 && this.mouseHitPos.x < (map.mapSize.x * map.tileSize.x) &&
				this.mouseHitPos.y > 0 && this.mouseHitPos.y < (map.mapSize.y * map.tileSize.y) &&
				this.mouseHitPos.z > 0 && this.mouseHitPos.z < (map.mapSize.z * map.tileSize.z);
		}//IsMouseOnLayer()
		
		/// <summary>
		/// Recalculates the position of the marker based on the location of the mouse pointer.
		/// </summary>
		private void RecalculateMarkerPosition()
		{
			// get reference to the tile map component
			TileMap3D map = (TileMap3D)this.target;
			
			// store the tile location (Column/Row) based on the current location of the mouse pointer
			Vector3 tilepos = this.GetTilePositionFromMouseLocation();
			
			// store the tile position in world space
			Vector3 pos = new Vector3(tilepos.x * map.tileSize.x, tilepos.y * map.tileSize.y, tilepos.z * map.tileSize.z);
			
			// set the TileMap.MarkerPosition value
			map.MarkerPosition = map.transform.position + new Vector3(pos.x + (map.tileSize.x / 2), pos.y + (map.tileSize.y / 2), pos.z + (map.tileSize.x / 2));
		}//RecalculateMarkerPosition()
		
		/// <summary>
		/// Calculates the position of the mouse over the tile map in local space coordinates.
		/// </summary>
		/// <returns>Returns true if the mouse is over the tile map.</returns>
		private bool UpdateHitPosition()
		{
			// get reference to the tile map component
			TileMap3D map = (TileMap3D)this.target;
			
			// build a plane object that 
			var p = new Plane(map.transform.TransformDirection(Vector3.forward), map.transform.position);
			
			// build a ray type from the current mouse position
			var ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
			
			// stores the hit location
			Vector3 hit = new Vector3();
			
			// stores the distance to the hit location
			float dist;
			
			// cast a ray to determine what location it intersects with the plane
			if(p.Raycast(ray, out dist))
			{
				// the ray hits the plane so we calculate the hit location in world space
				hit = ray.origin + (ray.direction.normalized * dist);
			}
			
			// convert the hit location from world space to local space
			var value = map.transform.InverseTransformPoint(hit);
			
			// if the value is different then the current mouse hit location set the 
			// new mouse hit location and return true indicating a successful hit test
			if(value != this.mouseHitPos)
			{
				this.mouseHitPos = value;
				return true;
			}
			
			// return false if the hit test failed
			return false;
		}//UpdateHitPosition()
	}//class
}//namespace