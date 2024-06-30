using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using DG.Tweening;

public class BettleMove : MonoBehaviour
{
    private int hp = 5;
    [SerializeField]private GameObject player;
    private Rigidbody2D rb;
    private Animator anim;
    private int digholeanim = Animator.StringToHash("DigHole");
    private int rushanim = Animator.StringToHash("Rush");
    private int leftrushanim = Animator.StringToHash("Left");
    private int walkanim = Animator.StringToHash("Walk");
    private int downanim = Animator.StringToHash("Down");
    private CircleCollider2D circleCollider;
    [SerializeField]private Vector2 rightrush;//右側から突撃初期位置
    [SerializeField] private Vector2 rightrushend;//止まる位置
    [SerializeField]private Vector3[] rightrushdetour;
    [SerializeField]private Vector2 leftrush;
    [SerializeField] private Vector2 leftrushend;
    [SerializeField]private Vector3[] leftrushdetour;
    [SerializeField]private float digholeup;
    private int nextmove;
    private bool movenow;
    [SerializeField] private float dighole;
    private bool digholenow;

    [SerializeField] private Vector3[] walkpath;
    private ObjectCollision oc;
    private bool downnow;

    private bool invinciblenow;
    private SpriteRenderer spriterenderer;

    [SerializeField] private GameObject rockeffect;

    private PlayerController controller;
    
    private void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        circleCollider=GetComponent<CircleCollider2D>();
        oc=GetComponent<ObjectCollision>();
        spriterenderer=GetComponent<SpriteRenderer>();
        nextmove = Random.Range(0,3);
        rightrushdetour[0] = rightrush;
        leftrushdetour[0] = leftrush;
        controller = player.GetComponent<PlayerController>();
        this.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (oc.step || oc.hit) 
            {
                oc.step = false;
                oc.hit = false;
                if (downnow)
                    {
                        if (!invinciblenow)
                            {
                                StartCoroutine(Damaged()); 
                            }
                    }
            }

        if (!movenow)
        {
            if (nextmove == 0)
            {
                StartCoroutine(BettleRush());
            }
            else if (nextmove == 1)
            {
                StartCoroutine(BettleRampage());
            }
            else if (nextmove == 2) 
            {
                StartCoroutine(DigHole());
            }
        }
        else 
        {
            if (digholenow)
            {
                if (this.transform.position.x == player.transform.position.x) 
                {
                    return;
                }
                if (this.transform.position.x < player.transform.position.x)//プレイヤーが右側にいる時
                {
                    rb.velocity = new Vector2(7, 0);
                }
                else 
                {
                    rb.velocity = new Vector2(-7, 0);
                }
            }
            return;
        }
    }

    private IEnumerator BettleWalk()//攻撃の後のお散歩
    {
        anim.SetBool(walkanim,true);
        yield return this.transform.DOPath(walkpath,3f).WaitForCompletion(); //自然にプレイヤーの方見るの不可能なのでアニメーションで対応
        anim.SetBool(walkanim, false);
    }

    private IEnumerator BettleRush() 
    {
        movenow= true;
        int rnd=Random.Range(0,2);
        if (rnd == 0)
        {
            anim.SetBool(rushanim, true);
            yield return this.transform.DOMove(rightrush, 2f).WaitForCompletion();
            yield return this.transform.DOPath(rightrushdetour,1f).WaitForCompletion();
            yield return this.transform.DOMove(new Vector2(rightrushend.x+1,rightrushend.y),1f).WaitForCompletion();
            yield return this.transform.DOMove(leftrush, 0.5f).WaitForCompletion();
            yield return this.transform.DOPath(leftrushdetour, 1f).WaitForCompletion();
            yield return this.transform.DOMove(leftrushend, 1f).WaitForCompletion();
            anim.SetBool(rushanim, false);
        }
        else
        {
            anim.SetBool(leftrushanim, true);
            yield return this.transform.DOMove(leftrush, 2f).WaitForCompletion();
            yield return this.transform.DOPath(leftrushdetour, 1f).WaitForCompletion();
            yield return this.transform.DOMove(new Vector2(leftrushend.x - 1, leftrushend.y), 1f).WaitForCompletion();
            yield return this.transform.DOMove(rightrush, 0.5f).WaitForCompletion();
            yield return this.transform.DOPath(rightrushdetour, 1f).WaitForCompletion();
            yield return this.transform.DOMove(rightrushend, 1f).WaitForCompletion();
            anim.SetBool(leftrushanim, false);
        }
        yield return StartCoroutine(Down());
        nextmove = Random.Range(0, 3);
        movenow = false;
    }

    private IEnumerator DigHole()
    {
        movenow= true;
        anim.SetBool(digholeanim,true);
        yield return this.transform.DOMoveY(dighole,2f).WaitForCompletion();
        rb.bodyType = RigidbodyType2D.Dynamic;
        digholenow = true;
        yield return new WaitForSeconds(2f);
        digholenow = false;
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        yield return this.transform.DOMoveY(digholeup,0.5f).SetDelay(0.2f).WaitForCompletion();
        anim.SetBool(digholeanim, false);
        yield return StartCoroutine(Down());
        nextmove = Random.Range(0, 3);

        movenow = false;
    }

    private IEnumerator Down()
    {
        anim.speed = 1f;
        anim.SetTrigger(downanim);
        downnow = true;
        yield return new WaitForSeconds(0.5f);
        rb.bodyType= RigidbodyType2D.Dynamic;
        circleCollider.isTrigger= false;
        yield return new WaitForSeconds(2f);
        if (hp<3) 
        {
            anim.speed = 1.5f;
        }
        downnow= false;
        circleCollider.isTrigger = true;
        rb.bodyType= RigidbodyType2D.Kinematic;
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(BettleWalk());
    }

    private IEnumerator Damaged() 
    {
        Debug.Log("通りました");
        StartCoroutine(Invincible());
        this.hp -= 1;
        if (hp == 0)
        {
            gameObject.SetActive(false);
        } else if (hp < 3)
        {
            //怒っ他時の処理
            anim.speed =1.5f;
            DOTween.timeScale = 1.5f;
            Debug.Log("怒った");
        }
        yield break;
    }

    private IEnumerator Invincible() 
    {
        invinciblenow = true;
        for (int i = 0; i < 10; i++) 
        {
            if (i % 2 == 0)
            {
                spriterenderer.enabled = false;
            }
            else 
            {
                spriterenderer.enabled = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
        invinciblenow = false;
    }

    private IEnumerator BettleRampage() 
    {
        movenow = true;
        yield return this.transform.DOMove(new Vector3(leftrushend.x,digholeup-1,0),1).WaitForCompletion();
        yield return this.transform.DOShakePosition(0.2f, 1, 10, 180, false).WaitForCompletion();
        for (int i=0;i<5;i++) 
        {
            if (i%2==0)
            {
                yield return this.transform.DOMoveX(rightrushend.x-(rightrushend.x-leftrushend.x)/2,0.5f).WaitForCompletion();
                yield return this.transform.DOShakePosition(0.2f, 1, 10, 180, false).WaitForCompletion();
                Instantiate(rockeffect, this.transform.transform.position, Quaternion.identity);
                yield return this.transform.DOMoveX(rightrushend.x, 0.5f).WaitForCompletion();
            }
            else
            {
                yield return this.transform.DOMoveX(rightrushend.x - (rightrushend.x-leftrushend.x)/2, 0.5f).WaitForCompletion();
                yield return this.transform.DOShakePosition(0.2f, 1, 10, 180, false).WaitForCompletion();
                Instantiate(rockeffect, this.transform.transform.position, Quaternion.identity);
                yield return this.transform.DOMoveX(leftrushend.x, 0.5f).WaitForCompletion();
            }
            Instantiate(rockeffect,this.transform.transform.position,Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }
        yield return StartCoroutine(Down());
        nextmove = Random.Range(0, 3);
        movenow = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")) 
        {
            if (!invinciblenow) 
            {
                controller.PlayerTakeDamage(); 
            }
        }
    }
}
