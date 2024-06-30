using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TorchScript : MonoBehaviour
{
    public Image gimmic;
    public Color color;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && (gimmic.color.a!=0 && gimmic.color.a != 0.5f))
        {
            color.a = 0;
            gimmic.color = color;
            StartCoroutine(LanternGoOut());
        }
    }

    private IEnumerator LanternGoOut() 
    {
        yield return new WaitForSeconds(10f);
        color.a = 0.5f;
        gimmic.color = color;
        yield return new WaitForSeconds(2f);
        color.a = 1f;
        gimmic.color = color;
    }
}
