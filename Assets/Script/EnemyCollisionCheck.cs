using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionCheck : MonoBehaviour
{
    public bool isOn = false;
    private string ground_tag = "Ground"; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ground_tag)
        {
            isOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag==ground_tag) 
        {
            isOn = false; 
        }
    }
}
