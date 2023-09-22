using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum StudentAnimationState
{
    Idle,
    Walking
}

public class StudentController : MonoBehaviour
{
    [SerializeField] private Student student;

    private NavMeshAgent navMeshAgent;
    private Camera cam;
    [SerializeField] private Animator animator;
    private StudentAnimationState animState;
    private Vector3 wanderPosition;
    [SerializeField] private float wanderRadius = 10f;
    [SerializeField] private float wanderTime = 20f;
    [SerializeField] private GameObject bubble;
    [SerializeField] private GameObject optionMenu;
    private Bounds floor;
    private GameObject pillar;

    private float timer;
    float rndX, rndZ;
    Vector3 checkBound;
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        floor = GameObject.FindGameObjectWithTag("Floor").GetComponent<Renderer>().bounds;
        //pillar = GameObject.Find("Pillar");
        timer = wanderTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    void OnMouseOver()
    {
        //bubble.SetActive(true);
    }

    void OnMouseExit()
    {
        //bubble.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            NavMeshData data = floor.GetComponent<NavMeshData>();
            rndX = Random.Range(data.sourceBounds.min.x , data.sourceBounds.max.x);
            rndZ = Random.Range(data.sourceBounds.min.z, data.sourceBounds.max.z);
            checkBound = new Vector3(rndX, pillar.transform.position.y, rndZ);
            //navMeshAgent.SetDestination(checkBound);
            pillar.transform.position = checkBound;

            //navMeshAgent.SetDestination(checkBound);
            //Debug.DrawRay(transform.position, checkBound);
            Debug.Log(checkBound);
        }*/

        UpdateAnimation();
        Wander();
        //if(Input.GetMouseButtonDown(0))
        //ShowStudentUI();
    }

    void UpdateAnimation()
    {
        switch (animState)
        {
            case StudentAnimationState.Idle:
                navMeshAgent.isStopped = true;
                animator.SetBool("CafeMove", false);
                break;
            case StudentAnimationState.Walking:
                navMeshAgent.isStopped = false;
                animator.SetBool("CafeMove", true);
                break;
        }
    }

    void Wander()
    {
        timer += Time.deltaTime;
        float distance = Vector3.Distance(transform.position, wanderPosition);
        if (distance <= 0.2f)
            animState = StudentAnimationState.Idle;

        if (timer >= wanderTime)
        {
            rndX = Random.Range(floor.min.x, floor.max.x);
            rndZ = Random.Range(floor.min.z, floor.max.z);
            checkBound = new Vector3(rndX, transform.position.y, rndZ);
            navMeshAgent.SetDestination(checkBound);
            Debug.Log(checkBound);
            /*do
            {
                rndX = Random.Range(floor.min.x, floor.max.x);
                rndZ = Random.Range(floor.min.z, floor.max.z);
                checkBound = new Vector3(rndX, transform.position.y, rndZ);
                navMeshAgent.SetDestination(checkBound);
                Debug.Log(checkBound);
            } while (navMeshAgent.pathEndPosition.x != checkBound.x && navMeshAgent.pathEndPosition.z != checkBound.z);*/

            //wanderPosition = RandomNavSphere(transform.position, wanderRadius, -1);
            //navMeshAgent.SetDestination(wanderPosition);
            navMeshAgent.isStopped = false;
            animState = StudentAnimationState.Walking;
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


    void ShowStudentUI()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            ToggleOptionMenu();
        }
    }

    void ToggleOptionMenu()
    {
        optionMenu.SetActive(!optionMenu.activeSelf);
    }

    void OnCollisionEnter(Collision collision)
    {
        animState = StudentAnimationState.Idle;
    }

    void OnCollisionExit(Collision collision)
    {
        animState = StudentAnimationState.Walking;
    }
}
