using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 1f;
    public bool isWeaponFlipped;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
