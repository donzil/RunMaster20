using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CreateGrid
{
	GameObject parentObject;  //reference to the GameEngine object in the scene for blocks to be added to.
	int[,] map;  //builds a matrix for importing the tile grid
	int[,] eventMap; //builds a matrix for importing the event grid
	Dictionary<int, TileTypes.Tile> tileDictionary = new Dictionary<int, TileTypes.Tile>();
	Dictionary<int, EventTypes.EventTile> eventDictionary = new Dictionary<int, EventTypes.EventTile>();

	public void CalculateEvents(GameObject player, UI screen)  // for calculating what the player can stand on.
	{
        screen.ShowDialogue(false); //hide dialogue because moving.
		int tileID;
		int eventID;

		if (CheckIndexExists(map, Mathf.RoundToInt(player.transform.position.x), Mathf.RoundToInt(player.transform.position.z)))
		{
			tileID = map[Mathf.RoundToInt(player.transform.position.x), Mathf.RoundToInt(player.transform.position.z)];
			eventID = eventMap[Mathf.RoundToInt(player.transform.position.x), Mathf.RoundToInt(player.transform.position.z)];
		}
		else
			tileID = eventID = 0;

		if (tileDictionary[tileID].walkable == 1)
		{
			player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(player.transform.position.x, tileDictionary[tileID].playerHeight, player.transform.position.z), 0.8f * Time.deltaTime); //set height
			if (eventID.Equals(0))
			{			
				//nothing happening
			}
			else if(eventID.Equals(1) || eventID.Equals(3))
			{
				SceneManager.LoadScene("SceneA");
			}
			else
			{
				string text = eventDictionary[eventID].name + ":\n\n" + eventDictionary[eventID].dialogue; //find text to show
				screen.PresentDialogue(text);
			}
			
		}
	}

	public void UpdateCompass(Vector3 pos)
	{
		//Vector3 pos = player.transform.position;

		Vector3[] directions = new Vector3[] { new Vector3(-1, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 0, 1), new Vector3(0, 0, -1) };//north/south/east/west
		string[] cardinals = new string[] { "North: ", "South: ", "East: ", "West: " };

		for (int i = 0; i < directions.Length; i++)
		{
			try
			{
				int northTileID = map[Mathf.RoundToInt(pos.x) + Mathf.RoundToInt(directions[i].x), Mathf.RoundToInt(pos.z) + Mathf.RoundToInt(directions[i].z)];
				//Debug.Log("Compass Helper - " + cardinals[i] + tileDictionary[northTileID].name);
			}
			catch
			{
				//Debug.Log(cardinals[i] + "void");
			}
		}

	}

	public bool CanMoveAhead(GameObject player)
	{
		int forwardTileID = CheckPosition(player, (Vector3.forward) / 2.0f);
		int upperLeftTileID = CheckPosition(player, (Vector3.forward + Vector3.left / 2) / 2.5f);
		int upperRightTileID = CheckPosition(player, (Vector3.forward + Vector3.right / 2) / 2.5f);

		if (forwardTileID == -1 || upperLeftTileID == -1 || upperRightTileID == -1)
			return false;

		//Debug.Log(upperLeftTileID +","+ forwardTileID + "," + upperRightTileID);
		if (tileDictionary[forwardTileID].walkable == 1 && tileDictionary[upperLeftTileID].walkable == 1 && tileDictionary[upperRightTileID].walkable == 1)
		{
			return true;
		}
		else
		{
			Debug.Log("cannot move");
			return false;
		}

	}


	public void MakeGrid(GameObject engine, GameObject player, Maps maps)
	{
		maps.GetMapDetails(out map, out eventMap, out tileDictionary, out eventDictionary);
		parentObject = engine;

		for (int i = 0; i < map.GetLength(0); i++)
		{
			for (int j = 0; j < map.GetLength(1); j++)
			{
				MakeTile(map[i, j], i, j, parentObject);
				MakeEvent(eventMap[i, j], i, j, parentObject);
			}
		}
	}
	
	void MakeTile(int tileID, int i, int j, GameObject parentObject)  // specifying tile instructions.
	{
		if (tileID == 0)  // if empty don't make
			return;
		if(tileID == 3)  // bridge
		{
			CreateGridObject(tileID, i, j, parentObject);
			CreateGridObject(2, i, j, parentObject);
		}
		if(tileID == 4)  // wall
		{
			CreateGridObject(tileID, i, j, parentObject);
			CreateGridObject(1, i, j, parentObject);
		}
		if (tileID == 6)  // tree
		{
			CreateGridObject(tileID, i, j, parentObject);
			CreateGridObject(7, i, j, parentObject);
			CreateGridObject(1, i, j, parentObject);
		}
		else  // everything else
			CreateGridObject(tileID, i, j, parentObject);
	}

	void MakeEvent(int ID, int i, int j, GameObject parentObject)
	{
		if (ID.Equals(0))
			return;
		GameObject interaction;
		interaction = GameObject.CreatePrimitive(eventDictionary[ID].shape);
		interaction.name = eventDictionary[ID].name;
		interaction.transform.SetParent(parentObject.transform);
		interaction.transform.position = Vector3.zero + new Vector3(i, eventDictionary[ID].height, j);
		interaction.transform.localScale = (eventDictionary[ID].scale);
		interaction.GetComponent<Renderer>().material.color = eventDictionary[ID].color;
	}

	void CreateGridObject(int ID, int i, int j, GameObject parentObject)  // creating tile
	{
		GameObject tile;
		tile = GameObject.CreatePrimitive(tileDictionary[ID].shape);
		tile.name = tileDictionary[ID].name;
		tile.transform.SetParent(parentObject.transform);
		tile.transform.position = Vector3.zero + new Vector3(i, tileDictionary[ID].height, j);
		tile.transform.localScale = (tileDictionary[ID].scale);
		tile.GetComponent<Renderer>().material.color = tileDictionary[ID].color;
	}

	public GameObject MakePlayer(int i, int j)  // create the player
	{
		GameObject player;
		player = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		player.name = "Player";
		Debug.Log("Setting player name");
		player.transform.position = Vector3.zero + new Vector3(i, 1, j);
		player.transform.localScale = new Vector3(0.8f, 1.0f, 0.8f);
		player.GetComponent<Renderer>().material.color = Color.grey;


		GameObject playerHead;
		playerHead = GameObject.CreatePrimitive(PrimitiveType.Cube);
		playerHead.name = "Player Head";
		playerHead.transform.position = Vector3.zero + new Vector3(i, 1, j) + new Vector3(0f, 0.5f, 0.26f);
		playerHead.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
		playerHead.GetComponent<Renderer>().material.color = Color.white;
		playerHead.transform.SetParent(player.transform);

		GameObject debugObjects = new GameObject();
		debugObjects.name = "Debug Objects";
		debugObjects.transform.SetParent(player.transform);
		debugObjects.SetActive(false);

		GameObject forwardMarker;
		forwardMarker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		forwardMarker.name = "ForwardPoint";
		forwardMarker.transform.SetParent(debugObjects.transform);
		forwardMarker.transform.position = player.transform.position + player.transform.TransformDirection(Vector3.forward) / 2.0f;
		forwardMarker.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		forwardMarker.GetComponent<Renderer>().material.color = Color.red;

		GameObject forwardLeftMarker;
		forwardLeftMarker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		forwardLeftMarker.name = "ForwardLeftPoint";
		forwardLeftMarker.transform.SetParent(debugObjects.transform);
		forwardLeftMarker.transform.position = player.transform.position + player.transform.TransformDirection(Vector3.forward + Vector3.left / 2) / 2.5f;
		forwardLeftMarker.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		forwardLeftMarker.GetComponent<Renderer>().material.color = Color.red;

		GameObject forwardRightMarker;
		forwardRightMarker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		forwardRightMarker.name = "ForwardRightPoint";
		forwardRightMarker.transform.SetParent(debugObjects.transform);
		forwardRightMarker.transform.position = player.transform.position + player.transform.TransformDirection(Vector3.forward + Vector3.right / 2) / 2.5f;
		forwardRightMarker.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		forwardRightMarker.GetComponent<Renderer>().material.color = Color.red;

		return player;
	}

	public Vector3 Vector3Abs(Vector3 vec)
	{
		return new Vector3(Mathf.Abs(vec.x), Mathf.Abs(vec.y), Mathf.Abs(vec.z)); 
	}

	public Vector3 Vector3Round(Vector3 vec)
	{
		return new Vector3(Mathf.Round(vec.x), Mathf.Round(vec.y), Mathf.Round(vec.z));
	}

	public bool CheckIndexExists(int[,] array, int i, int j)
    {
		//Debug.Log(i + "--" + array.GetLength(0));
		//Debug.Log(j + "--" + array.GetLength(1));

		if (i < 0 || i >= array.GetLength(0))
        {
			return false;
        }
		if (j < 0 || j >= array.GetLength(1))
		{
			return false;
		}

		else return true;
    }

	public int CheckPosition(GameObject player, Vector3 range)
    {
		Vector3 posToCheck;
		posToCheck = player.transform.position + player.transform.TransformDirection(range);
		if (!CheckIndexExists(map, Mathf.RoundToInt(posToCheck.x), Mathf.RoundToInt(posToCheck.z)))
			return -1;
		int tilePassStatus = map[Mathf.RoundToInt(posToCheck.x), Mathf.RoundToInt(posToCheck.z)]; // find the tile (must be int)
		return tilePassStatus;
	}

}


