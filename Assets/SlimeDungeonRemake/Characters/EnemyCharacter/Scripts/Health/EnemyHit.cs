using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private int _damageCount;
    [SerializeField] private HealthMainCharacter _mainCharacter;

    private void OnEnable()
    {
        _mainCharacter = FindObjectOfType<HealthMainCharacter>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<HealthMainCharacter>())
            _mainCharacter.ChangeHealth(_damageCount);
    }
}

