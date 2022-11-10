using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    DungeonGenerator _dungeonGenerator;

    public GameObject generator;
    public GameObject checkpointPrefab;
    public GameObject[] enemies;
    public GameObject[] powerups;

    private int tempIndex, currentRoomIndex;

    List<int> numbers = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        _dungeonGenerator = GameObject.FindObjectOfType<DungeonGenerator>();
        _dungeonGenerator.StartMazeGeneration();
        if (_dungeonGenerator.isMazeDone)
        {
            GameObject checkpoint = GameObject.Instantiate(checkpointPrefab, _dungeonGenerator.endPoint, Quaternion.identity);
        }

        foreach(GameObject enemy in enemies)
        {
            tempIndex = currentRoomIndex;
            currentRoomIndex = Random.Range(1, _dungeonGenerator.transform.childCount - 2);
            
            enemy.transform.position = _dungeonGenerator.transform.GetChild(currentRoomIndex).GetChild(5).transform.position;
        }
    }

  

    public int NewNumber(int r)
    {

        int a = 0;

        while (a == 0)
        {
            a = Random.Range(0, r);
            if (!numbers.Contains(a))
            {
                numbers.Add(a);
            }
            else
            {
                a = 0;
            }
        }
        return a;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
