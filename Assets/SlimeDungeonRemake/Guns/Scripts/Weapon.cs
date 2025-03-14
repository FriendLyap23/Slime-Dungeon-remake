using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    private float _cooldownTime;
    [SerializeField] private float _newCooldownTime;

    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private Transform _shootPosition;
    [SerializeField] private Pool _pool;

    public event Action OnBulletInitialize;

    private void Update()
    {
        if (_cooldownTime > 0)
            _cooldownTime -= Time.deltaTime;

        if (Input.GetAxis("Fire1") > 0 && _cooldownTime <= 0)
        {
            OnBulletInitialize?.Invoke();
            TurningBullet();
            _cooldownTime = _newCooldownTime;
        }
    }

    public void TurningBullet()
    {
        Quaternion bulletRotation = Quaternion.Euler(0, 0, _shootPosition.eulerAngles.z);
        var element = _pool.GetFreeElement(_shootPosition.position, _bulletPrefab, bulletRotation);
        element.GetComponent<Bullet>().Initialize(_shootPosition.right);
    }
}
