using UnityEngine;
using System.Collections;

public class Cell: MonoBehaviour, IHeapItem<Cell>{

	[HideInInspector] public bool walkable;
	[HideInInspector] public bool taken;
	[HideInInspector] public int height;

	[HideInInspector] public int gCost; //The cost from the cell to cellA
	[HideInInspector] public int hCost; //The cost from the cell to cellB

	[HideInInspector] public int gridX;
	[HideInInspector] public int gridY;

	[HideInInspector] public int _heapIndex;
	[HideInInspector] public Cell parent;

	public int fCost 
	{
		get 
		{
			return gCost + hCost;
		}
	}

	public int heapIndex
	{
		get
		{
			return _heapIndex;
		}

		set
		{
			_heapIndex = value;
		}
	}

	public void SetCell(bool _taken, bool _walkable, int _gridX, int _gridY, int _height )
	{
		taken = _taken;
		walkable = _walkable;
		gridX = _gridX;
		gridY = _gridY;
		height = _height;
	}

	public int CompareTo(Cell cell)
	{
		int value = fCost.CompareTo (cell.fCost);
		if (value == 0)
		{
			value = hCost.CompareTo (cell.hCost);
		}

		return value;
	}
}
