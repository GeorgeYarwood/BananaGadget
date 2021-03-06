﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    float healthreduction = 20f;
    float speedpenalty = 2f;

    bool infire = false;

    CharacterController player;

    private void OnTriggerEnter(Collider other)
    {
        
        player = other.gameObject.GetComponentInParent<CharacterController>();

        try
        {
            if (!player.currentfireResist)
            {
                player.currentMoveSpeed -= speedpenalty;
                infire = true;
                //Show fire effect on screen
            }
            else
            {
                Debug.Log("Player resistant to fire!");
            }

        }

        catch
        {
            Debug.Log("yes");
        }
        
      

    }

    private void Update()
    {
        //Take away health while player is in fire
        if (infire) 
        {
            player.currentHealth -= healthreduction * Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        try 
        {
            if (!player.currentfireResist)
            {
                infire = false;
                player = other.gameObject.GetComponentInParent<CharacterController>();
                player.currentMoveSpeed += speedpenalty;
                //Disable fire effect
            }
            else
            {
                Debug.Log("Player resistant to fire!");
            }
        }
        catch 
        {
            Debug.Log("yes");        
        }
       
    }

}
