using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	Vector2 direction;
	float speed;
	bool isReady = false;



	public void Init(Vector2 direction, float speed)
	{
		this.direction = direction;
		this.speed = speed;
		isReady = true;
	}


	public void BulletUpdate()
	{
		if (!isReady)
			return;

		transform.Translate(direction * speed * Time.deltaTime);
	}

}