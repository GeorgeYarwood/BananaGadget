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

    //drains over time
    float energycapacity = 2000f;

    float MaxJump = 25f;
    bool FireResitant = false;

    //Array of abilities we currently have equiped
    public Ability[] movementAbilties = new Ability[2];
    public Ability[] physicalAbilities = new Ability[2];


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        rb = Player.GetComponent<Rigidbody>();


        ApplyAbilities();

        CameraController.AddRayEvent("test", Test);
    }

    public void Test() 
    {
        Debug.Log("Working!");

    }

    public void ApplyAbilities() 
    {
        for(int i = 0; i<movementAbilties.Length; i++) 
        {
            MaxJump += movementAbilties[i].Jump;
            FireResitant = movementAbilties[i].FireResitant;
        }

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


        if (Input.GetKey("space"))
        {
            rb.AddForce(0, MaxJump, 0);

        }


    }
}
