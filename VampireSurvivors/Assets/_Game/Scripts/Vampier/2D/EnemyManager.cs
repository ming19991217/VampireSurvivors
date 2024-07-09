using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager
{

	Player player;
	List<Enemy> enemies = new();
	EnemyGridSystem enemyGrid;

	public EnemyManager(EnemyGridSystem enemyGridSystem, Player player)
	{
		this.player = player;
		this.enemyGrid = enemyGridSystem;
	}

	public void RegisterEnemy(Enemy enemyIns)
	{
		Vector2Int oldXY = enemyGrid.GetXY(enemyIns.transform.position);
		enemyGrid.AddValue(oldXY, enemyIns);

		enemyIns.Init(player, Random.Range(1, 3),
		 onMove: (enemy) =>
		 {
			 var xy = enemyGrid.GetXY(enemy.transform.position);
			 enemyGrid.UpdateGridPosition(enemy, xy, oldXY);
			 oldXY = xy;
		 });

		enemies.Add(enemyIns);
	}

	public void TakeDamage(Vector2Int xy, int damage)
	{
		var enemys = enemyGrid.GetValues(xy);
		if (enemys == null || enemys?.Count == 0)
			return;

		for (int i = 0; i < enemys.Count; i++)
		{
			enemys[i].HP.TakeDamage(damage);
		}

		var dead = enemies.Where(x => x.HP.CurrentHP <= 0).ToList();


		Debug.LogError("Dead Count: " + dead.Count);

		enemyGrid.ClearValues(xy);

		for (int i = 0; i < dead.Count; i++)
		{
			var enemy = dead[i];
			enemies.Remove(enemy);
			enemy.DestroyEnemy();
		}





	}


	public void EnemyUpdate()
	{
		foreach (var enemy in enemies)
		{
			enemy.UpdateEnemey();
		}
	}



}