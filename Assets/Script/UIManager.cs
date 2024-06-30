using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image image;
    public Text text;
    public int x;

    void Update()
    {
        // x�̒l�ɉ�����UI�v�f��\���܂��͔�\���ɂ���
        if (x == 0)
        {
            image.gameObject.SetActive(false); // �摜���\��
            text.gameObject.SetActive(false);  // �e�L�X�g���\��
        }
        else if (x >= 1)
        {
            image.gameObject.SetActive(true);  // �摜��\��
            text.gameObject.SetActive(true);   // �e�L�X�g��\��

            // �e�L�X�g�ɐ��l��\��
            text.text = x.ToString();
        }
    }
}
