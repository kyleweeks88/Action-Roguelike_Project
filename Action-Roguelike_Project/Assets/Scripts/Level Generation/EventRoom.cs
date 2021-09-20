using System.Collections.Generic;
using UnityEngine;

public class EventRoom : Room
{
    public string roomName;
    [SerializeField] Transform eventSpawnLocation;
    LevelGenerator levelGenerator;

    //List<GameObject> eventPrefabPool = new List<GameObject>();

    private void Start()
    {
        levelGenerator = GetComponentInParent<LevelGenerator>();
        if(levelGenerator != null)
        {
            Debug.Log(levelGenerator.transform.name);
            foreach (var item in levelGenerator.roomEventPrefabs)
            {
                Debug.Log(item.name);
            }
        }
        else
        {
            Debug.LogError("Error");
        }
    }

    void SpawnEventObject()
    {
        int rand = Random.Range(0, levelGenerator.roomEventPrefabs.Count);
        GameObject eventObject = Instantiate(levelGenerator.roomEventPrefabs[rand], eventSpawnLocation.position, eventSpawnLocation.rotation);
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
