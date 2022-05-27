using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [HideInInspector]
    public bool isEnabled = true;

    public float speed;
    Camera cam;

    [SerializeField] float maxZoomIn = 3;
    [SerializeField] float maxZoomOut = 8;
    [SerializeField] float zoomSensitivity = 0.2f;
    
    float targetZoom;

    private void Start()
    {
        cam = GetComponent<Camera>();
        targetZoom = (maxZoomIn + maxZoomOut) / 2;
    }

    void Update()
    {
        if (!isEnabled) { return; }

        if (Input.GetMouseButton(2))
        {
            float newX = Input.GetAxis("Mouse X") * speed * (cam.orthographicSize * 0.2f);
            float newY = Input.GetAxis("Mouse Y") * speed * (cam.orthographicSize * 0.2f);
            transform.position -= new Vector3(newX, newY) * Time.deltaTime;
        }

        //Zoom
        targetZoom -= Input.mouseScrollDelta.y * zoomSensitivity;
        if (targetZoom > maxZoomOut)
        {
            targetZoom = maxZoomOut;
        }
        if (targetZoom < maxZoomIn)
        {
            targetZoom = maxZoomIn;
        }

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, 0.1f);
    }
}
