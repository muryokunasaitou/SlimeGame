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


            // ���L��s���R�s�y����ΔC�ӂ̌��Ŕ�_�������������ł���
            LifeManage lifeManage = FindObjectOfType<LifeManage>(); // �X���C����LifeManage���A�^�b�`����K�v������
            lifeManage.TakeDamage();
        }

    }
}
