using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class PathFinding : MonoBehaviour {

	private Grid grid;

	[HideInInspector]
	public string climbCost;

	void Start () {
		grid = GetComponent<Grid> ();
	}
		
	public void Pathfinding(Vector3 pointA, Vector3 pointB)
	{
		Cell cellA = grid.FromPositionToCell (pointA);
		Cell cellB = grid.FromPositionToCell (pointB);

		Pathfinding (cellA, cellB);
	}

	public void Pathfinding (Cell cellA, Cell cellB) 
	{
		Stopwatch sw = new Stopwatch();
		sw.Start();

	//	List<Cell> openList = new List<Cell> ();
		Heap<Cell> openList = new Heap<Cell>(grid.gridSizeX * grid.gridSizeY);
		HashSet<Cell> closedList = new HashSet<Cell> ();

		openList.Add (cellA);

		while (true)
		{
//			Cell currentCell = openList [0];
//			foreach (Cell cell in openList)
//			{
//				if (cell.fCost <= currentCell.fCost)
//				{
//					if (cell.hCost < currentCell.hCost)
//					{
//						currentCell = cell;
//					}
//				}
//			}


			Cell currentCell = openList.RemoveFirstItem ();
			closedList.Add (currentCell);

			if (currentCell == cellB)
			{
				sw.Stop ();
				print ("Pathfinding Time: " + sw.ElapsedMilliseconds + " Millisecondes");
				grid.path = GetPath (cellA, cellB);
				break;
			}

			foreach (Cell neighbor in grid.GetNeighbor(currentCell))
			{
				if (!neighbor.walkable || closedList.Contains(neighbor))
				{
					continue;
				}

				int newCostToNeighbor = currentCell.gCost + Distance (currentCell, neighbor) + HeightCost(currentCell, neighbor);
				if (newCostToNeighbor < neighbor.gCost || !openList.Contains(neighbor))
				{
					neighbor.gCost = newCostToNeighbor;
					neighbor.hCost = Distance (neighbor, cellB);
					neighbor.parent = currentCell;

					if (!openList.Contains (neighbor)) {
						openList.Add (neighbor);
					} else {
						openList.SortUp (neighbor);
					}
				}
			}

		}
	}

	public List<Cell> GetPath(Cell cellA, Cell cellB)
	{
		int totalCost = 0;
		List<Cell> path = new List<Cell> ();
		grid.ResetColor ();
		Cell currentCell = cellB;

		while (true)
		{
			currentCell = currentCell.parent;
			if (currentCell == cellA)
			{
				GameUI.instance.UpdateClimbCost (totalCost);
				break;
			}
			totalCost += HeightCost (currentCell.parent, currentCell);
			path.Add (currentCell);
			grid.SetCellColor (currentCell, Color.gray);
		}

		return path;
	}

	private int Distance(Cell cellA, Cell cellB)
	{
		int distanceX = Mathf.Abs (cellA.gridX - cellB.gridX);
		int distanceY = Mathf.Abs (cellA.gridY - cellB.gridY);

		if (distanceX > distanceY)
		{
			return 10 * (distanceX - distanceY);
		} else 
			return 10 * (distanceY - distanceX);
	}

	private int HeightCost(Cell cellA, Cell cellB)
	{
		return (cellB.height > cellA.height) ? 30 : 0;
	}
}
