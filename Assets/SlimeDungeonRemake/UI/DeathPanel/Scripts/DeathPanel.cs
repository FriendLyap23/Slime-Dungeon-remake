using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private HealthMainCharacter _healthMainCharacter;
    [SerializeField] private TimeAccount _timeAccount;

    [SerializeField] private TMP_Text _stats;

    public bool _isPlayerDead { get; set; }

    private void Awake()
    {
        _healthMainCharacter.OnDeathCharacter += () => StartCoroutine(TurnPanel());
        _isPlayerDead = false;
    }

    private IEnumerator TurnPanel() 
    {
        Time.timeScale = 0;
        _panel.SetActive(true);
        _isPlayerDead = true;

        _stats.text = "Ваша статистика:\n" +
            $"Текущее время: {Convert.ToInt32(_timeAccount._timeCount)}\n" +
            $"Лучшее время: {Convert.ToInt32(_timeAccount._bestTimeCount)}";

        yield return new WaitForSecondsRealtime(2);

        while (_isPlayerDead)
        {
            if (Input.anyKey)
            {
                _panel.SetActive(false);
                PlayerPrefs.SetFloat("bestTime", _timeAccount._bestTimeCount);
                PlayerPrefs.Save();
                Time.timeScale = 1;
                SceneManager.LoadScene("MainScene");

                _isPlayerDead = false; 
            }
            yield return null; 
        }
    }

    private void OnDisable()
    {
        _healthMainCharacter.OnDeathCharacter -= () => StartCoroutine(TurnPanel());
    }
}
