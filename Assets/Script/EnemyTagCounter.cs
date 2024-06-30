using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public struct CharacterSkillsStats
{
    public int bunny;
    public int dog;
    public int bat;
    public int dino;
    public int opossum;
    public int pig;
}

public class EnemyTagCounter : MonoBehaviour
{
    public Dictionary<string, int> enemyTagCounters = new Dictionary<string, int>();
    private Dictionary<string, int> previousCounters = new Dictionary<string, int>(); // 追加

    public CharacterSkillsStats slimeStats;

    private void Start()
    {
        ResetCounters();
    }

    // スキルカウンター増加
    public void IncrementCounter(string enemyTag)
    {
        if (enemyTagCounters.ContainsKey(enemyTag))
        {
            enemyTagCounters[enemyTag]++;
        }
    }

        // スキルカウンター減少
    public void DecrementCounter(string enemyTag)
    {
        if (enemyTagCounters.ContainsKey(enemyTag))
        {
            enemyTagCounters[enemyTag]--;
        }
    }

    //カウンター取得
    public int GetCounter(string enemyTag)
    {
        if (enemyTagCounters.ContainsKey(enemyTag))
        {
            return enemyTagCounters[enemyTag];
        }
        return 0;
    }

    public Dictionary<string, int> GetAllCounters()
    {
        return enemyTagCounters;
    }

    // カウンターをバックアップする用関数
    public void BackUpCounters()
    {

    }

    // シーン切り替え時対策
    public void ReloadCounters()
    {
        // jsonファイルを読み込む処理を追加する
    }

    public void ResetCounters()
    {
        enemyTagCounters.Clear();
        enemyTagCounters.Add("Bunny", 0);
        enemyTagCounters.Add("Dog", 0);
        enemyTagCounters.Add("Bat", 0);
        enemyTagCounters.Add("Dino", 0);
        enemyTagCounters.Add("Opossum", 0);
        enemyTagCounters.Add("Pig", 0);

    }


    // 追加: カウンターが変更されたかどうかを確認する関数
    public bool IsCounterChanged()
    {
        foreach (var kvp in enemyTagCounters)
        {
            if (kvp.Value != previousCounters[kvp.Key])
            {
                return true;
            }
        }
        return false;
    }
}
