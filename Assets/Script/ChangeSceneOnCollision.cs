using UnityEngine;
using UnityEngine.SceneManagement;//�V�[���}�l�W�����g��L���ɂ���

public class ChangeSceneOnCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "LoadObject")
        {
            SceneManager.LoadScene("Stage1_HeigenScene");//save�f�[�^�����ƂɑJ�ڂł���悤�ɏC���\��
        }
    }
}