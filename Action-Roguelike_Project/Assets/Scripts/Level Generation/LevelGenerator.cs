using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Room startRoomPrefab, endRoomPrefab;
    public List<Room> roomPrefabs = new List<Room>();
    public Vector2 iterationRanged = new Vector2(3, 10);

    List<Doorway> allAvailableDoorways = new List<Doorway>();

    StartRoom startRoom;
    EndRoom endRoom;
    List<Room> placedRooms = new List<Room>();

    public LayerMask roomLayerMask;

    Coroutine generationCoroutine = null;

    private void Start()
    {
        generationCoroutine = StartCoroutine("GenerateLevel");
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
        //DeleteOverlappingDoorways();
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
        AddDoorwayToList(startRoom, ref allAvailableDoorways);

        // Position the room
        startRoom.transform.position = Vector3.zero;
        startRoom.transform.rotation = Quaternion.identity;
    }

    private void AddDoorwayToList(Room _room, ref List<Doorway> _doorwayList)
    {
        foreach (Doorway doorway in _room.doorways)
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
        List<Doorway> currentAvailableDoorways = new List<Doorway>(allAvailableDoorways);
        List<Doorway> currentRoomDoorways = new List<Doorway>();
        AddDoorwayToList(currentRoom, ref currentRoomDoorways);

        // Get doorways from current room and add them 
        // randomly to the list of available doorways.
        AddDoorwayToList(currentRoom, ref allAvailableDoorways);

        bool roomPlaced = false;

        // Try all available doorways
        foreach (Doorway availableDoorway in currentAvailableDoorways)
        {
            Doorway currentDoorway = currentRoomDoorways[Random.Range(0, currentRoomDoorways.Count)];

            // Position room
            PositionRoomAtDoorway(ref currentRoom, currentDoorway, availableDoorway);

            //  Check room overlaps
            if(CheckRoomOverlap(currentRoom))
            {
                // If overlap detected - skip to next iteration of the loop
                continue;
            }

            // No overlap, set this to true and...
            roomPlaced = true;

            // ...add room to list of placed rooms
            placedRooms.Add(currentRoom);

            // Remove occupied doorways
            currentDoorway.gameObject.SetActive(false);
            allAvailableDoorways.Remove(currentDoorway);

            availableDoorway.gameObject.SetActive(false);
            allAvailableDoorways.Remove(availableDoorway);

            // Exit loop if room has been placed.
            if (roomPlaced) { break; }
        }

        // Room couldn't be placed. Restart generator and try again
        if(!roomPlaced)
        {
            Destroy(currentRoom.gameObject);
            ResetLevelGenerator();
        }
    }

    private void PositionRoomAtDoorway(ref Room _room, Doorway _roomDoorway, Doorway _targetDoorway)
    {
        // Reset room position and rotation
        _room.transform.position = Vector3.zero;
        _room.transform.rotation = Quaternion.identity;

        // Rotate room to match previous doorway orientation
        Vector3 targetDoorwayEuler = _targetDoorway.transform.eulerAngles;
        Vector3 roomDoorwayEuler = _roomDoorway.transform.eulerAngles;
        float deltaAngle = Mathf.DeltaAngle(roomDoorwayEuler.y, targetDoorwayEuler.y);
        Quaternion currentRoomTargetRotation = Quaternion.AngleAxis(deltaAngle, Vector3.up);
        _room.transform.rotation = currentRoomTargetRotation * Quaternion.Euler(0, 180f, 0);

        // Position room
        Vector3 roomPositionOffset = _roomDoorway.transform.position - _room.transform.position;
        _room.transform.position = _targetDoorway.transform.position - roomPositionOffset;
    }

    bool CheckRoomOverlap(Room _room)
    {
        // Check if room's bounds are overlapping other rooms.
        Bounds bounds = _room.RoomBounds;
        bounds.center = _room.transform.position;
        bounds.Expand(-0.1f);

        Collider[] colliders = Physics.OverlapBox(bounds.center, bounds.size / 2, _room.transform.rotation, roomLayerMask);
        if(colliders.Length > 0)
        {
            // Ignore collisions with current room
            foreach (Collider collider in colliders)
            {
                if (collider.transform.parent.gameObject.Equals(_room.gameObject))
                {
                    continue;
                }
                else
                {
                    Debug.LogError("Overlap detected");
                    return true;
                }
            }
        }

        return false;
    }

    void PlaceEndRoom()
    {
        // Instantiate Room
        endRoom = Instantiate(endRoomPrefab) as EndRoom;
        endRoom.transform.parent = this.transform;

        // Create doorway lists to loop over
        List<Doorway> currentAvailableDoorways = new List<Doorway>(allAvailableDoorways);
        Doorway doorway = endRoom.doorways[0];

        bool roomPlaced = false;

        // Try all available doorways
        foreach (Doorway availableDoorway in currentAvailableDoorways)
        {
            Room room = (Room)endRoom;
            PositionRoomAtDoorway(ref room, doorway, availableDoorway);

            //  Check room overlaps
            if (CheckRoomOverlap(endRoom))
            {
                // If overlap detected - skip to next iteration of the loop
                continue;
            }

            // No overlap, set this to true and...
            roomPlaced = true;

            // Remove occupied doorways
            doorway.gameObject.SetActive(false);
            allAvailableDoorways.Remove(doorway);

            availableDoorway.gameObject.SetActive(false);
            allAvailableDoorways.Remove(availableDoorway);

            // Exit loop if room has been placed.
            if (roomPlaced) { break; }
        }

        // Room couldn't be placed. Restart.
        if(!roomPlaced)
        {
            ResetLevelGenerator();
        }
    }   
    
    void DeleteOverlappingDoorways()
    {
        foreach (Doorway doorway in allAvailableDoorways)
        {
            Collider[] doorColliders = Physics.OverlapBox(doorway.transform.position, Vector3.one, Quaternion.identity);
            if(doorColliders.Length == 2)
            {
                foreach (Collider collider in doorColliders)
                {
                    Destroy(collider.gameObject);
                }
            }
        }
    }

    void ResetLevelGenerator()
    {
        print("Reset level generator");

        StopCoroutine(generationCoroutine);

        // Deleate all rooms
        if (startRoom) { Destroy(startRoom.gameObject); }
        if (endRoom) { Destroy(endRoom.gameObject); }
        foreach (Room room in placedRooms)
        {
            Destroy(room.gameObject);
        }

        // Clear lists
        placedRooms.Clear();
        allAvailableDoorways.Clear();

        // Reset coroutine
        StartCoroutine("GenerateLevel");
    }
}
