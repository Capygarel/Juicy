using System.Collections;
using UnityEngine;

public class Douille : MonoBehaviour
{
    Rigidbody2D rb;
    public bool isFlipped;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Envoie la douille en l'air
        Vector2 vector = new Vector2(0, Random.Range(3f, 3.2f));
        vector.x = isFlipped ? Random.Range(0.5f, 0.7f) : Random.Range(-0.5f, -0.7f);
        rb.AddForce(vector * 70f);

        // Fait tourner la douille
        float rotationForce = isFlipped ? Random.Range(-8f, -12f) : Random.Range(8f, 12f);
        rb.AddTorque(rotationForce, ForceMode2D.Impulse);

        StartCoroutine(StopAndDestroy());
    }

    //Arr�te la douille apr�s un moment, la garde l� puis la supprime
    private IEnumerator StopAndDestroy()
    {
        yield return new WaitForSeconds(1f);
        rb.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(6f);
        Destroy(gameObject);
    }
}
