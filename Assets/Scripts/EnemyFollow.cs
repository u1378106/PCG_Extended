using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(player.position);
    }
}
