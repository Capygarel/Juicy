using UnityEngine;

public class MoveBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject marcheParticlePrefab;
    [SerializeField] private GameObject marcheParticle;

    [SerializeField] private Transform marcheParticleSpawnPoint;

    [SerializeField]
    private float speed;

    Vector2 moveDirection;

    void Start()
    {

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
        GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Lerp(0, moveDirection.x * speed, 0.7f),
                                     Mathf.Lerp(0, moveDirection.y * speed, 0.7f));
        if (marcheParticle == null && GetComponent<Rigidbody2D>().velocity.magnitude > 10f)
        {
            marcheParticle = Instantiate(marcheParticlePrefab);
            marcheParticle.transform.position = marcheParticleSpawnPoint.position;
        }
    }

    void FixedUpdate()
    {
        Move();
    }
}