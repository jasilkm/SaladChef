using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class TimerController : MonoBehaviour
{
    #region public properties
    public float PlayerOneGameTime { get; set; }
    public float PlayerTwoGameTime { get; set; }
    public bool IsGamePlaying { get; set; }


    public Text PlayerOneText;
    public Text PlayerTwoText;

    #endregion
    #region protected properties
    public event EventHandler<TImeCompletedEventArgs> PlayerTimeCompleted;
    public event EventHandler<GameOverEventArgs> GameCompleted;


    #endregion
    #region private methods
    private float elapsedTime;
    private float _playerUsedTime = 0;
    
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
        PlayerOneGameTime = 50;
        PlayerTwoGameTime = 50;
        IsGamePlaying = true;
        StartCoroutine("CountDown");
    }

    // Count Down
    IEnumerator CountDown()
    {
        if (IsGamePlaying)
        {
            while (PlayerOneGameTime > 0)
            {
                yield return new WaitForSeconds(1);
                PlayerOneGameTime--;
                PlayerTwoGameTime--;
                PlayerOneText.text = PlayerOneGameTime.ToString();
                PlayerTwoText.text = PlayerTwoGameTime.ToString();

                if (PlayerOneGameTime <= 0)
                {
                    OnTimeCompleted((int)Players.player1);
                }

                if (PlayerTwoGameTime <= 0)
                {
                    OnTimeCompleted((int)Players.player2);
                }

                if (PlayerOneGameTime == 0 && PlayerTwoGameTime == 0)

                    
                      OnGameOver();

            }

            while (PlayerTwoGameTime > 0)
            {
                yield return new WaitForSeconds(1);
                PlayerTwoGameTime--;
                PlayerTwoText.text = PlayerTwoGameTime.ToString();

               

                if (PlayerTwoGameTime <= 0)
                {
                    OnTimeCompleted((int)Players.player2);
                }

                if (PlayerOneGameTime == 0 && PlayerTwoGameTime == 0)


                    OnGameOver();

            }
        }
    }

    public void ResetGame()
    {
        PlayerOneGameTime = 300;
        PlayerTwoGameTime = 300;
    }

    #endregion
    #region protected  methods
    protected void OnTimeCompleted(int PlayerId)
    {
        TImeCompletedEventArgs args = new TImeCompletedEventArgs();
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


public class TImeCompletedEventArgs : System.EventArgs
{
    public int PlayerId { get; set; }
}

public class GameOverEventArgs : System.EventArgs
{
    public int PlayerId { get; set; }
    public int Score { get; set; }
}