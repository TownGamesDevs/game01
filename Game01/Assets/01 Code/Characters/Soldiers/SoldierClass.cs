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

	public BaseHP _baseHP { get; protected set; }
	public Range _range { get; protected set; }
	public Target _enemyTarget { get; protected set; }

	

}
