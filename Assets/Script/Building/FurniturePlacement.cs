using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurniturePlacement : MonoBehaviour
{
    [SerializeField] private GameObject plane;
    public GameObject Plane{
        get { return plane; }
        set { plane = value; }
    }
    [SerializeField] private bool canBuild = false;
    private Renderer planeRenderer;

    void Awake(){
        planeRenderer = plane.GetComponent<Renderer>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        planeRenderer.material.color = Color.green;
        canBuild = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeColor(Collider other, bool flag, Color color){
        if(other.tag == "Furnitures"){
            planeRenderer.material.color = color;
            canBuild = flag;
        }
    }

    private void OnTriggerEnter(Collider other){
        ChangeColor(other, false, Color.red);
    }

    private void OnTriggerStay(Collider other){
        ChangeColor(other, false, Color.red);
    }

    private void OnTriggerExit(Collider other){
        ChangeColor(other, true, Color.green);
    }
}
