using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class GameController : MonoBehaviour
{
    #region private properties
    #endregion
    #region public properties
    public PlayerMasterController playerMasterController;
    public CustomerController customerController;
    public VegetablesController vegetablesController;
    public HudController hudController;
    public TimerController timerController;
    public GameOverController gameOverController;
    #endregion
    #region protected properties
    #endregion
    #region private methods
    #endregion

    #region unity methods

    void Start()
    {
        timerController.PlayerTimeCompleted += new EventHandler<TImeCompletedEventArgs>(TimerController_TimeCompleted);
        timerController.GameCompleted += new EventHandler<GameOverEventArgs>(TimerController_GameCompleted);
        gameOverController.GameRestarted += new EventHandler<EventArgs>(GameOverController_GameRestarted);
        StartGame();
    }

    void Update()
    {
    }
    #endregion


    #region public methods
    public void StartGame()
    {
        playerMasterController.EnablePlayers();
        hudController.Init();
        customerController.GenerateCustomers((score, playerid) => {

            hudController.UpdatePlayerScore(score, playerid);

        });
    }


    #endregion


    public void GameOver()
    {

    }


    #region protected methods
    #endregion

    void TimerController_TimeCompleted(object sender, TImeCompletedEventArgs e)
    {
        playerMasterController.DisablePlayers(e.PlayerId);

    }

    void TimerController_GameCompleted (object sender, GameOverEventArgs args)
    {
        hudController.GetWinnerScoreAndPlayer((player,score)=> {

            gameOverController.Show(player.ToString(), score);
            customerController.StopSpawning();
        });

    }

    void GameOverController_GameRestarted(object sender, EventArgs args)
    {
        hudController.ResetGame();
        StartGame();
    }


}
