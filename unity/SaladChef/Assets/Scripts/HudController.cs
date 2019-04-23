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
    }

    void Update()
    {
    }
    #endregion


    #region public methods

    public void Init(Action timerCompletedHandler)
    {

    }

    public void UpdatePlayeOneScore(int score)
    {
        _playerOneScore.text = score.ToString(); 
    }

    public void UpdatePlayeTwoScore(int score)
    {
        _playerOneScore.text = score.ToString();
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

        vegObject.GetComponent<RectTransform>().localScale = new Vector3 (0.6f,0.6f,0.6f);
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
            _playerTwoList.Clear();
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


    public void UpdatePlayerTwoCollectedVeg()
    {

    }

    #endregion
    #region protected methods
    #endregion
}
