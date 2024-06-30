using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // ボタンをInspectorで関連付けるための変数
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
        // 指定のシーンを読み込む
        SceneManager.LoadScene("TitleScene");
    }

    void ReStart()
    {
        // 指定のシーンを読み込む

        //Debug.Log("やり直しは" + LifeManage.currentSceneName + "から");
        SceneManager.LoadScene(LifeManage.currentSceneName);
    }
}
