using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CounterDisplay : MonoBehaviour
{
    public EnemyTagCounter enemyTagCounter;
    public Text counterTextBunny;
    public Text counterTextDog;
    public Text counterTextBat;
    public Text counterTextDino;
    public Text counterTextOpossum;
    public Text counterTextPig;
    public Button Bunny;
    public Button Dog;
    public Button Bat;
    public Button Dino;
    public Button Opossum;
    public Button Pig;



    private void Update()
    {
        // カウンターの情報を取得
        Dictionary<string, int> counters = enemyTagCounter.GetAllCounters();

        // カウンターの値をUIテキストに表示
        foreach (var kvp in counters)
        {
            if (kvp.Key == "Bunny")
            {
                if (kvp.Value == 0)
                {
                    //Club.gameObject.SetActive(false); // 画像を非表示
                    Bunny.interactable = false;
                    counterTextBunny.gameObject.SetActive(false);  // テキストを非表示
                }
                else if (kvp.Value >= 1)
                {
                    //Club.gameObject.SetActive(true);  // 画像を表示
                    Bunny.interactable = true;
                    counterTextBunny.gameObject.SetActive(true);   // テキストを表示

                    // テキストに数値を表示
                    counterTextBunny.text = kvp.Value.ToString();
                }
                // counterTextA.text = $"EnemyTagA: {kvp.Value}";
            }
            else if (kvp.Key == "Dog")
            {
                if (kvp.Value == 0)
                {
                    //Club.gameObject.SetActive(false); // 画像を非表示
                    Dog.interactable = false;
                    counterTextDog.gameObject.SetActive(false);  // テキストを非表示
                }
                else if (kvp.Value >= 1)
                {
                    //Club.gameObject.SetActive(true);  // 画像を表示
                    Dog.interactable = true;
                    counterTextDog.gameObject.SetActive(true);   // テキストを表示

                    // テキストに数値を表示
                    counterTextDog.text = kvp.Value.ToString();
                }
                // counterTextB.text = $"EnemyTagB: {kvp.Value}";
            }
            else if (kvp.Key == "Bat")
            {
                if (kvp.Value == 0)
                {
                    //Club.gameObject.SetActive(false); // 画像を非表示
                    Bat.interactable = false;
                    counterTextBat.gameObject.SetActive(false);  // テキストを非表示
                }
                else if (kvp.Value >= 1)
                {
                    //Club.gameObject.SetActive(true);  // 画像を表示
                    Bat.interactable = true;
                    counterTextBat.gameObject.SetActive(true);   // テキストを表示

                    // テキストに数値を表示
                    counterTextBat.text = kvp.Value.ToString();
                }
                // counterTextB.text = $"EnemyTagB: {kvp.Value}";
            }
            else if (kvp.Key == "Dino")
            {
                if (kvp.Value == 0)
                {
                    //Club.gameObject.SetActive(false); // 画像を非表示
                    Dino.interactable = false;
                    counterTextDino.gameObject.SetActive(false);  // テキストを非表示
                }
                else if (kvp.Value >= 1)
                {
                    //Club.gameObject.SetActive(true);  // 画像を表示
                    Dino.interactable = true;
                    counterTextDino.gameObject.SetActive(true);   // テキストを表示

                    // テキストに数値を表示
                    counterTextDino.text = kvp.Value.ToString();
                }
                // counterTextB.text = $"EnemyTagB: {kvp.Value}";
            }
            else if (kvp.Key == "Opossum")
            {
                if (kvp.Value == 0)
                {
                    //Club.gameObject.SetActive(false); // 画像を非表示
                    Opossum.interactable = false;
                    counterTextOpossum.gameObject.SetActive(false);  // テキストを非表示
                }
                else if (kvp.Value >= 1)
                {
                    //Club.gameObject.SetActive(true);  // 画像を表示
                    Opossum.interactable = true;
                    counterTextOpossum.gameObject.SetActive(true);   // テキストを表示

                    // テキストに数値を表示
                    counterTextOpossum.text = kvp.Value.ToString();
                }
                // counterTextB.text = $"EnemyTagB: {kvp.Value}";
            }
            else if (kvp.Key == "Pig")
            {
                if (kvp.Value == 0)
                {
                    //Club.gameObject.SetActive(false); // 画像を非表示
                    Pig.interactable = false;
                    counterTextPig.gameObject.SetActive(false);  // テキストを非表示
                }
                else if (kvp.Value >= 1)
                {
                    //Club.gameObject.SetActive(true);  // 画像を表示
                    Pig.interactable = true;
                    counterTextPig.gameObject.SetActive(true);   // テキストを表示

                    // テキストに数値を表示
                    counterTextPig.text = kvp.Value.ToString();
                }
                // counterTextB.text = $"EnemyTagB: {kvp.Value}";
            }
            // 他のタグに対する表示も追加できます
        }
    }
}





