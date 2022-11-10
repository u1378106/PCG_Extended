using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPower : Weapon
{
    public GameObject weaponTrailEffect;
    public int stunDuration;

    public override void Activate()
    {
        Stun(weaponTrailEffect, GameObject.FindGameObjectWithTag("Player"));
    }
}
