using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Breakable
{
    protected bool isFacingRight;
    int randomCoint;
    protected int minSpawnCoins = 3;//掉落金币范围
    protected int maxSpawnCoins = 5;
    [SerializeField]protected float maxBumpXForce = 100;//金币掉诺受到的力
    [SerializeField]protected float minBumpYForce = 300;
    [SerializeField]protected float maxBumpYForce = 500;
    [SerializeField]GameObject coin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected void Direction()
    {
        if (transform.localScale.x == 1)
        {
            isFacingRight = true;
        }
        else if (transform.localScale.x == -1)
        {
            isFacingRight = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)//当发生碰撞时，这个方法会被调用
    {
        DelectCollisionEnter2D(collision);
    }
    protected virtual void DelectCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer==LayerMask.NameToLayer("HeroDetector"))//碰撞到角色就调用角色受伤方法
        {
            FindObjectOfType<PlayerController>().TakeDamage();
            FindObjectOfType<HitPause>().Stop(0.1f,0);
        }
        if (isDead&&collision.gameObject.layer==LayerMask.NameToLayer("Terrain"))//如果已经死亡那就让碰撞方法无效
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;//将物理效果设置为静态,即该方法不可用
            GetComponent<BoxCollider2D>().enabled = false;

        }
    }
    protected override void Dead()
    {
        base.Dead();
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;//将物理效果设置为静态,即该方法不可用
        GetComponent<BoxCollider2D>().enabled = false;
        SpawnCoins();
    }
    public virtual void SpawnCoins()//掉落金币
    {
        randomCoint = Random.Range(minSpawnCoins,maxSpawnCoins);
        for(int i = 0; i < randomCoint; i++)
        {
            GameObject geo = Instantiate(coin,transform.position,Quaternion.identity,transform.parent);
            Vector2 force = new Vector2(Random.Range(-maxBumpXForce,maxBumpXForce),Random.Range(minBumpYForce,maxBumpYForce));//金币受到力
            geo.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);

        }
    }
}
