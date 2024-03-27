using System.Collections.Generic;
using UnityEngine;

public class EnemySpatialGroups
{
	//https://www.youtube.com/watch?v=p47_LeMEFlY
	//
	//
	public static EnemySpatialGroups instance;

	public float cellSize = 1f;

	int width;
	int height;
	List<Enemy>[,] grid;

	public EnemySpatialGroups()
	{
		width = 1000;
		height = 1000;

		grid = new List<Enemy>[width, height];

		instance = this;
	}


	public Vector2Int GetGridIndex(Vector3 position)
	{
		int x = Mathf.FloorToInt(position.x / cellSize);
		int y = Mathf.FloorToInt(position.z / cellSize);
		return new Vector2Int(x, y);
	}

	public void AddEnemy(Vector2Int gridIndex, Enemy enemy)
	{
		if (grid[gridIndex.x, gridIndex.y] == null)
			grid[gridIndex.x, gridIndex.y] = new List<Enemy>();

		grid[gridIndex.x, gridIndex.y].Add(enemy);
	}

	public void RemoveEnemy(Vector2Int gridIndex, Enemy enemy)
	{
		if (grid[gridIndex.x, gridIndex.y] == null)
			return;

		grid[gridIndex.x, gridIndex.y].Remove(enemy);
	}

	public List<Enemy> GetEnemiesAtGridIndex(Vector2Int gridIndex)
	{
		return grid[gridIndex.x, gridIndex.y];
	}

}