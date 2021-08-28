using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Ref")]
    public Camera myCamera = null;
    public CinemachineFreeLook freeLook;
    public CinemachineVirtualCamera sprintCamera;
    public CinemachineVirtualCameraBase uiCamera;

    [SerializeField] Transform aimLookTarget;
    bool isAiming;
    PlayerManager playerMgmt;

    private void Awake()
    {
        playerMgmt = GetComponent<PlayerManager>();
    }

    private void Update()
    {
        if (isAiming) { Aim(); }
    }

    public void SetAim(bool _aim_)
    {
        isAiming = _aim_;

        if (_aim_)
        {
            transform.rotation = Quaternion.Euler(0f, freeLook.m_XAxis.Value, 0f);
            sprintCamera.m_Priority = 11;
            freeLook.m_YAxisRecentering.m_enabled = true;
            freeLook.m_RecenterToTargetHeading.m_enabled = true;
            //DOVirtual.Float(playerMgmt.aimRig.weight, 1f, 0.2f, SetAimRigthWeight);
        }
        else
        {
            sprintCamera.m_Priority = 9;
            freeLook.m_RecenterToTargetHeading.RecenterNow();
            freeLook.m_YAxisRecentering.RecenterNow();
            StartCoroutine(DelayDisableRecenter());

            //DOVirtual.Float(playerMgmt.aimRig.weight, 0f, 0.2f, SetAimRigthWeight);
        }
        //void SetAimRigWeight(float _weight_)
        //{
        //    aimRig.weight = weight;
        //}
    }

    IEnumerator DelayDisableRecenter()
    {
        yield return new WaitForSeconds(0.1f);
        freeLook.m_YAxisRecentering.m_enabled = false;
        freeLook.m_RecenterToTargetHeading.m_enabled = false;
    }

    void Aim()
    {
        var rot = aimLookTarget.localRotation.eulerAngles;
        rot.x -= playerMgmt.inputMgmt.lookDelta.y;
        if (rot.x > 180)
            rot.x -= 360;
        rot.x = Mathf.Clamp(rot.x, -80, 80);
        aimLookTarget.localRotation = Quaternion.Slerp(aimLookTarget.localRotation, Quaternion.Euler(rot), 0.5f);

        rot = transform.eulerAngles;
        rot.y += playerMgmt.inputMgmt.lookDelta.x;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rot), 0.5f);
    }
}
