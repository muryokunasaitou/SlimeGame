using UnityEngine;
using UnityEngine.SceneManagement;//シーンマネジメントを有効にする

public class ChangeSceneOnCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "LoadObject")
        {
            SceneManager.LoadScene("Stage1_HeigenScene");//saveデータをもとに遷移できるように修正予定
        }
    }
}