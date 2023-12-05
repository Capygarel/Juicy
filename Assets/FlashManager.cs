using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlashManager : MonoBehaviour
{
    public static FlashManager instance;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float duration = 1f;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Flash(SpriteRenderer sprite, float duration)
    {
        StartCoroutine(FlashCoroutine(duration));
    }

    IEnumerator FlashCoroutine(float duration)
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.white;

        yield return new WaitForSeconds(duration);

        spriteRenderer.color = originalColor;
    }
}





 


