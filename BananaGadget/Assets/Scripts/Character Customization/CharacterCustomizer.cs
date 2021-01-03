using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterCustomizer : MonoBehaviour
{
    public GameObject[] arm;
    public GameObject[] leg;
    public GameObject[] other;

    public GameObject armText;
    public GameObject legText;
    public GameObject otherText;



    void Start()
    {
        armText.GetComponent<Text>().text = arm[0].GetComponent<PartProperty>().description;
        legText.GetComponent<Text>().text = leg[0].GetComponent<PartProperty>().description;
        otherText.GetComponent<Text>().text = other[0].GetComponent<PartProperty>().description;
    }

    void Update()
    {
        
    }

    public void selectArm(int id)
    {
         for(int i=0; i<=arm.Length-1;i++)
        {
            arm[i].SetActive(false);
        }

        arm[id].SetActive(true);

        armText.GetComponent<Text>().text = arm[id].GetComponent<PartProperty>().description;
    }

    public void selectLeg(int id)
    {
        for (int i = 0; i <= leg.Length - 1; i++)
        {
            leg[i].SetActive(false);
        }

        leg[id].SetActive(true);

        legText.GetComponent<Text>().text = leg[id].GetComponent<PartProperty>().description;
    }

    public void selectOther(int id)
    {
        for (int i = 0; i <= other.Length - 1; i++)
        {
            other[i].SetActive(false);
        }

        other[id].SetActive(true);

        otherText.GetComponent<Text>().text = other[id].GetComponent<PartProperty>().description;
    }
}
