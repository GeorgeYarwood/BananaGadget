using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    public GameObject LookTarget;

    // Start is called before the first frame update
    void Start()
    {
        CameraController.MoveCam(LookTarget.transform.position, LookTarget.transform.rotation, true);    
    }

    public void BackToGame() 
    {
        CameraController.BackToPlayer(true);
        CharacterController.levelstart = true;
        CharacterController.initlvl = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
