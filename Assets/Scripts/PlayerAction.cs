using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerAction
{
	float stepSpeed = 1.5f;

	Vector3 angleVec = Vector3.zero;
	Vector3 moveAxisRaw;// = Vector3.zero;
	Vector3 moveAxis;// = Vector3.zero;
	public void PlayerControl(GameObject player, CreateGrid create)
    {
		if (moveAxis.x != 0.0f && Math.Abs(moveAxisRaw.x) >= 0.7f)
			angleVec.x = moveAxisRaw.x;
		if (moveAxisRaw.z != 0.0f && Mathf.Abs(moveAxis.x) < 0.1f)
			angleVec.x = 0.0f;

		if (moveAxis.z != 0.0f && Math.Abs(moveAxisRaw.z) >= 0.7f)
			angleVec.z = moveAxisRaw.z;
		if (moveAxisRaw.x != 0.0f && Mathf.Abs(moveAxis.z) < 0.1f)
			angleVec.z = 0.0f;

		player.transform.rotation = Quaternion.AngleAxis(-(Mathf.Atan2(angleVec.z, angleVec.x) * Mathf.Rad2Deg), Vector3.up); //convert normal vector to radian, to deg then angle player to that angle.

		if (create.CanMoveAhead(player) && moveAxisRaw != Vector3.zero)
			player.transform.Translate(Vector3.forward * Time.deltaTime * stepSpeed);

		moveAxisRaw = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
		moveAxis = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

		if(Input.GetKeyDown("v"))  //debug key
        {
			player.transform.Find("Debug Objects").gameObject.SetActive(!player.transform.Find("Debug Objects").gameObject.activeSelf);
		}
	}
}
