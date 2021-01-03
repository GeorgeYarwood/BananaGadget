using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    public GameObject LookTarget;

    static GameObject staticTarget;

    // Start is called before the first frame update
    void Start()
    {
        staticTarget = LookTarget;
        InitGame();
    }

    public void BackToGame() 
    {
        CameraController.BackToPlayer(true);
        CharacterController.levelstart = true;
        CharacterController.initlvl = true;
    }


    public static void InitGame() 
    {
        CameraController.MoveCam(staticTarget.transform.position, staticTarget.transform.rotation, true);
        Cursor.lockState = CursorLockMode.None;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
