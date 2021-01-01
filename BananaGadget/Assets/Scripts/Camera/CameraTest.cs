using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(test());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator test()
    {

        yield return new WaitForSeconds(5f);
        CameraController.BackToPlayer(true);

    }
}
