using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ne pas l'oublier sinon le shake marche pas 
using Cinemachine;


public class Caller : MonoBehaviour
{
    // Déclaraion de la source du shake
    CinemachineImpulseSource impulseSource;



    private void Start()
    {
       // Récupération du component sur l'objet pour générer la source du shake
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }


    void Update()
    {
        // création d'un event random mettre où vous voullez 
        if (Input.GetKeyDown(KeyCode.A))
        {            
            // Appel du shake manager avec l'impulse source 
            ShakeManager.instance.CameraShake(impulseSource);
        }
    }
}
