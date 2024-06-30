using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{

    public int currentLife = 5;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
