using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class GameOverController : MonoBehaviour
{
    [SerializeField] private Text _winnerText;
    [SerializeField] private Text _scoreText;
    [SerializeField] private GameObject _gameOverPanel;

    public event EventHandler<System.EventArgs> GameRestarted;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(string winner, int score)
    {
        _gameOverPanel.SetActive(true);
        GameOverScreenUpdate(winner, score);
    }

    public void Hide()
    {
        _gameOverPanel.SetActive(false);
    }

    private void GameOverScreenUpdate(string winner, int score)
    {
        if (score > 0)
        {
            _winnerText.text = winner.ToString();
            _scoreText.text = _scoreText.ToString();
        }
        else {
            _winnerText.text = "-";
            _scoreText.text = "-";
        }
    }

    public void OnRestartSelected()
    {
        GameRestarted?.Invoke(this,EventArgs.Empty);
        Hide();
    }
}
