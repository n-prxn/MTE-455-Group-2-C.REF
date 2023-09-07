using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FurniturePlacement : MonoBehaviour
{
    [SerializeField] private GameObject plane;
    public GameObject Plane
    {
        get { return plane; }
        set { plane = value; }
    }
    private bool canBuild = false;
    public bool CanBuild
    {
        get { return canBuild; }
        set { canBuild = value; }
    }
    private bool isCollided = false;
    private Renderer planeRenderer;

    void Awake()
    {
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
    private void ChangeColor(bool flag, Color color)
    {
        planeRenderer.material.color = color;
        canBuild = flag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Furniture" || other.tag == "Border")
        {
            isCollided = true;
            ChangeColor(false, Color.red);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Furniture" || other.tag == "Border")
        {
            isCollided = true;
            ChangeColor(false, Color.red);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Furniture" || other.tag == "Border")
        {
            isCollided = false;
            ChangeColor(true, Color.green);
        }
    }
}
