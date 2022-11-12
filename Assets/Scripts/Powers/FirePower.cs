using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePower : Weapon
{
    public GameObject weaponTrailEffect;

    public override void Activate()
    {
        Attack(weaponTrailEffect, GameObject.FindGameObjectWithTag("ShootPos"));
    }
}
