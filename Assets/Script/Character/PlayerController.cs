using JetBrains.Annotations;
using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] NDTDEvent ndtdEvent;
    public float speed;
    public Rigidbody2D rb;
    public Transform tf;
    private EnemyTagCounter enemyTagCounter;
    private int maxjump;
    private int restjump; //ジャンプ回数
    public Animator animator;
    private CapsuleCollider2D capsulecollider;
    [Header("Playerの踏みつけ判定足下から何割足すか")] public float stepOnRate;

    public GroundCheck ground; //接地判定用

    private float time = 1;
    public bool right = false;
    private bool small;
    private float cashe_steponrate;
    public bool isStopPlayer;
    private bool TDEventseti;
    //[SerializeField] private CursorScript cursorscript;
    //[SerializeField] private Skill_Activate skill_Activate;

    private bool invincible = false;
    private SpriteRenderer spriterenderer;

    private LifeManage lifeManage;

    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
        capsulecollider= GetComponent<CapsuleCollider2D>();
        enemyTagCounter= GetComponent<EnemyTagCounter>();
        maxjump = 3; //if文でフラグ取得の判定
        restjump = maxjump;
        cashe_steponrate = stepOnRate;
        spriterenderer= GetComponent<SpriteRenderer>();
        ndtdEvent = GetComponent<NDTDEvent>();

        lifeManage=FindObjectOfType<LifeManage>();
    }

    void Update()
    {
         restjump = ground.IsGround(maxjump, restjump);
         if (Input.GetKeyDown(KeyCode.Space))
         {
             Jump();
         }
         if(TDEventseti==true)
        {

        }
    }

    void FixedUpdate()
    {
            Move();
    }

    private void Jump()
    {
        if (/*restjump > 0*/restjump>0)
        {
            animator.SetTrigger("isJumping");
            rb.velocity = new Vector2(rb.velocity.x, 11);
            restjump -= 1;
        }
    }

    private void Move()
    {
        
        if (Input.GetKey(KeyCode.D))
        {
            if (!small)
            {
                this.transform.localScale = Vector3.one;
            }
            else 
            {
                this.transform.localScale = new Vector3(1f, 0.5f, 1f);
            }
            if (!right) {
                time = 1;
                right = true; 
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (rb.velocity.x < 10)
                {
                    animator.SetBool("Dash", true);
                    rb.velocity = new Vector2(speed * time, VelocityYControl());
                    time += Time.fixedDeltaTime;
                    
                }
                else
                {
                    
                    rb.velocity = new Vector2(rb.velocity.x, VelocityYControl());
                }
            }
            else 
            {
                animator.SetBool("Dash", false);
                time = 1;
                rb.velocity = new Vector2(speed, VelocityYControl());
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (!small)
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
            }
            else 
            {
                this.transform.localScale = new Vector3(-1f, 0.5f, 1f);
            }

            if (right) 
            {
                time = 1;
                right = false;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (rb.velocity.x > -10)
                {
                    animator.SetBool("Dash", true);
                    rb.velocity = new Vector2(-speed * time, VelocityYControl());
                    time += Time.fixedDeltaTime;
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, VelocityYControl());
                }
            }
            else 
            {
                animator.SetBool("Dash", false);
                time = 1;
                rb.velocity = new Vector2(-speed, VelocityYControl());
            }
        }
        else
        {
            animator.SetBool("Dash", false);
            time = 1;
            rb.velocity = new Vector2(0, VelocityYControl());
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //会話イベント時の接地判定
        if (ndtdEvent.isStartTDE == true && (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "BreakGround"
            || collision.gameObject.tag == "BreakObject"))
        {
            Debug.Log("地面フラグ");
            ndtdEvent.isPlayerToLand = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Playerへのダメージを与えるタグ欄
        if (//Enemy
            collision.gameObject.CompareTag("Bunny") || collision.gameObject.CompareTag("Dog") ||
            collision.gameObject.CompareTag("Bat") || collision.gameObject.CompareTag("Dino") ||
            collision.gameObject.CompareTag("Bear") || collision.gameObject.CompareTag("Opossum") ||
            collision.gameObject.CompareTag("Pig") || collision.gameObject.CompareTag("Vulture") 
            )
       {
            float stepOnHeight = (capsulecollider.size.y * (stepOnRate / 100f));
        //踏みつけ判定のワールド座標
        float judgePos = transform.position.y - (capsulecollider.size.y / 2f) + stepOnHeight;
        foreach (ContactPoint2D p in collision.contacts)
        {
            if (p.point.y < judgePos)
            {
                    //踏んだ時の処理
                    animator.SetTrigger("isEat");
                    collision.gameObject.GetComponent<ObjectCollision>().step = true;
            }
            else
            {
                        if (!small)
                        {
                            //ダウンする
                            //animator.Play("Player_Down"); //死んだときのアニメーション
                            //down = true;
                            // 下記二行をコピペすれば任意の個所で被ダメ処理が実装できる
                            //LifeManage lifeManage = FindObjectOfType<LifeManage>(); // スライムにLifeManageをアタッチする必要がある
                            /*lifeManage.TakeDamage();
                            StartCoroutine(InvincivleTime()); //無敵時間処理も兼ねたPlayerTakeDamageに*/
                            PlayerTakeDamage();
                        }
                        else
                        {
                            small = false;
                            stepOnRate = cashe_steponrate; //エディターで入力した初期値へ
                            if (right)
                            {
                                this.transform.localScale = Vector3.one;
                            }
                            else
                            {
                                this.transform.localScale = new Vector3(-1, 1, 1);
                            }
                        }
            }
        }
        
       }
    }


    public void SmallSlime() 
    {
        stepOnRate = 40;
        if (right)
        {
            transform.localScale = new Vector3(1f, 0.5f, 1f);
        }
        else 
        {
            transform.localScale = new Vector3(-1f, 0.5f, 1f);
        }
        small = true;
    }

    private float VelocityYControl() 
    {
        if (rb.velocity.y>-30)
        {
            return rb.velocity.y;
        }
        else 
        {
            return -30f;
        }
    }

    public float GetVelocityY()
    {
        return rb.velocity.y;
    }

    private IEnumerator InvincivleTime() 
    {
        for (int i=0;i<10;i++) 
        {
            if (i%2==0)
            {
                spriterenderer.enabled = false;
            }
            else 
            {
                spriterenderer.enabled = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
        invincible= false;
    }


    public void PlayerTakeDamage() 
    {
        if (!invincible) 
        { 
        lifeManage.TakeDamage();
        invincible = true;
        StartCoroutine(InvincivleTime()); //無敵時間処理
        }
    }

    public void GetItem() 
    {
        lifeManage.GetItem();
    }

    public void Death() 
    {
        lifeManage.InstantDeath();
    }
}