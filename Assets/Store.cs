using System;

using UnityEngine;

public class Store : MonoBehaviour
{
    private int tshirtsMade;
    private int tshirtPrice;
    private int level = 1;
    
    public int TshirtsMade
    {
        get => tshirtsMade;
        private set => tshirtsMade = value;
    }

    public int TshirtPrice
    {
        get => tshirtPrice;
        private set => tshirtPrice = value;
    }

    private void Awake()
    {
        TshirtPrice = level;
    }

    public event Action<int, int> AllTshirtsSold;
    public event Action<int> TshirtMade;

    
    /// <summary>
    /// 
    /// </summary>
    public void Sell()
    {
        var earn = TshirtsMade * TshirtPrice;
        AllTshirtsSold?.Invoke(TshirtsMade, earn);
        TshirtsMade = 0;
    }

    /// <summary>
    /// 
    /// </summary>
    public void AddTshirtMade()
    {
        TshirtsMade += 1;
        TshirtMade?.Invoke(1);
    }
    
}
