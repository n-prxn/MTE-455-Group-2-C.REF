using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentParent : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
