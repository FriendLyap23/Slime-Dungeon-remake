using System;
using TMPro;
using UnityEngine;

public class TimeText : MonoBehaviour
{
    [SerializeField] private TMP_Text _bestTimeText;
    [SerializeField] private TMP_Text _timeText;

    [SerializeField] private TimeAccount _timeAccount;

    private void Awake()
    {
        _timeAccount.OnTimeTextChange += UpdateTimeText;
        _timeAccount.OnBestTimeTextChange += UpdateBestTimeCount;
    }

    private void UpdateTimeText()
    {
        _timeText.text = "Время:" + Convert.ToInt32(_timeAccount._timeCount);
    }

    public void UpdateBestTimeCount()
    {
        _bestTimeText.text = "Лучшее время:" + Convert.ToInt32(_timeAccount._bestTimeCount);
    }

    private void OnDisable()
    {
        _timeAccount.OnTimeTextChange -= UpdateTimeText;
        _timeAccount.OnBestTimeTextChange -= UpdateBestTimeCount;
    }

}
