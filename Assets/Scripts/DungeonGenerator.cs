using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DungeonGenerator : MonoBehaviour
{
    public RoomData[] rooms;
    RoomData dungeonToLoad;

    public Vector3 startPoint;
    public Vector3 endPoint;
    public int roomCount;
    public int dungoenCounter;
    public bool isMazeDone;
    List<RoomData> dungoens = new List<RoomData>();
    private GameObject dungeonName;
    public GameObject dungeonNamePrefab;

    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

        public int ProbabilityOfSpawning(int x, int y, int index)
        {

            if (x >= rooms[index].minPosition.x && x <= rooms[index].maxPosition.x && y >= rooms[index].minPosition.y && y <= rooms[index].maxPosition.y)
            {
                return rooms[index].obligatory ? 2 : 1;
            }

            return 0;
        }

    public Vector2Int size;
    public int startPos = 0;
    public Vector2 offset;

    List<Cell> board;

 
    private void Awake()
    {
        for (int i = 1; i < 4; i++)
        {
            dungoens.Add((RoomData)(Resources.Load("RoomObjects/NewRoom" + i)));
        }

        RandomDungeon();
    }

    public void RandomDungeon()
    {

        dungeonToLoad = dungoens[Random.Range(0, dungoens.Count)];
    }


    public void StartMazeGeneration()
    {
        if (transform.childCount != 0)
        {
            foreach (Transform cell in transform)
            {
                Destroy(cell.gameObject);
            }
        }
        MazeGenerator();

        startPoint = transform.GetChild(0).GetChild(5).transform.position;
        Camera.main.transform.position = startPoint;
        roomCount = transform.childCount;
        endPoint = transform.GetChild(roomCount - 1).GetChild(5).transform.position;
        dungoenCounter++;
        Destroy(dungeonName);
        dungeonName = GameObject.Instantiate(dungeonNamePrefab, startPoint, Quaternion.identity);
        dungeonName.transform.GetChild(0).transform.position = startPoint;
        dungeonName.GetComponentInChildren<TextMeshProUGUI>().text = "Dungeon " + dungoenCounter;
    }

    void GenerateDungeon()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Cell currentCell = board[(i + j * size.x)];
                if (currentCell.visited)
                {                
                    List<int> availableRooms = new List<int>();

                    for (int k = 0; k < rooms.Length; k++)
                    {
                        int p = ProbabilityOfSpawning(i, j, k);

                        if (p == 2)
                        {                          
                            break;
                        }
                        else if (p == 1)
                        {
                            availableRooms.Add(k);
                        }
                    }

                    if (dungeonToLoad.name == "NewRoom1")                  
                        offset = new Vector2(8, 8);
                    else if (dungeonToLoad.name == "NewRoom2")
                        offset = new Vector2(4, 4);
                    else
                        offset = new Vector2(5.5f, 5.5f);
                    var newRoom = Instantiate(dungeonToLoad.room, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                    newRoom.UpdateRoom(currentCell.status);
                    newRoom.name += " " + i + "-" + j;

                }
            }
        }
        isMazeDone = true;
    }

    void MazeGenerator()
    {
        board = new List<Cell>();

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPos;

        Stack<int> path = new Stack<int>();

        int k = 0;

        while (k < 1000)
        {
            k++;

            board[currentCell].visited = true;

            if (currentCell == board.Count - 1)
            {
                break;
            }

            //Check the cell's neighbors
            List<int> neighbors = CheckNeighbors(currentCell);

            if (neighbors.Count == 0)
            {
                if (path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);

                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                if (newCell > currentCell)
                {
                    //down or right
                    if (newCell - 1 == currentCell)
                    {
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;
                    }
                    else
                    {
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;
                    }
                }
                else
                {
                    //up or left
                    if (newCell + 1 == currentCell)
                    {
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;
                    }
                    else
                    {
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[currentCell].status[1] = true;
                    }
                }

            }

        }
        GenerateDungeon();
    }

    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();

        //check up neighbor
        if (cell - size.x >= 0 && !board[(cell - size.x)].visited)
        {
            neighbors.Add((cell - size.x));
        }

        //check down neighbor
        if (cell + size.x < board.Count && !board[(cell + size.x)].visited)
        {
            neighbors.Add((cell + size.x));
        }

        //check right neighbor
        if ((cell + 1) % size.x != 0 && !board[(cell + 1)].visited)
        {
            neighbors.Add((cell + 1));
        }

        //check left neighbor
        if (cell % size.x != 0 && !board[(cell - 1)].visited)
        {
            neighbors.Add((cell - 1));
        }

        return neighbors;
    }
}