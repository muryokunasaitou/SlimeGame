using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Activate : MonoBehaviour
{
    private PlayerController controller;
    private EnemyTagCounter enemyTagCounter;
    public Skill_Table skill_table;
    public float impulse;
    public GameObject firePre;  // 発射するオブジェクトのプレハブ
    public Transform spawnPoint;         // 発射位置
    public float fireSpeed = 10f;  // 発射速度
    public float fireLifetime = 2f; // 発射オブジェクトの寿命（秒）
    // private float cooldownTime = 2f; // Fireのクールタイム
    // private float lastFireTime; // 最後に発射した時間
    // private bool canFire = true; // Fire発射可否判定
    public string enemyTag = "FireTarget"; // 敵タグを指定
    private Transform enemy;

    private void Start()
    {
        controller= GetComponent<PlayerController>();
        enemyTagCounter=GetComponent<EnemyTagCounter>();
    }

    public void Bunny()
    {
        Debug.Log("スキル発動");
        // スキル発動前にデクリメント処理
        enemyTagCounter.DecrementCounter("Bunny");
        Instantiate(skill_table.skill[0].skill_effect, this.transform.position - new Vector3(0, -1, 0), Quaternion.identity);
        controller.rb.velocity = new Vector2(controller.rb.velocity.x,0);
        controller.rb.AddForce(new Vector3(0, 60, 0), ForceMode2D.Impulse);
    }

    public void Bite()
    {
        // スキル発動前にデクリメント処理
        enemyTagCounter.DecrementCounter("Dog");
        GameObject child;
        child = Instantiate(skill_table.skill[1].skill_effect,this.transform.position, Quaternion.identity);
        child.gameObject.GetComponent<BiteClass>().KamuSkill(controller.right, this.gameObject);
        this.gameObject.SetActive(false);
    }

    public void UltraSounds() 
    {
        // スキル発動前にデクリメント処理
        enemyTagCounter.DecrementCounter("Bat");
        GameObject ultrasounds=Instantiate(skill_table.skill[2].skill_effect,this.transform.position,Quaternion.identity);
        ultrasounds.transform.SetParent(this.transform);
        ultrasounds.tag = "PlayerAttack";
        Destroy(ultrasounds,2f);
    }

    public void SmallSlime() 
    {
        // スキル発動前にデクリメント処理
        enemyTagCounter.DecrementCounter("Opposum");
        Instantiate(skill_table.skill[3].skill_effect,this.transform.position,Quaternion.identity);
        controller.SmallSlime();
    }

    public void Fire()
    {
        // スキル発動前にデクリメント処理
        enemyTagCounter.DecrementCounter("Dino");

        // 最も近い敵の位置を取得
        FindNearestEnemy();

        // プレハブから新しい発射オブジェクトを生成
        GameObject Fire = Instantiate(firePre, spawnPoint.position, Quaternion.identity);



        // 発射方向の計算
        if (enemy != null)
        {
            // Debug.Log("発射します");
            // 発射方向の計算
            Vector2 launchDirection = (enemy.position - spawnPoint.position).normalized;

            Fire.GetComponent<Rigidbody2D>().velocity = launchDirection * fireSpeed;
        }


        // 一定時間後に発射オブジェクトを破棄
        Destroy(Fire, fireLifetime);

        // クールダウン開始
        // canFire = false;
        // Debug.Log("クールダウン開始");
        // 最後に発射した時間を更新
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
                // Debug.Log(obj.tag + "を確認");
                if (IndexOf(enemyTag, obj.tag) != -1) // タグが指定されたものの中にあるか確認
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
            // Debug.Log("取得したタグは" + value);
            return 0;
        }
    // Debug.Log("タグの取得に失敗");
    return -1;
    }

    public void PigImpact()
    {
        // スキル発動前にデクリメント処理
        enemyTagCounter.DecrementCounter("Pig");
        StartCoroutine(Impact());
    }


    private IEnumerator Impact()
    {
        //下に力かける処理
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
