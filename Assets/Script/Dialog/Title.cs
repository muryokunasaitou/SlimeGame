using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField] NDTDEvent ndtdEvent;
    void Start()
    {
            //����N�����̏��������s
            ndtdEvent.Title();
    }
}
