using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void EnablePlayers()
    {
        Player1.EnablePlayer();
        Player2.EnablePlayer();
    }

    public void DisablePlayers()
    {
        Player1.DisablePlayer();
        Player2.DisablePlayer();
    }
}
