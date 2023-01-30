using UnityEngine;

namespace Interactable.Enemy
{
    public class EnemyMovementAnimateRotator : EnemyMovement
    {
        [SerializeField] private Animator _animator;
        private readonly int _rotate = Animator.StringToHash("Rotate");
        private bool _isFirstUse = true;

        protected override void LookAtNewPoint()
        {
            base.LookAtNewPoint();
            
            if (_isFirstUse)
            {
                _isFirstUse = false;
                return;
            }

            _animator.Play(_rotate);
        }
    }
}