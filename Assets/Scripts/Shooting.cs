using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
	[SerializeField]
	private LayerMask mask;

	[SerializeField]
	private int damage;

	[SerializeField]
	private GameObject shootRef;

	[SerializeField]
	private GameObject playerPower;

	AudioManager audioManager;


	private void Start()
    {
		audioManager = GameObject.FindObjectOfType<AudioManager>();
	}

    private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Shoot();
		}
	}

	private void Shoot()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, mask))
		{
			EnemyAI ai = hit.collider.GetComponent<EnemyAI>();
			if(ai != null)
			{
				audioManager.shoot.Play();
				GameObject projectile = Instantiate(playerPower, shootRef.transform.position, Quaternion.identity);
				projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
				ai.TakeDamage(damage);
			}
		}
	}
}
