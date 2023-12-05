using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShooterManager : MonoBehaviour
{   
    private Transform player;
    private Camera cam;
    [SerializeField] private GameObject muzzle;
    [SerializeField] private GameObject particlePrefab;


    [SerializeField] GameObject bulletPrefab;
    private GameObject bulletInstance;

    [SerializeField] private float fireSpeed = 0.7f;
    private float rechargeTime;




    private bool isFlipped = false;
    private Vector3 offsetGun = new Vector3(0.3f, -0.25f, 0);
    private Vector3 offsetGunFlipped = new Vector3(-0.3f, -0.25f, 0);
    [SerializeField] private AnimationCurve gunKickbackAnimationCurve;
    private float time = 0;

    [SerializeField] private List<AudioClip> sound = new List<AudioClip>();
    [SerializeField] private float volume;



    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent;
        cam = Camera.main;
        rechargeTime = fireSpeed;
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
        //KickBack Animation

        

        //Instantie la balle au bout du flingue, lui indique si l'arme est flip pour qu'elle parte dans le bon sens
        Instantiate(particlePrefab, muzzle.transform.position, this.transform.rotation);

        bulletInstance = Instantiate(bulletPrefab, muzzle.transform.position, this.transform.rotation);

        bulletInstance.GetComponent<MoveForward>().isWeaponFlipped = isFlipped;


        //SoundManager.Instance.PlaySoundInList(sound, volume);


        rechargeTime = 0;
    }

    void UpdateRotation()
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;


        //dir entre player et mousePos
        Vector3 shotDirection;
        Debug.Log((mousePosition - muzzle.transform.position).magnitude);
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

    IEnumerator KickbackAnimation()
    {
        float speed = gunKickbackAnimationCurve.Evaluate(time);
        time += Time.deltaTime; 

        

        yield return null;
    }
}

