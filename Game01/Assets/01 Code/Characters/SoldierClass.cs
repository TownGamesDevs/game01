using TMPro;
using UnityEngine;

public class SoldierClass : MonoBehaviour
{
	public enum BaseHP
	{
		_100 = 100,
		_75 = 75,
		_125 = 125,
		_150 = 150,
		_175 = 175,
		_200 = 200
	}

	public enum Range
	{
		Short,
		Medium,
		Long
	}

	public enum Target
	{
		Closest,
		Strongest,
		Fastest,
	}


	// Constants
	const int DEFAULT_HP = 100;
	const int MAX_HP = 200;

	// Variables
	private int _current_hp;
	public BaseHP _baseHP { get; protected set; }
	public Range _range { get; protected set; }
	public Target _enemyTarget { get; protected set; }

	

}
