using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Loot : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject player;
    [SerializeField] float pickupRange, speed;

    private bool isFollowing = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (player.transform.position - transform.position);
        direction.z = 0;

        Debug.Log(direction.magnitude);

        if (direction.magnitude < pickupRange || isFollowing)
        {
            GetComponent<Rigidbody>().AddForce(direction.normalized*speed);
            isFollowing = true ;
        }

        if (direction.magnitude < .5f)
        {
            Destroy(this.gameObject);
        }
    }
}

