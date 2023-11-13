using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class AnimationSwitcher : MonoBehaviour
    {
        public AnimationSwitcher(Animator animator)
        {
            _animator = animator;
        }
        private Animator _animator;
        private string _currentAnimationType;
        public void PlayAnimation(string animationType, bool active)
        {
            if (!active)
            {
                PlayAnimation(_currentAnimationType);
                return;
            }
            //_animator.ResetTrigger(animationType);
            _currentAnimationType = animationType;
            PlayAnimation(_currentAnimationType);
        }
        private void PlayAnimation(string animationType)
        {
            _animator.SetTrigger(animationType);
            //_animator.ResetTrigger(animationType);
        }
    }
}
