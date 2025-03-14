using System;
using UnityEngine;

public class TimeAccount : MonoBehaviour
{
    public float _timeCount { get; set; }
    public float _bestTimeCount { get; set; }

    public event Action OnTimeTextChange;
    public event Action OnBestTimeTextChange;

    private void Start()
    {
        _bestTimeCount = PlayerPrefs.GetFloat("bestTime", 0);
    }

    private void Update()
    {
        _timeCount += Time.deltaTime;
        OnTimeTextChange?.Invoke();
        OnBestTimeTextChange?.Invoke();

        if (_timeCount > _bestTimeCount)
            UpdateBestTimeCount();
    }

    public void UpdateBestTimeCount()
    {
        _bestTimeCount = _timeCount;
        OnBestTimeTextChange?.Invoke();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("bestTime", _bestTimeCount);
        PlayerPrefs.Save();
    }
}
