using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigImpact : MonoBehaviour
{
    private void Hakai()
    {
        Destroy(this.gameObject,1f);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (CompareTag("PlayerAttack"))
        {
            if (collider.gameObject.CompareTag("Bunny") || collider.gameObject.CompareTag("Dog") ||
            collider.gameObject.CompareTag("Bat") || collider.gameObject.CompareTag("Dino") ||
            collider.gameObject.CompareTag("Bear") || collider.gameObject.CompareTag("Opossum") ||
            collider.gameObject.CompareTag("Pig") || collider.gameObject.CompareTag("Vulture"))
            {
                collider.gameObject.GetComponent<ObjectCollision>().hit = true;
            }
            
        }
        else
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                collider.gameObject.GetComponent<PlayerController>().PlayerTakeDamage();
            }
        }
    }
}
