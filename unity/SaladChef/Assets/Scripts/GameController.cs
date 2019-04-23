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
    #endregion
    #region protected properties
    #endregion
    #region private methods
    #endregion

    #region unity methods

    void Start()
    {
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
}
