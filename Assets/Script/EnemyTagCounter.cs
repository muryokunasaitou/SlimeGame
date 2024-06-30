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
    private Dictionary<string, int> previousCounters = new Dictionary<string, int>(); // �ǉ�

    public CharacterSkillsStats slimeStats;

    private void Start()
    {
        ResetCounters();
    }

    // �X�L���J�E���^�[����
    public void IncrementCounter(string enemyTag)
    {
        if (enemyTagCounters.ContainsKey(enemyTag))
        {
            enemyTagCounters[enemyTag]++;
        }
    }

        // �X�L���J�E���^�[����
    public void DecrementCounter(string enemyTag)
    {
        if (enemyTagCounters.ContainsKey(enemyTag))
        {
            enemyTagCounters[enemyTag]--;
        }
    }

    //�J�E���^�[�擾
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

    // �J�E���^�[���o�b�N�A�b�v����p�֐�
    public void BackUpCounters()
    {

    }

    // �V�[���؂�ւ����΍�
    public void ReloadCounters()
    {
        // json�t�@�C����ǂݍ��ޏ�����ǉ�����
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


    // �ǉ�: �J�E���^�[���ύX���ꂽ���ǂ������m�F����֐�
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
