using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    private List<Image> weaponIcons;

    [SerializeField]
    private List<Weapon> weaponSet;

    int weaponIndex;

    Color tempColor;

    public Weapon currentPower;

    AudioManager audioManager;

    private FirePower firePower;

    private IcePower icePower;

    private ElectricPower electricPower;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    public void CheckForWeapon(int i, string weaponName)
    {
        audioManager.collect.Play();

        weaponIcons[i - 1].sprite = Resources.Load<Sprite>("Icons/" + weaponName);
        tempColor = weaponIcons[i - 1].color;
        tempColor.a = 0.7f;
        weaponIcons[i - 1].color = tempColor;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            audioManager.weaponChange.Play();
            currentPower = weaponSet[0];

            tempColor.a = 1f;
            weaponIcons[0].color = tempColor;

            tempColor.a = 0.7f;
            weaponIcons[1].color = tempColor;
            weaponIcons[2].color = tempColor;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            audioManager.weaponChange.Play();
            currentPower = weaponSet[1];

            tempColor.a = 1f;
            weaponIcons[1].color = tempColor;

            tempColor.a = 0.7f;
            weaponIcons[0].color = tempColor;
            weaponIcons[2].color = tempColor;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            audioManager.weaponChange.Play();
            currentPower = weaponSet[2];

            tempColor.a = 1f;
            weaponIcons[2].color = tempColor;

            tempColor.a = 0.7f;
            weaponIcons[0].color = tempColor;
            weaponIcons[1].color = tempColor;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attacking();
        }
    }

    private void Attacking()
    {       
        //audioManager.shoot.Play();
        currentPower.Activate();       
    }

    private void OnCollisionEnter(Collision other)
    {
       
        if (other.gameObject.name == "FirePower")
        {
            Debug.Log("collided with ... " + other.gameObject.name);
            weaponIndex++;
            weaponSet.Add(Resources.Load<Weapon>("WeaponData/Fire"));
            CheckForWeapon(weaponIndex, "FirePower");
            Destroy(other.gameObject);
        }

        else if (other.gameObject.name == "IcePower")
        {
            weaponIndex++;
            weaponSet.Add(Resources.Load<Weapon>("WeaponData/Ice"));
            CheckForWeapon(weaponIndex, "IcePower");
            Destroy(other.gameObject);
        }

        else if (other.gameObject.name == "ElectricPower")
        {
            weaponIndex++;
            weaponSet.Add(Resources.Load<Weapon>("WeaponData/Electric"));
            CheckForWeapon(weaponIndex, "ElectricPower");
            Destroy(other.gameObject);

        }
        else if (other.gameObject.name == "BFG")
        {
            weaponIndex++;
            weaponSet.Add(Resources.Load<Weapon>("WeaponData/BFG"));
            CheckForWeapon(weaponIndex, "BFGPower");
            Destroy(other.gameObject);

        }
    }
}
