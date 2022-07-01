using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Camera cam;
    [SerializeField] Vector3 camOffset;
    [SerializeField] Vector3 cameraRotation;

    void Start()
    {

        cam = GameManager.Instance.cam;
        cam.transform.localRotation = Quaternion.Euler(cameraRotation);
    }
    
    void FixedUpdate()
    {        
            Vector3 myPos = transform.position;
            myPos.y = .5f;
            myPos.z = transform.position.z;

            Vector3 camPos = Vector3.Lerp(cam.transform.position, myPos + camOffset, Time.deltaTime * 500f);
            float camPosX = Mathf.Lerp(cam.transform.position.x, transform.position.x + camOffset.x, Time.deltaTime * 2.5f);
            camPos.x = camPosX * .85f;
            cam.transform.position = camPos;       
    }
}
