using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Activate : MonoBehaviour
{
    private PlayerController controller;
    private EnemyTagCounter enemyTagCounter;
    public Skill_Table skill_table;
    public float impulse;
    public GameObject firePre;  // ���˂���I�u�W�F�N�g�̃v���n�u
    public Transform spawnPoint;         // ���ˈʒu
    public float fireSpeed = 10f;  // ���ˑ��x
    public float fireLifetime = 2f; // ���˃I�u�W�F�N�g�̎����i�b�j
    // private float cooldownTime = 2f; // Fire�̃N�[���^�C��
    // private float lastFireTime; // �Ō�ɔ��˂�������
    // private bool canFire = true; // Fire���ˉ۔���
    public string enemyTag = "FireTarget"; // �G�^�O���w��
    private Transform enemy;

    private void Start()
    {
        controller= GetComponent<PlayerController>();
        enemyTagCounter=GetComponent<EnemyTagCounter>();
    }

    public void Bunny()
    {
        Debug.Log("�X�L������");
        // �X�L�������O�Ƀf�N�������g����
        enemyTagCounter.DecrementCounter("Bunny");
        Instantiate(skill_table.skill[0].skill_effect, this.transform.position - new Vector3(0, -1, 0), Quaternion.identity);
        controller.rb.velocity = new Vector2(controller.rb.velocity.x,0);
        controller.rb.AddForce(new Vector3(0, 60, 0), ForceMode2D.Impulse);
    }

    public void Bite()
    {
        // �X�L�������O�Ƀf�N�������g����
        enemyTagCounter.DecrementCounter("Dog");
        GameObject child;
        child = Instantiate(skill_table.skill[1].skill_effect,this.transform.position, Quaternion.identity);
        child.gameObject.GetComponent<BiteClass>().KamuSkill(controller.right, this.gameObject);
        this.gameObject.SetActive(false);
    }

    public void UltraSounds() 
    {
        // �X�L�������O�Ƀf�N�������g����
        enemyTagCounter.DecrementCounter("Bat");
        GameObject ultrasounds=Instantiate(skill_table.skill[2].skill_effect,this.transform.position,Quaternion.identity);
        ultrasounds.transform.SetParent(this.transform);
        ultrasounds.tag = "PlayerAttack";
        Destroy(ultrasounds,2f);
    }

    public void SmallSlime() 
    {
        // �X�L�������O�Ƀf�N�������g����
        enemyTagCounter.DecrementCounter("Opposum");
        Instantiate(skill_table.skill[3].skill_effect,this.transform.position,Quaternion.identity);
        controller.SmallSlime();
    }

    public void Fire()
    {
        // �X�L�������O�Ƀf�N�������g����
        enemyTagCounter.DecrementCounter("Dino");

        // �ł��߂��G�̈ʒu���擾
        FindNearestEnemy();

        // �v���n�u����V�������˃I�u�W�F�N�g�𐶐�
        GameObject Fire = Instantiate(firePre, spawnPoint.position, Quaternion.identity);



        // ���˕����̌v�Z
        if (enemy != null)
        {
            // Debug.Log("���˂��܂�");
            // ���˕����̌v�Z
            Vector2 launchDirection = (enemy.position - spawnPoint.position).normalized;

            Fire.GetComponent<Rigidbody2D>().velocity = launchDirection * fireSpeed;
        }


        // ��莞�Ԍ�ɔ��˃I�u�W�F�N�g��j��
        Destroy(Fire, fireLifetime);

        // �N�[���_�E���J�n
        // canFire = false;
        // Debug.Log("�N�[���_�E���J�n");
        // �Ō�ɔ��˂������Ԃ��X�V
        // lastFireTime = Time.time;

    }

    private void FindNearestEnemy()
    {
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();
        float minDistance = Mathf.Infinity;
        enemy = null;

        foreach (GameObject obj in allGameObjects)
        {
            if (obj.tag != "Untagged")
            {
                // Debug.Log(obj.tag + "���m�F");
                if (IndexOf(enemyTag, obj.tag) != -1) // �^�O���w�肳�ꂽ���̂̒��ɂ��邩�m�F
                {
                    float distance = Vector3.Distance(obj.transform.position, spawnPoint.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        enemy = obj.transform;
                    }
                }
            }
        }
    }

    private int IndexOf(string tag, string value)
    {
        if (tag == value)
        {
            // Debug.Log("�擾�����^�O��" + value);
            return 0;
        }
    // Debug.Log("�^�O�̎擾�Ɏ��s");
    return -1;
    }

    public void PigImpact()
    {
        // �X�L�������O�Ƀf�N�������g����
        enemyTagCounter.DecrementCounter("Pig");
        StartCoroutine(Impact());
    }


    private IEnumerator Impact()
    {
        //���ɗ͂����鏈��
        controller.rb.AddForce(new Vector3(0, 100, 0),ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.3f);
        controller.rb.AddForce(new Vector3(0, -150, 0), ForceMode2D.Impulse);
        yield return new WaitUntil(() => controller.rb.velocity.y == 0);
        float i = impulse;
        Debug.Log(i);
        GameObject impact1 = Instantiate(skill_table.skill[5].skill_effect, new Vector2(this.transform.position.x - 1.5f, this.transform.position.y), Quaternion.identity);
        GameObject impact2 = Instantiate(skill_table.skill[5].skill_effect, new Vector2(this.transform.position.x + 1.5f, this.transform.position.y), Quaternion.identity);
        impact1.tag = "PlayerAttack";
        impact2.tag = "PlayerAttack";
        if (i > -1)
        {

        }
        else if (i > -5)
        {
            impact1.transform.localScale *=1.2f;
            impact2.transform.localScale *=1.2f;
        }
        else if (i > -10)
        {
            impact1.transform.localScale *= 1.4f;
            impact2.transform.localScale *= 1.4f;
        }
        else if (i > -15)
        {
            impact1.transform.localScale *= 1.6f;
            impact2.transform.localScale *= 1.6f;
        }
        else if (i > -20)
        {
            impact1.transform.localScale *= 1.8f;
            impact2.transform.localScale *= 1.8f;
        }
        else if (i > -30)
        {
            impact1.transform.localScale *= 2.5f;
            impact2.transform.localScale *= 2.5f;
        }
    }
}
