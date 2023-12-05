using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ne pas l'oublier sinon le shake marche pas 
using Cinemachine;


public class Caller : MonoBehaviour
{
    // D�claraion de la source du shake
    CinemachineImpulseSource impulseSource;



    private void Start()
    {
       // R�cup�ration du component sur l'objet pour g�n�rer la source du shake
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }


    void Update()
    {
        // cr�ation d'un event random mettre o� vous voullez 
        if (Input.GetKeyDown(KeyCode.A))
        {            
            // Appel du shake manager avec l'impulse source 
            ShakeManager.instance.CameraShake(impulseSource);
        }
    }
}
