using Assets.Scripts;
using Assets.Scripts.Bullet;
using System.Collections;
using Unity.Collections;
using UnityEngine;

public class BulletBase : Bullet
{
    [Tooltip("How much damage applt to the hit object")]
    public float Damagevalue;
    [Tooltip("Enable if bullet will damage on area")]
    public bool IsAreaDamage;
    [Tooltip("Define radius if is area adamge it will not work if area damage is not enabled")]
    public float Radius;
    [Tooltip("Define perabolic speed")]
    public float Speed = 10;
    [Tooltip("Define how much this bullet can damage")]
    public float DamageValue = 10;
    [ReadOnly]
    [Tooltip("This is the target to attack and it is read only property")]
    public Transform TargetToAttach;
    [Tooltip("Set bullet type form the dropdown")]
    public BulletType BulletType;

    public virtual void Awake()
    {
        GamePlayPoolManager = FindObjectOfType<GamePlayPoolManager>();
    }
    public virtual void Setup(Transform target)
    {
        TargetToAttach = target;
        StartCoroutine(Parabola());
    }
    public virtual void ApplyDamage()
    {
        CreepBase CreepBase = TargetToAttach.gameObject.GetComponentInParent<CreepBase>();
        if (CreepBase)
        {
            CreepBase.Attacked(DamageValue);
        }
    }
    IEnumerator Parabola()
    {
        WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
        float distanceToTarget = Vector3.Distance(transform.position, TargetToAttach.position);
        Vector3 targetPos = TargetToAttach.position;
        float currentDist = Vector3.Distance(transform.position, TargetToAttach.position);
        float Angle = Mathf.Min(1, Vector3.Distance(transform.position, targetPos) / distanceToTarget) * 45;
        while (currentDist >= 0.2f)
        {
            transform.LookAt(targetPos);
            // Enable if bullet has some kind visual direction like arrow or something 
            transform.rotation = transform.rotation * Quaternion.Euler(Mathf.Clamp(-Angle, -42, 42), 0, 0); 
            currentDist = Vector3.Distance(transform.position, TargetToAttach.position);
            transform.Translate(Vector3.forward * Mathf.Min(Speed * Time.deltaTime, currentDist));
            Angle = Mathf.Min(1, Vector3.Distance(transform.position, targetPos) / distanceToTarget) * 45;
            targetPos = TargetToAttach.position;
            yield return WaitForEndOfFrame;
        }
        StopCoroutine(Parabola());
        ApplyDamage();
        GamePlayPoolManager.DestroyBullet(BulletType,this);


}
    public virtual void ResetSafe(Vector3 ResetLocation)
    {

        gameObject.SetActive(true);
        gameObject.transform.SetPositionAndRotation(ResetLocation, Quaternion.identity);

    }
    public virtual void DestroySafe()
    {
        if (gameObject != null)
        {
            gameObject.SetActive(false);
        }
    }
    [SerializeField]
    GamePlayPoolManager GamePlayPoolManager;
}
