using Assets.Scripts;
using Assets.Scripts.Creeps;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayPoolManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Tooltip("Pool of small creeps being created this will be used for optimization so if can avoid destroy and instantiate")]
    public List<CreepBase> SmallCreeps;
    [Tooltip("Pool of big creeps being created this will be used for optimization so if can avoid destroy and instantiate")]
    public List<CreepBase> BigCreeps;
    [Tooltip("Pool of big shpere bullets being created this will be used for optimization so if can avoid destroy and instantiate")]
    public List<BulletBase> NormalBullets;
    [Tooltip("Pool of big shpere bullets being created this will be used for optimization so if can avoid destroy and instantiate")]
    public List<BulletBase> IceBullets;
    [Tooltip("Small Creep perset or prefab to instantiate")]
    public GameObject SmallCreepPrest;
    [Tooltip("Big Creep perset or prefab to instantiate")]
    public GameObject BigCreepPrest;
    [Tooltip("Normal Bullet perset or prefab to instantiate")]
    public GameObject NormalBulletPrest;
    [Tooltip("Ice Bulelt perset or prefab to instantiate")]
    public GameObject IceBulletPresetPrest;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public BulletBase GetBullet(BulletType BulletType)
    {
        switch (BulletType)
        {
            case BulletType.Noraml:
                if (NormalBullets.Count > 0)
                {
                    BulletBase Bullet = NormalBullets[0];
                    NormalBullets.RemoveAt(0);
                    return Bullet;

                }
                return CreateBullet(BulletType);
            case BulletType.Ice:
                if (IceBullets.Count > 0)
                {
                    BulletBase Bullet = IceBullets[0];
                    IceBullets.RemoveAt(0);
                    return Bullet;

                }
                return CreateBullet(BulletType);
        }
        return null;   
    }
        public CreepBase GetCreep(CreepType CreepType)
    {
        switch (CreepType)
        {
            case CreepType.SmallCreepType:
                if (SmallCreeps.Count > 0)
                {
                    CreepBase creep = SmallCreeps[0];
                    SmallCreeps.RemoveAt(0);
                    return creep;

                }
                return CreateCreep(CreepType);
            case CreepType.BigCreepType:
                if (BigCreeps.Count > 0)
                {
                    CreepBase creep = BigCreeps[0];
                    SmallCreeps.RemoveAt(0);
                    return creep;

                }
                return CreateCreep(CreepType);
        }
        return null;
    }
    public CreepBase CreateCreep(CreepType CreepType)
    {

        switch (CreepType)
        {
            case CreepType.SmallCreepType:
                return Instantiate(SmallCreepPrest).GetComponent<CreepBase>();
            case CreepType.BigCreepType:
                return Instantiate(BigCreepPrest).GetComponent<CreepBase>();
        }
        return null;
    }
    public BulletBase CreateBullet(BulletType BulletType)
    {

        switch (BulletType)
        {
            case BulletType.Noraml:
                return Instantiate(NormalBulletPrest).GetComponent<BulletBase>();
            case BulletType.Ice:
                return Instantiate(IceBulletPresetPrest).GetComponent<BulletBase>();
        }
        return null;
    }
    public void DestroyCreep(CreepType CreepType , CreepBase CreepInstance)
    {

        switch (CreepType)
        {
            case CreepType.SmallCreepType:
                SmallCreeps.Add(CreepInstance);
                CreepInstance.DestroySafe();
                break;
            case CreepType.BigCreepType:
                BigCreeps.Add(CreepInstance);
                CreepInstance.DestroySafe();
                break;
        }
    }
    public void DestroyBullet(BulletType BulletType, BulletBase BulletBase)
    {
        switch (BulletType)
        {
            case BulletType.Noraml:
                NormalBullets.Add(BulletBase);
                BulletBase.DestroySafe();
                break;
            case BulletType.Ice:
                IceBullets.Add(BulletBase);
                BulletBase.DestroySafe();
                break;
        }
    }

}

