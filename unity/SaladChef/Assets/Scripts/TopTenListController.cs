using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class TopTenListController : MonoBehaviour
{
    public TopScoreItem topScoreItem;
    public Transform ScrollView;

    private TopTenList topTenList;
    private List<TopScoreItem> ScoreItemList = new List<TopScoreItem>();
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTopTenList(int playeroneScore, int playerotwoScore)
    {
         topTenList = PersistenceManager.GetSharedInstance().GetTopTenList();
        if (topTenList == null)
        {
            topTenList = new TopTenList();
        }
        
        if (topTenList.PlayerOneList.Count >= 10)
        {
            int playerOneMinScore = topTenList.PlayerOneList.Min();
            int playerTwoMinScore = topTenList.PlayerTwoList.Min();
            
            if (playeroneScore > playerOneMinScore)
            {
                if (!topTenList.PlayerOneList.Contains(playerOneMinScore))
                {
                    topTenList.PlayerOneList = topTenList.PlayerOneList.OrderByDescending(s => s).ToList();
                    topTenList.PlayerOneList.RemoveAt(topTenList.PlayerOneList.Count - 1);
                    topTenList.PlayerOneList.Add(playeroneScore);
                }
            }
            if (playerotwoScore> playerTwoMinScore) {
                if (!topTenList.PlayerTwoList.Contains(playerTwoMinScore))
                {
                    topTenList.PlayerTwoList = topTenList.PlayerTwoList.OrderByDescending(s => s).ToList();
                    topTenList.PlayerTwoList.RemoveAt(topTenList.PlayerTwoList.Count - 1);
                    topTenList.PlayerTwoList.Add(playerotwoScore);
                }
            }
        }
        else
        {
            if (playeroneScore > 0)
            {
                topTenList.PlayerOneList.Add(playeroneScore);
            }

            if (playerotwoScore > 0)
            {
                topTenList.PlayerTwoList.Add(playerotwoScore);
            }
          
        }
      
        ClearTopTenList();
        CreateTopTenList();
        PersistenceManager.GetSharedInstance().SaveTopTenList(topTenList);
    }

    private void ClearTopTenList()
    {
        if (ScoreItemList.Count > 0)
        {
            for (int i = 0; i < ScoreItemList.Count; i++)
            {
                Destroy(ScoreItemList[i].gameObject);
            }
            ScoreItemList.Clear();
        }

    }

    private void CreateTopTenList()
    {
        int cnt = topTenList.PlayerOneList.Count > topTenList.PlayerTwoList.Count ? topTenList.PlayerOneList.Count : topTenList.PlayerTwoList.Count;

        topTenList.PlayerOneList=  topTenList.PlayerOneList.OrderByDescending(x => x).ToList();
        topTenList.PlayerTwoList = topTenList.PlayerTwoList.OrderByDescending(x => x).ToList();

        if (topTenList != null && cnt > 0)
        {
                
                for (int i = 0; i < cnt; i++)
            {

                var item = Instantiate(topScoreItem);
                item.transform.SetParent(ScrollView.transform);
                TopScoreItem Item = item.GetComponent<TopScoreItem>();
                ScoreItemList.Add(Item);
                if (topTenList.PlayerOneList.Count > i)
                {
                    Item.Player1Score.text = topTenList.PlayerOneList[i].ToString();
                }
                else
                {
                    Item.Player1Score.text = "-";
                }
                if (topTenList.PlayerTwoList.Count > i)
                {
                    Item.Player2Score.text = topTenList.PlayerTwoList[i].ToString();
                }
                else
                {
                    Item.Player2Score.text = "-";
                }

            }
        }

    }
}

public class TopTenList
{
    public List<int> PlayerOneList { get; set; }
    public List<int> PlayerTwoList { get; set; }

    public TopTenList()
    {
        PlayerOneList = new List<int>();
        PlayerTwoList = new List<int>();
    }
}