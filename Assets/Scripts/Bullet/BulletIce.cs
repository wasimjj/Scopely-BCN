using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Bullet
{
    class BulletIce : BulletBase
    {
        [Range(.1f, 1.0f)]
        [Tooltip("Set the how much speed is going to be slow down")]
        public float SlowDownSpell;
        [Tooltip("Set the time how much ")]
        public float SpellTime;
        private float SpeedBeforSpell;
        private bool IsUnderSpell;
        public override void ApplyDamage()
        {
            CreepBase CreepBase = TargetToAttach.GetComponent<CreepBase>();
            if (CreepBase)
            {
                StopCoroutine("StartSlowDown");
                CreepBase.Speed *= SlowDownSpell;
                IsUnderSpell = true;
                StartCoroutine("StartSlowDown");
                if (!IsUnderSpell)
                {
                    SpeedBeforSpell = CreepBase.Speed;
                }
            }
        }
        IEnumerator StartSlowDown()
        {
            yield return new WaitForSeconds(SpellTime);
            CreepBase CreepBase = TargetToAttach.GetComponent<CreepBase>();
            IsUnderSpell = false;
            if (CreepBase)
            {
                CreepBase.Speed = SpeedBeforSpell;
            }
        }
    }
}
