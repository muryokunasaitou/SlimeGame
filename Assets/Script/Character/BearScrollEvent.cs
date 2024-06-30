using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
using Unity.VisualScripting;
using DG.Tweening;

public class BearScrollEvent : MonoBehaviour
{
    public GameObject parent;
    public Vector3 scrollspeed= new Vector3(0.7f, 0, 0); //スクロールスピード
    public Vector3 upspeed; //スピードアップ時の速度
    private bool a;
    private Animator bearanim;
    private int upint=0;
    private int downint=0;

    private int roat = Animator.StringToHash("Roat");
    private int stay = Animator.StringToHash("Stay");
    private int bearbreak = Animator.StringToHash("Break");

    [SerializeField] private float[] eventcoordinate; //イベントの座標
    private int i = 0;
    [SerializeField] private UnityEvent[] events; //イベントの座標に到達した時何のイベントを起こすか

    private bool staynow = true;

    private void Start()
    {
        bearanim = GetComponent<Animator>();
    }

    private void Update() 
    {
        if (!staynow) 
        {
            parent.transform.position -= scrollspeed; //現在は親オブジェクトの座標準拠(熊そのものだとワールド座標に変換する処理が入りそうなため)

            if (parent.transform.position.x < eventcoordinate[i]) 
            {
                events[i].Invoke();
                i++;
            }
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    main_camera.SetActive(!main_camera.activeSelf);
        //    chase_camera.SetActive(!chase_camera.activeSelf);
        //}
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bunny") || collision.gameObject.CompareTag("Dog") ||
            collision.gameObject.CompareTag("Bat") || collision.gameObject.CompareTag("Dino") ||
            collision.gameObject.CompareTag("Bear") || collision.gameObject.CompareTag("Opossum") ||
            collision.gameObject.CompareTag("Pig") || collision.gameObject.CompareTag("Vulture"))
        {
            collision.gameObject.GetComponent<ObjectCollision>().hit = true;
        }
    }

    public void SpeedUp() 
    {
        scrollspeed*=1.5f;
    }

    public void BearSpeedUp() //スピードダウンを必ず入れるなら、引数で秒数指定して呼び出す
    {
        this.gameObject.transform.position += scrollspeed;
    }

    public void BearSpeedDown()
    {
        this.gameObject.transform.position -= scrollspeed;
    }

    public void BearStay(float time) //指定された秒数待つ
    {
        StartCoroutine(BearStayTime(time));
    }

    public void BearRoat()
    {
        bearanim.SetTrigger(roat);
        BearStay(2f); //後隙　敵が集まってくるまでの時間を指定
    }

    public void BearBreak()
    {
        bearanim.SetTrigger(bearbreak);
        //エフェクトや判定など
    }

    private IEnumerator BearStayTime(float time)
    {
        bearanim.SetBool(stay, true);
        staynow = true;
        yield return new WaitForSeconds(time);
        staynow = false;
        bearanim.SetBool(stay, false);
    }

    public void DiagonalUp(float time) 
    {
        if (upint == 0)
        {
            transform.DOMoveX(671.3f, time).SetEase(Ease.Linear);
            transform.DOMoveY(-9, time).SetEase(Ease.Linear);
        }
        if (upint == 1)
        {
            transform.DOMoveX(625.3f, time).SetEase(Ease.Linear);
            transform.DOMoveY(-5, time).SetEase(Ease.Linear);
        }
        if (upint == 2)
        {
            transform.DOMoveX(597.7f, time).SetEase(Ease.Linear);
            transform.DOMoveY(-1, time).SetEase(Ease.Linear);
        }
        if (upint == 3)
        {
            transform.DOMoveX(267, time).SetEase(Ease.Linear);
            transform.DOMoveY(-1, time).SetEase(Ease.Linear);
        }

        upint += 1;
    }

    public void DiagonalDown(float time)
    {
        if (downint == 0)
        {
            transform.DOMoveX(357, time).SetEase(Ease.Linear);
            transform.DOMoveY(-9, time).SetEase(Ease.Linear);
        }

        downint += 1;
    }

    private IEnumerator Diagonal(float time,bool b)//bool値は上がるか下がるか trueで上がる
    {
        if (b)
        {
            scrollspeed.y = 0.01f; //どれくらい上がるか(下がるか)
        }
        else 
        {
            scrollspeed.y = -0.01f;
        }
        yield return new WaitForSeconds(time);
        scrollspeed.y = 0;
    }

    //やられた時の処理
}