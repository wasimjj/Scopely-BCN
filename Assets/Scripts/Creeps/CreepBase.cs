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
    public virtual void Awake()
    {
        GamePlayPoolManager = FindObjectOfType<GamePlayPoolManager>();
    }
    public void Setup(float SpeedLocal, float HealthLocal , Transform Target )
    {
        Speed = SpeedLocal;
        TargetToAttach = Target;
        if (HealthLocal > 0)
        {
            Health = HealthLocal;
            MaxHealth = HealthLocal;
            return;
        }
        Health = MaxHealth;
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }
    public override void Attacked(float demage)
    {
        Health -= demage;
        if (Health <= 0)
        {
            GamePlayPoolManager.DestroyCreep(CreepType, this);
        }
        Debug.Log("Got Attaked::"+demage);
    }

    public override void Move(float Speed)
    {
       if (TargetToAttach)
        {
            transform.LookAt(TargetToAttach);
            transform.position = Vector3.MoveTowards(transform.position, TargetToAttach.position, Speed * Time.deltaTime);
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
}