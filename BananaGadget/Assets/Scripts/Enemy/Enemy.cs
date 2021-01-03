using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    public float Health = 100f;
    public Text HealthTxt;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthTxt.text = Health.ToString();
        if(Health <= 0f) 
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }







        }
}
