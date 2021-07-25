using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerBase : MonoBehaviour
{
    [Tooltip("Add Player Health / base health")]
    public float Health;
 
    public float MaxHealth;
    void Awake()
    {
        GamePlayManager = FindObjectOfType<GamePlayManager>();
        MaxHealth = Health;
    }
    public void Attached(float Damage)
    {
        Health = Mathf.Max(0,Health- Damage);
        if (Health <= 0)
        {
            GamePlayManager.OnLoseDelegate();
        }
        GamePlayManager.OnHealthUpdateDelegate(Mathf.Clamp(Health / MaxHealth,0.0f, 1.0f));


    }

    [SerializeField]
    GamePlayManager GamePlayManager;
}
