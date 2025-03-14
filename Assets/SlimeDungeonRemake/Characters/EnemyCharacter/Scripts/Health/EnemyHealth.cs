using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _healthEnemy;
    [SerializeField] private PoolObject _poolObject;

    public int TakeDamage(int damage)
    {
        _healthEnemy -= damage;
        Debug.Log($"Жизней оталось {_healthEnemy}");

        if (_healthEnemy <= 0)
            Destroy(gameObject);

        return _healthEnemy;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Bullet bullet = other.gameObject.GetComponent<Bullet>();

        if (bullet != null) 
            TakeDamage(bullet.damageCount);
    }
}
