using UnityEngine;

[CreateAssetMenu(menuName = "Weapon Data")]
public class WeaponData : ScriptableObject
{
    public int Damage;
    public string message;
    public GameObject weapon;
    public int cooldownTime;
    public GameObject weaponTrailEffect;
    public int stunDuration;
    public int freezeDuartion;
}
