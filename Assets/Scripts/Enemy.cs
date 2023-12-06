using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public int maxHp;

    public GameObject ParticlePrefab;

    public List<GameObject> moneyPrefabs;

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

        GameObject currentBill;
        for(int i = 0; i < Random.Range(1, 8); i++) 
        { 
            currentBill = Instantiate(moneyPrefabs[Random.Range(0,2)], transform.position, Quaternion.identity);

            currentBill.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-2f , 2f), Random.Range(-2f, 2f), -3f), ForceMode.Impulse);
            currentBill.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-10,10), Random.Range(-10,10), Random.Range(-10, 10)), ForceMode.Impulse);
        }

        Destroy(gameObject);
    }

}
