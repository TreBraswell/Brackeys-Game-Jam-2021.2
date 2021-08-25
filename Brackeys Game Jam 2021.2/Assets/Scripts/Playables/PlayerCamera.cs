using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private Transform player, lookRoot;


    public float sensitivity = 300f;
    public bool invert;


    private Vector2 current_Mouse_look, look_Angles;


    // Update is called once per frame
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
       // LockAndUnluckCursor();

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            //LookAround();
        }
    }




    //Cursor visible/hidden
    void LockAndUnluckCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

    }//Cursor lock
    void LookAround()
    {
        current_Mouse_look = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
     
        look_Angles.y += current_Mouse_look.y * sensitivity;



        player.localRotation = Quaternion.Euler(0f, look_Angles.y, 0f);

    }//loo
}
