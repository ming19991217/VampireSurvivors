using System.Collections.Generic;
using UnityEngine;

public class BulletManager
{
	List<Bullet> bullets = new();

	EnemyManager enemyManager;
	EnemyGridSystem enemyGridSystem;

	public BulletManager(EnemyManager enemyManager, EnemyGridSystem enemyGridSystem)
	{
		this.enemyGridSystem = enemyGridSystem;
		this.enemyManager = enemyManager;
	}

	public void AddBullet(Weapon weapon, Bullet bullet)
	{
		bullet.Init(weapon.Direction, Random.Range(5, 10));
		bullets.Add(bullet);
	}

	public void BulletUpdate()
	{
		for (int i = 0; i < bullets.Count; i++)
		{
			bullets[i].BulletUpdate();
		}

		for (int i = 0; i < bullets.Count; i++)
		{
			TriggerCheck(bullets[i]);
		}

	}

	void TriggerCheck(Bullet bullet)
	{
		var xy = enemyGridSystem.GetXY(bullet.transform.position);
		var enemys = enemyGridSystem.GetValues(xy);

		if (enemys?.Count > 0)
		{
			enemyManager.TakeDamage(xy, 1);
			bullets.Remove(bullet);
			GameObject.Destroy(bullet.gameObject);
		}
	}





}