using Assets.Scripts;
using Assets.Scripts.Turret;
using System.Collections;
using Unity.Collections;
using UnityEngine;

public class TurretBase : Turret
{
    
    [Tooltip("Creep Target to attack and it is read only property ")]
    [ReadOnly]
    public Transform TargetCreep;
    [Tooltip("Creep Target to attack ")]
    [ReadOnly]
    bool IsActiveForFiring ;
    [Tooltip("Difine Turret Raduis or range for to detect creep and attach ")]
    public float Radius;
    [Tooltip("Difine cool down time for next bullet shot")]
    public float CoolDownTime;
    [Tooltip("Define sensor time after that time turret will detect enemis around it  in seconds")]
    public float SensorTime;
    [Tooltip("Select type of the bullet from dropdown")]
    public BulletType BulletType;
    [Tooltip("Define the cost of each turret in coins")]
    public int CostInCoins;


    public virtual void Awake()
    {
        GamePlayPoolManager = FindObjectOfType<GamePlayPoolManager>();
    }
    public virtual void CheckForCreepsInRaduis()
    {
        CheckIfCreepOutOfRange();
        int layerMask = 1 << LayerMask.NameToLayer("Creep");
        Collider[] colliders = Physics.OverlapSphere(transform.position, Radius, layerMask);
        if (colliders.Length > 0)
        {

            if (TargetCreep == null)
            {
                TargetCreep = colliders[0].gameObject.GetComponentInParent<CreepBase>().transform;
                StartFire();
            }

            return;
        }
        if (TargetCreep != null)
        {
            StopFire();
        }
        TargetCreep = null;

        void CheckIfCreepOutOfRange()
        {
            if (TargetCreep != null)
            {
                if (!TargetCreep.gameObject.activeSelf || 
                    Vector3.Distance(transform.position, TargetCreep.position) >= Radius)
                {
                    TargetCreep = null;
                    StopFire();
                }
            }
        }


    
}
    IEnumerator CheckForCreepsInRaduisLoop()
    {
        WaitForSeconds WaitForSeconds = new WaitForSeconds(SensorTime);
        while (true)
        {
            
            CheckForCreepsInRaduis();
            yield return WaitForSeconds;
        }
    }
    public virtual void StartFire()
    {
        StartCoroutine("StartFireLoop");

    }
    public virtual void StopFire()
    {
        IsActiveForFiring = false;
        StopCoroutine("StartFireLoop");
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 5);
    }
    IEnumerator StartFireLoop()
    {
        WaitForSeconds WaitForSeconds = new WaitForSeconds(CoolDownTime);
        while (true)
        {
            yield return WaitForSeconds;
            Shoot();
        }
    }
    public virtual void Shoot() 
    {
        if (TargetCreep)
        {
            BulletBase Bullet = null;
            switch (BulletType)
            {
                case BulletType.Noraml:
                    Bullet = GamePlayPoolManager.GetBullet(BulletType.Noraml).GetComponent<BulletBase>();
                    break;
                case BulletType.Ice:
                    Bullet = GamePlayPoolManager.GetBullet(BulletType.Ice).GetComponent<BulletBase>();
                    break;
            }
            if (Bullet)
            {
                Bullet.ResetSafe(transform.position);
                Bullet.Setup(TargetCreep);
            }
         
        }
      
    }
    [SerializeField]
    GamePlayPoolManager GamePlayPoolManager;
}
