using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursorController : MonoBehaviour
{

    public Texture2D[] cursorTexture = new Texture2D[2];

    public MovementPlayer movementPlayer;


    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.SetCursor(cursorTexture[0], new Vector2(cursorTexture[0].width / 2f, cursorTexture[0].height / 2f), CursorMode.Auto);

        movementPlayer = GetComponent<MovementPlayer>();
    }

    private void Update()
    {
        //if (movementPlayer.IsInGrappleRange())
        //{
        //    Cursor.visible = true;
        //    Cursor.lockState = CursorLockMode.Confined;
        //    Cursor.SetCursor(cursorTexture[0], new Vector2(cursorTexture[0].width / 2f, cursorTexture[0].height / 2f), CursorMode.Auto);
        //}
        //else
        //{
        //    Cursor.visible = true;
        //    Cursor.lockState = CursorLockMode.Confined;
        //    Cursor.SetCursor(cursorTexture[1], new Vector2(cursorTexture[1].width / 2f, cursorTexture[1].height / 2f), CursorMode.Auto);
        //}
    }

}

