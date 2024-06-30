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
    public Vector3 scrollspeed= new Vector3(0.7f, 0, 0); //�X�N���[���X�s�[�h
    public Vector3 upspeed; //�X�s�[�h�A�b�v���̑��x
    private bool a;
    private Animator bearanim;
    private int upint=0;
    private int downint=0;

    private int roat = Animator.StringToHash("Roat");
    private int stay = Animator.StringToHash("Stay");
    private int bearbreak = Animator.StringToHash("Break");

    [SerializeField] private float[] eventcoordinate; //�C�x���g�̍��W
    private int i = 0;
    [SerializeField] private UnityEvent[] events; //�C�x���g�̍��W�ɓ��B���������̃C�x���g���N������

    private bool staynow = true;

    private void Start()
    {
        bearanim = GetComponent<Animator>();
    }

    private void Update() 
    {
        if (!staynow) 
        {
            parent.transform.position -= scrollspeed; //���݂͐e�I�u�W�F�N�g�̍��W����(�F���̂��̂��ƃ��[���h���W�ɕϊ����鏈�������肻���Ȃ���)

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

    public void BearSpeedUp() //�X�s�[�h�_�E����K�������Ȃ�A�����ŕb���w�肵�ČĂяo��
    {
        this.gameObject.transform.position += scrollspeed;
    }

    public void BearSpeedDown()
    {
        this.gameObject.transform.position -= scrollspeed;
    }

    public void BearStay(float time) //�w�肳�ꂽ�b���҂�
    {
        StartCoroutine(BearStayTime(time));
    }

    public void BearRoat()
    {
        bearanim.SetTrigger(roat);
        BearStay(2f); //�㌄�@�G���W�܂��Ă���܂ł̎��Ԃ��w��
    }

    public void BearBreak()
    {
        bearanim.SetTrigger(bearbreak);
        //�G�t�F�N�g�┻��Ȃ�
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

    private IEnumerator Diagonal(float time,bool b)//bool�l�͏オ�邩�����邩 true�ŏオ��
    {
        if (b)
        {
            scrollspeed.y = 0.01f; //�ǂꂭ�炢�オ�邩(�����邩)
        }
        else 
        {
            scrollspeed.y = -0.01f;
        }
        yield return new WaitForSeconds(time);
        scrollspeed.y = 0;
    }

    //���ꂽ���̏���
}