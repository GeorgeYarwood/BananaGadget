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

    Animator playerAnim;

    Rigidbody rb;

    public States PlayerState;


    public Enemy[] enemies = new Enemy[4];


    //Base values

    //Movement Speed
    float MoveSpeed = 20f;
    //Drains over time
    float energycapacity = 400;
    float MaxJump = 300f;
    bool FireResitant = false;
    bool FrostResitant = false;
    float Health = 100f;
    float Dmg = 4f;


    //Values we adjust
    public float currentMoveSpeed;
    public float currentEnergyCapacity;
    public float currentMaxJump;
    public bool  currentfireResist;
    public bool  currentfrostResist;
    public float currentHealth;
    public float currentDmg;

    //Array of all available abilities
    public Ability[] abilities = new Ability[2];


    //Array of abilities we currently have equiped
    public Ability[] currentAbilities = new Ability[3];


    public Text energyTxt;
    public Text healthTxt;


    public int rangeDmgEvent;
    public int meleeDmgEvent;
    public int DoorEvent;

    public GameObject Respawn;

    bool JCoolDown = false;

    bool isAirborne= false;

    // Start is called before the first frame update
    void Start()
    {
        ResetStats();

        Player = GameObject.FindGameObjectWithTag("Player");

        playerAnim = Player.GetComponentInChildren<Animator>();

        rb = Player.GetComponent<Rigidbody>();


        ApplyAbilities();

        //CameraController.AddRayEvent("test", Test);
        DoorEvent = CameraController.AddRayEvent("doorButton", Door, 10);
    }

    private void ResetStats()
    {
        currentMoveSpeed = MoveSpeed;
        currentEnergyCapacity = energycapacity;
        currentMaxJump = MaxJump;



        currentfireResist = FireResitant;
        currentfrostResist = FrostResitant;
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

    public void RangedDamage() 
    {
        Transform currentTransform = CameraController.RayQueue[rangeDmgEvent].hit;

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
    public void MeleeDamage()
    {
        Transform currentTransform = CameraController.RayQueue[meleeDmgEvent].hit;

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

            //Apply frost resistance
            if (!currentfrostResist)
            {
                currentfrostResist = currentAbilities[i].FrostResitant;
            }


            //If we have a melee ability
            if (currentAbilities[i].abType == global::abilityType.melee)
            {
                //Set melee damage function
                meleeDmgEvent = CameraController.AddRayEvent("Enemy", MeleeDamage, 10);

            }
            //If we have a ranged ability
            if (currentAbilities[i].abType == global::abilityType.ranged)
            {
                //Set melee damage function
                rangeDmgEvent = CameraController.AddRayEvent("Enemy", RangedDamage, 40);

            }

            //If we have a movement ability
            if (currentAbilities[i].abType == global::abilityType.movement)
            {

            }


        }

    }


    void Update()
    {
        energyTxt.text = "Energy Capacity: " + currentEnergyCapacity.ToString();
        healthTxt.text = "Health: " + currentHealth.ToString();

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

                playerAnim.SetTrigger("Idle");

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
                playerAnim.SetTrigger("Run");

                if (Input.GetKeyUp("w") || Input.GetKeyUp("s") || Input.GetKeyUp("a") || Input.GetKeyUp("d"))
                {
                    PlayerState = States.idle;

                }
                if (isAirborne)
                {
                    PlayerState = States.jump;
                }
                break;
            case States.jump:
                playerAnim.SetTrigger("Jump");

                if (!isAirborne)
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

        //Re-enable and reset all enemies
        for(int i = 0; i < enemies.Length; i++) 
        {
            enemies[i].Health = 100f;
            enemies[i].gameObject.SetActive(true);
        }

    }
    
    // For dem fast physex init
    void FixedUpdate()
    {
        //If we're not in the air
        if(Player.transform.position.y < 2f) 
        {
            isAirborne = false;


            //Keyboard events
            if (Input.GetKey("w"))
            {
                rb.AddForce(0, 0, currentMoveSpeed);

            }
            if (Input.GetKey("s"))
            {
                rb.AddForce(0, 0, -currentMoveSpeed);

            }

            if (Input.GetKey("d"))
            {
                rb.AddForce(currentMoveSpeed, 0, 0);

            }

            if (Input.GetKey("a"))
            {
                rb.AddForce(-currentMoveSpeed, 0, 0);

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
        yield return new WaitForSeconds(3);
        JCoolDown = false;
    }
}
