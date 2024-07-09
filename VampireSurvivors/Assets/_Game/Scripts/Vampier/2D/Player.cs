using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	public HP HP { get; private set; }

	PlayerController_2D playerController;

	bool isReady = false;


	public void Init()
	{
		playerController = new();
		playerController.Init(this.transform);
		HP = new HP(100);
		isReady = true;
	}

	private void FixedUpdate()
	{
		if (!isReady)
			return;
		playerController.UpdateMove();
	}

}