using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CineMachineFollowSwitcher : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Transform groupTransform;
    [SerializeField] Transform playerTransform;

    [SerializeField] CinemachineVirtualCamera virtualCamera;

    public void ChangeToGroupCamera()
    {
        virtualCamera.Follow = groupTransform;
    }

    public void ChangeToPlayerCamera()
    {
        virtualCamera.Follow = playerTransform;
    }
}
