using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    [SerializeField] NDTDEvent ndtdEvent;
    //辞書のキーと同じ文字列で判定
    void Start()
    {
            //初回起動時の処理を実行
            ndtdEvent.Stage1();
    }
    //最初の犬を倒したときにスキルなどの説明ダイアログ
    
}
