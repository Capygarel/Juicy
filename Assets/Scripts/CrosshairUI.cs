using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairUI : MonoBehaviour
{
    [SerializeField] private float resizeFactor;
    public Texture2D crosshairImage;
    // Start is called before the first frame update
    void Start()
    {
        //crosshairImage.Reinitialize(Mathf.RoundToInt(crosshairImage.width * resizeFactor), Mathf.RoundToInt(crosshairImage.height * resizeFactor));
        SetCrosshair(true);
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = Input.mousePosition;
        //crosshairImage.Reinitialize(Mathf.RoundToInt(crosshairImage.width * resizeFactor), Mathf.RoundToInt(crosshairImage.height * resizeFactor));

    }

    public void SetCrosshair(bool active)
    {
        Vector2 cursorOffset = new Vector2(crosshairImage.width / 2, crosshairImage.height / 2);
        if (active)
            Cursor.SetCursor(crosshairImage, cursorOffset, CursorMode.Auto);
        else
            Cursor.SetCursor(null, cursorOffset, CursorMode.Auto);
    }

}
