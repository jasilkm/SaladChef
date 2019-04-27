using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class HudController : MonoBehaviour
{
    #region private properties
    [SerializeField] private Text _playerOneTime;
    [SerializeField] private Text _playerTwoTime;
    [SerializeField] private Text _playerOneScore;
    [SerializeField] private Text _playerTwoScore;
    [SerializeField] private HorizontalLayoutGroup PlayerOneVegCollected;
    [SerializeField] private HorizontalLayoutGroup PlayerTwoVegCollected;
    private List<GameObject> _playerOneList;
    private List<GameObject> _playerTwoList;
    private Action<int, int> _gameCompleted;
    [SerializeField] private TimerController _timerController;

    #endregion
    #region public properties
    #endregion
    #region protected properties
    #endregion
    #region private methods
    private GameObject CreateImage(Sprite veg)
    {
        GameObject vegObject = new GameObject();
        Image NewImage = vegObject.AddComponent<Image>();
        NewImage.sprite = veg;
        return vegObject;
    }


    #endregion

    #region unity methods

    void Start()
    {
        _playerOneList = new List<GameObject>();
        _playerTwoList = new List<GameObject>();
        _playerOneScore.text = "300";
        }
    void Update()
    {
    }
    #endregion


    #region public methods

    public void Init()
    {
        _timerController.StartTimer();
    }

    public void ResetGame()
    {
        
        ClearHud();
        _timerController.ResetGame();
    }

    public void UpdatePlayerScore(int score,int playerId)
    {
        if (playerId == 1)
        {
            var currentScore = Convert.ToInt32(score); ;
            currentScore += score;
            if (currentScore > 0)
                _playerOneScore.text = currentScore.ToString();
            else
                _playerOneScore.text = 0.ToString();
        }
        else if (playerId == 2)
        {
            var currentScore = Convert.ToInt32(score);
            currentScore += score;

            if (currentScore > 0)
                _playerTwoScore.text = currentScore.ToString();
            else
                _playerTwoScore.text = 0.ToString();
        }
    
    }

   

    public void UpdatePlayersCollectedVeg(Sprite veg, int playerID)
    {
        GameObject vegObject = CreateImage(veg);
        if (playerID == 1)
        {
            vegObject.GetComponent<RectTransform>().SetParent(PlayerOneVegCollected.transform);
            _playerOneList.Add(vegObject);

        }
        else if (playerID == 2)
        {
            vegObject.GetComponent<RectTransform>().SetParent(PlayerTwoVegCollected.transform);
            _playerTwoList.Add(vegObject);
        }

        vegObject.GetComponent<RectTransform>().localScale = new Vector3 (0.5f,0.5f,0.5f);
       // vegObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
        vegObject.SetActive(true);
    }
    /// <summary>
    /// Remove collected veg reference  based on Player id
    /// </summary>
    /// <param name="playerID"></param>
    public void ClearCollectedVeg(int playerID)
    {
        if (playerID == 1)
        {
            foreach (var obj in _playerOneList)
            {
                Destroy(obj);
            }
            _playerOneList.Clear();
        }
        else if (playerID == 2)
        {
            
            foreach (var obj in _playerTwoList)
            {
                Destroy(obj);
            }
            _playerTwoList.Clear();
        }
    }



    public void GetWinnerScoreAndPlayer( Action <int, int> playersScore)
    {
        int playerOneScore = 0;
        int playerTwoScore = 0;
        int.TryParse(_playerOneScore.text.ToString(), out playerOneScore);
        int.TryParse(_playerTwoScore.text.ToString(), out playerTwoScore);
        //    Players player = playerOneScore > playerTwoScore ? Players.player1 : Players.player2;
        playersScore(playerOneScore , playerTwoScore);
    }

    public void UpdateBonusTime(int playerId,float time)
    {
        if ((int)playerId == 1)
            _timerController.PlayerOneGameTime += time;
        else if ((int)playerId == 2)
            _timerController.PlayerTwoGameTime += time;
    }

    private void ClearHud()
    {

      
            foreach (var obj in _playerOneList)
            {
            if (obj != null)
            {
                Destroy(obj);
            }
               
            }
           
      
            foreach (var obj in _playerTwoList)
            {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        _playerOneList.Clear();
        _playerTwoList.Clear();
        _playerOneTime.text = "0";
        _playerTwoScore.text = "0";


    }
    #endregion
    #region protected methods
    #endregion
}
