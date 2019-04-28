using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class TimerController : MonoBehaviour
{
    #region public properties
  
    public bool IsGamePlaying { get; set; }


    public Text PlayerOneText;
    public Text PlayerTwoText;


    #endregion
    #region protected properties
    public event EventHandler<TimeCompletedEventArgs> PlayerTimeCompleted;
    public event EventHandler<GameOverEventArgs> GameCompleted;


    #endregion
    #region private methods
    private float elapsedTime;
    private float _playerUsedTime = 0;
    
    private float _playerOneGameTime { get; set; }
    private float _playerTwoGameTime { get; set; }

    #endregion

    #region unity methods

    void Start()
    {

    }

    void Update()
    {
    }
    #endregion


    #region public methods
    public void StartTimer()
    {
        _playerOneGameTime = _playerTwoGameTime = SaladChefConstants.GAME_PLAY_TIME;
        IsGamePlaying = true;
        StartCoroutine("CountDownPlayerTwo");
        StartCoroutine("CountDownPlayerOne");
    }


    public void UpdatePlayerTime(Players players)
    {
        if (players == Players.player1)
            _playerOneGameTime += 20;
        else if (players == Players.player2)
            _playerTwoGameTime += 20;
    }
    // Count Down

    IEnumerator CountDownPlayerTwo() {

        while (_playerTwoGameTime > 0)
        {
            yield return new WaitForSeconds(1);
            _playerTwoGameTime--;
            PlayerTwoText.text = _playerTwoGameTime.ToString();
            if (_playerOneGameTime <= 0)
            {
                OnTimeCompleted((int)Players.player2);
            }
            if (_playerOneGameTime == 0 && _playerTwoGameTime == 0)
                OnGameOver();
        }
    }

    IEnumerator CountDownPlayerOne()
    {
        if (IsGamePlaying)
        {
            while (_playerOneGameTime > 0)
            {
                yield return new WaitForSeconds(1);
                _playerOneGameTime--;
                PlayerOneText.text = _playerOneGameTime.ToString();
                if (_playerTwoGameTime <= 0)
                {
                    OnTimeCompleted((int)Players.player2);
                }

                if (_playerOneGameTime == 0 && _playerTwoGameTime == 0)
                      OnGameOver();

            }

           
        }
    }

    public void ResetGame()
    {
        _playerOneGameTime = _playerTwoGameTime= SaladChefConstants.GAME_PLAY_TIME;
    }

    #endregion
    #region protected  methods
    protected void OnTimeCompleted(int PlayerId)
    {
        TimeCompletedEventArgs args = new TimeCompletedEventArgs();
        if (PlayerTimeCompleted != null)
        {
            args.PlayerId = PlayerId;
            PlayerTimeCompleted(this, args);
        }
    }
    protected void OnGameOver()
    {
        GameOverEventArgs args = new GameOverEventArgs();
        GameCompleted?.Invoke(this, args);
    }
    #endregion

}


public class TimeCompletedEventArgs : System.EventArgs
{
    public int PlayerId { get; set; }
}

public class GameOverEventArgs : System.EventArgs
{
    public int PlayerId { get; set; }
    public int Score { get; set; }
}