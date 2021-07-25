using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]  
    GameObject WinPopup;
    
    [SerializeField]
    GameObject LosePopup;
    
    [SerializeField]
    Text TextCoins;
   
    [Tooltip("Add Player Health / base health")]
    [SerializeField]
    Image PlayerHealth;


    public delegate void OnNotifyDelegate();


    void Awake()
    {
        GamePlayManager = FindObjectOfType<GamePlayManager>();
        GamePlayManager.OnWinDelegate = OnWinCallback;
        GamePlayManager.OnLoseDelegate = OnLoseCallback;
        GamePlayManager.OnCoinsUpdateDelegate = OnCoisUpdateCallback;
        GamePlayManager.OnHealthUpdateDelegate += OnHealthUpdateCallback;
    }
  
    private void OnWinCallback()
    {
        WinPopup.SetActive(true);
    }
    private void OnLoseCallback()
    {
        LosePopup.SetActive(true);
    }
    private void OnCoisUpdateCallback(int Coins)
    {
        TextCoins.text = Coins.ToString();
    }
    private void OnHealthUpdateCallback(float Health)
    {
        PlayerHealth.rectTransform.localScale = new Vector3(Health, 1, 1);
    }

    [SerializeField]
    GamePlayManager GamePlayManager;
}
