using System;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform _target;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private float _speed = 3;

    public event Action HitEvent;

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Vector2 direction = _target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        UpdateSpriteDirection(angle);

        Quaternion rotation = Quaternion.AngleAxis(ClampedAngle(angle), Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _speed * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }


    private void UpdateSpriteDirection(float angle)
    {
        _spriteRenderer.flipX = angle > 30 || angle < -30;
    }

    private float ClampedAngle(float angle)
    {
        return _spriteRenderer.flipX ? 0 : Mathf.Clamp(angle, -45, 45);
    }
}