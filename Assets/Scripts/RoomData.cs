using UnityEngine;

[CreateAssetMenu(menuName = "Room Data")]
public class RoomData : ScriptableObject
{
    public GameObject room;
    public Vector2Int minPosition;
    public Vector2Int maxPosition;
    public bool obligatory;
}
