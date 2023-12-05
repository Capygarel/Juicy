using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject particlePrefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.CompareTag("Wall"));
        if (collision.CompareTag("Enemy"))
        {
            if (collision.GetComponent<Enemy>().hp > 1 ) Instantiate(particlePrefab, transform.position, transform.rotation).GetComponent<Rigidbody2D>();
            collision.GetComponent<Enemy>().TakeDamage(1);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
