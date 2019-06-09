using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;
    public Camera finishLineCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera.enabled = true;
        finishLineCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        mainCamera.enabled = false;
        finishLineCamera.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        mainCamera.enabled = true;
        finishLineCamera.enabled = false;
    }
}
