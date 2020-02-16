using UnityEngine;

public class Tshirts : Clothes
{
	private void Awake()
	{
		Debug.LogFormat("{0} on Awake.", this);

		Level = 1;
	}
}
