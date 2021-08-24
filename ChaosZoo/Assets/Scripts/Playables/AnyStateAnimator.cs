using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AnyStateAnimator : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private Dictionary<string, AnyStateAnimation> animations = new Dictionary<string, AnyStateAnimation>();


    private string currentAnimationLegs = string.Empty;
    private string currentAnimationBody = string.Empty;
    private string currentAnimationArms = string.Empty;

    public string[] callBackables = new string[] { "Attack" };

    public Animator Animator { get => animator; }

    private void Awake()
    {
        this.animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Animate();


    }

    public void CreateAnimations(RIG rig, params string[] animsToAdd)
    {
        AnyStateAnimation[] newAnimations = new AnyStateAnimation[animsToAdd.Length];
        List<string> higerPriorityAnimations = new List<string>();

        for (int i = 0; i < animsToAdd.Length; i++)
        {


            newAnimations[i] = new AnyStateAnimation(rig, animsToAdd[i], higerPriorityAnimations.ToArray());

            higerPriorityAnimations.Add(animsToAdd[i]);
        }

        AddAnimations(newAnimations);
    }
    public void AddAnimations(params AnyStateAnimation[] newAnimations)
    {

        for (int i = 0; i < newAnimations.Length; i++)
        {

            this.animations.Add(newAnimations[i].name, newAnimations[i]);
        }
    }

    //To hoold callbacks
    System.Action callBackHolder = delegate { };
    public void TryPlayAnimation(string newAnimation, System.Action callback = null)
    {
        if (callback != null)
        {
            callBackHolder = callback;
        }

        switch (animations[newAnimation].AnimationRig)
        {
            case RIG.BODY:
                PlayAnimation(ref currentAnimationBody);
                break;

            case RIG.LEGS:
                PlayAnimation(ref currentAnimationLegs);
                break;
            case RIG.ARMS:

                PlayAnimation(ref currentAnimationArms);
                break;
            default:
                break;

        }
        void PlayAnimation(ref string currentAnimation)
        {

            if (currentAnimation == "")
            {
                animations[newAnimation].active = true;
                currentAnimation = newAnimation;
            }

            else if (currentAnimation != newAnimation && !animations[newAnimation].higherPrio.Contains(currentAnimation) || !animations[currentAnimation].active)
            {
                animations[currentAnimation].active = false;
                currentAnimation = newAnimation;
                animations[newAnimation].active = true;
            }

        }
    }


    private void Animate()
    {
        foreach (string key in animations.Keys)
        {
            animator.SetBool(key, animations[key].active);
        }
    }

    public void OnAnimationDone(string animation)
    {
        animations[animation].active = false;

        if (callBackHolder != null && callBackables.Contains(animation))
        {

            callBackHolder();
            callBackHolder = null;
        }
    }





}

