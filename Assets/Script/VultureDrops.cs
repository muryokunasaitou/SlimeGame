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
        rb.bodyType = RigidbodyType2D.Static;　//動かない状態
    }

    void Update()
    {
        if(enemy_Move.vul_isgrap == false)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;　//落とす処理に合わせて動く状態へ　
            gameObject.transform.parent = null;     //ハゲとの親子を解除
        }
    }
}
