using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class ImpulseManager : MonoBehaviour
{
    [SerializeField] PlayerEventChannel playerEventChannel;
    public CinemachineImpulseSource _damageImpulse;

    void Start()
    {
        playerEventChannel.damageRecieved_Event += DamageImpulse;
    }

    public void DamageImpulse(float dmgVal)
    {
        _damageImpulse.GenerateImpulse();
        // TAKE IN AN ImpulseSource AND MANAGE IT'S SETTINGS
        // BASED ON HEALTH VALUE? DAMAGE AMOUNT? CRITICAL HIT?
    }

    public void HandleDirectionalImpulse(CinemachineImpulseSource _source, Vector3 _dir)
    {
        _source.GenerateImpulse(new Vector3(-_dir.x, _dir.y, -_dir.z));
    }
}
