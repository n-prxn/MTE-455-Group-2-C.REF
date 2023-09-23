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
    private NavMeshAgent navMeshAgent;
    private Camera cam;
    private Animator animator;
    private StudentAnimationState animState;
    private Vector3 wanderPosition;
    [SerializeField] private float wanderRadius = 10f;
    [SerializeField] private float wanderTime = 20f;
    [SerializeField] private float distanceRadius = 0.2f;
    [SerializeField] private GameObject bubble;
    [SerializeField] private GameObject optionMenu;
    private Bounds floor;
    private float timer;
    float rndX, rndZ;
    Vector3 checkBound;

    void Awake()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        floor = GameObject.FindGameObjectWithTag("Floor").GetComponent<Renderer>().bounds;
        timer = wanderTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        navMeshAgent.stoppingDistance = distanceRadius;
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
        UpdateAnimation();
        Wander();
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
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            animState = StudentAnimationState.Idle;
            navMeshAgent.SetDestination(this.transform.position);
        }

        if (navMeshAgent.isStopped == true)
        {
            timer += Time.deltaTime;

            if (timer >= wanderTime)
            {
                StartCoroutine(SetRandomDestination());
                wanderPosition = navMeshAgent.pathEndPosition;
                animState = StudentAnimationState.Walking;
                timer = 0f;
            }
        }
    }

    private IEnumerator SetRandomDestination()
    {
        do
        {
            rndX = Random.Range(floor.min.x, floor.max.x);
            rndZ = Random.Range(floor.min.z, floor.max.z);
            checkBound = new Vector3(rndX, transform.position.y, rndZ);
            navMeshAgent.SetDestination(checkBound);

            yield return new WaitUntil(() => navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete);
        } while (checkBound != navMeshAgent.pathEndPosition);
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

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Student")
        {
            animState = StudentAnimationState.Idle;
        }
    }
}
