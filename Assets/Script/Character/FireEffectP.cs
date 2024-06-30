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

            // ObjectCollisionコンポーネントのstep変数をtrueに設定
            if (objectCollision != null)
            {
                objectCollision.step = true;
            }
            Debug.Log("あたりました");
        }

    }
}
