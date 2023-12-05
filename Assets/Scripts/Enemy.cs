using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public int maxHp;

    public GameObject ParticlePrefab;

    private void Start()
    {
        hp = maxHp;
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity *= 0.90f;
    }
    
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) Die();
    }

    private void Die()
    {
        EnemiesManager.instance.Die(gameObject);
        Instantiate(ParticlePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
