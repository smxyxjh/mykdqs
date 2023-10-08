using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : Enemy
{
    // Start is called before the first frame update
    //bool isFacingRight;//判断朝向
    //bool isDead;//判断存活状态
    private Rigidbody2D rigi;
    public float movementSpeed;
    bool isGrounded;
    public GameObject groundCheck;//地面对象的当前世界位置。OverlapCircle方法需要知道从哪个位置开始检查圆形与地面相交
    public int circleRadius;//检测半径
    public LayerMask ground;//检查是否与圆形相交的地面对象的引用

    public Collider2D fecingDetector;//表示要检查的碰撞器
    public ContactFilter2D contact;//表示与之相交的碰撞器

    public float hurtForce=10.0f;//受到伤害的力
    public float DeadForce;
    bool forceMovement=true;//判断是否受伤受伤不能移动
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
    private void Movement()//移动
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
    private void FacingDetect()//面向检测,改变朝向
    {
        if (isDead)
        {
            return;
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position,circleRadius,ground);//利用Physics2D类中的OverlapCircle方法
                                                                                                 //来检查一个圆形是否与一个给定的地面对象相交。",
        if (!isGrounded)
        {
            Flip();
        }
        else
        {
            List<Collider2D> hitColliders = new List<Collider2D>();
            int count = Physics2D.OverlapCollider(fecingDetector,contact, hitColliders);//利用Physics2D类中的OverlapCollider方法来检查一个碰撞器是否与另一个碰撞器相交，
                                                                                                 //并返回与之相交的碰撞器的数量。

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
                    // 特定物体上的碰撞器存在于碰撞中的碰撞器中  
                    // 执行相关操作  
                }
                else
                {
                    // 特定物体上的碰撞器不存在于碰撞中的碰撞器中  
                    // 执行其他操作  
                    Flip();
                }
                
            }
        }
    }
    private void Flip()//怪物翻转
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
    IEnumerator DelayHurt(Vector2 vector)//受到伤害反馈方法
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
