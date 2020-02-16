using System;
using System.Collections;

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
		protected set
		{
			if (value <= 0)
				Debug.LogErrorFormat(
					"You're trying to set level zero or less. Omg, what a shame :facepalm: : '{0}",
					level);
			else level = value;
		}
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

	/// <summary>
	/// Action invoked when items were made
	/// </summary>
	public event Action<int> Made;

	/// <summary>
	/// Action invoked Amount of items changed
	/// </summary>
	public event Action AmountChanged;

	/// <summary>
	/// Action invoked when Level of items was upgraded
	/// </summary>
	public event Action<int> UpgradeBought;

	public event Action<int> LevelIncreased;

	[SerializeField]
	protected Button sellButton;

	[SerializeField]
	protected Button makeItemButton;

	/// <summary>
	/// Button for upgrading Level of Item
	/// </summary>
	[SerializeField]
	protected Button upgradeButton;

	private MoneyManager moneyManager;

	private int amount;

	private int price;

	private int level;

	private int OwnedMoneyAmount => moneyManager.Amount;

	/// <summary>
	/// Money needed to buy an upgrade
	/// </summary>
	public int MoneyNeededToUpgrade => (int)(Level * Level * Level + 0.1 * (Level + Level));

	/// <summary>
	/// 
	/// </summary>
	protected virtual void Awake()
	{
		Debug.LogFormat("{0} on Awake.", this);

		Level = 1;
		LevelIncreased?.Invoke(Level);
		moneyManager = FindObjectOfType<MoneyManager>();
		CheckIfUpgradeCanBeBought();
		moneyManager.AmountChanged += CheckIfUpgradeCanBeBought;
	}

	private void Start()
	{
		Debug.LogFormat("{0} on Start.", this);

		sellButton.onClick.AddListener(SellAll);
		makeItemButton.onClick.AddListener(MakeOneItem);
		upgradeButton.onClick.AddListener(BuyUpgrade);
		StartCoroutine(MakeItemAutomatically());
	}

	private void Update()
	{
		// CheckIfUpgradeCanBeBought();
	}

	private void OnDestroy()
	{
		try
		{
			moneyManager.AmountChanged -= CheckIfUpgradeCanBeBought;
		}
		catch (NullReferenceException e) { }
	}

	private IEnumerator MakeItemAutomatically()
	{
		while (true)
		{
			yield return new WaitForSeconds(5);

			Amount += Level;
			AmountChanged?.Invoke();
			Debug.LogFormat("{0} : Item made automatically. New Amount: {1}", this, Amount);
		}
	}

	private void SellAll()
	{
		SoldAll?.Invoke(Amount, Price);
		Amount = 0;
		AmountChanged?.Invoke();
		Debug.Log(this + " Sold");
	}

	private void MakeOneItem()
	{
		Amount += Level;

		// Debug.Log(this + " Made. Current amount: " + Amount);
		Made?.Invoke(Level);
		AmountChanged?.Invoke();
	}

	private void IncreaseLevel()
	{
		Level++;
		LevelIncreased?.Invoke(Level);
		Debug.LogFormat("Level increased. New level: {0}", Level);
		CheckIfUpgradeCanBeBought();
	}

	private void BuyUpgrade()
	{
		UpgradeBought?.Invoke(MoneyNeededToUpgrade);
		Debug.LogFormat("Upgrade bought with money: {0}", MoneyNeededToUpgrade);

		IncreaseLevel();
	}

	private void CheckIfUpgradeCanBeBought()
	{
		if (OwnedMoneyAmount >= MoneyNeededToUpgrade) upgradeButton.interactable = true;
		else upgradeButton.interactable = false;
	}
}
