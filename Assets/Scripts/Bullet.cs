using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hurtParticlePrefab;
    public GameObject impactParticlePrefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.GetComponent<Enemy>().hp > 1 ) Instantiate(hurtParticlePrefab, transform.position, transform.rotation);
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
