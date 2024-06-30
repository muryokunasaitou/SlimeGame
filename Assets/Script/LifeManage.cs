using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeManage : MonoBehaviour
{
    public int maxLife = 5; // ライフの最大値
    public int currentLife; // 現在のライフ
    public GameObject heartPrefab; // ハートのプレハブ
    public GameObject heartAreaPrefab; // ユニークなオブジェクトのプレハブ
    public static string currentSceneName; // ゲームオーバー時のシーン名

    private void Start()
    {
        int objectCount = FindObjectsOfType<LifeManage>().Length;
        if (objectCount == 1)
        {
            //// GameManagerのインスタンスが存在するか確認してからcurrentLifeを設定する
            //if (GameManager.instance != null)
            //{
            //    currentLife = GameManager.instance.hearts;
            //    Debug.Log("Current Lifeをインスタンスから取得");
            //}
            //else
            //{
            //    currentLife = maxLife; // GameManagerが初期化されていない場合、最大値で初期化する
            //    Debug.Log("Current Lifeを最大値から取得");
            //}

            //// ハートの初期化処理を実行
            //Debug.Log("Current Life: " + currentLife);
            //InitializeHearts();
            //// ハートの初期化処理を実行
            Debug.Log(currentLife);
            //currentLife = GameManager.instance.hearts;
            currentLife = maxLife;
            InitializeHearts();
        }
    }

    void InitializeHearts()
    {
        float heartWidth = 30f; // ハートの幅
        float startPosition = -850f;

        for (int i = 0; i < maxLife; i++)
        {
            GameObject heart = Instantiate(heartPrefab, transform);
            heart.transform.localPosition = new Vector2(startPosition + i * (heartWidth + 40), 460f); // ハートの位置を設定
            heart.SetActive(true); // ハートをアクティブにする
        }

        UpdateHearts(); // ハートの表示を初期化する
    }
    public void TakeDamage()
    {

        if (currentLife > 0)
        {
            currentLife--;
            UpdateHearts();
            Debug.Log(currentLife);
        }

        if(currentLife <= 0)
        {
            GameOver();
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Image heartImage = transform.GetChild(i).GetComponent<Image>();
            if (i < currentLife)
            {
                heartImage.enabled = true; // ハートを表示
            }
            else
            {
                heartImage.enabled = false; // ハートを非表示
            }
        }
    }

    void GameOver()
    {
        // 現在のシーンの名前を取得し、変数に格納する
        currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log(currentSceneName);
        SceneManager.LoadScene("GameOver");
    }

    public void InstantDeath() 
    {
        currentLife = 0;
        GameOver();
    }

    public void GetItem() 
    {
        if (currentLife<maxLife) 
        {
            currentLife++;
            UpdateHearts();
        }
    }
}
