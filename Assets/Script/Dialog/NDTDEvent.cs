using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Events;
using Unity.VisualScripting;
using DG.Tweening;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class NDTDEvent : MonoBehaviour
{
    [SerializeField] AnimateNDialog animateNDialog;
    [SerializeField] AnimateTDialog animateTDialog;
    //[SerializeField] PlayerController pc;
    [SerializeField] CameraShake camShake;
    [SerializeField] BearScrollEvent bse;
    [SerializeField] GameObject cameraob;
    Camera camera;
    PlayerController pc;
    public GameObject NDTDparent;
    public Text ndEventText;
    public Text nameText;
    public Text tdEventText;
    private string[] parts;
    int i = 0;
    public bool isNDEvent;
    public bool isTDEvent;
    public bool isTalkStop;
    public bool isCamShake;
    float c_seconds;
    public bool isStopPlayer;
    public bool isStartTDE;
    public bool isPlayerToLand;
    private string callOnece;
    private float elapsedTime = 0;
    ////リスト定義
    //ステージや種類ごとにリストを分ける
    private List<string> ndList_s1 = new List<string>();
    private List<string> tdList_s1 = new List<string>();
    string title = "SLIME RPG へようこそ,,あなたはスライムとして冒険してもらいます,,そこの家のドアに行くと冒険が始まります,,Aキー、Dキーで左右に移動\nFキーを押しながら移動で早く動けます,,Spaceキーでジャンプすることが出来ます,,では、よい冒険を！";
    string stage1 = "魔物の縄張りに住むスライムは平和に暮らしていました,,しかし、あることで縄張りを治める四天王にムカついてしまいました,,スライムは四天王を倒す旅に出ます";
    string skill;
    string guooo;
    string bibiru;
    
    [SerializeField] private float[] eventcoordinate_nd; //イベントの座標nd
    [SerializeField] private float[] eventcoordinate_td; //イベントの座標td
    private int i_nd = 0;
    private int i_td = 0;
    [SerializeField] private UnityEvent[] events_nd; //イベントの座標に到達した時何のイベントを起こすか
    [SerializeField] private UnityEvent[] events_td;

    void Start()
    {
        camera = cameraob.gameObject.GetComponent<Camera>();
        pc = GetComponent<PlayerController>();

        if(SceneManager.GetActiveScene().name == "Stage1_HeigenScene") //シーン名ごとに分岐収納
        {
            //会話文追加。「,,」で文章の区切り。タグはUnityの方と合わせて、NDEventを付ける

            ndList_s1.Add("あっ、あれは犬の魔物です,,近づくと突撃してくるから注意して！,,無防備な上から覆いかぶされば魔物を食べることができるよ");
            ndList_s1.Add("イベントタグBの文章です");
            ndList_s1.Add("イベントタグCの文章です");
            ndList_s1.Add("イベントタグDの文章です");

            skill = "やったね！\n魔物を飲み込めたね！,,スライムは飲み込んだ敵の情報を取り込んでスキルとして使えるようになるよ,,スキルは画面の下に表示されるよ,,使いたいスキルにカーソルを合わせてクリックしてね";
            //Stage1初回起動時の表示文章、実行はStage1スクリプト

            //カメラが揺れる秒数
            c_seconds = camShake.shakeDuration;
            //会話文追加。「,,」で名前と文章の区切り。最初は名前。Unityの方のタグと合わせる
            //連続で会話イベント等する場合は区切り文字で余分にpart.Lengthを増やしておくこと
            tdList_s1.Add("ライバル,,よう！,,お前しってるか？,,四天王がここらを支配して1年、やつら天狗になってやがる,,いい加減野放しにできないよな,,なに？お前が倒すだって？,,ばかいえ、俺が倒すんだよ！,,お前より俺の必殺技のが…,, ");
            tdList_s1.Add("四天王クマ,, クマーっ！");
            tdList_s1.Add("イベントタグDの文章です");

            // 表示方法が変わっている文章
            guooo = "???,,グゥオオオオオオォォォォォォ！！！！！,, ";
            bibiru = "ライバル,,ヒェッ…,,.........,,..................,,ふん、どうやら四天王の1人がご立腹のようだぜ,,ここらを牛耳っているのはクマのやつだな,,いい加減、取り締め料として食料を取られ続ける訳にはいかねぇ!,,お前、ちょっと様子見て来いよ,,この先にクマの根城があるぞ,,俺？俺は何かあった時に動けるように陰から見てるからよ";
        }
    }

    void Update()
    {
        if (isPlayerToLand && isStopPlayer == false)
        {
            StartTDEvent2();
        }
        //イベントの消化
        if (NDTDparent.transform.position.x > eventcoordinate_nd[i_nd])
        {
            events_nd[i_nd].Invoke();
            
        }
        if (NDTDparent.transform.position.x > eventcoordinate_td[i_td])
        {
            events_td[i_td].Invoke();
            
        }
        //ナレーターダイアログが開いている間、左クリックを押すと、文章送り
        if (animateNDialog.IsOpen && animateNDialog.n_SentenceTrigger==true && parts != null)
        {
            if (i < parts.Length - 1)
            {
                i++;
                ndEventText.text = parts[i];
                animateNDialog.n_SentenceTrigger = false;　//クリックを押さなくても文章送りされるのを防ぐ
            }

            //最後の文章になって、左クリックを押すとフラグオフ、Textオブジェクトを非アクティブにする
            if (i >= parts.Length - 1)
            {
                Array.Clear(parts, 0, parts.Length);
                isNDEvent = false;
                i = 0;
            }
        }
        //トークダイアログが開いている間、左クリックを押すと、文章送り
        if (animateTDialog.t_IsOpen && animateTDialog.t_SentenceTrigger == true && parts != null && isTalkStop == false)
        {
            if (i < parts.Length - 1)
            {
                i++;
                tdEventText.text = parts[i];
                animateTDialog.t_SentenceTrigger = false;　//クリックを押さなくても文章送りされるのを防ぐ
            }
            //差し込みイベント
            if (i_td == 1 && tdEventText.text == " ")
            {
                TalkChange("guooo");
            }
            //最後の文章になって、左クリックを押すとフラグオフ、Textオブジェクトを非アクティブにする
            if (i >= parts.Length - 1)
            {
                //Array.Clear(parts, 0, parts.Length);
                isTDEvent = false;
                i = 0;
                pc.enabled = true;
                isStartTDE = false;
                isPlayerToLand = false;
                isStopPlayer = false;
            }
        }
    }

    public void StartNDEvent_s1()
    {
        isNDEvent = true;
        // リストから会話内容を取得
        if (ndList_s1[i_nd] != null)
        {
            string text = ndList_s1[i_nd];
            DisplayNDEvent(text);
            i_nd++;
            animateNDialog.DialogNarratorOpen();
        }
        else
        {
            //エラーハンドリングの内容をここに記述
            Debug.Log("listの文章がない");
        }
    }
    public void Title()
    {
        isNDEvent = true;
        DisplayNDEvent(title);
        animateNDialog.DialogNarratorOpen();
    }
    public void Stage1()
    {
        isNDEvent = true;
        DisplayNDEvent(stage1);
        animateNDialog.DialogNarratorOpen();
    }
    public void Skilldiscribe()
    {
        isNDEvent = true;
        DisplayNDEvent(skill);
        animateNDialog.DialogNarratorOpen();
    }
    void DisplayNDEvent(string text)
    {
        string delimiter = ",,"; //区切り文字
        //区切り文字で区切ったパートを配列に入れる処理
        parts = text.Split(new[] { delimiter }, StringSplitOptions.None);
        //最初の文章入力
        ndEventText.text = parts[0];
    }
    public void StartTDEvent()
    {
        pc.rb.velocity = new Vector2(0, pc.rb.velocity.y);
        pc.animator.SetBool("Dash", false);
        pc.enabled = false;
        isStartTDE = true;
    }
    public void StartTDEvent2()
    {
        Debug.Log("StartTDEvent2");
        isStopPlayer = true;
        isTDEvent = true;
        // リストから会話内容を取得
        if (tdList_s1[i_td] != null)
        {
            string text = tdList_s1[i_td];
            // 会話内容を表示
            DisplayTDEvent(text);
            i_td++;
            animateTDialog.TDialogOpen();
        }
        else
        {
            //エラーハンドリングの内容をここに記述
            Debug.Log("トークダイアログのタグか文章の辞書が上手く参照されていないか同じ文章を表示させない処理をしたか");
        }
    }
    void DisplayTDEvent(string text)
    {
        string delimiter = ",,"; //区切り文字
        //区切り文字で区切ったパートを配列に入れる処理
        parts = text.Split(new[] { delimiter }, StringSplitOptions.None);
        //名前入力
        nameText.text = parts[0];
        //最初の文章入力
        tdEventText.text = parts[1];
    }

    //話の最中に他の人物が話をするためのもの（俺：…、相手：…　等）
    private void TalkChange(string key)
    {
        i = 0;
        if (key == "guooo")
        {
            isTalkStop = true;
            DisplayTDEvent(guooo);
            isCamShake = true;
            Time.timeScale = 1;
            StartCoroutine(AfterShakeTalk(c_seconds, "bibiru"));
        }
    }

    IEnumerator AfterShakeTalk(float seconds, string key)
    {
        if(key == "bibiru")
        {
            yield return new WaitForSeconds(seconds);
            isTalkStop = false;
            Time.timeScale = 0;
            DisplayTDEvent(bibiru);
        }
    }

    public void Bearmeet()
    {
        //float time = 0.0f;
        //float timelimit = 5.0f;
        //time += Time.deltaTime;
        //while(time <= timelimit)
        //{
            camera.orthographicSize = camera.orthographicSize + 1; //カメラ拡大
        //}
        
        StartTDEvent();
    }
}
