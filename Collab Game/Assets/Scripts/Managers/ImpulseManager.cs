using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class ImpulseManager : MonoBehaviour
{
    public UnityEvent damageImpulse;

    public CinemachineImpulseSource _damageImpulse;

    public void DamageImpulse()
    {
        // TAKE IN AN ImpulseSource AND MANAGE IT'S SETTINGS
        // BASED ON HEALTH VALUE? DAMAGE AMOUNT? CRITICAL HIT?
    }
}
