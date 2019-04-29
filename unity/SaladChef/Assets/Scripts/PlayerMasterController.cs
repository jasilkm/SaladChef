using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class PlayerMasterController : MonoBehaviour
{
    public PlayerController Player1;
    public PlayerController Player2;

    private Coroutine _bonusTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// this method with compare user requested veg and Player given
    /// </summary>
    /// <param name="playerPickedVeg">Player picked Vegitable </param>
    /// <param name="consumerRequestedVeg"> consumer requested veg</param>
    public void CompareVegetables(List<Vegetable> playerPickedVeg, List<Vegetable> consumerRequestedVeg, Action<bool> completedHandler)
    {
        List<int> pp = new List<int>();
        List<int> cr = new List<int>();

        foreach (var item in playerPickedVeg)
        {
            pp.Add((int)item.saladIngredients);
        }

        foreach (var item in consumerRequestedVeg)
        {
            cr.Add((int)item.saladIngredients);
        }
        IEnumerable<int> differenceQuery = pp.Except(cr);

        foreach (int s in differenceQuery)
            Debug.Log("Not Matched" + (SaladIngredients)s);

        if (differenceQuery.Count() > 0)
        {
            completedHandler(false);
        }
        else
        {
            completedHandler(true);
        }
    }

    // Update Player speed when Player has collected Power up
    public void UpdateSpeed(Players player)
    {
        if (player == Players.player1)
            Player1.Speed = SaladChefConstants.PLAYER_BONUS_SPEED;
        else if (player == Players.player2)
            Player2.Speed = SaladChefConstants.PLAYER_BONUS_SPEED;
        // _bonusTime = StartCoroutine(setSpeed(player) );

        StartCoroutine("setSpeed", player);
    }

    // When a speed Power up has collected  wll start this IEnumerator to slow down when desired time is achived
    IEnumerator setSpeed(Players player)
    {
        Debug.Log("Speed increased");


        yield return new WaitForSeconds(20);

        Debug.Log("Speed reduced");
        if (player == Players.player1)
            Player1.Speed = SaladChefConstants.PLAYER_SPEED;
        else if (player == Players.player2)
            Player2.Speed = SaladChefConstants.PLAYER_SPEED;

    }

    public void EnablePlayers()
    {
        Player1.EnablePlayer();
        Player2.EnablePlayer();
    }

    public void DisablePlayers(int playerID)
    {

        if (playerID == 1)
        {
            Player1.DisablePlayer();
        }
        else
        {
            Player2.DisablePlayer();
        }

    }
    // Reset all 
    public void ResetPlayer()
    {
        Player1.EnablePlayer();
        Player2.EnablePlayer();
        Player1.RemoveVegetable();
        Player2.RemoveVegetable();
        Player1.RemoveSlicedVegetables();
        Player2.RemoveSlicedVegetables();
        if (_bonusTime != null)
        {
            StopCoroutine(_bonusTime);
        }
        Player1.transform.localPosition = new Vector3(-5.55f, 0, 0);
        Player2.transform.localPosition = new Vector3(5.55f, 0, 0);
    }
}
