using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{

    public enum abilityType { melee, ranged, movement };
    public enum States { idle, walk, jump };

    //Player in our scene
    GameObject Player;

    Rigidbody rb;

    public States PlayerState;


    //Base values

    //Movement Speed
    float MoveSpeed = 20f;
    //Drains over time
    float energycapacity = 2000;
    float MaxJump = 300f;
    bool FireResitant = false;
    float Health = 100f;
    float Dmg = 4f;


    //Values we adjust
    float currentMoveSpeed;
    float currentEnergyCapacity;
    float currentMaxJump;
    bool  currentfireResist;
    float currentHealth;
    float currentDmg;

    //Array of all available abilities
    public Ability[] abilities = new Ability[2];


    //Array of abilities we currently have equiped
    public Ability[] currentAbilities = new Ability[3];


    public Text energyTxt;


    public int DmgEvent;
    public int DoorEvent;

    public GameObject Respawn;

    bool JCoolDown = false;

    bool isAirborne= false;

    // Start is called before the first frame update
    void Start()
    {
        ResetStats();

        Player = GameObject.FindGameObjectWithTag("Player");

        rb = Player.GetComponent<Rigidbody>();


        ApplyAbilities();

        CameraController.AddRayEvent("test", Test);
        DmgEvent = CameraController.AddRayEvent("Enemy", Damage);
        DoorEvent = CameraController.AddRayEvent("doorButton", Door);
    }

    private void ResetStats()
    {
        currentMoveSpeed = MoveSpeed;
        currentEnergyCapacity = energycapacity;
        currentMaxJump = MaxJump;
        currentfireResist = FireResitant;
        currentHealth = Health;
        currentDmg = Dmg;
    }

    public void Test() 
    {
        Debug.Log("Working!");

    }

    public void Door()
    {
        Transform currentDoor = CameraController.RayQueue[DoorEvent].hit;

        Animator anim = currentDoor.GetComponentInChildren<Animator>();

        anim.SetTrigger("Open");

    }

    public void Damage() 
    {
        Transform currentTransform = CameraController.RayQueue[DmgEvent].hit;

        Enemy currentEnemy = currentTransform.GetComponentInChildren<Enemy>();

        try
        {
            currentEnemy.Health -= currentDmg;
        }
        catch 
        {
            Debug.Log("Enemey not available, perhaps it's dead?");
            
        }
    }

    public void ApplyAbilities() 
    {
        for(int i = 0; i< currentAbilities.Length; i++) 
        {
            //Apply jump stats
            currentMaxJump += currentAbilities[i].Jump;

            //Add movement speed
            currentMoveSpeed += currentAbilities[i].Speed;

            //Add energy
            currentEnergyCapacity += currentAbilities[i].Energy;

            //Add damage
            currentDmg += currentAbilities[i].Damage;

            //Apply fire resistance
            if (!currentfireResist) 
            {
                currentfireResist = currentAbilities[i].FireResitant;
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
        energyTxt.text = "Energy Capacity: " + currentEnergyCapacity.ToString();


        if(currentEnergyCapacity > 0f)
        {
            currentEnergyCapacity -= 1f * Time.deltaTime;
        }

        //Check for loss conditions

        if(currentEnergyCapacity <= 0f || currentHealth <= 0f) 
        {
            //Fail mission
            ResetStats();
            ApplyAbilities();
            //Reload Level
            //
            RespawnPlayer();
        }



        switch (PlayerState) 
        {
            case States.idle:
                if (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d"))
                {
                    PlayerState = States.walk;

                }
                if (isAirborne)
                {
                    PlayerState = States.jump;
                }

                break;

            case States.walk:

                if (!Input.GetKey("w") || !Input.GetKey("s") || !Input.GetKey("a") || !Input.GetKey("d"))
                {
                    PlayerState = States.idle;

                }
                if (isAirborne)
                {
                    PlayerState = States.jump;
                }
                break;
            case States.jump:
              
                if(!isAirborne)
                {
                    PlayerState = States.idle;
                }

                break;
        }
       


        Debug.Log(PlayerState);
    }

    void RespawnPlayer() 
    {
        //Move player back to respawn point
        Player.transform.position = Respawn.transform.position;

    }
    
    // For dem fast physex init
    void FixedUpdate()
    {
        //If we're not in the air
        if(Player.transform.position.y < 1f) 
        {
            isAirborne = false;


            //Keyboard events
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






            if (Input.GetKey("space"))
            {
                if (!JCoolDown) 
                {
                    isAirborne = true;
                    rb.AddForce(0, currentMaxJump, 0);
                    StartCoroutine(JumpCooldown());
                }

            

        }


    }

    IEnumerator JumpCooldown() 
    {
        JCoolDown = true;
        yield return new WaitForSeconds(1.5f);
        JCoolDown = false;
    }
}
