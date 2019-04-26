using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayeTopScoreItem : MonoBehaviour
{

    public Text Player1Score;
    public Text Player2Score;
    // Start is called before the first frame update


   public void UpdateScore(int player1, int player2)
    {
        Player1Score.text = player1.ToString();
        Player2Score.text = player2.ToString();
    }
}
