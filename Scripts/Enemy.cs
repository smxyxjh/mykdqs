using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Breakable
{
    protected bool isFacingRight;
    int randomCoint;
    protected int minSpawnCoins = 3;//�����ҷ�Χ
    protected int maxSpawnCoins = 5;
    [SerializeField]protected float maxBumpXForce = 100;//��ҵ�ŵ�ܵ�����
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

    private void OnCollisionEnter2D(Collision2D collision)//��������ײʱ����������ᱻ����
    {
        DelectCollisionEnter2D(collision);
    }
    protected virtual void DelectCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer==LayerMask.NameToLayer("HeroDetector"))//��ײ����ɫ�͵��ý�ɫ���˷���
        {
            FindObjectOfType<PlayerController>().TakeDamage();
            FindObjectOfType<HitPause>().Stop(0.1f,0);
        }
        if (isDead&&collision.gameObject.layer==LayerMask.NameToLayer("Terrain"))//����Ѿ������Ǿ�����ײ������Ч
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;//������Ч������Ϊ��̬,���÷���������
            GetComponent<BoxCollider2D>().enabled = false;

        }
    }
    protected override void Dead()
    {
        base.Dead();
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;//������Ч������Ϊ��̬,���÷���������
        GetComponent<BoxCollider2D>().enabled = false;
        SpawnCoins();
    }
    public virtual void SpawnCoins()//������
    {
        randomCoint = Random.Range(minSpawnCoins,maxSpawnCoins);
        for(int i = 0; i < randomCoint; i++)
        {
            GameObject geo = Instantiate(coin,transform.position,Quaternion.identity,transform.parent);
            Vector2 force = new Vector2(Random.Range(-maxBumpXForce,maxBumpXForce),Random.Range(minBumpYForce,maxBumpYForce));//����ܵ���
            geo.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);

        }
    }
}
