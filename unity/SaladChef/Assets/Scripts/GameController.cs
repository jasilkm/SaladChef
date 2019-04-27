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
    public WaitPlateController waitPlateController1;
    public WaitPlateController waitPlateController2;
    public TopTenListController topTenListController;
    #endregion
    #region protected properties
    #endregion
    #region private methods
    #endregion

    #region unity methods

    void Start()
    {
        // the evenrt will receive when a player timer reached 0 so his movemnet will restricted
        timerController.PlayerTimeCompleted += new EventHandler<TImeCompletedEventArgs>(TimerController_TimeCompleted);
        // when both player time has completed
        timerController.GameCompleted += new EventHandler<GameOverEventArgs>(TimerController_GameCompleted);
        // player restrated gamefrom game over screen
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
        // enable the player
        playerMasterController.EnablePlayers();
        // initiliazing hud
        hudController.Init();
        // generating customer and the call back will update user score
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
        hudController.GetWinnerScoreAndPlayer((player1, player2) => {

            playerMasterController.ResetPlayer();
            gameOverController.Show(player1, player2);
            topTenListController.UpdateTopTenList(player1, player2);
            waitPlateController1.ResetPlate();
            waitPlateController2.ResetPlate();
            customerController.StopSpawning();

        });

    }

    void GameOverController_GameRestarted(object sender, EventArgs args)
    {
        playerMasterController.ResetPlayer();
        hudController.ResetGame();
        StartGame();
    }


}
