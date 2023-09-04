using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurniturePlacement : MonoBehaviour
{
    [SerializeField] private int widthGrid = 1;
    [SerializeField] private int lengthGrid = 1;
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
        if (!isCollided)
            CheckOutsideGrid(FurniturePlacementManager.instance.CurrentCursorPos, widthGrid, lengthGrid);
    }

    private void ChangeColor(bool flag, Color color)
    {
        planeRenderer.material.color = color;
        canBuild = flag;
    }

    public void CheckOutsideGrid(Vector3 pos, int objWidth, int objLength)
    {
        if (pos.x > (10 - widthGrid / 2) || pos.z > (10 - widthGrid / 2) - 1 || pos.x < (-10 + lengthGrid / 2) || pos.z < (-10 + lengthGrid / 2))
        {
            planeRenderer.material.color = Color.red;
            canBuild = false;
        }
        else
        {
            planeRenderer.material.color = Color.green;
            canBuild = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Furniture")
        {
            isCollided = true;
            ChangeColor(false, Color.red);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Furniture")
        {
            isCollided = true;
            ChangeColor(false, Color.red);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Furniture")
        {
            isCollided = false;
            ChangeColor(true, Color.green);
        }
    }
}
