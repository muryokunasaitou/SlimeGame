using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollJump : MonoBehaviour
{
    [SerializeField] PlayerController pc;
    private bool isTauch;
    [Header("�ǂ̕����ɔ�Ԃ�")]
    public bool up;
    public bool right;
    public bool left;
    public bool down;
    [Header("�W�����v�̋���")]
    public float jumppower=50;
    private float scale_x;
    void Start()
    {
        scale_x = gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Player") && isTauch == false )
        {
            isTauch = true;
            StartCoroutine(ScrollMove());
            Invoke("IsTauch",1); //1�b�ԃW�����v���A���ŋN���Ȃ�����
            PlayerPush();   
        }
    }
    void PlayerPush()
    {
        if(up == true)
        {
            pc.rb.AddForce(Vector2.up * jumppower, ForceMode2D.Impulse);
        }
        if(right)
        {
            pc.rb.AddForce(Vector2.right * jumppower,ForceMode2D.Impulse);
        }
        if (left)
        {
            pc.rb.AddForce(Vector2.left * jumppower, ForceMode2D.Impulse);
        }
        if (down == true)
        {
            pc.rb.AddForce(Vector2.down * jumppower, ForceMode2D.Impulse);
        }
    }
    void IsTauch()
    {
        isTauch = false;
    }
    IEnumerator ScrollMove() //�X�N���[���̂΂˂̓���
    {
        pc.enabled = false;�@�@�@�@�@�@�@//Player�̊������[���ɂ��āA�����̊ԓ������Ȃ�
        pc.rb.velocity = Vector2.zero;
        transform.localScale = new Vector3(scale_x, 0.8f, 1);
        yield return new WaitForSeconds(0.05f);
        transform.localScale = new Vector3(scale_x, 0.6f, 1);
        yield return new WaitForSeconds(0.05f);
        transform.localScale = new Vector3(scale_x, 0.3f, 1);
        yield return new WaitForSeconds(0.1f);
        pc.enabled = true;
        transform.localScale = new Vector3(scale_x, 0.8f, 1);
        yield return new WaitForSeconds(0.05f);
        transform.localScale = new Vector3(scale_x, 1, 1);
    }
}
