using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : Enemy
{
    // Start is called before the first frame update
    //bool isFacingRight;//�жϳ���
    //bool isDead;//�жϴ��״̬
    private Rigidbody2D rigi;
    public float movementSpeed;
    bool isGrounded;
    public GameObject groundCheck;//�������ĵ�ǰ����λ�á�OverlapCircle������Ҫ֪�����ĸ�λ�ÿ�ʼ���Բ��������ཻ
    public int circleRadius;//���뾶
    public LayerMask ground;//����Ƿ���Բ���ཻ�ĵ�����������

    public Collider2D fecingDetector;//��ʾҪ������ײ��
    public ContactFilter2D contact;//��ʾ��֮�ཻ����ײ��

    public float hurtForce=10.0f;//�ܵ��˺�����
    public float DeadForce;
    bool forceMovement=true;//�ж��Ƿ��������˲����ƶ�
    private Animator ani;
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsDead();
        Direction();
        Movement();
        FacingDetect();
      
            }
/*    private void Direction()
    {
        if (transform.localScale.x == 1)
        {
            isFacingRight = true;
        }
        else if (transform.localScale.x == -1)
        {
            isFacingRight = false;
        }
    }*/
    private void Movement()//�ƶ�
    {
        if (!isDead && forceMovement)
        {
            if (isFacingRight)
            {
                rigi.velocity = Vector2.right*movementSpeed;
            }
            else
            {
                rigi.velocity = Vector2.left*movementSpeed;
            }
        }
    }
    private void FacingDetect()//������,�ı䳯��
    {
        if (isDead)
        {
            return;
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position,circleRadius,ground);//����Physics2D���е�OverlapCircle����
                                                                                                 //�����һ��Բ���Ƿ���һ�������ĵ�������ཻ��",
        if (!isGrounded)
        {
            Flip();
        }
        else
        {
            List<Collider2D> hitColliders = new List<Collider2D>();
            int count = Physics2D.OverlapCollider(fecingDetector,contact, hitColliders);//����Physics2D���е�OverlapCollider���������һ����ײ���Ƿ�����һ����ײ���ཻ��
                                                                                                 //��������֮�ཻ����ײ����������

            if (count > 0 )
            {
                bool hasSpecificCollider = false;
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.gameObject.tag == "Player")
                    {
                        hasSpecificCollider = true;
                        break;
                    }
                }
                if (hasSpecificCollider)
                {
                    // �ض������ϵ���ײ����������ײ�е���ײ����  
                    // ִ����ز���  
                }
                else
                {
                    // �ض������ϵ���ײ������������ײ�е���ײ����  
                    // ִ����������  
                    Flip();
                }
                
            }
        }
    }
    private void Flip()//���﷭ת
    {
        Vector3 vector = transform.localScale;
        vector.x = -1 * vector.x;
        transform.localScale = vector;
    }

    public override void Hurt(int damage, Transform attackPosition)
    {
        base.Hurt(damage);
        Vector2 vector = transform.position - attackPosition.position;
        StartCoroutine(DelayHurt(vector));
    }
    IEnumerator DelayHurt(Vector2 vector)//�ܵ��˺���������
    {
        rigi.velocity = Vector2.zero;
        forceMovement = false;
        if (vector.x > 0)
        {
            rigi.AddForce(new Vector2(hurtForce, 0),ForceMode2D.Impulse);
        }
        else
        {
            rigi.AddForce(new Vector2(-hurtForce, 0), ForceMode2D.Impulse);

        }
        yield return new WaitForSeconds(0.3f);
        forceMovement = true;
    }
    protected override void Dead()
    {
        base.Dead();
        StartCoroutine(DelayDead());
    }
    IEnumerator DelayDead()
    {
        Vector3 diff =(GameObject.FindWithTag("Player").transform.position - transform.position).normalized;
        rigi.velocity = Vector2.zero;
        if (diff.x < 0)
        {
            rigi.AddForce(Vector2.right*DeadForce);
        }
        else
        {
            rigi.AddForce(Vector2.left * DeadForce);
        }
        if (ani != null)
        {
            ani.SetBool("Dead", true);
        }
        yield return new WaitForSeconds(0.2f);
        GetComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Static;
        GetComponent<BoxCollider2D>().enabled = false;

    }
}
