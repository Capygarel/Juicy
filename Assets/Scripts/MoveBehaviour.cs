using System.Security.Cryptography;
using UnityEngine;

public class MoveBehaviour : MonoBehaviour
{
    [SerializeField] private AnimationCurve accelerationCurve, decelerationCurve;
    [SerializeField] private GameObject marcheParticlePrefab;
    [SerializeField] private GameObject marcheParticle;

    [SerializeField] private Transform marcheParticleSpawnPoint;

    [SerializeField]
    private float speed;

    [SerializeField] private float acceleration, deceleration;

    private Vector3 previousVelocity;

    Vector2 moveDirection;

    void Start()
    {
        deceleration = 0;
        acceleration = 0;
    }

    private void Update()
    {
        TakeInputs();
    }


    private void TakeInputs()
    {
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    private void Move()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (marcheParticle == null && GetComponent<Rigidbody2D>().velocity.magnitude > 8f)
        {
            marcheParticle = Instantiate(marcheParticlePrefab);
            marcheParticle.transform.position = marcheParticleSpawnPoint.position;
        }

        if(moveDirection.magnitude > 0.1f)
        {
            deceleration = 0;
            acceleration += Time.deltaTime;
            rb.velocity = rb.velocity * Time.deltaTime;
            rb.velocity += new Vector2(moveDirection.x, moveDirection.y) * Time.deltaTime * accelerationCurve.Evaluate(acceleration) * 10 * speed;
        }
        else
        {
            if (acceleration != 0)
            { 
                deceleration = decelerationCurve[decelerationCurve.length - 1].time -acceleration;
                if(deceleration < 0) deceleration = 0;
                previousVelocity = rb.velocity;
            }
            deceleration += Time.deltaTime;
            acceleration = 0;
            Debug.Log(deceleration + "  " +decelerationCurve.Evaluate(deceleration));
            rb.velocity = previousVelocity * decelerationCurve.Evaluate(deceleration);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity *= Time.deltaTime;
    }

    void FixedUpdate()
    {
        Move();
    }
}