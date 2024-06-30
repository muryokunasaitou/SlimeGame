using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            //collider.gameObject.GetComponent<PlayerController>().down = true;


            // 下記二行をコピペすれば任意の個所で被ダメ処理が実装できる
            LifeManage lifeManage = FindObjectOfType<LifeManage>(); // スライムにLifeManageをアタッチする必要がある
            lifeManage.TakeDamage();
        }

    }
}
