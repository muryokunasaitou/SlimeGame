using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimateTDialog : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private int layer;
    [SerializeField] NDTDEvent ndtdEvent;

    // IsOpen�t���O�i�A�j���[�^�[�R���g���[���[���Œ�`�����t���O�j
    private static readonly int ParamIsOpen = Animator.StringToHash("TDIsOpen");
    public bool t_IsOpen => gameObject.activeSelf;// �_�C�A���O�͊J���Ă��邩�ǂ���
    public bool t_IsTransition = false;// �A�j���[�V���������ǂ���
    public bool t_SentenceTrigger = false;//���͑���Ɏg��bool�l

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    private void Update()
    {

        //�_�C�A���O���J���Ă��č��N���b�N�������當�͑����bool��true�ɂ���
        if (Input.GetMouseButtonDown(0) && gameObject.activeSelf == true)
        {
            t_SentenceTrigger = true;
        }

        //���͂��\������Ă��āA�\���t���O��false�ɂȂ��������
        if (t_SentenceTrigger == true && ndtdEvent.isTDEvent == false)
        {
            TDialogClose();
            t_SentenceTrigger = false;
        }
    }

    //private void DialogNarratorOpen()
    public void TDialogOpen()

    {
        if (t_IsOpen || t_IsTransition) return; // �s������h�~
        gameObject.SetActive(true); // DialogNarrator���A�N�e�B�u�ɂ���
        m_Animator.SetBool(ParamIsOpen, true); // IsOpen�t���O��true�ɃZ�b�g
        // �A�j���[�V�����ҋ@
        StartCoroutine(WaitAnimation("Shown"));
        Time.timeScale = 0;
    }

    private void TDialogClose()
    {

        if (!t_IsOpen || t_IsTransition) return;
        m_Animator.SetBool(ParamIsOpen, false); // IsOpen�t���O��false�ɃZ�b�g
        // �A�j���[�V�����ҋ@���A�I�������p�l�����̂��A�N�e�B�u�ɂ���
        StartCoroutine(WaitAnimation("Hidden", () => gameObject.SetActive(false)));
        Time.timeScale = 1;
    }

    private IEnumerator WaitAnimation(string stateName, UnityAction onCompleted = null)
    {
        //���̃u�[���l��true�̊Ԃ͏�2�̊֐��������Ȃ�
        t_IsTransition = true;

        yield return new WaitUntil(() =>
        {
            //�X�e�[�g���ω����A�A�j���[�V�������I������܂őҋ@
            var state = m_Animator.GetCurrentAnimatorStateInfo(layer);
            return state.IsName(stateName) && state.normalizedTime >= 1;
        });

        t_IsTransition = false;
        onCompleted?.Invoke();
    }
}

