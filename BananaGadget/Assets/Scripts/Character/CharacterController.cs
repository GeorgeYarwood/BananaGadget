using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //Player in our scene
    GameObject Player;

    Rigidbody rb;

    //Movement Speed
    float MoveSpeed = 8f;




    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        rb = Player.GetComponent<Rigidbody>();


        CameraController.AddRayEvent("test", Test);
    }

    public void Test() 
    {
        Debug.Log("Working!");

    }

    // For dem fast physex init
    void FixedUpdate()
    {

        //Keybaord events

        if (Input.GetKey("w"))
        {
            rb.AddForce(0, 0, MoveSpeed);
        
        }
        if (Input.GetKey("s"))
        {
            rb.AddForce(0, 0, -MoveSpeed);

        }

        if (Input.GetKey("d"))
        {
            rb.AddForce(MoveSpeed, 0, 0);

        }

        if (Input.GetKey("a"))
        {
            rb.AddForce(-MoveSpeed, 0, 0);

        }

        
    }
}
