using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShooterManager : MonoBehaviour
{
    [SerializeField] private GameObject particlePrefab;


    [SerializeField] private float fireSpeed = 0.7f;
    private float rechargeTime;
    private Transform player;
    private Camera camera;
    [SerializeField] GameObject bulletPrefab;

    private GameObject bulletInstance;
    
    private bool isFlipped = false ;

    [SerializeField] private List<AudioClip> sound = new List<AudioClip>();
    [SerializeField] private float volume;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent;
        camera = Camera.main;
        rechargeTime = fireSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 shotDirection = (mousePosition - player.transform.position).normalized;

        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * shotDirection;

        // (resulting in the X axis facing the target)
        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward,upwards : rotatedVectorToTarget);
        
        transform.rotation = targetRotation;

        isFlipped = (mousePosition.x - transform.parent.position.x) < 0;
        transform.parent.GetComponent<SpriteRenderer>().flipX = isFlipped;
        transform.GetComponent<SpriteRenderer>().flipY = isFlipped;

        if (isFlipped)
            transform.position = new Vector2(-0.3f, -0.25f);
        else
            transform.position = new Vector2(0.3f, -0.25f);

        rechargeTime += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && rechargeTime >= fireSpeed)
        {
            Instantiate(particlePrefab, this.transform.position, this.transform.rotation);
            bulletInstance = Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
            bulletInstance.GetComponent<MoveForward>().isWeaponFlipped = isFlipped;
            //SoundManager.Instance.PlaySoundInList(sound, volume);
            rechargeTime = 0;
        }
    }
}

