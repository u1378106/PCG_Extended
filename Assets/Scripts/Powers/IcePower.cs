using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePower : Weapon
{
    public GameObject weaponTrailEffect;
    public int freezeDuartion;
    private GameObject player;
    public override void Activate()
    {
        Freeze(weaponTrailEffect, GameObject.FindGameObjectWithTag("ShootPos"));
    }
}
