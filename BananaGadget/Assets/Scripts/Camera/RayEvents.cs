using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayEvents
{
    public string Tag;
    public System.Action FunctionToRun;
    public Transform hit;
    public float dist;

    public RayEvents(string newTag, System.Action newFunction, float newDist)
    {
        Tag = newTag;
        dist = newDist;
        FunctionToRun = newFunction;
    }

}
