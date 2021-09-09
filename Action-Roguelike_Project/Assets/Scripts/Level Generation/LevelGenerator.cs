using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Room startRoomPrefab, endRoomPrefab;
    public List<Room> roomPrefabs = new List<Room>();
    public Vector2 iterationRanged = new Vector2(3, 10);

    List<Doorway> availableDoorways = new List<Doorway>();

    StartRoom startRoom;
    EndRoom endRoom;
    List<Room> placedRooms = new List<Room>();

    public LayerMask roomLayerMask;

    private void Start()
    {
        StartCoroutine("GenerateLevel");
    }

    IEnumerator GenerateLevel()
    {
        WaitForSeconds startup = new WaitForSeconds(1);
        WaitForFixedUpdate interval = new WaitForFixedUpdate();

        yield return startup;

        // Place start room
        PlaceStartRoom();
        yield return interval;

        // Random iterations
        int iterations = Random.Range((int)iterationRanged.x, (int)iterationRanged.y);

        for (int i = 0; i < iterations; i++)
        {
            // Place random room from list
            PlaceRoom();
            yield return interval;
        }

        // Place end room
        PlaceEndRoom();
        yield return interval;

        // Level generation finished
        print("Level generation finished");

        yield return new WaitForSeconds(3);
        ResetLevelGenerator();
    }

    void PlaceStartRoom()
    {
        // Instantiate room
        startRoom = Instantiate(startRoomPrefab) as StartRoom;
        startRoom.transform.parent = this.transform;

        // Get doorways from current room and add them randomly to 
        // a list of available doorways
        AddDoorwayToList(startRoom, ref availableDoorways);

        // Position the room
        startRoom.transform.position = Vector3.zero;
        startRoom.transform.rotation = Quaternion.identity;
    }

    private void AddDoorwayToList(Room _room, ref List<Doorway> _doorwayList)
    {
        foreach(Doorway doorway in _room.doorways)
        {
            int r = Random.Range(0, _doorwayList.Count);
            _doorwayList.Insert(r, doorway);
        }
    }

    void PlaceRoom()
    {
        // Instantiate Room
        Room currentRoom = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Count)]) as Room;
        currentRoom.transform.parent = this.transform;

        // Create doorway lists to loop over
        List<Doorway> allAvailableDoorways = new List<Doorway>(availableDoorways);
        List<Doorway> currentRoomDoorways = new List<Doorway>();
        AddDoorwayToList(currentRoom, ref currentRoomDoorways);

        // Get doorways from current room and add them 
        // randomly to the list of available doorways.
        AddDoorwayToList(currentRoom, ref availableDoorways);

        bool roomPlaced = false;

        // Try all available doorways


        print("Random room placed");
    }

    void PlaceEndRoom()
    {
        print("Place end room");
    }   
    
    void ResetLevelGenerator()
    {
        print("Reset level generator");

        StopCoroutine("GenerateLevel");

        // Deleate all rooms
        if (startRoom) { Destroy(startRoom.gameObject); }
        if (endRoom) { Destroy(endRoom.gameObject); }
        foreach (Room room in placedRooms)
        {
            Destroy(room.gameObject);
        }

        // Clear lists
        placedRooms.Clear();
        availableDoorways.Clear();

        // Reset coroutine
        StartCoroutine("GenerateLevel");
    }
}
