using System;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public SwitchGun switchGun;

    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _minCapacity;
    [SerializeField] private int _maxCapacity;

    [Space(10)]
    [SerializeField] private Transform _container;

    [Space(10)]
    [SerializeField] private bool _autoExpand;

    private List<PoolObject> _pool;

    private void OnValidate()
    {
        if (_autoExpand)
            _maxCapacity = Int32.MaxValue;
    }

    private void OnEnable()
    {
        switchGun.ChangeGun += CreatePoolHandler;
    }

    private void OnDisable()
    {
        _pool.Clear();
        switchGun.ChangeGun -= CreatePoolHandler;
    }

    private void CreatePoolHandler()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        _pool = new List<PoolObject>(_minCapacity);

        for (int i = 0; i < _minCapacity; i++)
            CreateElement(_prefab);
    }

    private PoolObject CreateElement(GameObject prefab, bool isActivebyDefault = false)
    {
        var createdObject = Instantiate(prefab, _container).GetComponent<PoolObject>();
        createdObject.gameObject.SetActive(isActivebyDefault);

        _pool.Add(createdObject);
        return createdObject;
    }

    public bool TryGetElement(out PoolObject element)
    {
        foreach (var item in _pool)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                element = item;
                item.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public PoolObject GetFreeElement(Vector3 position, GameObject bulletPrefab, Quaternion quaternion)
    {
        if (TryGetElement(out var element))
        {
            element.transform.position = position;
            element.transform.rotation = quaternion;
            element.SetPrefab(bulletPrefab);
            return element;
        }

        if (_autoExpand)
            return CreateElement(bulletPrefab, true);

        if (_pool.Count < _maxCapacity)
        {
            return CreateElement(bulletPrefab, true);
        }

        throw new Exception("Pool is over!");
    }
}
