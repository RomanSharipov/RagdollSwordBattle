using System;
using UnityEngine;

public class PlayIdleAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private EnemyMovement _enemyMovement;
    private readonly int _idle = Animator.StringToHash("Idle");

    public void Start()
    {
        if (_enemyMovement.enabled == false)
        {
            _animator.Play(_idle);
        }
    }
}
