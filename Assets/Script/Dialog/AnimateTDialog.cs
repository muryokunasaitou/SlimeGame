using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimateTDialog : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private int layer;
    [SerializeField] NDTDEvent ndtdEvent;

    // IsOpenフラグ（アニメーターコントローラー内で定義したフラグ）
    private static readonly int ParamIsOpen = Animator.StringToHash("TDIsOpen");
    public bool t_IsOpen => gameObject.activeSelf;// ダイアログは開いているかどうか
    public bool t_IsTransition = false;// アニメーション中かどうか
    public bool t_SentenceTrigger = false;//文章送りに使うbool値

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
            t_SentenceTrigger = true;
        }

        //文章が表示されていて、表示フラグがfalseになったら閉じる
        if (t_SentenceTrigger == true && ndtdEvent.isTDEvent == false)
        {
            TDialogClose();
            t_SentenceTrigger = false;
        }
    }

    //private void DialogNarratorOpen()
    public void TDialogOpen()

    {
        if (t_IsOpen || t_IsTransition) return; // 不正操作防止
        gameObject.SetActive(true); // DialogNarratorをアクティブにする
        m_Animator.SetBool(ParamIsOpen, true); // IsOpenフラグをtrueにセット
        // アニメーション待機
        StartCoroutine(WaitAnimation("Shown"));
        Time.timeScale = 0;
    }

    private void TDialogClose()
    {

        if (!t_IsOpen || t_IsTransition) return;
        m_Animator.SetBool(ParamIsOpen, false); // IsOpenフラグをfalseにセット
        // アニメーション待機し、終わったらパネル自体を非アクティブにする
        StartCoroutine(WaitAnimation("Hidden", () => gameObject.SetActive(false)));
        Time.timeScale = 1;
    }

    private IEnumerator WaitAnimation(string stateName, UnityAction onCompleted = null)
    {
        //このブール値がtrueの間は上2つの関数が動かない
        t_IsTransition = true;

        yield return new WaitUntil(() =>
        {
            //ステートが変化し、アニメーションが終了するまで待機
            var state = m_Animator.GetCurrentAnimatorStateInfo(layer);
            return state.IsName(stateName) && state.normalizedTime >= 1;
        });

        t_IsTransition = false;
        onCompleted?.Invoke();
    }
}

