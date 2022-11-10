using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    DungeonGenerator dungeonGenerator;

    Fading fading;

    

    private void Start()
    {
        dungeonGenerator = GameObject.FindObjectOfType<DungeonGenerator>();
        fading = GameObject.FindObjectOfType<Fading>();     
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag.Equals("MainCamera"))
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(RoomGeneration());          
        }
    }

    IEnumerator RoomGeneration()
    {      
        fading.HandleFade();
        fading.isFaded = true;

        yield return new WaitForSeconds(1f);
        dungeonGenerator.RandomDungeon();
        fading.HandleFade();
        fading.isFaded = false;

        dungeonGenerator.StartMazeGeneration();
        this.transform.position = dungeonGenerator.endPoint;
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
