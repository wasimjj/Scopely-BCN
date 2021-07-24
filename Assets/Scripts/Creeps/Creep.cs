using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Creeps
{
    public abstract class Creep : UnityEngine.MonoBehaviour
    {
        public float Attackvalue;
        public float Attackedvalue;
        public float Health;
        public float MaxHealth;
        public float Speed;
        abstract public void Move(float Speed);
        abstract public void Attack();
        abstract public int OnDeath();
        abstract public void Attacked(float demage);
    }
}
