using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserInput : MonoBehaviour {

	private Grid grid;
	private PathFinding pathfinding;
	private List<Cell> userSelection = new List<Cell>();

	void Start()
	{
		grid = GetComponent<Grid> ();
		pathfinding = GetComponent<PathFinding> ();
	}

	void Update () {

		//User Mouse Input

		if (Input.GetMouseButtonDown(0))
		{
			Plane plane = new Plane (Vector3.up, Vector3.zero);
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			Vector3 hitPosition;
			float distance;

			if (plane.Raycast(ray, out distance))
			{
				hitPosition = ray.GetPoint (distance);
				Cell cell = grid.FromPositionToCell (hitPosition);

				if(!cell.taken && cell.walkable)
				{
					cell.taken = true;
					grid.SetCellColor (cell, Color.yellow);
					userSelection.Add (cell);

					if (userSelection.Count > 2)
					{
						Cell thirdCell = userSelection[0];
						grid.SetCellColor (thirdCell, Color.white);
						thirdCell.taken = false;
						userSelection.RemoveAt(0);
					}

					if (userSelection.Count > 1)
					{
						pathfinding.Pathfinding (userSelection [0], userSelection [1]);
					}
				}
			}
		}
	}
}
