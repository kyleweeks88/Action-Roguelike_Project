using UnityEngine;

public class EventRoom : Room
{
    // EVENT ROOM CODE
    public string roomName;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            foreach (Doorway doorway in doorways)
            {
                doorway.gameObject.SetActive(true);
            }
        }
    }
}
