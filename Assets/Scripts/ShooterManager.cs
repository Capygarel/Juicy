using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;


public class ShooterManager : MonoBehaviour
{   
    private Transform player;
    private Camera cam;
    [SerializeField] private GameObject muzzle;
    [SerializeField] private GameObject particlePrefab;

    [SerializeField] private GameObject douillePrefab;

    [SerializeField] GameObject bulletPrefab;
    private GameObject bulletInstance;

    [SerializeField] private float fireSpeed = 0.7f;
    private float rechargeTime;

    [SerializeField] ParticleSystem smoke;
    [SerializeField] float smokeDelay;

    private bool isFlipped = false;
    private Vector3 offsetGun = new Vector3(0.3f, -0.25f, 0);
    private Vector3 offsetGunFlipped = new Vector3(-0.3f, -0.25f, 0);
    [SerializeField] private AnimationCurve gunKickbackAnimationCurve;
    [SerializeField] private float kickbackScale = 0.1f;
    private float timeKickBack = 0;

    [SerializeField] CinemachineImpulseSource impulseSource;


    [SerializeField] private List<AudioClip> sound = new List<AudioClip>();
    [SerializeField] private float volumeFire, volumeReload;



    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent;
        cam = Camera.main;
        fireSpeed = sound[0].length + sound[1].length;
        rechargeTime = fireSpeed ;

        impulseSource = GetComponent<CinemachineImpulseSource>();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateRotation();

        UpdatePos();
       

        rechargeTime += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && rechargeTime >= fireSpeed)
            Fire();
    }

    void Fire()
    {
        StartCoroutine(KickbackAnimation());

        

        //Instantie la balle au bout du flingue, lui indique si l'arme est flip pour qu'elle parte dans le bon sens
        Instantiate(particlePrefab, muzzle.transform.position, this.transform.rotation);

        bulletInstance = Instantiate(bulletPrefab, muzzle.transform.position, this.transform.rotation);

        bulletInstance.GetComponent<MoveForward>().isWeaponFlipped = isFlipped;

        ShakeManager.instance.CameraShake(impulseSource);


        //Instantie la douille
        var douille = Instantiate(douillePrefab);
        douille.transform.position = transform.position;
        douille.GetComponent<Douille>().isFlipped = isFlipped;

        


        StartCoroutine(PlaySound());

        StartCoroutine(PlaySmoke());
        


        rechargeTime = 0;
    }

    void UpdateRotation()
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;


        //dir entre player et mousePos
        Vector3 shotDirection;

        if ((mousePosition - muzzle.transform.position).magnitude < 0.35f)
            shotDirection = (mousePosition - this.transform.position).normalized;
        else
            shotDirection = (mousePosition - muzzle.transform.position).normalized;

        //Crée le Vecteur de rotation autour de Z
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * shotDirection;

        // X axis facing the target
        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);

        transform.rotation = targetRotation;

        isFlipped = (mousePosition.x - transform.parent.position.x) < 0;

    }

    void UpdatePos()
    {

        transform.parent.GetComponent<SpriteRenderer>().flipX = isFlipped;
        transform.GetComponent<SpriteRenderer>().flipY = isFlipped;

        if (isFlipped)
        {
            transform.position = transform.parent.position + offsetGunFlipped;
            transform.GetChild(0).localPosition = new Vector3(0.36f, -0.053f, 0);
        }
        else
        {
            transform.position = transform.parent.position + offsetGun;
            transform.GetChild(0).localPosition = new Vector3(0.36f, 0.053f, 0);
        }
    }

    IEnumerator PlaySound()
    {
        SoundManager.Instance.PlaySound(sound[0], volumeFire, UnityEngine.Random.Range(1f, 1.5f));


        yield return new WaitForSeconds(sound[0].length  );


        SoundManager.Instance.PlaySound(sound[1], volumeReload, 1.2f);

    }

    IEnumerator PlaySmoke()
    {
        yield return new WaitForSeconds (smokeDelay);

        smoke.Play();
    }

    IEnumerator KickbackAnimation()
    {

        Vector3 originPosGun = transform.localPosition;
        bool tempIsFlipped = isFlipped;

        while(timeKickBack < fireSpeed && tempIsFlipped == isFlipped) 
        {
            float speed = gunKickbackAnimationCurve.Evaluate(timeKickBack);

            Vector3 kickbackDirection = (muzzle.transform.position - transform.position).normalized;


            
            transform.localPosition = (originPosGun - ( kickbackDirection * kickbackScale * speed));

            timeKickBack += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originPosGun;
        timeKickBack = 0;


    }
}

