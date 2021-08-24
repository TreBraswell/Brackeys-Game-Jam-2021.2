using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    
    private Animator animator;


    //Animation priority - attack over jump
    private string[] animationPrioritiy = new string[] { "Walk", "Jump", "Attack" };


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetFloat(string anim, float value)
    {
        animator.SetFloat(anim, value);
    }

    public void SetBool(string anim, bool value)
    {
        animator.SetBool(anim, value);
    }

    public void OnAnimationDone(string anim)
    {
        animator.SetBool(anim, false);
    }





}
