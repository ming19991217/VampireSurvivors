using System;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	Vector2 direction;
	float speed;
	EnemySpatialGroups enemySpatialGroups;
	Action<List<Enemy>> onHitEnemies;

	bool isReady = false;

	public void Init(Vector2 direction, float speed, Action<List<Enemy>> onHitEnemy)
	{
		this.direction = direction;
		this.speed = speed;
		this.enemySpatialGroups = EnemySpatialGroups.instance;
		this.onHitEnemies = onHitEnemy;
		isReady = true;
	}

	void FixedUpdate()
	{
		if (!isReady)
			return;

		transform.Translate(direction * speed * Time.deltaTime);
		var gridIndex = enemySpatialGroups.GetGridIndex(this.transform.position);
		var enemys = enemySpatialGroups.GetEnemiesAtGridIndex(gridIndex);

		if (enemys == null || enemys.Count > 0)
			onHitEnemies(enemys);
	}

}