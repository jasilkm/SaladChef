using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class PlayerMasterController : MonoBehaviour
{
    public PlayerController Player1;
    public PlayerController Player2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// this method with compare user requested veg and Player Choosed
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
}
