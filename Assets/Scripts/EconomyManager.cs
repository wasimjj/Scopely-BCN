using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    [Tooltip("Game econmy coins")]
    public int Coins;

    void Awake()
    {
        GamePlayManager = FindObjectOfType<GamePlayManager>();
    }
    void Start()
    {
        GamePlayManager.OnCoinsUpdateDelegate(Coins);

    }
    public void AddCoins(int coins)
    {
        Coins += coins;
        GamePlayManager.OnCoinsUpdateDelegate(Coins);
    }
    public void DeductCoins(int coins)
    {
        Coins -= coins;
        if (Coins < 0)
        {
            Coins = 0;
        }
        GamePlayManager.OnCoinsUpdateDelegate(Coins);
    }
    [SerializeField]
    GamePlayManager GamePlayManager;
}
