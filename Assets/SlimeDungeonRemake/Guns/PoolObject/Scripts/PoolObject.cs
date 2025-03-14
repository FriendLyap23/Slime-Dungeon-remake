using UnityEngine;

public class PoolObject : MonoBehaviour
{
    private GameObject _currentPrefab;

    public void SetPrefab(GameObject newPrefab) 
    {
        if (_currentPrefab != newPrefab)
            _currentPrefab = newPrefab;
    }

    public void ReturnToPool() 
    {
        gameObject.SetActive(false);
    }
}
