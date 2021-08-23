using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator :MonoBehaviour
{
    [SerializeField]
    private Animator animator;



    public void SetBool(string anim,bool active)
    {
        animator.SetBool(anim, active);
    }

    public void SetFloat(string param, float value)
    {
        animator.SetFloat(param, value);
    }
    
}
