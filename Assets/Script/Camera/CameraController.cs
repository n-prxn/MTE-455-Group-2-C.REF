using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    private Camera cam;

    [SerializeField] private float camSpeed;

    void Awake(){
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        MoveByKB();
    }

    private void MoveByKB(){
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 dir = transform.forward * zInput + transform.right * xInput;
        transform.position += dir * camSpeed * Time.deltaTime;
        //transform.position = Clamp(corner1.position,corner2.position);
    }

    /*Vector3 Clamp(Vector3 lowerLeft, Vector3 topRight){
        Vector3 pos = new Vector3(Mathf.Clamp(transform.position.x,lowerLeft.x,topRight.x),
                            transform.position.y,
                            Mathf.Clamp(transform.position.z,lowerLeft.z,topRight.z));
        return pos;
    }*/
}
