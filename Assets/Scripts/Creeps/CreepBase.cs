using Assets.Scripts;
using Assets.Scripts.Creeps;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepBase : Creep
{
    [Tooltip("Assign target to attack ")]
    public Transform TargetToAttach;

    [Tooltip("Define the distance to attack to player in unity units")]
    public GameObject DeistanceToAttack;
    
    [Tooltip("Set creep type form the dropdown")]
    public CreepType CreepType;
    
    [Tooltip("Define how much this coins on his kill")]
    public int Coins = 1;
    public virtual void Awake()
    {
        GamePlayPoolManager = FindObjectOfType<GamePlayPoolManager>();
        GamePlayManager = FindObjectOfType<GamePlayManager>();
    }
    public void Setup(CreepsInfo CreepsInfo, Transform Target )
    {
        Speed = CreepsInfo.MoveSpped;
        TargetToAttach = Target;
        Attackvalue = CreepsInfo.AttackValue;
        if (CreepsInfo.Health > 0)
        {
            Health = CreepsInfo.Health;
            MaxHealth = CreepsInfo.Health;
            return;
        }
        Health = MaxHealth;
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }
    public override void Attacked(float Damage)
    {
        Health -= Damage;
        if (Health <= 0)
        {
            GamePlayPoolManager.DestroyCreep(CreepType, this);
            GamePlayManager.OnCreepKilledDelegate(Coins);
        }
    }

    public override void Move(float Speed)
    {
       if (TargetToAttach)
        {
            transform.LookAt(TargetToAttach);
            transform.position = Vector3.MoveTowards(transform.position, TargetToAttach.position, Speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, TargetToAttach.position) <= 0.5f)
            {
                PlayerBase PlayerBase = TargetToAttach.GetComponent<PlayerBase>();
                if (PlayerBase)
                {
                    PlayerBase.Attached(Attackvalue);
                }
                GamePlayPoolManager.DestroyCreep(CreepType, this);
            }
            return;
        }
    }

    public override int OnDeath()
    {
        throw new System.NotImplementedException();
    }
    public virtual void ResetSafe(Vector3 ResetLocation) 
    {
      
        Health = MaxHealth;
        gameObject.SetActive(true);
        gameObject.transform.SetPositionAndRotation(ResetLocation, Quaternion.identity);
      
    }
    public virtual void DestroySafe()
    {
        if(gameObject  != null)
        {
            gameObject.SetActive(false);
        }
    }
    [SerializeField]
    GamePlayPoolManager GamePlayPoolManager;
    [SerializeField]
    GamePlayManager GamePlayManager;
}