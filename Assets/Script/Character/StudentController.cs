using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StudentController : MonoBehaviour
{
    [SerializeField] private Student student;

    private NavMeshAgent navMeshAgent;
    private Camera cam;

    [SerializeField] private float wanderRadius = 10f;
    [SerializeField] private float wanderTime = 20f;
    [SerializeField] private GameObject bubble;
    [SerializeField] private GameObject optionMenu;

    private float timer;
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        timer = wanderTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    void OnMouseOver(){
        //bubble.SetActive(true);
    }

    void OnMouseExit(){
        //bubble.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Wander();
        //if(Input.GetMouseButtonDown(0))
            //ShowStudentUI();
    }

    void Wander()
    {
        timer += Time.deltaTime;
        if (timer >= wanderTime)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            navMeshAgent.SetDestination(newPos);
            timer = 0f;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, LayerMask layerMask)
    {
        Vector3 randDirection = Random.insideUnitSphere * distance;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, distance, layerMask);

        return navHit.position;
    }


    void ShowStudentUI(){
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 1000)){
            if(EventSystem.current.IsPointerOverGameObject())
                return;

            ToggleOptionMenu();
        }
    }

    void ToggleOptionMenu(){
        optionMenu.SetActive(!optionMenu.activeSelf);
    }
}
