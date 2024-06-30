using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour
{
    //�Ȃ񂩂�����Particle������ăG�f�B�^�ŃA�^�b�`���܂��傤
    [SerializeField] private ParticleSystem particle;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BreakObject"))
        {
            Debug.Log("��������");
            Destroy(Instantiate(particle, collision.gameObject.transform.position + Vector3.one * 0.5f, Quaternion.identity), 0.85f);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Bunny") || collision.gameObject.CompareTag("Dog") ||
            collision.gameObject.CompareTag("Bat") || collision.gameObject.CompareTag("Dino") ||
            collision.gameObject.CompareTag("Opossum") ||
            collision.gameObject.CompareTag("Pig") || collision.gameObject.CompareTag("Vulture"))
        {
            collision.gameObject.GetComponent<ObjectCollision>().hit = true;
        }
    }

}
