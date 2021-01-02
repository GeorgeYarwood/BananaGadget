using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frost : MonoBehaviour
{
    float speedreduction = 4f;
    CharacterController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        player = other.gameObject.GetComponentInParent<CharacterController>();
        if (!player.currentfrostResist)
        {
           
            player.currentMoveSpeed -= speedreduction;
            //Show frost effect on screen
        }
        else
        {
            Debug.Log("Player is immune to frost!");
        }

    }


    private void OnTriggerExit(Collider other)
    {
        player = other.gameObject.GetComponentInParent<CharacterController>();

        if (!player.currentfrostResist)
        {
            player.currentMoveSpeed += speedreduction;
            //Disable frost effect
        }
        else
        {
            Debug.Log("Player is immune to frost!");
        }
    }

}
