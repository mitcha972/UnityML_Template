using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    private Camera cam;

    [SerializeField] private float zoomStep = 0.1f;
    [SerializeField] private float minCamSize = 2f; 
    [SerializeField] private float maxCamSize = 10f;
    //[SerializeField] private Slider slider;

    private Vector3 dragOrigin;
    private Vector3 lastMousePosition;
    [SerializeField] private float rotationSpeed = 5f;

    void Start()
    {
        //InitializeSlider();
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        PanCamera();
        ZoomCamera();
        RotateCamera();
    }

    private void PanCamera()
    {
        // Middle Click Drag
        if (Input.GetMouseButtonDown(2))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 diff = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += diff;
        }
    }

    private void ZoomCamera()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            ZoomIn();

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            ZoomOut();
    }

    private void RotateCamera()
    {
        if (Input.GetMouseButtonDown(1))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            float rotationY = delta.x * rotationSpeed * Time.deltaTime;
            float rotationX = -delta.y * rotationSpeed * Time.deltaTime;

            cam.transform.eulerAngles += new Vector3(rotationX, rotationY, 0);
            lastMousePosition = Input.mousePosition;
        }
    }

    public void ZoomIn()
    {
        float newSize = cam.orthographicSize - (cam.orthographicSize * zoomStep);
        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);

        //UpdateSlider((int)newSize);
    }

    public void ZoomOut()
    {
        float newSize = cam.orthographicSize + (cam.orthographicSize * zoomStep);
        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);

        //UpdateSlider((int)newSize);
    }

    // private void UpdateSlider(int NewSize)
    // {
    //     if (!slider)
    //         return;

    //     slider.value = NewSize;
    // }

    // private void InitializeSlider()
    // {
    //     if (!slider)
    //         return;

    //     slider.maxValue = maxCamSize;
    // }
}
