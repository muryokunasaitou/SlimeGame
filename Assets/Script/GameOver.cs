using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // �{�^����Inspector�Ŋ֘A�t���邽�߂̕ϐ�
    public Button ButtonRestart;
    public Button ButtonGiveup;
    public string LastScene;

    void Start()
    {

        ButtonGiveup.onClick.AddListener(ToTitle);
        ButtonRestart.onClick.AddListener(ReStart);
    }


    void ToTitle()
    {
        // �w��̃V�[����ǂݍ���
        SceneManager.LoadScene("TitleScene");
    }

    void ReStart()
    {
        // �w��̃V�[����ǂݍ���

        //Debug.Log("��蒼����" + LifeManage.currentSceneName + "����");
        SceneManager.LoadScene(LifeManage.currentSceneName);
    }
}
