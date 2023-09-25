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

    [SerializeField] private float minZoomSize;
    [SerializeField] private float maxZoomSize;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private bool canMove = true;

    private float zoomModifier;

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
        if (canMove)
        {
            MoveDrag();
        }
    }

    private void MoveDrag()
    {
        if (!GameManager.Instance.uiIsOpen)
        {
            if (Input.GetMouseButton(0)) //Move by left Muse button
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

    }

    private void Zoom()
    {
        if (!GameManager.Instance.uiIsOpen)
        {
            zoomModifier = -Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        }

        float zoomSize = cam.orthographicSize;
        if (zoomSize < minZoomSize && zoomModifier < 0f)
            return;
        else if (zoomSize > maxZoomSize && zoomModifier > 0f)
            return;

        cam.orthographicSize += zoomModifier;
    }
}
