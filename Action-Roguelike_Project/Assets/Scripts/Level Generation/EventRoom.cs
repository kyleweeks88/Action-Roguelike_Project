using System.Collections.Generic;
using UnityEngine;

public class EventRoom : Room
{
    //public event System.Action OnRoomComplete;
    public delegate void OnRoomComplete(Room room);
    public event OnRoomComplete onRoomCompleteDelegate;
    public string roomName;
    [SerializeField] Transform eventSpawnLocation;
    LevelGenerator levelGenerator;

    public List<RoomEvent> eventPrefabPool = new List<RoomEvent>();
    //public List<GameObject> roomEventPrefabs = new List<GameObject>();

    private void Start()
    {
        //LoadPrefabEvent();
        levelGenerator = GetComponentInParent<LevelGenerator>();
        //if (levelGenerator != null)
        //{
        //    Debug.Log(levelGenerator.transform.name);
        //    foreach (var item in levelGenerator.roomEventPrefabs)
        //    {
        //        Debug.Log(item.name);
        //    }
        //}
        //else
        //{
        //    Debug.LogError("Error");
        //}
    }

    public void LoadPrefabEvent()
    {
        RoomEvent[] _prefabPool = Resources.LoadAll<RoomEvent>("Room Events");
        int length = _prefabPool.Length;

        if (length != 0)
        {
            foreach (RoomEvent eventPrefab in _prefabPool)
            {
                eventPrefabPool.Add(eventPrefab);
                Debug.Log("LoadPrefabEvent");
            }
        }
    }

    void SpawnEventObject()
    {
        Debug.Log("SpawnEventObject");
        //int rand = Random.Range(0, levelGenerator.roomEventPrefabs.Count);
        int rand = Random.Range(0, eventPrefabPool.Count);
        RoomEvent eventObject = Instantiate(eventPrefabPool[rand], eventSpawnLocation.position, eventSpawnLocation.rotation) as RoomEvent;
        eventObject.OnEventComplete += RoomComplete;
        eventObject.transform.parent = this.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            foreach (Doorway doorway in doorways)
            {
                doorway.gameObject.SetActive(true);
            }

            SpawnEventObject();
            print("Entered");
        }
    }

    void RoomComplete()
    {
        print("ROOM COMPLETE!");
        onRoomCompleteDelegate?.Invoke(this);

        //foreach (Doorway doorway in doorways)
        //{
        //    doorway.gameObject.SetActive(false);
        //}
    }
}
