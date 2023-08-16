using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    private Camera cam;
    private Vector3 dragOrigin, Origin, Difference, newCam, resetCam;
    private bool drag = false;

    [SerializeField] private Transform lowLeft, topRight, font, back;

    [SerializeField] private float kbSpeed;
    [SerializeField] private float mouseSpeed;
    [SerializeField] private float smoothSpeed = 0.125f;

    [SerializeField] private float minZoomSize;
    [SerializeField] private float maxZoomSize;

    [SerializeField] private float zoomModifier;


    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        resetCam = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Zoom();
        MoveByKB();
        // MoveByMouse();
        MoveDrag();
    }

    private void MoveByKB()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 dir = transform.up * zInput + transform.right * xInput;
        Vector3 desiredPos = transform.position;
        desiredPos += dir * kbSpeed * Time.deltaTime;

        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = smoothPos;
    }

    private void MoveByMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0))
            return;

        Vector3 pos = cam.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * mouseSpeed, 0, pos.y * mouseSpeed);

        Vector3 smoothPos = Vector3.Lerp(transform.position, transform.position - move, smoothSpeed);
        transform.position = smoothPos;
    }

    private void MoveDrag()
    {
        if (Input.GetMouseButton(2)) //Move by middle Muse button
        {
            dragOrigin = Input.mousePosition;
            Difference = (cam.ScreenToWorldPoint(dragOrigin)) - cam.transform.position;
            if (drag == false)
            {
                drag = true;
                Origin = cam.ScreenToWorldPoint(dragOrigin);
            }
        }
        else
        {
            drag = false;
        }

        if (drag)
        {
            newCam = Origin - Difference;
            newCam.x = Mathf.Clamp(newCam.x, lowLeft.position.x, topRight.position.x);
            // newCam.y = resetCam.y;
            newCam.y = Mathf.Clamp(newCam.y, lowLeft.position.y, topRight.position.y);
            newCam.z = Mathf.Clamp(newCam.z, lowLeft.position.z, topRight.position.z);
            cam.transform.position = newCam;
        }

    }

    private void Zoom()
    {
        zoomModifier = -Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetKey(KeyCode.Z))
            zoomModifier = 0.01f;
        if (Input.GetKey(KeyCode.X))
            zoomModifier = -0.01f;

        float zoomSize = cam.orthographicSize;
        if (zoomSize < minZoomSize && zoomModifier < 0f)
            return;
        else if (zoomSize > maxZoomSize && zoomModifier > 0f)
            return;

        cam.orthographicSize += zoomModifier;
    }
}
