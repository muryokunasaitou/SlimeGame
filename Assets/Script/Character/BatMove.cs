using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BatMove : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    private bool move = false;
    private bool punish = false;
    private GameObject player;
    private ObjectCollision oc;
    [SerializeField] private GameObject ultraSounds;
    private Animator anim;

    private int batfall = Animator.StringToHash("BatFall");
    private int batattack = Animator.StringToHash("BatAttack");
    private int batmove = Animator.StringToHash("BatMove");

    private Vector3 destination;

    void Start()
    {
        anim= GetComponent<Animator>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        oc= GetComponent<ObjectCollision>();
    }

    private void FixedUpdate()
    {
        if (oc.step || oc.hit)
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject,5f);
            EnemyTagCounter enemyTagCounter = FindObjectOfType<EnemyTagCounter>();
            enemyTagCounter.IncrementCounter("Bat");
        }

        if (spriteRenderer.isVisible)
        {

            if (!punish && move)
            {
                anim.SetTrigger(batattack);
                StartCoroutine(Magic());
                Debug.Log("終了");
            }

        }
        else if (!move)
        {
            rb.Sleep();
            //元居たところに戻る処理
        }
    }

    private IEnumerator FirstMove()
    {
        Debug.Log("aaaaa");
        anim.SetTrigger(batfall); //
        while (destination.y+1 < this.transform.position.y)
        {
            rb.velocity = new Vector3(0, -3, 0);
            yield return null;
        }
        rb.velocity =Vector2.zero;
        move = true;
        Debug.Log("first終了");
                yield break;
            //プレイヤーの高さまで動き終わるまで待つ処
    }

    private IEnumerator Magic() 
    {
        punish= true;
        GameObject magic=Instantiate(ultraSounds, this.transform.position, Quaternion.identity);//スキル発動兼後隙 スキルの時間より長めに待機させる 何故かエフェクトが残るのでエフェクトの時間で消す
        magic.gameObject.transform.SetParent(this.gameObject.transform);
        yield return new WaitForSeconds(1.5f);
        Destroy(magic);
        anim.SetTrigger(batmove);
        yield return new WaitForSeconds(2.5f);
        if (player.transform.position.x < this.transform.position.x)
        {
            destination.x = -5;
            this.transform.localScale=new Vector3(-1,1,1);
        }
        else
        {
            destination.x = 5;
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        if (Mathf.Abs(player.transform.position.y - this.transform.position.y) < 1)
        {
            destination.y = Random.Range(-1.5f,1.5f);
        }
        else if (player.transform.position.y < this.transform.position.y)
        {
            destination.y = -5;
        }
        else 
        {
            destination.y = 5;
        }
        rb.velocity = destination;
        yield return new WaitForSeconds(1.5f);
        rb.velocity = Vector2.zero;
        punish = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !move) 
        {
            //最初のムーブ プレイヤーの高さまでなど
            player = collision.gameObject;
            destination = player.transform.position;
            Debug.Log(destination);
            StartCoroutine(FirstMove());
        }
    }
}
