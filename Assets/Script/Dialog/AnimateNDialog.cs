using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimateNDialog : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private int layer;
    [SerializeField] NDTDEvent ndtdEvent;

    // IsOpenフラグ（アニメーターコントローラー内で定義したフラグ）
    private static readonly int ParamIsOpen = Animator.StringToHash("IsOpen");
    public bool IsOpen => gameObject.activeSelf;// ダイアログは開いているかどうか
    public bool IsTransition = false;// アニメーション中かどうか
    public bool n_SentenceTrigger = false;//文章送りに使うbool値

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    private void Update()
    {
        //ダイアログが開いていて左クリックをしたら文章送りのboolをtrueにする
        if (Input.GetMouseButtonDown(0) && gameObject.activeSelf == true)
        {
            n_SentenceTrigger = true;
        }
        //文章が表示されていて、表示フラグがfalseになったら閉じる
        if (n_SentenceTrigger == true && ndtdEvent.isNDEvent == false)
        {
            DialogNarratorClose();
            n_SentenceTrigger = false;
        }
    }

    //private void DialogNarratorOpen()
    public void DialogNarratorOpen()
    {       
        if (IsOpen || IsTransition) return; // 不正操作防止
        gameObject.SetActive(true); // DialogNarratorをアクティブにする
        m_Animator.SetBool(ParamIsOpen, true); // IsOpenフラグをtrueにセット
        // アニメーション待機
        StartCoroutine(WaitAnimation("Shown"));
        Time.timeScale = 0;
    }

    public void DialogNarratorClose()
    {
        if (!IsOpen || IsTransition) return;
        m_Animator.SetBool(ParamIsOpen, false); // IsOpenフラグをfalseにセット
        // アニメーション待機し、終わったらパネル自体を非アクティブにする
        StartCoroutine(WaitAnimation("Hidden", () => gameObject.SetActive(false)));
        Time.timeScale = 1;
    }

    private IEnumerator WaitAnimation(string stateName, UnityAction onCompleted = null)
    {
        //このブール値がtrueの間は上2つの関数が動かない
        IsTransition = true;
        yield return new WaitUntil(() =>
        {
            //ステートが変化し、アニメーションが終了するまで待機
            var state = m_Animator.GetCurrentAnimatorStateInfo(layer);
            return state.IsName(stateName) && state.normalizedTime >= 1;
        });
        IsTransition = false;
        //恐らくラムダ式のnullチェック
        onCompleted?.Invoke();
    }
}