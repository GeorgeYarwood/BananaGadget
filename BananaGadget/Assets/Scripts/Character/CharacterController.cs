using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{

    public enum abilityType { melee, ranged, movement };

    //Player in our scene
    GameObject Player;

    Rigidbody rb;

    //Movement Speed
    float MoveSpeed = 20f;

    //drains over time
    float energycapacity = 2000f;

    float MaxJump = 25f;
    bool FireResitant = false;
    

    //Array of all available abilities
    public Ability[] abilities = new Ability[2];


    //Array of abilities we currently have equiped
    public Ability[] currentAbilities = new Ability[3];


    public Text energyTxt;



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

    public void Fire() 
    {
        
    }

    public void ApplyAbilities() 
    {
        for(int i = 0; i< currentAbilities.Length; i++) 
        {
            //Apply jump stats
            MaxJump += currentAbilities[i].Jump;

            //Add movement speed
            MoveSpeed += currentAbilities[i].Speed;

            //Add energy
            energycapacity += currentAbilities[i].Energy;

            //Apply fire resistance
            if (!FireResitant) 
            {
                FireResitant = currentAbilities[i].FireResitant;
            }


            //If we have a melee ability
            if (currentAbilities[i].abType.Equals(abilityType.melee)) 
            {
               
            }
            //If we have a ranged ability
            if (currentAbilities[i].abType.Equals(abilityType.ranged))
            {
               
            }

            //If we have a movement ability
            if (currentAbilities[i].abType.Equals(abilityType.movement))
            {

            }


        }

    }


    void Update()
    {
        energyTxt.text = "Energy Capacity: " + energycapacity.ToString();


        if(energycapacity > 1f)
        {
            energycapacity -= 1f * Time.deltaTime;
        }
        else 
        {
            //Fail mission
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
