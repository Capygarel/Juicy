using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlashManager : MonoBehaviour
{
    public static FlashManager instance;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float duration;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Flash(SpriteRenderer sprite, float duration)
    {
        StartCoroutine(FlashCoroutine(sprite ,duration));
    }

    IEnumerator FlashCoroutine(SpriteRenderer sprite, float duration)
    {
        Color originalColor = sprite.color;
        sprite.color = Color.red;

        yield return new WaitForSeconds(duration);

        sprite.color = originalColor;
    }
}





 


