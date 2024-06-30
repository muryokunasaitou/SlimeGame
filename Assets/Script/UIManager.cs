using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image image;
    public Text text;
    public int x;

    void Update()
    {
        // xの値に応じてUI要素を表示または非表示にする
        if (x == 0)
        {
            image.gameObject.SetActive(false); // 画像を非表示
            text.gameObject.SetActive(false);  // テキストを非表示
        }
        else if (x >= 1)
        {
            image.gameObject.SetActive(true);  // 画像を表示
            text.gameObject.SetActive(true);   // テキストを表示

            // テキストに数値を表示
            text.text = x.ToString();
        }
    }
}
