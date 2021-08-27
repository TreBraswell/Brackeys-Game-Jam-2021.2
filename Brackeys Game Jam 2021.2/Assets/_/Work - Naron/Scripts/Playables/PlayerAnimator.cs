namespace BGJ20212.Game.Naron
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        System.Action callbackFunc;
        //Animation priority - attack over jump
        private string[] animationPrioritiy = new string[] {"Walk", "Jump", "Attack"};


            
        public void SetFloat(string anim, float value)
        {
            animator.SetFloat(anim, value);
        }

        public void SetBool(string anim, bool value)
        {
            //Good
            animator.SetBool(anim, value);
        }


        public void OnAnimationDone(string anim)
        {
            animator.SetBool(anim, false);
        }

        public void SetTrigger(string anim)
        {
            animator.SetTrigger(anim);
        }
        public void CallCallBack()
        {
            if(callbackFunc != null)
            {
                callbackFunc();
                callbackFunc = null;
            }
        }
        public void SetTrigger(string anim, System.Action callback)
        {
            animator.SetTrigger(anim);

            callbackFunc = callback;
        }
    }
}
