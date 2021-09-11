using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maps
{

        int[,] baseMap = new int[,]{{0,0,1,0,0,0,0,0,0,0},
                                    {0,5,5,1,1,1,4,4,0,0},
                                    {0,0,5,5,5,2,2,4,4,0},
                                    {0,0,1,1,5,2,2,2,1,0},
                                    {0,0,0,1,2,2,2,1,1,1},
                                    {0,0,1,1,1,3,1,1,1,1},
                                    {0,0,6,6,1,2,1,4,1,4},
                                    {0,0,0,6,1,1,1,0,1,0},
                                    {0,1,0,0,5,5,1,0,0,0},
                                    {0,0,0,0,0,0,1,0,0,0}};

        int[,] eventMap = new int[,]{   {0,0,0,0,0,0,0,0,0,0},
                                        {0,1,0,0,0,2,0,0,0,0},
                                        {0,0,0,0,0,0,0,0,0,0},
                                        {0,0,0,0,0,0,0,0,0,0},
                                        {0,0,0,0,0,0,0,0,0,0},
                                        {0,0,0,0,0,0,0,0,0,0},
                                        {0,0,0,0,0,0,0,0,0,0},
                                        {0,0,0,0,0,0,0,0,3,0},
                                        {0,0,0,0,0,0,0,0,0,0},
                                        {0,0,0,0,0,0,0,0,0,0}};

        Dictionary<int, TileTypes.Tile> mapTileDictionary = new Dictionary<int, TileTypes.Tile>();
        Dictionary<int, EventTypes.EventTile> mapEventDictionary = new Dictionary<int, EventTypes.EventTile>();

        public void GetMapDetails(out int[,] baseMapReturn, out int[,] eventMapReturn, out Dictionary<int, TileTypes.Tile> tileDictionary, out Dictionary<int, EventTypes.EventTile> eventDictionary)
        {
            CreateDictionaries();
            baseMapReturn = baseMap;
            eventMapReturn = eventMap;
            tileDictionary = mapTileDictionary;
            eventDictionary = mapEventDictionary;
        }

        public void CreateDictionaries()
        {
            //(ID, Name, Shape, Scale, Color, Center of block height, Player float height, Passable

            TileTypes.Tile noTile = new TileTypes.Tile(0, "void", PrimitiveType.Cube, new Vector3(1.0f, 1.0f, 1.0f), Color.clear, 0.0f, 1.0f, 0);   //void
            TileTypes.Tile grass = new TileTypes.Tile(1, "Grass tile", PrimitiveType.Cube, new Vector3(1.0f, 1.0f, 1.0f), Color.green, 0.0f, 0.95f, 1);   //grass
            TileTypes.Tile water = new TileTypes.Tile(2, "Water tile", PrimitiveType.Cube, new Vector3(1.0f, 0.5f, 1.0f), Color.blue, 0.0f, 0.4f, 2);   //water
            TileTypes.Tile bridge = new TileTypes.Tile(3, "Bridge tile", PrimitiveType.Cube, new Vector3(1.0f, 0.2f, 1.0f), new Color(0.6f, 0.6f, 0.0f, 1.0f), 0.45f, 1.05f, 1);  //bridge
            TileTypes.Tile wall = new TileTypes.Tile(4, "Wall tile", PrimitiveType.Cube, new Vector3(1.0f, 1.0f, 1.0f), Color.grey, 1.0f, 1.0f, 0);   //wall
            TileTypes.Tile sand = new TileTypes.Tile(5, "Sand tile", PrimitiveType.Cube, new Vector3(1.0f, 0.7f, 1.0f), new Color(0.8f, 0.8f, 0.3f, 1.0f), 0.1f, 0.8f, 1);   //sand
            TileTypes.Tile treeLeaves = new TileTypes.Tile(6, "Tree branch tile", PrimitiveType.Cylinder, new Vector3(0.7f, 0.35f, 0.7f), new Color(0.1f, 0.6f, 0.0f), 1.3f, 1.0f, 0); // tree branch
            TileTypes.Tile treeTrunk = new TileTypes.Tile(7, "Tree trunk tile", PrimitiveType.Cylinder, new Vector3(0.5f, 0.30f, 0.5f), new Color(0.5f, 0.5f, 0.0f), 0.7f, 1.0f, 0); // tree trunk
            
            //(ID, Name, Shape, Scale, Color, Center of block height, text
            EventTypes.EventTile noEvent = new EventTypes.EventTile(0, "No Event", PrimitiveType.Cube, new Vector3(1.0f, 1.0f, 1.0f), Color.clear, 0.0f, "");   //void
            EventTypes.EventTile oldMan = new EventTypes.EventTile(1, "Old man Jenkins", PrimitiveType.Cube, new Vector3(0.6f, 0.6f, 0.6f), new Color(0.6f, 0.6f, 0.6f, 1.0f), 0.8f, "Hello there, I'm just fishing."); // Old Man Jenkins
            EventTypes.EventTile westExit = new EventTypes.EventTile(2, "West Exit", PrimitiveType.Sphere, new Vector3(0.2f, 0.2f, 0.2f), Color.red, 1.0f, "You exit West");
            EventTypes.EventTile southExit = new EventTypes.EventTile(3, "South Exit", PrimitiveType.Sphere, new Vector3(0.2f, 0.2f, 0.2f), Color.red, 1.0f, "You exit South");

            mapTileDictionary.Add(0, noTile);
            mapTileDictionary.Add(1, grass);
            mapTileDictionary.Add(2, water);
            mapTileDictionary.Add(3, bridge);
            mapTileDictionary.Add(4, wall);
            mapTileDictionary.Add(5, sand);
            mapTileDictionary.Add(6, treeLeaves);
            mapTileDictionary.Add(7, treeTrunk);

            mapEventDictionary.Add(0, noEvent);
            mapEventDictionary.Add(1, westExit);
            mapEventDictionary.Add(2, oldMan);
            mapEventDictionary.Add(3, southExit);

        }


}

