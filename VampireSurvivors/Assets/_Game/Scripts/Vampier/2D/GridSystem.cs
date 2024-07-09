using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class GridSystem<T>
{
	//https://www.youtube.com/watch?v=p47_LeMEFlY
	//
	//
	//
	int width;
	int height;
	List<T>[,] gridArray;
	int cellSize;

	public GridSystem(int width, int height, int cellSize, Transform displayRoot)
	{
		this.width = width;
		this.height = height;
		this.cellSize = cellSize;


		gridArray = new List<T>[width, height];

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				gridArray[x, y] = new List<T>();
				CreateWorldText("0", displayRoot, Color.white, GetCellPostition(x, y), 10, TextAnchor.MiddleCenter, TextAlignment.Center);
			}
		}

		Vector3 GetCellPostition(int x, int y)
		{
			return new Vector3(x - width * .5f, y - height * .5f) * cellSize;
		}

	}

	public Vector2Int GetXY(Vector3 worldPosition)
	{
		int x, y;
		x = Mathf.FloorToInt((worldPosition.x + width * .5f) / cellSize);
		y = Mathf.FloorToInt((worldPosition.y + height * .5f) / cellSize);
		return new Vector2Int(x, y);
	}

	public void AddValue(Vector2Int xy, T value)
	{
		if (xy.x >= 0 && xy.y >= 0 && xy.x < width && xy.y < height)
		{
			gridArray[xy.x, xy.y].Add(value);
		}
	}

	public void RemoveValue(Vector2Int xy, T value)
	{
		if (xy.x >= 0 && xy.y >= 0 && xy.x < width && xy.y < height)
		{
			gridArray[xy.x, xy.y].Remove(value);
		}
	}

	public void ClearValues(Vector2Int xy)
	{
		if (xy.x >= 0 && xy.y >= 0 && xy.x < width && xy.y < height)
		{
			gridArray[xy.x, xy.y].Clear();
		}
	}


	public List<T> GetValues(Vector2Int xy)
	{
		if (xy.x >= 0 && xy.y >= 0 && xy.x < width && xy.y < height)
		{
			return gridArray[xy.x, xy.y];
		}
		else
		{
			return null;
		}
	}


	static TextMesh CreateWorldText(string text, Transform parent, Color color, Vector3 localPosition = default(Vector3), int fontSize = 40, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = 0)
	{
		GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
		Transform transform = gameObject.transform;
		transform.SetParent(parent, false);
		transform.localPosition = localPosition;
		TextMesh textMesh = gameObject.GetComponent<TextMesh>();
		textMesh.anchor = textAnchor;
		textMesh.alignment = textAlignment;
		textMesh.text = text;
		textMesh.fontSize = fontSize;
		textMesh.color = color;
		textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
		return textMesh;
	}

}

public class EnemyGridSystem : GridSystem<Enemy>
{
	public EnemyGridSystem(int width, int height, int cellSize, Transform displayRoot) : base(width, height, cellSize, displayRoot)
	{

	}

	public void UpdateGridPosition(Enemy enemy, Vector2Int newXY, Vector2Int oldXY)
	{
		RemoveValue(oldXY, enemy);
		AddValue(newXY, enemy);
	}
}