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
					value);
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

	/// <summary>
	/// How fast the production is. This parameter is inversed, so the smaller it is, the faster the production is.
	/// </summary>
	public float ProductionSpeedInversed
	{
		get => productionSpeedInversed;
		private set
		{
			if (value <= 0f)
				Debug.LogErrorFormat(
					"You're trying to set productionSpeedInversed to zero or less. Omg, what a shame :facepalm: : '{0}",
					value);
			else productionSpeedInversed = value;
		}
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
	/// Action invoked when Upgrade was bought
	/// </summary>
	public event Action<int> UpgradeBought;

	/// <summary>
	/// Action invoked when Level of items was upgraded
	/// </summary>
	public event Action<int> LevelIncreased;

	/// <summary>
	/// Button for selling all made Clothes
	/// </summary>
	[SerializeField]
	protected Button sellButton;

	/// <summary>
	/// Button for making one item
	/// </summary>
	[SerializeField]
	protected Button makeItemButton;

	/// <summary>
	/// Button for upgrading Level of Item
	/// </summary>
	[SerializeField]
	protected Button upgradeButton;

	/// <summary>
	/// First button for accelerating production 
	/// </summary>
	[SerializeField]
	protected Button accelerateButton1;

	/// <summary>
	/// Second button for accelerating production 
	/// </summary>
	[SerializeField]
	protected Button accelerateButton2;

	/// <summary>
	/// Third button for accelerating production 
	/// </summary>
	[SerializeField]
	protected Button accelerateButton3;

	/// <summary>
	/// Fourth button for accelerating production 
	/// </summary>
	[SerializeField]
	protected Button accelerateButton4;

	private float productionSpeedInversed;

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

		ProductionSpeedInversed = 5f;
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

	private void OnDestroy()
	{
		try
		{
			moneyManager.AmountChanged -= CheckIfUpgradeCanBeBought;
		}
#pragma warning disable 168
		catch (NullReferenceException e) { }
#pragma warning restore 168
	}

	private IEnumerator MakeItemAutomatically()
	{
		while (true)
		{
			yield return new WaitForSeconds(ProductionSpeedInversed);

			Amount += Level;
			AmountChanged?.Invoke();
			Debug.LogFormat("{0} : Item made automatically. New Amount: {1}", this, Amount);
		}

		// ReSharper disable once IteratorNeverReturns
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

	private void AccelerateProduction() { }
}
