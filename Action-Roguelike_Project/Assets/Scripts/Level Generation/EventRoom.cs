using System.Collections.Generic;
using UnityEngine;

public class EventRoom : Room
{
    public string roomName;
    [SerializeField] Transform eventSpawnLocation;
    LevelGenerator levelGenerator;

    public List<GameObject> eventPrefabPool = new List<GameObject>();
    //public List<GameObject> roomEventPrefabs = new List<GameObject>();

    private void Start()
    {
        //LoadPrefabEvent();
        //levelGenerator = GetComponentInParent<LevelGenerator>();
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
        GameObject[] _prefabPool = Resources.LoadAll<GameObject>("Room Events");
        int length = _prefabPool.Length;

        if (length != 0)
        {
            foreach (GameObject eventPrefab in _prefabPool)
            {
                eventPrefabPool.Add(eventPrefab);
            }
        }
    }

    void SpawnEventObject()
    {
        //int rand = Random.Range(0, levelGenerator.roomEventPrefabs.Count);
        int rand = Random.Range(0, eventPrefabPool.Count);
        GameObject eventObject = Instantiate(eventPrefabPool[rand], eventSpawnLocation.position, eventSpawnLocation.rotation);
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
}
