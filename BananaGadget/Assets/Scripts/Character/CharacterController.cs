using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //Player in our scene
    GameObject Player;

    //Movement Speed
    float MoveSpeed = 8f;

    //Mouse sensitivity
    float MouseSens = 2f;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        CameraController.AddRayEvent("test", Test);
    }

    public void Test() 
    {
        Debug.Log("Working!");

    }

    // Update is called once per frame
    void Update()
    {
     
        

    }
}
