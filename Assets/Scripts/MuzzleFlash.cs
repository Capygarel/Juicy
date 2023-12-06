using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MuzzleFlash : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Light2D muzzleLight;
    [SerializeField] float maxRadius, flashSpeed, minRadius;


    void Start()
    {
        minRadius = muzzleLight.pointLightOuterRadius;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(muzzleLight.pointLightOuterRadius < maxRadius)
        {
            muzzleLight.pointLightOuterRadius += Time.deltaTime * flashSpeed;
        }
        else
        {
            muzzleLight.pointLightOuterRadius = minRadius;
            this.gameObject.SetActive(false);
        }
    }
}
