using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTshirts : MonoBehaviour
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

    
}
