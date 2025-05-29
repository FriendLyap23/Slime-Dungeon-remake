using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private PoolObject _poolObject;

    [field: SerializeField] public int damageCount { get; set; }

    public void Initialize(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction * _speed;

        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_lifeTime);
        _poolObject.ReturnToPool();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            _poolObject.ReturnToPool(); 
        }
    }
}
