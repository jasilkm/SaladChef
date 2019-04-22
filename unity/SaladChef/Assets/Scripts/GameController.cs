using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region private properties
    #endregion
    #region public properties
    public PlayerMasterController playerMasterController;
    public CustomerController customerController;
    public VegetablesController vegetablesController;
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
        customerController.GenerateCustomers();
    }

    public void GameOver()
    {

    }

    #endregion
    #region protected methods
    #endregion
}
