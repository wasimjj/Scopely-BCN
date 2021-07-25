using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Turret
{
    class TurretNormal : TurretBase
    {
        void Start()
        {
            TargetCreep = null;
            StartCoroutine("CheckForCreepsInRaduisLoop");
        }
    }
}
