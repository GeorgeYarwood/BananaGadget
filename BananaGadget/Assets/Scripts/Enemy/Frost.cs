using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frost : MonoBehaviour
{
    float speedreduction = 4f;

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
        CharacterController player = other.gameObject.GetComponentInParent<CharacterController>();
        player.currentMoveSpeed -= speedreduction;
        //Show frost effect on screen
    }


    private void OnTriggerExit(Collider other)
    {
        CharacterController player = other.gameObject.GetComponentInParent<CharacterController>();
        player.currentMoveSpeed += speedreduction;
        //Disable frost effect
    }

}
