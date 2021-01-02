using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    float healthreduction = 5f;
    float speedpenalty = 2f;

    bool infire = false;

    CharacterController player;

    private void OnTriggerEnter(Collider other)
    {
        player = other.gameObject.GetComponentInParent<CharacterController>();
        player.currentMoveSpeed -= speedpenalty;
        infire = true;
        //Show fire effect on screen
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
        infire = false;
        player = other.gameObject.GetComponentInParent<CharacterController>();
        player.currentMoveSpeed += speedpenalty;
        //Disable fire effect
    }

}
