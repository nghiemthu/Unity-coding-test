using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	public int gridSizeX;
	public int gridSizeY;

	public LayerMask collisionMask;
	public Cell cellPrefab;

	[Range (0,1)]
	public float outlinePercent;

	public int seed;

	public Cell[,] cells;

	public Color lowestColor;
	public Color highestColor;

	[HideInInspector]
	public List<Cell> path = new List<Cell>();

	void Awake()
	{
		GenerateGrid ();
	}

	public void GenerateGrid () 
	{
		//Grid Holder 
		Transform gridHolder = transform.FindChild("Grid Holder");
		if (gridHolder != null)
		{
			Destroy (gridHolder.gameObject);
		}
		gridHolder = new GameObject ("Grid Holder").transform;
		gridHolder.transform.parent = transform;

		//Generate Grid Cell
		cells = new Cell[gridSizeX, gridSizeY];
		Vector3 bottomLeftPosition =  transform.position - gridSizeX/2 * Vector3.right -gridSizeY/2*Vector3.forward;

		System.Random prng = new System.Random (seed);

		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = 0; y < gridSizeY; y++)
			{
				int randomHeight = prng.Next (0,128);
				Vector3 cellPosition = bottomLeftPosition + Vector3.right * (x + .5f) + Vector3.forward * (y + .5f);
				bool walkable = Physics.CheckSphere (cellPosition, .5f , collisionMask);
				Cell newCell = Instantiate (cellPrefab, cellPosition, Quaternion.Euler(Vector3.right * 90)) as Cell;

				newCell.transform.parent = gridHolder;
				newCell.transform.localScale = Vector3.one * (1 - outlinePercent);

				float heightPercent = (float)randomHeight / 127;
				newCell.GetComponent<Renderer>().material.color = Color.Lerp (lowestColor, highestColor, heightPercent);


				newCell.SetCell (false, !walkable, x, y, randomHeight);
				cells[x,y] = newCell;
			}
		}
	}

	public void SetCellColor(Cell cell, Color color)
	{
		Material cellMaterial = cell.GetComponent<Renderer> ().material;
		cellMaterial.color = color;
	}

	public void ResetColor()
	{
		foreach (Cell cell in cells)
		{
			if (cell.walkable && !cell.taken)
			{
				float heightPercent = (float)cell.height / 127;
				cell.GetComponent<Renderer>().material.color = Color.Lerp (lowestColor, highestColor, heightPercent);
			}
		}
	}

	public List<Cell> GetNeighbor(Cell cell)
	{
		List<Cell> neighborList = new List<Cell> ();
		for (int x=-1; x<=1; x++)
		{
			for (int y = -1; y<=1; y++)
			{
				if (x==0 && y==0)
				{
					continue;
				}
					
				if (x == 0 || y==0)
				{
					int neighborX = cell.gridX + x;
					int neighborY = cell.gridY + y;

					if (neighborX >= 0 && neighborX < gridSizeX && neighborY >= 0 && neighborY < gridSizeY)
					{
						neighborList.Add (cells[neighborX,neighborY]);
					}
				}
			}
		}

		return neighborList;
	}
		
	public Cell FromPositionToCell(Vector3 position)
	{
		int gridX = (int)(position.x + gridSizeX / 2);
		int gridY = (int)(position.z + gridSizeY / 2);
		gridX = Mathf.Clamp (gridX, 0, gridSizeX -1);
		gridY = Mathf.Clamp (gridY, 0, gridSizeY -1);

		return cells [gridX, gridY];
	}

//	private void OnDrawGizmos()
//	{
//		if (cells != null)
//		{
//			foreach(Cell cell in cells)
//			{
//				Gizmos.color = !cell.walkable? Color.red: cell.taken? Color.yellow: Color.white;
//				if (path.Contains(cell))
//					Gizmos.color = Color.black;
//				Gizmos.DrawCube (cell.transform.position, Vector3.one * .9f);
//			}
//		}
//	}
}
