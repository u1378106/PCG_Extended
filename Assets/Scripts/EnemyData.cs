using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public int Health;
    public GameObject weapon;
    public GameObject hitEffect;
    public GameObject destroyEffect;
    public int cooldownTime;
    public int stunDuration;
    public int freezeDuartion;
}
