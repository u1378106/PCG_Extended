using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    [SerializeField]
    private float currentHealth;

    [SerializeField]
    private GameObject damageParticle;
    [SerializeField]
    private GameObject destroyParticle;
    [SerializeField]
    private Image healthBar;

    AudioManager audioManager;
    PlayerWeapon _playerWeapon;

    private void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        _playerWeapon = GameObject.FindObjectOfType<PlayerWeapon>();
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject _damageParticle = Instantiate(damageParticle, this.transform.position, Quaternion.identity);
        Destroy(other.gameObject);
        if (_playerWeapon.currentPower.name == "Electric")
        {
            Stun(_playerWeapon.currentPower.GetComponent<ElectricPower>().stunDuration);
            TakeDamage(other.gameObject.GetComponent<Weapon>().Damage);
        }
        else if (_playerWeapon.currentPower.name == "Ice")
        {
            Freeze(_playerWeapon.currentPower.GetComponent<IcePower>().freezeDuartion);
            TakeDamage(other.gameObject.GetComponent<Weapon>().Damage);
        }
        else
            TakeDamage(other.gameObject.GetComponent<Weapon>().Damage);
    }

    internal void TakeDamage(float amount)
    {
        audioManager.damage.Play();
        currentHealth -= amount;
        healthBar.fillAmount -= amount / 100;

        if (currentHealth <= 0)
        {
            audioManager.blast.Play();
            GameObject _destroyParticle = Instantiate(destroyParticle, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    internal void Stun(int seconds)
    {
        Debug.Log("Stun for : " + seconds);
        StartCoroutine(Stunned(seconds));
    }

    IEnumerator Stunned(int time)
    {
        this.GetComponent<EnemyFollow>().enabled = false;
        this.GetComponent<NavMeshAgent>().enabled = false;
        yield return new WaitForSeconds(time);
        this.GetComponent<EnemyFollow>().enabled = true;
        this.GetComponent<NavMeshAgent>().enabled = true;
    }

    internal void Freeze(int seconds)
    {
        Debug.Log("Frozen for : " + seconds);
        StartCoroutine(Frozen(seconds));
    }

    IEnumerator Frozen(int time)
    {
        this.GetComponent<EnemyFollow>().enabled = false;
        this.GetComponent<NavMeshAgent>().enabled = false;
        yield return new WaitForSeconds(time);
        this.GetComponent<EnemyFollow>().enabled = true;
        this.GetComponent<NavMeshAgent>().enabled = true;
    }
}
