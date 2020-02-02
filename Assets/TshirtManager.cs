using System;

using UnityEngine;

public class TshirtManager : MonoBehaviour
{
    private int tshirtAmountMade;
    private int priceToSell;
    private int level = 1;
    
    public int TshirtAmountMade
    {
        get => tshirtAmountMade;
        private set => tshirtAmountMade = value;
    }

    public int PriceToSell
    {
        get => priceToSell;
        private set => priceToSell = value;
    }

    private void Awake()
    {
        PriceToSell = level;
    }

    public static event Action<int> Sold;
    public static event Action<int> Made;

    
    /// <summary>
    /// 
    /// </summary>
    public void Sell()
    {
        var earn = TshirtAmountMade * PriceToSell;
        TshirtAmountMade = 0;
        Sold?.Invoke(earn);
    }

    /// <summary>
    /// 
    /// </summary>
    public void AddTshirtMade()
    {
        TshirtAmountMade += 1;
        Made?.Invoke(1);
    }
    
}
