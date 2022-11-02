using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Vector2 maxFollowOffset = new Vector2(-1f, 6f);
    [SerializeField] private Vector2 cameraVelocity = new Vector2(4f, 0.25f);
    [SerializeField] private CinemachineVirtualCamera virtualCamera = null;


    private CinemachineTransposer transposer;

    public void Start()
    {
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

        virtualCamera.gameObject.SetActive(true);

        enabled = true;
    }


    private void Update()
    {
        /*float h = Input.GetAxis("Mouse X") * cameraVelocity.x;
        float v = Input.GetAxis("Mouse Y") * cameraVelocity.y;
        float deltaTime = Time.deltaTime;
        Vector2 lookAxis = new Vector2(h,v);
        
        transposer.m_FollowOffset.y = Mathf.Clamp(
            transposer.m_FollowOffset.y - (lookAxis.y * cameraVelocity.y * deltaTime),
            maxFollowOffset.x,
            maxFollowOffset.y);
        
        transform.RotateAround(playerTransform.position, new Vector3(0f, 1, 0f), lookAxis.x * 100 * deltaTime);*/
    }
}