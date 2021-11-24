using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationExitManager : MonoBehaviour
{
    [SerializeField] GameScene_SO locationToLoad;
    [SerializeField] bool showLoadingScreen;

    [Header("Broadcasting On...")]
    [SerializeField] LoadEventChannel_SO locationExitLoadChannel;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //locationsToLoad
        }
    }
}
