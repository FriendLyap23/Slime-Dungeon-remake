using UnityEngine;

public class Character : MonoBehaviour
{
    public Vector2 _direction { get; set; }
    public Rigidbody2D _rigidbody { get; set; }

    [field: SerializeField] public float _speed { get; set; } = 10;

    public virtual void MoveCharacter() { }
    public virtual void FlipCharacter() { }
}
