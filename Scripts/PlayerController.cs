using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("移动参数")]
    public float moveX;
    public float moveY;
    Vector3 flippedScale = new Vector3(-1, 1, 1);
    public float moveSpeed = 10.0f;
    public float jumpForce = 120f;
    public float jumpTimer = 1.0f;
    [SerializeField] public GameManage gameManage;
    [Header("应用的组件")]
    [SerializeField]private CinemaShaking cinemaShaking;//屏幕震动组件
    [SerializeField] private Rigidbody2D rigi;
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterAudio characterAudio;
    [SerializeField] private Attack attack;
    [SerializeField] private CharacterEffect characterEffect;
    [Header("状态判断")]
    [SerializeField] bool isFacingRight;//判断方向的变量
    [SerializeField] int moveChangeAni;//判断是否移动的变量
    [SerializeField] bool isOnGround;//判断是否掉落
    [SerializeField] bool isFistland = true;

    [Header("攻击参数")]
    [SerializeField] public float downRecolForce = 60.0f;//攻击下面物体的反作用力
    [SerializeField] public float RecolForce;//攻击的反作用力
    [SerializeField] public float hurtForce = 1f;//受伤的力
    [SerializeField] float lastSlashTime;//最后攻击的时间间隔
    [SerializeField] float slashIntervalTime=0.2f;//连击的时间间隔
    [SerializeField] int slashCount;//判断是否能连击
    [SerializeField] float maxComboTime = 0.4f;
    [SerializeField] private Vector2 m_Gravity = new Vector2(0, 5f);

    [SerializeField] int slashDamage=1;
    [SerializeField] bool canMove;
    private void Awake()
    {
        isFistland = true;
    }
    private void FixedUpdate()//有物理动作的update方法是时间更新
    {
        Jump();
     

    }
    void Start()
    {
        characterEffect = FindObjectOfType<CharacterEffect>();
        characterAudio = FindObjectOfType<CharacterAudio>();
        rigi = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cinemaShaking =FindObjectOfType<CinemaShaking>();
        GetComponent<Rigidbody2D>().gravityScale = m_Gravity.y;
        attack = FindObjectOfType<Attack>();
        canMove = true;
        FindObjectOfType<SoulOrb>().HideOrd();
        FindObjectOfType<SoulOrb>().HideHealthItem();
        gameManage = FindObjectOfType<GameManage>();
    }

    // Update is called once per frame
    void Update()
    {
        ResetComboTime();
        PlayerAttack();
        Movement();
        Direction();
        if (isFistland)
        {
            FindObjectOfType<SoulOrb>().DelayShowOrb(1.1f);
            //FindObjectOfType<SoulOrb>().ShowHealthItem();
            isFistland = false;

        }
        /*        if (Input.GetKeyDown(KeyCode.X))
                {
                    TakeDamage();
                }*/
    }
   private void Movement()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        if (canMove && gameManage.IsEnableInput())
        {
      rigi.velocity = new Vector2(moveX*moveSpeed,rigi.velocity.y);//移动
        }
  

        if (moveX > 0)
        {
            moveChangeAni = 1;
        }else if (moveX < 0)
        {
            moveChangeAni = 1;
        }
        else
        {
            moveChangeAni = 0;
        }

        animator.SetInteger("movement",moveChangeAni);
    }
    private void Direction()//调整朝向
    {
        if (moveX > 0)
        {
            isFacingRight = true;
            transform.localScale = flippedScale;
        }else if (moveX < 0)
        {
            isFacingRight = false;
            transform.localScale = Vector3.one;
        }
    }
    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && gameManage.IsEnableInput())
        {
            jumpTimer -= Time.deltaTime;
            if (jumpTimer > 0)
            {
               rigi.AddForce(new Vector2(0,jumpForce),ForceMode2D.Force);
         
                animator.SetTrigger("jump");
                characterAudio.Play(CharacterAudio.AudioType.jump,true);
                characterEffect.DoEffect(CharacterEffect.EffectType.DustJump, true);
            }

        }
    }
    private void OnCollisionEnterHideHealthItem2D(Collision2D collision)//首次碰撞返回
    {
        Grouding(collision,false);
        
    }
    private void OnCollisionStay2D(Collision2D collision)//持续碰撞返回
    {
        Grouding(collision,false);
        if (!isOnGround)
        {
            gameManage.SetEnableInput(false);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)//碰撞结束离开时返回
    {
        Grouding(collision,true);
        gameManage.SetEnableInput(true);

    }
    private void Grouding(Collision2D col,bool exitState)
    {
        if (exitState)//离开为真
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Terrain"))
            {
                isOnGround = false;
            }
        }
        else
        {
            if(col.gameObject.layer == LayerMask.NameToLayer("Terrain") && !isOnGround && col.contacts[0].normal==Vector2.up)//从上往下
            {
                JumpCancle();
                characterEffect.DoEffect(CharacterEffect.EffectType.Falltrail, true);
                characterEffect.DoEffect(CharacterEffect.EffectType.DustJump, false);
                isOnGround = true;  
                
            }
            else if (col.gameObject.layer == LayerMask.NameToLayer("Terrain") && !isOnGround && col.contacts[0].normal == Vector2.down)
            {
                JumpCancle();
            }
        }
        animator.SetBool("isOnGround", isOnGround);
    }
    private void JumpCancle()
    {
        animator.ResetTrigger("jump");
        jumpTimer = 0.25f;
    }
    public void TakeDamage()
    {
        cinemaShaking.CinemaShake();//调用震动
        StartCoroutine(FindObjectOfType<Invincibility>().SetInvincibility());
        FindObjectOfType<Health>().Hurt();
        if (isFacingRight)//如果朝向为右
        {
            rigi.velocity = new Vector2(1, 1) * hurtForce;
        }
        else
        {
            rigi.velocity = new Vector2(-1, 1) * hurtForce;
        }
        animator.Play("TakeDamage");
        characterAudio.Play(CharacterAudio.AudioType.takedamage, true);
        PlayHitParticals();
    }
    public void PlayHitParticals()
    {
        characterEffect.DoEffect(CharacterEffect.EffectType.HitR, true);
        characterEffect.DoEffect(CharacterEffect.EffectType.HitL, true);
    }
    private void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.J) && gameManage.IsEnableInput())//人物活着
        {
            if (Time.time >= lastSlashTime + slashIntervalTime)
            {
                lastSlashTime = Time.time;

                if (moveY > 0)
                {
                    SlashAndDetect(Attack.AttackType.UpSlash);
                    //向上攻击
                    animator.Play("UpSlash");
                }else if(!isOnGround && moveY < 0)
                {
                    SlashAndDetect(Attack.AttackType.DownSlash);
                    animator.Play("DownSlash");
                    rigi.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
                }
                else
                {
                    slashCount++;
                    switch (slashCount)
                    {
                        case 1:
                            SlashAndDetect(Attack.AttackType.Slash);
                            animator.Play("Slash");
                            break;
                        case 2:
                            SlashAndDetect(Attack.AttackType.AltSlash);
                            animator.Play("AltSlash");
                            slashCount = 0;
                            break;
                    }
                }
                

            }
        }
    }
    private void ResetComboTime()//清除连击
    {
        if(Time.time>=lastSlashTime+maxComboTime && slashCount != 0)
        {
            slashCount = 0;
        }
    }
    private void SlashAndDetect(Attack.AttackType attackType)
    {
        List<Collider2D> colliders = new List<Collider2D>();
        attack.Play(attackType, ref colliders);
        bool hasEnemy=false;
        bool hasDamagePlayer=false;

        foreach(Collider2D col in colliders)//判断攻击的对象
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("EnemyDetector"))
            {
                hasEnemy = true;
                Crawler rigi1 = col.GetComponent<Crawler>();
                break;
            }
        }
        foreach(Collider2D col in colliders)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("DamagePlayer"))
            {
                hasDamagePlayer = true;
        
                break;
            }
        }
        if (hasEnemy)
        {

            if (attackType == Attack.AttackType.DownSlash)
            {
                
                AddDownRecoiForce();
               
            }
            else
            {
                StartCoroutine(AddRecoiForce());
            }
        }
        if (hasDamagePlayer)
        {
            if (attackType == Attack.AttackType.DownSlash)
            {

                AddDownRecoiForce();
            }
        }
        foreach(Collider2D col in colliders)
        {
            Breakable breakable = col.GetComponent<Breakable>();
            if (breakable!=null){
                breakable.Hurt(slashDamage,transform);
            }
        }
    }
    private void AddDownRecoiForce()//攻击到下面物体给角色一个反冲击力
    {
        rigi.velocity.Set(rigi.velocity.x, 0);
        rigi.AddForce(Vector2.up * downRecolForce, ForceMode2D.Impulse);// ForceMode2D.Impulse一个突然的力

    }
    IEnumerator AddRecoiForce()
    {
        canMove = false;
        if (isFacingRight)
        {
            rigi.AddForce(Vector2.left * RecolForce,ForceMode2D.Impulse);
        }
        else
        {
            rigi.AddForce(Vector2.right * RecolForce,ForceMode2D.Impulse); 
        }

        yield return new WaitForSeconds(0.2f);
        canMove = true;
    }
}
