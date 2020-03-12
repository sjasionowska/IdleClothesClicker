using UnityEngine;

public class Tshirts : Clothes
{
	protected override void Awake()
	{
		base.Awake();
		Debug.LogFormat("{0} on Awake.", this);

		Level = 1;
	}
}
