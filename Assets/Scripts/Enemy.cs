using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int hp;
    public int maxHp;

    public GameObject ParticlePrefab;

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
        Instantiate(ParticlePrefab);
        Destroy(gameObject);
    }

}
