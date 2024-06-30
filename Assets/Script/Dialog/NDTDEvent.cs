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
    ////���X�g��`
    //�X�e�[�W���ނ��ƂɃ��X�g�𕪂���
    private List<string> ndList_s1 = new List<string>();
    private List<string> tdList_s1 = new List<string>();
    string title = "SLIME RPG �ւ悤����,,���Ȃ��̓X���C���Ƃ��Ė`�����Ă��炢�܂�,,�����̉Ƃ̃h�A�ɍs���Ɩ`�����n�܂�܂�,,A�L�[�AD�L�[�ō��E�Ɉړ�\nF�L�[�������Ȃ���ړ��ő��������܂�,,Space�L�[�ŃW�����v���邱�Ƃ��o���܂�,,�ł́A�悢�`�����I";
    string stage1 = "�����̓꒣��ɏZ�ރX���C���͕��a�ɕ�炵�Ă��܂���,,�������A���邱�Ƃœ꒣������߂�l�V���Ƀ��J���Ă��܂��܂���,,�X���C���͎l�V����|�����ɏo�܂�";
    string skill;
    string guooo;
    string bibiru;
    
    [SerializeField] private float[] eventcoordinate_nd; //�C�x���g�̍��Wnd
    [SerializeField] private float[] eventcoordinate_td; //�C�x���g�̍��Wtd
    private int i_nd = 0;
    private int i_td = 0;
    [SerializeField] private UnityEvent[] events_nd; //�C�x���g�̍��W�ɓ��B���������̃C�x���g���N������
    [SerializeField] private UnityEvent[] events_td;

    void Start()
    {
        camera = cameraob.gameObject.GetComponent<Camera>();
        pc = GetComponent<PlayerController>();

        if(SceneManager.GetActiveScene().name == "Stage1_HeigenScene") //�V�[�������Ƃɕ�����[
        {
            //��b���ǉ��B�u,,�v�ŕ��͂̋�؂�B�^�O��Unity�̕��ƍ��킹�āANDEvent��t����

            ndList_s1.Add("�����A����͌��̖����ł�,,�߂Â��Ɠˌ����Ă��邩�璍�ӂ��āI,,���h���ȏォ�畢�����Ԃ���Ζ�����H�ׂ邱�Ƃ��ł����");
            ndList_s1.Add("�C�x���g�^�OB�̕��͂ł�");
            ndList_s1.Add("�C�x���g�^�OC�̕��͂ł�");
            ndList_s1.Add("�C�x���g�^�OD�̕��͂ł�");

            skill = "������ˁI\n���������ݍ��߂��ˁI,,�X���C���͈��ݍ��񂾓G�̏�����荞��ŃX�L���Ƃ��Ďg����悤�ɂȂ��,,�X�L���͉�ʂ̉��ɕ\��������,,�g�������X�L���ɃJ�[�\�������킹�ăN���b�N���Ă�";
            //Stage1����N�����̕\�����́A���s��Stage1�X�N���v�g

            //�J�������h���b��
            c_seconds = camShake.shakeDuration;
            //��b���ǉ��B�u,,�v�Ŗ��O�ƕ��͂̋�؂�B�ŏ��͖��O�BUnity�̕��̃^�O�ƍ��킹��
            //�A���ŉ�b�C�x���g������ꍇ�͋�؂蕶���ŗ]����part.Length�𑝂₵�Ă�������
            tdList_s1.Add("���C�o��,,�悤�I,,���O�����Ă邩�H,,�l�V������������x�z����1�N�A���V��ɂȂ��Ă₪��,,��������������ɂł��Ȃ����,,�ȂɁH���O���|�������āH,,�΂������A�����|���񂾂�I,,���O��艴�̕K�E�Z�̂��c,, ");
            tdList_s1.Add("�l�V���N�},, �N�}�[���I");
            tdList_s1.Add("�C�x���g�^�OD�̕��͂ł�");

            // �\�����@���ς���Ă��镶��
            guooo = "???,,�O�D�I�I�I�I�I�I�H�H�H�H�H�H�I�I�I�I�I,, ";
            bibiru = "���C�o��,,�q�F�b�c,,.........,,..................,,�ӂ�A�ǂ����l�V����1�l���������̂悤����,,��������������Ă���̂̓N�}�̂����,,���������A�����ߗ��Ƃ��ĐH�������ꑱ�����ɂ͂����˂�!,,���O�A������Ɨl�q���ė�����,,���̐�ɃN�}�̍��邪���邼,,���H���͉������������ɓ�����悤�ɉA���猩�Ă邩���";
        }
    }

    void Update()
    {
        if (isPlayerToLand && isStopPlayer == false)
        {
            StartTDEvent2();
        }
        //�C�x���g�̏���
        if (NDTDparent.transform.position.x > eventcoordinate_nd[i_nd])
        {
            events_nd[i_nd].Invoke();
            
        }
        if (NDTDparent.transform.position.x > eventcoordinate_td[i_td])
        {
            events_td[i_td].Invoke();
            
        }
        //�i���[�^�[�_�C�A���O���J���Ă���ԁA���N���b�N�������ƁA���͑���
        if (animateNDialog.IsOpen && animateNDialog.n_SentenceTrigger==true && parts != null)
        {
            if (i < parts.Length - 1)
            {
                i++;
                ndEventText.text = parts[i];
                animateNDialog.n_SentenceTrigger = false;�@//�N���b�N�������Ȃ��Ă����͑��肳���̂�h��
            }

            //�Ō�̕��͂ɂȂ��āA���N���b�N�������ƃt���O�I�t�AText�I�u�W�F�N�g���A�N�e�B�u�ɂ���
            if (i >= parts.Length - 1)
            {
                Array.Clear(parts, 0, parts.Length);
                isNDEvent = false;
                i = 0;
            }
        }
        //�g�[�N�_�C�A���O���J���Ă���ԁA���N���b�N�������ƁA���͑���
        if (animateTDialog.t_IsOpen && animateTDialog.t_SentenceTrigger == true && parts != null && isTalkStop == false)
        {
            if (i < parts.Length - 1)
            {
                i++;
                tdEventText.text = parts[i];
                animateTDialog.t_SentenceTrigger = false;�@//�N���b�N�������Ȃ��Ă����͑��肳���̂�h��
            }
            //�������݃C�x���g
            if (i_td == 1 && tdEventText.text == " ")
            {
                TalkChange("guooo");
            }
            //�Ō�̕��͂ɂȂ��āA���N���b�N�������ƃt���O�I�t�AText�I�u�W�F�N�g���A�N�e�B�u�ɂ���
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
        // ���X�g�����b���e���擾
        if (ndList_s1[i_nd] != null)
        {
            string text = ndList_s1[i_nd];
            DisplayNDEvent(text);
            i_nd++;
            animateNDialog.DialogNarratorOpen();
        }
        else
        {
            //�G���[�n���h�����O�̓��e�������ɋL�q
            Debug.Log("list�̕��͂��Ȃ�");
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
        string delimiter = ",,"; //��؂蕶��
        //��؂蕶���ŋ�؂����p�[�g��z��ɓ���鏈��
        parts = text.Split(new[] { delimiter }, StringSplitOptions.None);
        //�ŏ��̕��͓���
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
        // ���X�g�����b���e���擾
        if (tdList_s1[i_td] != null)
        {
            string text = tdList_s1[i_td];
            // ��b���e��\��
            DisplayTDEvent(text);
            i_td++;
            animateTDialog.TDialogOpen();
        }
        else
        {
            //�G���[�n���h�����O�̓��e�������ɋL�q
            Debug.Log("�g�[�N�_�C�A���O�̃^�O�����͂̎�������肭�Q�Ƃ���Ă��Ȃ����������͂�\�������Ȃ�������������");
        }
    }
    void DisplayTDEvent(string text)
    {
        string delimiter = ",,"; //��؂蕶��
        //��؂蕶���ŋ�؂����p�[�g��z��ɓ���鏈��
        parts = text.Split(new[] { delimiter }, StringSplitOptions.None);
        //���O����
        nameText.text = parts[0];
        //�ŏ��̕��͓���
        tdEventText.text = parts[1];
    }

    //�b�̍Œ��ɑ��̐l�����b�����邽�߂̂��́i���F�c�A����F�c�@���j
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
            camera.orthographicSize = camera.orthographicSize + 1; //�J�����g��
        //}
        
        StartTDEvent();
    }
}
