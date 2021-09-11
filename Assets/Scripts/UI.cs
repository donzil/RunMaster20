using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI
{

	GameObject cameraObject;
	GameObject canvasObject;
	GameObject dialogueBoxObject;
	GameObject dialogueBackgroundObject;
	GameObject dialogueTitleObject;
	GameObject dialogueTextObject;

    public void makeUI()
    {
		cameraObject = new GameObject("Camera");
		canvasObject = new GameObject("Canvas");
		dialogueBoxObject = new GameObject("DialogueBox");
		dialogueBackgroundObject = new GameObject("DialogueBackground");
		dialogueTitleObject = new GameObject("DialogueTitle");
		dialogueTextObject = new GameObject("DialogueText");

		cameraObject.AddComponent<Camera>().orthographic = true;
		cameraObject.transform.position = new Vector3(26.5f, 20, 13.09f);
		cameraObject.transform.rotation = Quaternion.Euler(40, -110, 0);
		cameraObject.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
		cameraObject.GetComponent<Camera>().backgroundColor = Color.black;
		cameraObject.GetComponent<Camera>().orthographicSize = 4.5f;

		canvasObject.layer = 5; //set UI layer
		canvasObject.AddComponent<RectTransform>();
		canvasObject.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
		canvasObject.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
		canvasObject.AddComponent<GraphicRaycaster>();

		dialogueBoxObject.transform.SetParent(canvasObject.transform, false);
		dialogueBackgroundObject.transform.SetParent(dialogueBoxObject.transform, false);
		dialogueTitleObject.transform.SetParent(dialogueBoxObject.transform, false);
		dialogueTextObject.transform.SetParent(dialogueBoxObject.transform, false);
		dialogueTextObject.AddComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
	}


	public void PresentDialogue(string text)
    {
		dialogueTextObject.GetComponent<Text>().text = text;
		ShowDialogue(true);

	}

	public void ShowDialogue(bool action)
	{
		dialogueBoxObject.SetActive(action);
	}

}
