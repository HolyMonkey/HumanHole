using UnityEngine;

namespace HumanHole.Scripts.Handlers
{
    public class TapHandHandler : MonoBehaviour
    {
        private readonly int _tapAnimationHash = Animator.StringToHash("Tap");
    
        [SerializeField] private Animator _handAnimator;
        [SerializeField] private GameObject _hand;

        public void Enable()
        {
            _hand.SetActive(true);
            PlayHandAnimation();
        }

        public void Disable()
        {
            _hand.SetActive(false);
            StopHandAnimation();
        }

        private void PlayHandAnimation() => 
            _handAnimator.SetBool(_tapAnimationHash, true);

        private void StopHandAnimation() => 
            _handAnimator.SetBool(_tapAnimationHash, false);
    }
}
