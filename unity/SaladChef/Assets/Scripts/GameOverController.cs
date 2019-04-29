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

    public void Show(int player1, int player2)
    {
        _gameOverPanel.SetActive(true);
        GameOverScreenUpdate(player1, player2);
    }

    public void Hide()
    {
        _gameOverPanel.SetActive(false);
    }

    private void GameOverScreenUpdate(int player1, int player2)
    {
         GetWinnerAndScore(player1, player2, (playerName,score)=>{
            _winnerText.text = playerName;
            _scoreText.text = score.ToString();
        });

       
    }

    private void GetWinnerAndScore(int player1, int player2 , Action <string, int>  winnerAndScore)
    {
      
        /// If both player has earned same score game will be TIE
            if (player1 == player2)
            {
            // in case Both player has not  Earned  we wont show any score on game over
                if (player1 == 0)
                {
                    winnerAndScore("-", 0);
                }
                else
                {
                    winnerAndScore("Game TIE", player2); // If both player Earned same score we show messges as TIE
                }
            }
            else
            {
                // Calculating Will score
                Players winner = player1 > player2 ? Players.player1 : Players.player2;
                int score = player1 > player2 ? player1 : player2;
                // Updating Score uising Action delegates
                winnerAndScore(winner.ToString(), score);
            }
       

       

    }

    // Hae Restart Button Fired
    public void OnRestartSelected()
    {
        GameRestarted?.Invoke(this,EventArgs.Empty);
        Hide();
    }
}
