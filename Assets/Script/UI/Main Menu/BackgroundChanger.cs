using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundChanger : MonoBehaviour
{
    [Header("Setting")]
    [Range(1, 10)][SerializeField] private float changeTime;
    [Header("Image")]
    [SerializeField] private Image backgroundImage;
    public List<Sprite> backgrounds = new List<Sprite>();
    private float timer = 0;
    private int direction = 0, currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        timer = changeTime;
        direction = Random.Range(0, 3);
        currentIndex = Random.Range(0, backgrounds.Count - 1);
        backgroundImage.transform.localPosition = Vector3.zero;
        backgrounds = backgrounds.OrderBy(x => Random.value).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= changeTime)
        {
            currentIndex++;
            if (currentIndex > backgrounds.Count - 1)
                currentIndex = 0;

            backgroundImage.sprite = backgrounds[currentIndex];
            backgroundImage.transform.localPosition = Vector3.zero;
            direction = Random.Range(0, 3);
            timer = 0;
        }
    }

    void FixedUpdate()
    {
        BackgroundMoveAnimation(direction);
    }

    void BackgroundMoveAnimation(int direction)
    {
        Vector3 dirV = Vector3.zero;
        switch (direction)
        {
            case 0: //Up
                dirV = new Vector3(0, 0.1f, 0);
                break;
            case 1: //Right
                dirV = new Vector3(0.05f, 0, 0);
                break;
            case 2: //Down
                dirV = new Vector3(0, -0.1f, 0);
                break;
            case 3: //Left
                dirV = new Vector3(-0.05f, 0, 0);
                break;
            default:
                break;
        }
        backgroundImage.transform.position += (dirV * (2f / changeTime));
    }
}
