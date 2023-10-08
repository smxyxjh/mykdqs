using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour//可被摧毁的问题的父类
{
    [SerializeField]protected int health;
    protected bool isDead;

    protected virtual void CheckIsDead()
    {
        if (health <= 0&&!isDead)
        {
            Dead();
        }
    }
    public virtual void Hurt(int damage,Transform attackPosition)
    {
        if (!isDead)
        {
            health -= damage;
        }
    }
    public virtual void Hurt(int damage)
    {
        if (!isDead)
        {
            health -= damage;
        }
    }
    protected virtual void Dead()
    {
        isDead = true;
 
    }

}
