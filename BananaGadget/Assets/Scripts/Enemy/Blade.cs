using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    float healthreduction = 35f;

    bool inblade = false;

    CharacterController player;
    private void OnTriggerEnter(Collider other)
    {

        player = other.gameObject.GetComponentInParent<CharacterController>();

        inblade = true;


    }

    private void Update()
    {
        //Take away health while player is in fire
        if (inblade)
        {
            player.currentHealth -= healthreduction * Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        inblade = false;
        player = other.gameObject.GetComponentInParent<CharacterController>();
        //Disable fire effect

    }
}

