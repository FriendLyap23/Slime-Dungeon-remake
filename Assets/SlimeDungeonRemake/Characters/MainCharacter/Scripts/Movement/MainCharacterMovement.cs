using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MainCharacterMovement : Character, IDashable
{
    public bool _facingRight { get; set; } = true;

    public bool _canDash { get; set; } = true;
    public bool _isDashing { get; set; }

    [field:SerializeField] public float _dashingPower { get; set; }
    [field:SerializeField] public float _dasgingTime { get; set; }
    [field:SerializeField] public float _dashingCooldown { get; set; }

    [field:SerializeField] public TrailRenderer _trailRenderer { get; set; }

    public Action DashEvent;
    public Action WalkEvent;
    public Action IDLEEvent;

    private void Start()
    {
        _rigidbody ??= GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isDashing)
            return;

        MoveCharacter();

        if (Input.GetButtonDown("Dash") && _canDash)
        {
            StartCoroutine(DashCharacter());
        }
    }

    public override void MoveCharacter()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        _direction = new Vector2(moveHorizontal, moveVertical);
        _rigidbody.velocity = Vector2.ClampMagnitude(_direction, 1) * _speed;

        if (Mathf.Abs(moveHorizontal) > 0.1f || Mathf.Abs(moveVertical) > 0.1f)
            WalkEvent?.Invoke();
        else
            IDLEEvent?.Invoke();

        if (moveHorizontal > 0 && !_facingRight) 
            FlipCharacter();

        if (moveHorizontal < 0 && _facingRight)
            FlipCharacter();
    }

    public override void FlipCharacter()
    {
        _facingRight = !_facingRight;

        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        foreach (Transform child in transform)
        {
            child.localScale = new Vector3(child.localScale.x * -1, child.localScale.y, child.localScale.z);
        }
    }

    public IEnumerator DashCharacter()
    {
        _canDash = false;
        _isDashing = true;

        DashEvent?.Invoke();
        _rigidbody.velocity = _direction.normalized * _dashingPower;
        _trailRenderer.emitting = true;
        yield return new WaitForSeconds(_dasgingTime);

        _trailRenderer.emitting = false;
        _isDashing = false;
        yield return new WaitForSeconds(_dashingCooldown);
        _canDash = true;
    }
}


