using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEvent : MonoBehaviour
{
    public event System.Action OnEventComplete;
    // RoomEvent will choose a random event to happen.

    // These events will load data from Scriptable Objects?

    public virtual void EventComplete()
    {
        OnEventComplete?.Invoke();
    }
}
