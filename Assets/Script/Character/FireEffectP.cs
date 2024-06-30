using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffectP : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (collider.gameObject.CompareTag("FireTarget"))
        if (other.CompareTag("FireTarget"))
        {
            // collider.gameObject.GetComponent<ObjectCollision>().step = true;
            ObjectCollision objectCollision = other.GetComponentInParent<ObjectCollision>();

            // ObjectCollision�R���|�[�l���g��step�ϐ���true�ɐݒ�
            if (objectCollision != null)
            {
                objectCollision.step = true;
            }
            Debug.Log("������܂���");
        }

    }
}
