using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum RIG { BODY, LEGS, ARMS }
public class AnyStateAnimation
{
    public RIG AnimationRig { get; private set; }

    public string[] higherPrio;

    public string name;

    public bool active;

    public AnyStateAnimation(RIG rig, string name, string[] higherPrio)
    {
        this.AnimationRig = rig;
        this.name = name;
        this.higherPrio = higherPrio;
    }


}
