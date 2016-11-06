using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

	public static GameUI instance;

	public Text climbCost; 

	void Start()
	{
		if (instance == null)
		{
			instance = this;
		}

		UpdateClimbCost (0);
	}
	
	public void UpdateClimbCost (int _climbCost) {
		climbCost.text = "Climb Cost: " + _climbCost ;
	}
}
