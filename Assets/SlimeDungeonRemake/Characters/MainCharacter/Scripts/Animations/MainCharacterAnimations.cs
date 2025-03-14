using UnityEngine;

public class MainCharacterAnimations : MonoBehaviour
{
    [SerializeField] private MainCharacterMovement _characterMovement;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _characterMovement = GetComponent<MainCharacterMovement>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _characterMovement.WalkEvent += WalkAnimation;
        _characterMovement.IDLEEvent += IDLEAnimation;
        _characterMovement.DashEvent += DashAnimation;
    }

    private void OnDisable()
    {
        _characterMovement.WalkEvent -= WalkAnimation;
        _characterMovement.IDLEEvent -= IDLEAnimation;
        _characterMovement.DashEvent -= DashAnimation;
    }

    private void IDLEAnimation()
    {
        _animator.ResetTrigger("isWalking");
        _animator.ResetTrigger("isRolling");
        _animator.SetTrigger("isIDLE");
    }

    private void WalkAnimation()
    {
        _animator.ResetTrigger("isIDLE");
        _animator.ResetTrigger("isRolling");
        _animator.SetTrigger("isWalking");
    }

    private void DashAnimation()
    {
        _animator.ResetTrigger("isWalking");
        _animator.ResetTrigger("isIDLE");
        _animator.SetTrigger("isRolling");
    }
}

