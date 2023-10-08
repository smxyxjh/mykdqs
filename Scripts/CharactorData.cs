using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorData : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private int health;
    [SerializeField] private bool isLeak;
    [SerializeField] private bool isDead;

    private GameManage gameManager;
    private Animator animator;
    private CharacterEffect characterEffect;
    void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManage>();
        characterEffect = FindObjectOfType<CharacterEffect>();
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsDead();
        CheckLeakHealth();
    }
    private void CheckLeakHealth()//判断受伤状态
    {
        if(health==1 && !isLeak)
        {
            isLeak = true;
            characterEffect.DoEffect(CharacterEffect.EffectType.Lowfheakth, true);
        }
        else
        {
            isLeak = false;
            characterEffect.DoEffect(CharacterEffect.EffectType.Lowfheakth, false);
        }
    }
    private void CheckIsDead()//判断是否死亡
    {
        if (health <= 0&&!isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("HeroDetector"),LayerMask.NameToLayer("EnemyDetector"),true);
        isDead = true;
        animator.SetTrigger("Dead");
        gameManager.SetEnableInput(false);
    }
    public int GetCurrentHealt()//获取当前生命值
    {
        return health;
    }
    public void LoseHealth(int health)
    {
        this.health -= health;
    }
    public bool GetDeadStatement()
    {
        CheckIsDead();
        return isDead;
    }
}
