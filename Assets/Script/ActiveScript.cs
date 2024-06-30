using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveScript : MonoBehaviour
{
    public GameObject activetarget;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")) 
        {
            activetarget.SetActive(true);
        }
    }

    //Œã–ß‚è‚³‚¹‚È‚¢‚æ‚¤‚Éspriterenderer‚ÌActive(True)‚ð–Y‚ê‚È‚¢‚±‚Æ
}
