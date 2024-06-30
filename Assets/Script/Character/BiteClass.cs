using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteClass : MonoBehaviour
{
    private GameObject oyaOb;
    private Rigidbody2D rb;

    private void Update()
    {

    }

    public void KamuSkill(bool right, GameObject kariOb)
    {
        oyaOb = kariOb;
        rb = GetComponent<Rigidbody2D>();
        if (right)
        {
            gameObject.transform.localScale = Vector3.one;
            rb.AddForce(new Vector3(10, 0, 0), ForceMode2D.Impulse);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-1,1,1);
            rb.AddForce(new Vector3(-10, 0, 0), ForceMode2D.Impulse);
        }
        Invoke(("Stop"),0.5f);
        Invoke(("Destroy1"), 1f);
    }

    private void Destroy1()
    {
        oyaOb.gameObject.SetActive(true);
        Destroy(this.gameObject);
    }

    private void Stop()
    {
        rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bunny")|| collision.gameObject.CompareTag("Dog")|| collision.gameObject.CompareTag("Bat")
            || collision.gameObject.CompareTag("Opossum"))
        {
            //Destroy(collision.gameObject);
            collision.gameObject.GetComponent<ObjectCollision>().hit=true;
        }
    }
}
