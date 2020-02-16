using System;

using UnityEngine;
using UnityEngine.UI;

public abstract class Clothes : MonoBehaviour
{
#region Public Properties
	/// <summary>
	/// Level of Clothes. The higher it is, the more units are made in the background.
	/// </summary>
	public int Level
	{
		get => level;
		protected set => level = value;
	}

	/// <summary>
	/// Amount of Clothes made / owned.
	/// </summary>
	public int Amount
	{
		get => amount;
		private set => amount = value;
	}

	/// <summary>
	/// Price for one item of Clothes.
	/// </summary>
	public int Price
	{
		get
		{
			price = level;
			return price;
		}
	}

	public void Upgrade()
	{
		Level++;
	}
#endregion

	/// <summary>
	/// Sold Action with parameters for Amount and Price of sold items
	/// </summary>
	public event Action<int, int> SoldAll;

	public event Action<int> Made;

	public event Action AmountChanged;

	[SerializeField]
	protected Button sellButton;

	[SerializeField]
	protected Button makeItemButton;

	private int amount;

	protected int price;

	private int level;

	private Store store;

	private void SellAll()
	{
		SoldAll?.Invoke(Amount, Price);
		Amount = 0;
		AmountChanged?.Invoke();
		Debug.Log(this + " Sold");
	}

	private void MakeOneItem()
	{
		Amount++;
		Debug.Log(this + " Made. Current amount: " + Amount);
		Made?.Invoke(1);
		AmountChanged?.Invoke();
	}

	private void Awake()
	{		Debug.LogFormat("{0} on Awake.", this);

		store = FindObjectOfType<Store>();
	}

	private void Start()
	{
		sellButton.onClick.AddListener(SellAll);
		makeItemButton.onClick.AddListener(MakeOneItem);
	}

	private void OnDestroy() { }
}
