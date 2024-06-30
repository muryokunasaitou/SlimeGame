using System.Collections;
using System.Collections.Generic;
using System.Xml.Xsl;
using UnityEngine;
using System.Runtime.CompilerServices;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.Events;
using DG.Tweening;
using Unity.VisualScripting;

public class Enemy_Move : MonoBehaviour
{
    public float speed;
    private float time = 0;
    //public float gravity;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private bool right = false;
    // private bool dead = false;
    private ObjectCollision oc;
    public EnemyCollisionCheck check;
    private GameObject playerOb;
    private float playerpos_x;
    private float enemypos_x;
    private Vector2 startPos;
    private float distance;
    private bool coroutine = false;
    private bool enemyRight = true;
    public GameObject firePre;  // ���˂���I�u�W�F�N�g�̃v���n�u
    public Transform spawnPoint;         // ���ˈʒu
    public float fireSpeed = 10f;  // ���ˑ��x
    public float fireLifetime = 2f; // ���˃I�u�W�F�N�g�̎����i�b�j
    private float cooldownTime = 2f; // Fire�̃N�[���^�C��
    private float lastFireTime; // �Ō�ɔ��˂�������
    private bool canFire = true; // Fire���ˉ۔���
    public float bunnyJ_Height; //�E�T�M�̃W�����v����
    private bool timestop;
    private bool opposumright;
    private Coroutine pigcoroutine = null;
    public GameObject pigimpact;
    [Header("player��dino�Ŏg������")]
    public Transform Player;
    private Vector3 e_pos;
    private Vector3 p_pos;
    private bool vul_arri;
    public bool vul_isgrap = true;
    private bool ground_reland; //�n�ʂ��痣��čĂђn�ʂɒ��n���邵�����̔���

    [SerializeField] private bool Bunny, BunnybitMove, Bat, Dog, Opossum, Pig, Dino ,Vulture;
    

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        oc = GetComponent<ObjectCollision>();
        playerOb = GameObject.FindWithTag("Player");
        startPos = transform.position;    //��ŃZ�[�u�����n�_�ɖ߂������ɏ����ʒu�Ɉړ��A��������R�[�h����
    }

    private void FixedUpdate()
    {
        if (oc.hit) 
        {
            gameObject.SetActive(false);
            return;
        }
        
        if (!oc.step)
        {

            if (check.isOn)
            {
                right = !right;
            }


            if (Bunny && spriteRenderer.isVisible)
            {
                playerpos_x = playerOb.transform.position.x;
                enemypos_x = transform.position.x;
                BunnyMove();
            }
            else if (Bat)
            {
                BatMove();

            }
            else if (spriteRenderer.isVisible && Dog && coroutine == false)
            {
                //Player�ƓG�̈ʒu�ϐ��̗p��
                playerpos_x = playerOb.transform.position.x;
                enemypos_x = transform.position.x;
                DogMove();

            }
            else if (Opossum)
            {
                OpossumMove();
                if (time < 0.5)
                {
                    time += Time.fixedDeltaTime;
                }
                else
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        opposumright = true;
                    }
                    else
                    {
                        opposumright = false;
                    }
                    time = 0;
                }
            }
            else if (Pig)
            {
                PigMove();
            }
            else if (Dino)
            {
                playerpos_x = playerOb.transform.position.x;
                enemypos_x = transform.position.x;
                DinoMove();
            }
            else if (Vulture && coroutine == false)
            {
                e_pos = transform.position;
                p_pos = playerOb.transform.position;
                VultureMove();
            }
        }
        else
        {
            //�G�I�u�W�F�N�g��j�󂷂�
            gameObject.SetActive(false);
            // �v���C���[�ɓ��������ꍇ�A�G�̃^�O�ɉ������J�E���^�[�𑝉�
            EnemyTagCounter enemyTagCounter = FindObjectOfType<EnemyTagCounter>();
            // EnemyTags = GameObject.FindGameObjectsWithTag("Enemy");
            if (gameObject.CompareTag("Bunny"))
            {
                enemyTagCounter.IncrementCounter("Bunny");
            }
            else if (gameObject.CompareTag("Dog"))
            {
                enemyTagCounter.IncrementCounter("Dog");
            }
            else if (gameObject.CompareTag("Bat"))
            {
                enemyTagCounter.IncrementCounter("Bat");
            }
            else if (gameObject.CompareTag("Dino"))
            {
                enemyTagCounter.IncrementCounter("Dino");
            }
            else if (gameObject.CompareTag("Opossum"))
            {
                enemyTagCounter.IncrementCounter("Opossum");
            }
            else if (gameObject.CompareTag("Pig"))
            {
                enemyTagCounter.IncrementCounter("Pig");
            }
            Debug.Log(enemyTagCounter);
        }
    }

    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �v���C���[�ɓ��������ꍇ�A�G�̃^�O�ɉ������J�E���^�[�𑝉�
            EnemyTagCounter enemyTagCounter = FindObjectOfType<EnemyTagCounter>();
            enemyTagCounter.IncrementCounter("EnemyTagA");

            // ���̌�A�G�I�u�W�F�N�g��j�󂷂�Ȃǂ̏������s��
            Destroy(this.gameObject);
        }
    }*/
    private void VultureMove()
    {
        distance = Vector2.Distance(e_pos, p_pos);
        //������20�ȓ�������̈ʒu�ɒ����܂ł̈�񂫂�̈ړ����x
        if (distance < 20 && vul_arri == false)
        {
            transform.position = Vector3.Lerp(e_pos, new Vector3(p_pos.x + 5, p_pos.y + 6), 0.5f * Time.deltaTime);
        }
        //����̈ʒu�ɒ�������Ƀt���O�I��
        if(p_pos.x+5 >= e_pos.x && p_pos.y+6 >= e_pos.y && vul_arri == false)
        {
            vul_arri = true;
        }
        //�ʏ�̓����Aplayer�̑O�Ńz�o�����O
        if(vul_arri == true)
        {
            transform.position = Vector3.Lerp(e_pos, new Vector3(p_pos.x + 5, p_pos.y + 6), speed * Time.deltaTime);
        }
        if (spriteRenderer.isVisible)
        {
            time += Time.fixedDeltaTime; //��ʓ��ɗ��Ă��玞�Ԍv��
        }
        if (time >= 5)
        {
            vul_isgrap = false;�@//����ł�����̂𗎂Ƃ�
            time = 0;
        }
        //�A���Ă�����
        if(vul_isgrap == false && time >= 2)
        {
            transform.position = Vector3.Lerp(e_pos, new Vector3(p_pos.x + 20, p_pos.y + 20), 0.2f * Time.deltaTime);
            transform.localScale = Vector3.one;
        }

    }
    IEnumerator VultureGrab()�@//�͂ޓ�����������������ǉ����邩��
    {
        yield return null;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("BreakGround")
            && ground_reland == false && Bunny == true)
        {
            ground_reland = true;
        }
    }

    private void BunnyMove()
    {
        animator.SetInteger("BunnyMove", (int)rb.velocity.y);
        if (ground_reland == true && timestop == false)
        {
            time += Time.fixedDeltaTime;
        }

        if (playerpos_x > enemypos_x) //�G�̈ʒu��Player��荶�̏ꍇ
        {
            if (timestop == false && ground_reland == true)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                if(BunnybitMove == false)
                {
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }
            }
            if (time > 2 && timestop == false) // ���b���ƂɃW�����v
            {
                time = 0;
                timestop = true;
                rb.velocity = new Vector2(speed, bunnyJ_Height);
                StartCoroutine(Reland_F());
            }
        }
        else if (playerpos_x < enemypos_x) //�G�̈ʒu��Player���E�̏ꍇ
        {
            if (timestop == false && ground_reland == true)
            {
                transform.localScale = new Vector3(1, 1, 1);
                if (BunnybitMove == false)
                {
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                }
            }
            if (time > 2 && timestop == false) // ���b���ƂɃW�����v
            {
                time = 0;
                timestop = true;
                rb.velocity = new Vector2(-speed, bunnyJ_Height);
                StartCoroutine(Reland_F());
            }
        }
        if (ground_reland == true && timestop == true) //��d�W�����v��h������
        {
            time = 0;
            timestop = false;
        }
    }
    IEnumerator Reland_F()
    {
        yield return new WaitForSeconds(0.2f);
        ground_reland = false;
    }

    private void BatMove()
    {
        //�啂̓���
    }
    private void DinoMove()
    {
        distance = Vector3.Distance(transform.position, playerOb.transform.position);
        //if (distance <= 1) //�G�̈ʒu��Player���E�̏ꍇ
        //{
        //    rb.velocity = new Vector2(0, rb.velocity.y);
        //    DinoFire();

        //}
        if (playerpos_x < enemypos_x) //�G�̈ʒu��Player���E�̏ꍇ
        {
            transform.localScale = new Vector3(1, 1, 1);
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (distance <= 5 && canFire)
            {
                DinoFire();
            }
            if (!canFire)
            {
                float elapsedCooldownTime = Time.time - lastFireTime;
                if (elapsedCooldownTime >= cooldownTime)
                {
                    canFire = true;
                }
            }

        }
        else if (playerpos_x >= enemypos_x) //�G�̈ʒu��Player��荶�̏ꍇ
        {
            transform.localScale = new Vector3(-1, 1, 1);
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (distance <= 5 && canFire)
            {
                DinoFire();
            }
            if (!canFire)
            {
                float elapsedCooldownTime = Time.time - lastFireTime;
                if (elapsedCooldownTime >= cooldownTime)
                {
                    canFire = true;
                }
            }
        }
    }

    private void DinoFire()
    {
        // �v���n�u����V�������˃I�u�W�F�N�g�𐶐�
        GameObject Fire = Instantiate(firePre, spawnPoint.position, Quaternion.identity);

        // ���˕����̌v�Z
        Vector2 launchDirection = (Player.position - spawnPoint.position).normalized;

        Fire.GetComponent<Rigidbody2D>().velocity = launchDirection * fireSpeed;

        // ��莞�Ԍ�ɔ��˃I�u�W�F�N�g��j��
        Destroy(Fire, fireLifetime);

        // �N�[���_�E���J�n
        canFire = false;
        //Debug.Log("�N�[���_�E���J�n");
        // �Ō�ɔ��˂������Ԃ��X�V
        lastFireTime = Time.time;
    }



    private void DogMove()
    {
        if (playerpos_x < enemypos_x)�@//�G�̈ʒu��Player���E�̏ꍇ
        {
            enemyRight = true;
            //���͍��ɕ���,animation
            transform.localScale = new Vector3(1, 1, 1);
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            animator.SetFloat("speed", rb.velocity.x * -1);
            //Player��enemy�̈ʒu�ϐ�
            distance = Vector3.Distance(transform.position, playerOb.transform.position);
            //Player�Ƃ̈ʒu��5�ȓ��ŃA�j���[�V�����ύX
            if (distance <= 5)
            {
                animator.SetBool("goState", false);
                StartCoroutine(DogAttack());
            }
        }
        else if (playerpos_x > enemypos_x)�@//�G�̈ʒu��Player��荶�̏ꍇ
        {
            enemyRight = false;
            //���͉E�ɕ���,animation
            transform.localScale = new Vector3(-1, 1, 1);
            rb.velocity = new Vector2(speed, rb.velocity.y);
            animator.SetFloat("speed", rb.velocity.x);
            //Player��enemy�̈ʒu�ϐ�
            distance = Vector3.Distance(transform.position, playerOb.transform.position);
            //Player�Ƃ̈ʒu��5�ȓ��ŃA�j���[�V�����ύX
            if (distance <= 5)
            {
                animator.SetBool("goState", false);
                StartCoroutine(DogAttack());
            }
        }
    }

    IEnumerator DogAttack()
    {
        coroutine = true;
        animator.SetBool("attack", true);
        yield return new WaitForSeconds(1);
        //�����E�ɂ���̂ō��ɓˌ��AAnimation�؂�ւ�
        if (enemyRight == true)
        {
            rb.AddForce(new Vector2(-15, 0), ForceMode2D.Impulse);
            Invoke(("Stop"), 0.4f);
            StartCoroutine(WaitAnimation("Dog_Attack"));            
            yield return new WaitForSeconds(2);           
        }
        //�������ɂ���̂ŉE�ɓˌ��AAnimation�؂�ւ�
        else if (enemyRight == false)
        {
            rb.AddForce(new Vector2(15, 1), ForceMode2D.Impulse);
            Invoke(("Stop"), 0.4f);
            StartCoroutine(WaitAnimation("Dog_Attack"));
            yield return new WaitForSeconds(2);    
        }
        animator.SetTrigger("stop");
        animator.SetBool("goState", true);
        animator.SetBool("attack", false);
        
        coroutine = false;
    }

    private void Stop()
    {
        rb.velocity = Vector2.zero;
    }

    private IEnumerator WaitAnimation(string stateName, UnityAction onCompleted = null)
    {
        yield return new WaitUntil(() =>
        {
            //�X�e�[�g���ω����A�A�j���[�V�������I������܂őҋ@
            var state = animator.GetCurrentAnimatorStateInfo(0);
            return state.IsName(stateName) && state.normalizedTime >= 1;
        });
        onCompleted?.Invoke();
    }

    private void OpossumMove()
    {
        if (spriteRenderer.isVisible)
        {
            int xVector = -1;
            if (opposumright)
            {
                xVector = 1;
                transform.localScale = new Vector3(-0.5f, 0.5f, 1);
            }
            else
            {
                transform.localScale = new Vector3(0.5f, 0.5f, 1);
            }
            rb.velocity = new Vector2(xVector * speed, rb.velocity.y);
        }
        else
        {
            rb.Sleep();
        }
    }


    private void PigMove()
    {
        if (spriteRenderer.isVisible)
        {
            int xVector = -1;
            if (playerOb.transform.position.x>this.transform.position.x)
            {
                xVector = 1;
                transform.localScale = new Vector3(-0.8f, 0.8f, 1);
            }
            else
            {
                transform.localScale = new Vector3(0.8f, 0.8f, 1);
            }

            if (pigcoroutine == null)
            {
                if ((playerOb.transform.position - this.transform.position).sqrMagnitude < 25)
                {
                    animator.SetTrigger("PigJump");
                    pigcoroutine = StartCoroutine(PigJump(xVector));
                }
                else
                {
                    rb.velocity = new Vector2(xVector * speed, rb.velocity.y);
                }
            }
            
            else 
            {

            }
        }
        else
        {
            rb.Sleep();
        }
    }

    private IEnumerator PigJump(int xVector) 
    {
        rb.gravityScale = 0;
        rb.velocity = new Vector2(3*xVector, 6);
        yield return new WaitForSeconds(0.5f); //�㏸
        rb.velocity = Vector2.zero;
        //yield return new WaitForSeconds(0.4f);//�؋󎞊�
        float rnd = Random.Range(2,10)/10f;
        this.transform.DOShakePosition(rnd-0.1f,1f,20,90f,false); //�����ł�
        rb.velocity = new Vector2(0, -20);//���~
        yield return new WaitUntil(() =>rb.velocity.y==0);
        Instantiate(pigimpact, new Vector2(this.transform.position.x-1.5f,this.transform.position.y),Quaternion.identity);
        Instantiate(pigimpact, new Vector2(this.transform.position.x + 1.5f, this.transform.position.y), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("PigRun");
        yield return new WaitForSeconds(1f);
        rb.gravityScale = 1;
        yield return new WaitForSeconds(0.5f);
        pigcoroutine = null;
        yield break;
    }
}
