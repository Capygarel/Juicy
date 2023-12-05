using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float knockBackForce;

    public GameObject hurtParticlePrefab;
    public GameObject impactParticlePrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.GetComponent<Enemy>().hp > 1 ) Instantiate(hurtParticlePrefab, transform.position, transform.rotation);
            Vector3 vectEnnemyToBullet = (collision.transform.position - transform.position).normalized;
            collision.GetComponent<Rigidbody2D>().AddForce((vectEnnemyToBullet + transform.right).normalized * knockBackForce);
            collision.GetComponent<Enemy>().TakeDamage(1);
            popParticles();
        }
        if (collision.CompareTag("Wall"))
        {
            popParticles();
        }
    }

    void popParticles()
    {
        Instantiate(impactParticlePrefab, transform.position, transform.rotation).GetComponent<Rigidbody2D>();
        Destroy(gameObject);
    }
}
