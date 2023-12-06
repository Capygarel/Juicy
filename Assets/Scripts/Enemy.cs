using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public int maxHp;

    public GameObject particlePrefab, cadavrePrefab, bloodPrefab;

    public List<GameObject> moneyPrefabs;

    [SerializeField] AudioSource hitSound, deathSound, bloodSound;

    [SerializeField] float volumeHit, volumeDeath, volumeBlood, rangePitchLowHit, rangePitchHighHit, rangePitchLowDeath, rangePitchHighDeath;

    private bool isFlipped;

    private Animator animator;

    private void Start()
    {
        hp = maxHp;
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity *= 0.90f;


        if(Input.GetKeyDown(KeyCode.Space)) {
            Die();
        }
    }
    
   

    public void TakeDamage(int damage, Vector3 collision)
    {
        animator.SetTrigger("IsHurted");
        float pitch = Random.Range(rangePitchLowHit, rangePitchHighHit);
        FaceHit(collision);

        hp -= damage;
        if (hp <= 0)
        {
            
            Die();
        }
        else
        {
            SoundManager.Instance.PlaySound(hitSound, volumeHit, pitch, transform.position);

            Debug.Log(pitch);
            FlashManager.instance.Flash(this.gameObject.GetComponent<SpriteRenderer>(), 0.15f);
        }
    }

    private void Die()
    {

        float pitchDeath = Random.Range(rangePitchLowDeath, rangePitchHighDeath);
        SoundManager.Instance.PlaySound(deathSound, volumeDeath,pitchDeath, transform.position);
        SoundManager.Instance.PlaySound(bloodSound, volumeBlood, transform.position);



        Instantiate(particlePrefab, transform.position, Quaternion.identity);

        animator.SetTrigger("IsDying");

        GameObject currentBill;
        for(int i = 0; i < Random.Range(1, 8); i++) 
        { 
            currentBill = Instantiate(moneyPrefabs[Random.Range(0,2)], transform.position, Quaternion.identity);

            currentBill.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-2f , 2f), Random.Range(-2f, 2f), -3f), ForceMode.Impulse);
            currentBill.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-10,10), Random.Range(-10,10), Random.Range(-10, 10)), ForceMode.Impulse);
        }
    }

    public void VraimentDeadCetteFois()
    {
        Destroy(gameObject);
        EnemiesManager.instance.Die(gameObject);
        GameObject cadavre = Instantiate(cadavrePrefab);
        cadavre.transform.position = transform.position;

        Instantiate(bloodPrefab, transform.position, Quaternion.Euler(0,0,Random.Range(0,360)));


        cadavre.GetComponent<SpriteRenderer>().flipX = isFlipped;
    }

    public void FaceHit(Vector3 collision)
    {

        if (collision.x < transform.position.x && !isFlipped)
        {
            isFlipped = true;
            GetComponent<SpriteRenderer>().flipX = isFlipped;
        }
        else if(collision.x > transform.position.x && isFlipped)
        {
            isFlipped = false;
            GetComponent<SpriteRenderer>().flipX = isFlipped;
        }
    }
}
