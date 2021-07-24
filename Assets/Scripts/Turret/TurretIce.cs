using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretIce : TurretBase
{
    void Start()
    {
        TargetCreep = null;
        StartCoroutine("CheckForCreepsInRaduisLoop");

    }
}
