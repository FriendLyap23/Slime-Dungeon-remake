using System;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGun : MonoBehaviour
{
    private int _currentWeaponIndex;
    private Dictionary<int, int> _scoreToWeaponIndexMap;
    private HashSet<int> _usedKeys;

    [SerializeField] private TimeAccount _scoreValue;

    [SerializeField] private GameObject[] _weapons;

    public event Action ChangeGun;  

    private void Start()
    {
        _currentWeaponIndex = 0;
        _scoreToWeaponIndexMap = new Dictionary<int, int>
        {
            {0, 0},
            {1, 1},
            {50, 2},
            {100, 3},
            {150, 4},
            {250, 5},
        };

        _usedKeys = new HashSet<int>();
    }

    private void Update()
    {
        foreach (var scoreWeaponPair in _scoreToWeaponIndexMap)
        {
            if (_scoreValue._timeCount >= scoreWeaponPair.Key
                && _currentWeaponIndex != scoreWeaponPair.Value && !_usedKeys.Contains(scoreWeaponPair.Value))
            {
                ChangeWeapon(scoreWeaponPair.Value);
                _usedKeys.Add(scoreWeaponPair.Value);
                ChangeGun?.Invoke();
                break;
            }
        }
    }

    private void ChangeWeapon(int weaponIndex) 
    {
        _weapons[_currentWeaponIndex].SetActive(false);
        _currentWeaponIndex = weaponIndex;
        _weapons[_currentWeaponIndex].SetActive(true);
    }
}
