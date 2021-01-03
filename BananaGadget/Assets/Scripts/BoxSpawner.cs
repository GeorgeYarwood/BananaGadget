using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject box;

    public Transform spawnloc;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawner());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawner() 
    {
        Instantiate(box, spawnloc.transform.position, spawnloc.transform.rotation);

        yield return new WaitForSeconds(3f);

        StartCoroutine(spawner());
    }
}
