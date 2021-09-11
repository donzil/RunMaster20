using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class RunEngine : MonoBehaviour
{
	GameObject player;

	Maps maps = new Maps();  // Reference to Maps.cs for method access.
	CreateGrid create = new CreateGrid(); // Reference to CreateGrid.cs for method access.
	PlayerAction action = new PlayerAction(); // Reference to PlayerAction.cs for method access.
	UI screen = new UI(); // Reference to UI.cs for method access.

	void Awake()
	{
		player = create.MakePlayer(2, 2);
		create.MakeGrid(this.gameObject, player, maps);
		screen.makeUI();
	}

	void Update()
	{
		action.PlayerControl(player, create); //for moving and player interaction
		create.CalculateEvents(player, screen);  //for height, events and safe passage
	}


}


