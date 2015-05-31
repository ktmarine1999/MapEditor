using UnityEngine;
using System.Xml;
using System.Collections;

namespace MapEditor.Tiled
{
	public static class TiledParser
	{
		public static Map LoadMap(string fileName)
		{
			// Create a new map
			Map map = new Map();

			// Create a new XML Document 
			XmlDocument doc = new XmlDocument();

			// Load the file
			doc.Load(fileName);

			//Create the map node(root)
			XmlNode mapNode = doc.DocumentElement;

			SetMapData(mapNode, ref map);

			return map;
		}//LoadMap()

		static void SetMapData(XmlNode mapNode, ref Map map)
		{
			// Get the attributes of the map
			XmlAttributeCollection mapAttributes = mapNode.Attributes;

			// Go through each attribute and set the corrasponding property
			foreach(XmlAttribute att in mapAttributes)
			{
				switch(att.Name)
				{
					case "version":
						map.Version = att.Value;
						break;
					case "orientation":
						SetMapOrientation(att.Value, ref map);
						break;
					case "renderorder":
						SetMapRenderorder(att.Value, ref map);
						break;
					case "width":
						map.Width = int.Parse(att.Value);
						break; 
					case "height":
						map.Height = int.Parse(att.Value);
						break; 
					case "tilewidth":
						map.TileWidth = int.Parse(att.Value);
						break; 
					case "tileheight":
						map.TileHeight = int.Parse(att.Value);
						break; 
					case "nextobjectid":
						map.NextObjectID = int.Parse(att.Value);
						break; 
					default:
						Debug.Log(att.Name + ": " + att.Value);//These are the ones with no cases
						break;
				}//switch(att.Name
			}//foreach(mapAttributes)

			// Loop through all the child nodes of the map node
			foreach(XmlNode childNode in mapNode.ChildNodes)
			{
				// load each individual node based on it's name
				switch(childNode.Name)
				{
					case "tileset":
						SetTileSetData(childNode, ref map);
						break;
					case "layer":
						SetLayerData(childNode, ref map);
						break;
					default:
						Debug.Log(childNode.Name);//These are the ones with no cases
						break;
				}//switch(childNode.Name)
			}//foreach(childNode);

		}//SetMapData()

		static void SetMapOrientation(string value, ref Map map)
		{
			switch(value)
			{
				case "orthogonal":
					map.Orientation = Orientation.Orthogonal;
					return;
				case "isometric":
					map.Orientation = Orientation.Orthogonal;
					return;
				case "Staggered":
					map.Orientation = Orientation.Orthogonal;
					return;
				default:
					map.Orientation = Orientation.Orthogonal;
					return;
			}//switch(value)
		}//SetMapOrientation

		static void SetMapRenderorder(string value, ref Map map)
		{
			switch(value)
			{
				case "right-down":
					map.MapRenderOrder = RenderOrder.Right_Down;
					return;
				case "right-up":
					map.MapRenderOrder = RenderOrder.Right_Up;
					return;
				case "left-down":
					map.MapRenderOrder = RenderOrder.Left_Down;
					return;
				case "left-up":
					map.MapRenderOrder = RenderOrder.Left_Up;
					return;
				default:
					map.MapRenderOrder = RenderOrder.Right_Down;
					return;
			}//switch(value)
		}//SetMapRenderOrder

		static void SetTileSetData(XmlNode tileSetNode, ref Map map)
		{
			// create a new tile set to populate
			TileSet tileSet = new TileSet();

			// Get the attributes of the tileSet
			XmlAttributeCollection tileSetAttributes = tileSetNode.Attributes;

			// Go through each attribute and set the corrasponding property
			foreach(XmlAttribute att in tileSetAttributes)
			{
				switch(att.Name)
				{
					case "firstgid":
						tileSet.FirstId = int.Parse(att.Value);
						break;
					case "name":
						tileSet.Name = att.Value;
						break;
					case "tilewidth":
						tileSet.TileWidth = int.Parse(att.Value);
						break;
					case "tileheight":
						tileSet.TileHeight = int.Parse(att.Value);
						break;
					case "spacing":
						tileSet.Spacing = int.Parse(att.Value);
						break;
					case "margin":
						tileSet.Margin = int.Parse(att.Value);
						break;
					case "tileoffsetx":
						tileSet.TileOffsetX = int.Parse(att.Value);
						break;
					case "tileoffsety":
						tileSet.TileOffsetY = int.Parse(att.Value);
						break;
					default:
						Debug.Log(att.Name + ": " + att.Value);//These are the ones with no cases
						break;
				}//switch(att.Name
			}//foreach(tileSetAttributes)

			//Loop through the child nodes the only information we care about is the image source
			foreach(XmlNode childNode in tileSetNode.ChildNodes)
			{
				switch(childNode.Name)
				{
					case "image":
						foreach(XmlAttribute att in childNode.Attributes)
						{
							switch(att.Name)
							{
								case "source":
									tileSet.Image = att.Value;
									break;
								default:
									Debug.Log(att.Name + ": " + att.Value);//These are the ones with no cases
									break;
							}//switch(att.name
						}//Foreach(att)
						break;
					default:
						Debug.Log(childNode.Name);//These are the ones with no cases
						break;
				}//switch(chilnode)
			}//for each(childNode)

			//Add loading of the texture for the tile set

			// add the tile set to the map
			map.TileSets.Add(tileSet);
		}//SetTileSetData

		static void SetLayerData(XmlNode layerNode, ref Map map)
		{
			// create a new layer to populate
			Layer layer = new Layer();

			// Loop through the layer attributes and get the data we nees
			foreach(XmlAttribute att in layerNode.Attributes)
			{
				switch(att.Name)
				{
					case "name":
						layer.Name = att.Value;
						break;
					case "height":
						layer.Height = int.Parse(att.Value);
						break; 
					case "width":
						layer.Width = int.Parse(att.Value);
						break;
					default:
						Debug.Log(att.Name + ": " + att.Value);//These are the ones with no cases
						break;
				}//switch(att.Name
			}//foreach(LayerAttributes)

			// loop throgh the layers children and get the data we need
			foreach(XmlNode childNode in layerNode.ChildNodes)
			{
				switch(childNode.Name)
				{
					case "data":
						LoadTiles(childNode, ref layer);
						break;
					default:
						Debug.Log(childNode.Name);//These are the ones with no cases
						break;
				}//switch(chilnode)
			}//for each(childNode)

			// add the layer to the map
			map.Layers.Add(layer);
		}//SetLayerData

		static void LoadTiles(XmlNode tileNode, ref Layer layer)
		{
			// Loop through the layer attributes and get the data we nees
			foreach(XmlAttribute att in tileNode.Attributes)
			{
				switch(att.Name)
				{
					default:
						Debug.Log(att.Name + ": " + att.Value);//These are the ones with no cases
						break;
				}//switch(att.Name
			}//foreach(LayerAttributes)
			
			// loop throgh the layers children and get the data we need
			foreach(XmlNode childNode in tileNode.ChildNodes)
			{
				switch(childNode.Name)
				{
					case "tile":
						LoadTileData(childNode, ref layer);
						break;
					default:
						Debug.Log(childNode.Name);//These are the ones with no cases
						break;
				}//switch(chilnode)
			}//for each(childNode)
		}//LoadTiles

		static void LoadTileData(XmlNode tileNode, ref Layer layer)
		{
			//Create a new tile
			Tile tile = new Tile();

			// Loop through the layer attributes and get the data we nees
			foreach(XmlAttribute att in tileNode.Attributes)
			{
				switch(att.Name)
				{
					case "gid":
						tile.gid = int.Parse(att.Value) - 1;
						break;
					default:
						Debug.Log(att.Name + ": " + att.Value);//These are the ones with no cases
						break;
				}//switch(att.Name
			}//foreach(LayerAttributes)
			
			// loop throgh the layers children and get the data we need
			foreach(XmlNode childNode in tileNode.ChildNodes)
			{
				switch(childNode.Name)
				{
					default:
						Debug.Log(childNode.Name);//These are the ones with no cases
						break;
				}//switch(chilnode)
			}//for each(childNode)

			// add the tile to the layer
			layer.Tiles.Add(tile);
		}//LoadTileData
	}//class
}//namespaces