using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoStone : Breakable
{
    [SerializeField] GameObject coin;
    [SerializeField] int minSpawnCoins;
    [SerializeField] int maxSpawnCoins;
    [SerializeField] float maxBumpXForce;
    [SerializeField] float minBumpXForce;
    [SerializeField] float maxBumpYForce;

    private Animator animator;
    private AudioSource audioSource;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        CheckIsDead();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Attack"))
        {
            Hurt(1, FindObjectOfType<Attack>().transform);
        }
    }
    public override void Hurt(int damage,Transform attackPosition)
    {
        base.Hurt(damage, attackPosition);
        Vector2 vector = attackPosition.position - transform.position;
        if (vector.x > 0)
        {
            //�������Ч
        }else
        {
            //���ҵ���Ч
        }
        SpawnCoin();
        animator.SetTrigger("Hurt");
    }
    protected override void Dead()
    {
        base.Dead();
        //��Ч
        animator.SetTrigger("Dead");
    }
    private void SpawnCoin()
    {
        int randomCoint = Random.Range(minSpawnCoins, maxSpawnCoins);
        for (int i = 0; i < randomCoint; i++)
        {
            GameObject geo = Instantiate(coin, transform.position, Quaternion.identity, transform.parent);
            Vector2 force = new Vector2(Random.Range(-maxBumpXForce, maxBumpXForce), Random.Range(0, maxBumpYForce));//����ܵ���
            geo.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }
    }
}
