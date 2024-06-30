using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeManage : MonoBehaviour
{
    public int maxLife = 5; // ���C�t�̍ő�l
    public int currentLife; // ���݂̃��C�t
    public GameObject heartPrefab; // �n�[�g�̃v���n�u
    public GameObject heartAreaPrefab; // ���j�[�N�ȃI�u�W�F�N�g�̃v���n�u
    public static string currentSceneName; // �Q�[���I�[�o�[���̃V�[����

    private void Start()
    {
        int objectCount = FindObjectsOfType<LifeManage>().Length;
        if (objectCount == 1)
        {
            //// GameManager�̃C���X�^���X�����݂��邩�m�F���Ă���currentLife��ݒ肷��
            //if (GameManager.instance != null)
            //{
            //    currentLife = GameManager.instance.hearts;
            //    Debug.Log("Current Life���C���X�^���X����擾");
            //}
            //else
            //{
            //    currentLife = maxLife; // GameManager������������Ă��Ȃ��ꍇ�A�ő�l�ŏ���������
            //    Debug.Log("Current Life���ő�l����擾");
            //}

            //// �n�[�g�̏��������������s
            //Debug.Log("Current Life: " + currentLife);
            //InitializeHearts();
            //// �n�[�g�̏��������������s
            Debug.Log(currentLife);
            //currentLife = GameManager.instance.hearts;
            currentLife = maxLife;
            InitializeHearts();
        }
    }

    void InitializeHearts()
    {
        float heartWidth = 30f; // �n�[�g�̕�
        float startPosition = -850f;

        for (int i = 0; i < maxLife; i++)
        {
            GameObject heart = Instantiate(heartPrefab, transform);
            heart.transform.localPosition = new Vector2(startPosition + i * (heartWidth + 40), 460f); // �n�[�g�̈ʒu��ݒ�
            heart.SetActive(true); // �n�[�g���A�N�e�B�u�ɂ���
        }

        UpdateHearts(); // �n�[�g�̕\��������������
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
                heartImage.enabled = true; // �n�[�g��\��
            }
            else
            {
                heartImage.enabled = false; // �n�[�g���\��
            }
        }
    }

    void GameOver()
    {
        // ���݂̃V�[���̖��O���擾���A�ϐ��Ɋi�[����
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
