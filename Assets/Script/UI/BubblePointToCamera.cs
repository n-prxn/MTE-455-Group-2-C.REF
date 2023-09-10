using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePointToCamera : MonoBehaviour
{
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cam = Camera.main;
        transform.eulerAngles = cam.transform.eulerAngles;
    }
}
