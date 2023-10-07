using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum StudentAnimationState
{
    Idle,
    Walking,
    CafeReact
}

public class StudentController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Camera cam;
    private Animator animator;
    public StudentAnimationState animState;
    private Vector3 wanderPosition;
    [SerializeField] private float wanderTime = 20f;
    [SerializeField] private float distanceRadius = 0.2f;
    [SerializeField] private GameObject bubble;
    [SerializeField] private GameObject optionMenu;
    private Bounds floor;
    private float timer;
    float rndX, rndZ;
    Vector3 mouseDownOrigin;
    Vector3 checkBound;
    [SerializeField] private AudioSource audioSource;
    public AudioClip studentReactVoices;
    private GachaPool gachaPool;

    void Awake()
    {
        audioSource = GameObject.FindGameObjectWithTag("Voice Audio").GetComponent<AudioSource>();
        gachaPool = GameObject.Find("Gacha").GetComponent<GachaPool>();
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
        OnLeftClick();
    }

    void UpdateAnimation()
    {
        switch (animState)
        {
            case StudentAnimationState.Idle:
                navMeshAgent.isStopped = true;
                animator.Play("Cafe_Idle");
                animator.SetBool("CafeMove", false);
                animator.SetBool("CafeReact", false);
                break;
            case StudentAnimationState.Walking:
                navMeshAgent.isStopped = false;
                animator.SetBool("CafeMove", true);
                animator.SetBool("CafeReact", false);
                break;
            case StudentAnimationState.CafeReact:
                animator.SetBool("CafeMove", false);
                animator.SetBool("CafeReact", true);
                StartCoroutine(PlayAndWaitForAnim("Cafe_Reaction"));
                // animator.Play("Cafe_Reaction");
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

    void OnLeftClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownOrigin = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0) && mouseDownOrigin == Input.mousePosition)
        {
            OnClickStudent();
        }
    }

    void OnClickStudent()
    {

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Student")))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            navMeshAgent.isStopped = true;
            hit.collider.gameObject.GetComponent<StudentController>().animState = StudentAnimationState.CafeReact;
            LookAtCam(hit.collider.gameObject);
            if (hit.collider.gameObject == gameObject)
            {
                ShowBubble(hit.collider.gameObject);
            }
        }
    }

    protected void LookAtCam(GameObject clickStudent)
    {
        if (clickStudent == null)
            return;

        if (!clickStudent.GetComponent<NavMeshAgent>().isStopped)
        {
            return;
        }

        Vector3 dir = (Camera.main.transform.position - clickStudent.transform.position).normalized;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        clickStudent.transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    public void PlayStudentVoice(GameObject clickStudent)
    {
        if (clickStudent == null)
            return;

        if (audioSource.isPlaying)
            return;

        if (clickStudent.GetComponent<StudentController>().studentReactVoices != null)
        {
            audioSource.Stop();
            audioSource.clip = clickStudent.GetComponent<StudentController>().studentReactVoices;
            audioSource.Play();
            //audioSource.PlayOneShot(clickStudent.GetComponent<StudentController>().studentReactVoices);
        }
    }

    void ShowBubble(GameObject clickStudent)
    {
        Debug.Log("show bubble");
        if (audioSource.isPlaying)
        {
            Debug.Log("audio os play");
            return;
        }

        PlayStudentVoice(clickStudent);

        int studentID = int.Parse(gameObject.name.Substring(0, 2));
        bubble.transform.GetChild(0).GetComponent<TMP_Text>().text = gachaPool.StudentsPool.Find(x => x.id == studentID).cafeText;
        bubble.SetActive(true);
    }

    public IEnumerator PlayAndWaitForAnim(string stateName)
    {
        animator.Play(stateName);

        //Wait until we enter the current state
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
        {
            yield return null;
        }

        //Now, Wait until the current state is done playing
        while ((animator.GetCurrentAnimatorStateInfo(0).normalizedTime) % 1 < 0.99f)
        {
            yield return null;
        }
        animState = StudentAnimationState.Idle;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Student")
        {
            animState = StudentAnimationState.Idle;
        }
    }
}
