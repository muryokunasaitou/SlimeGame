using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VultureDrops : MonoBehaviour
{
    [SerializeField] Enemy_Move enemy_Move;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;�@//�����Ȃ����
    }

    void Update()
    {
        if(enemy_Move.vul_isgrap == false)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;�@//���Ƃ������ɍ��킹�ē�����Ԃց@
            gameObject.transform.parent = null;     //�n�Q�Ƃ̐e�q������
        }
    }
}
