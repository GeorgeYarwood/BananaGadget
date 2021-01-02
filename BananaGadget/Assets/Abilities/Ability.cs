using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum abilityType { melee, ranged, movement };

[CreateAssetMenu(fileName = "Ability", menuName = "GameData/Ability")]

public class Ability : ScriptableObject
{

    //Abilties will modify the base stats of robot

    public float Jump;

    public float Speed;

    public float Damage;

    public float Energy;

    public bool FireResitant;
    public bool FrostResitant;

    public abilityType abType;

    public string abilityName;

    public Sprite abilityIcon;

    public GameObject abilityModel;

    public bool hascooldown;

    public bool hasModel;

    public bool doesdamage;


}
