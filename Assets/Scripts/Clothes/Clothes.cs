using System;
using System.Collections;

using BigInteger = System.Numerics.BigInteger;

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

	/// <summary>
	/// Money needed to buy first acceleration update
	/// </summary>
	public int MoneyNeedToAccelerate1
	{
		get => moneyNeedToAccelerate1;
	}

	/// <summary>
	/// Money needed to buy second acceleration update
	/// </summary>
	public int MoneyNeedToAccelerate2
	{
		get => moneyNeedToAccelerate2;
	}

	/// <summary>
	/// Money needed to buy third acceleration update
	/// </summary>
	public int MoneyNeedToAccelerate3
	{
		get => moneyNeedToAccelerate3;
	}

	/// <summary>
	/// Money needed to buy fourth acceleration update
	/// </summary>
	public int MoneyNeedToAccelerate4
	{
		get => moneyNeedToAccelerate4;
	}

	/// <summary>
	/// Money needed to buy fifth acceleration update
	/// </summary>
	public int MoneyNeedToAccelerate5
	{
		get => moneyNeedToAccelerate5;
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
	public event Action LevelIncreased;

	/// <summary>
	/// Action invoked when ProductionSpeed is increased
	/// </summary>
	public event Action ProductionSpeedIncreased;

	/// <summary>
	/// Action invoked when every achievement was achieved.
	/// </summary>
	public event Action GameFinished;

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

	/// <summary>
	/// Fifth button for accelerating production 
	/// </summary>
	[SerializeField]
	protected Button accelerateButton5;

	private MoneyManager moneyManager;

	private UIManager uiManager;

	private float productionSpeedInversed;

	private int amount;

	private int price;

	private int level;

	private int moneyNeedToAccelerate1 = 1000;

	private int moneyNeedToAccelerate2 = 10000;

	private int moneyNeedToAccelerate3 = 100000;

	private int moneyNeedToAccelerate4 = 500000;

	private int moneyNeedToAccelerate5 = 1000000;

	private bool accelerationBought1;

	private bool accelerationBought2;

	private bool accelerationBought3;

	private bool accelerationBought4;

	private bool accelerationBought5;

	private BigInteger OwnedMoneyAmount => moneyManager.Amount;

	/// <summary>
	/// Money needed to buy an upgrade.
	/// </summary>
	public int MoneyNeededToUpgrade => (int)(Level * Level * Level + 0.1 * (Level + Level));

	/// <summary>
	/// Unity's Awake method.
	/// </summary>
	protected virtual void Awake()
	{
		Debug.LogFormat("{0} on Awake.", this);

		ProductionSpeedInversed = 5f;
		Level = 1;
		LevelIncreased?.Invoke();
		moneyManager = FindObjectOfType<MoneyManager>();
		uiManager = FindObjectOfType<UIManager>();
		CheckIfAnyUpgradeCanBeBought();
		moneyManager.AmountChanged += CheckIfAnyUpgradeCanBeBought;
		uiManager.GameStarted += StartGame;
		uiManager.GameFinished += StopGame;
	}

	private void Start()
	{
		Debug.LogFormat("{0} on Start.", this);

		sellButton.onClick.AddListener(SellAll);
		makeItemButton.onClick.AddListener(MakeOneItem);
		upgradeButton.onClick.AddListener(BuyUpgrade);
		accelerateButton1.onClick.AddListener(AccelerateProduction1);
		accelerateButton2.onClick.AddListener(AccelerateProduction2);
		accelerateButton3.onClick.AddListener(AccelerateProduction3);
		accelerateButton4.onClick.AddListener(AccelerateProduction4);
		accelerateButton5.onClick.AddListener(AccelerateProduction5);
	}

	private void OnDestroy()
	{
		try
		{
			moneyManager.AmountChanged -= CheckIfAnyUpgradeCanBeBought;
		}
#pragma warning disable 168
		catch (NullReferenceException e) { }
#pragma warning restore 168

		try
		{
			uiManager.GameStarted -= StartGame;
		}
#pragma warning disable 168
		catch (NullReferenceException e) { }
#pragma warning restore 168

		try
		{
			uiManager.GameFinished -= StopGame;
		}
#pragma warning disable 168
		catch (NullReferenceException e) { }
#pragma warning restore 168
	}

	private void StartGame()
	{
		StartCoroutine(MakeItemAutomatically());
	}

	private void StopGame()
	{
		StopAllCoroutines();
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
		LevelIncreased?.Invoke();
		Debug.LogFormat("Level increased. New level: {0}", Level);
		CheckIfAnyUpgradeCanBeBought();
	}

	private void BuyUpgrade()
	{
		UpgradeBought?.Invoke(MoneyNeededToUpgrade);
		Debug.LogFormat("Upgrade bought with money: {0}", MoneyNeededToUpgrade);

		IncreaseLevel();
	}

	private void CheckIfAnyUpgradeCanBeBought()
	{
		if (OwnedMoneyAmount >= MoneyNeededToUpgrade) upgradeButton.interactable = true;
		else upgradeButton.interactable = false;
		if (OwnedMoneyAmount >= MoneyNeedToAccelerate1 && !accelerationBought1) accelerateButton1.interactable = true;
		else accelerateButton1.interactable = false;
		if (OwnedMoneyAmount >= MoneyNeedToAccelerate2 && !accelerationBought2) accelerateButton2.interactable = true;
		else accelerateButton2.interactable = false;
		if (OwnedMoneyAmount >= MoneyNeedToAccelerate3 && !accelerationBought3) accelerateButton3.interactable = true;
		else accelerateButton3.interactable = false;
		if (OwnedMoneyAmount >= MoneyNeedToAccelerate4 && !accelerationBought4) accelerateButton4.interactable = true;
		else accelerateButton4.interactable = false;
		if (OwnedMoneyAmount >= MoneyNeedToAccelerate5 && !accelerationBought5) accelerateButton5.interactable = true;
		else accelerateButton5.interactable = false;
	}

	private void AccelerateProduction1()
	{
		ProductionSpeedInversed /= 2;
		accelerationBought1 = true;

		// accelerateButton1.image.color = Color.gray;
		accelerateButton1.image.color = new Color(0.5f, 0.5f, 0.5f, 0.3f);

		ProductionSpeedIncreased?.Invoke();
		UpgradeBought?.Invoke(MoneyNeedToAccelerate1);

		CheckIfAnyUpgradeCanBeBought();

		CheckGameFinish();
	}

	private void AccelerateProduction2()
	{
		ProductionSpeedInversed /= 2;
		accelerationBought2 = true;
		accelerateButton2.image.color = new Color(0.5f, 0.5f, 0.5f, 0.3f);

		ProductionSpeedIncreased?.Invoke();
		UpgradeBought?.Invoke(MoneyNeedToAccelerate2);

		CheckIfAnyUpgradeCanBeBought();

		CheckGameFinish();
	}

	private void AccelerateProduction3()
	{
		ProductionSpeedInversed /= 2;
		accelerationBought3 = true;
		accelerateButton3.image.color = new Color(0.5f, 0.5f, 0.5f, 0.3f);

		ProductionSpeedIncreased?.Invoke();
		UpgradeBought?.Invoke(MoneyNeedToAccelerate3);

		CheckIfAnyUpgradeCanBeBought();

		CheckGameFinish();
	}

	private void AccelerateProduction4()
	{
		ProductionSpeedInversed /= 2;
		accelerationBought4 = true;
		accelerateButton4.image.color = new Color(0.5f, 0.5f, 0.5f, 0.3f);

		ProductionSpeedIncreased?.Invoke();
		UpgradeBought?.Invoke(MoneyNeedToAccelerate4);

		CheckIfAnyUpgradeCanBeBought();

		CheckGameFinish();
	}

	private void AccelerateProduction5()
	{
		ProductionSpeedInversed /= 2;
		accelerationBought5 = true;
		accelerateButton5.image.color = new Color(0.5f, 0.5f, 0.5f, 0.3f);

		ProductionSpeedIncreased?.Invoke();
		UpgradeBought?.Invoke(MoneyNeedToAccelerate5);

		CheckIfAnyUpgradeCanBeBought();

		CheckGameFinish();
	}

	/// <summary>
	/// Checks if game is finished and if yes, invokes the event
	/// </summary>
	public void CheckGameFinish()
	{
		if (accelerationBought1 &&
		    accelerationBought2 &&
		    accelerationBought3 &&
		    accelerationBought4 &&
		    accelerationBought5)
		{
			Debug.LogError("You've finished the game!!!!");
			GameFinished?.Invoke();
		}
	}
}
