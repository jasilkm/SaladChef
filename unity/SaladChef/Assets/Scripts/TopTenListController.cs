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

    // Top Ten list 

    public void UpdateTopTenList(int playeroneScore, int playerotwoScore)
    {
        // Checking any Player list store in Persistance
         topTenList = PersistenceManager.GetSharedInstance().GetTopTenList();

         // if player has playing first time he wont have any score in top ten list so we creating a new List for add
        if (topTenList == null)
        {
            topTenList = new TopTenList();
        }
        
        // We will store only 10 Score borad data for both player
        if (topTenList.PlayerOneList.Count >= 10)
        {
            // Calculating both player Minum score for replace if current score is higher than min
            int playerOneMinScore = topTenList.PlayerOneList.Min();
            int playerTwoMinScore = topTenList.PlayerTwoList.Min();
            
            if (playeroneScore > playerOneMinScore)
            {
                // Checking user earned score already have in the list 
                if (!topTenList.PlayerOneList.Contains(playerOneMinScore))
                {
                    // Adding score in last row
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
            //To ten list has less than 10 we will add if same score does not exisits
            if (playeroneScore > 0)
            {
                if (!topTenList.PlayerOneList.Contains(playeroneScore))
                    topTenList.PlayerOneList.Add(playeroneScore);
            }

            if (playerotwoScore > 0)
            {
                if (!topTenList.PlayerTwoList.Contains(playerotwoScore))
                    topTenList.PlayerTwoList.Add(playerotwoScore);
            }
          
        }
      // Clear if allready created list in Game over screen
        ClearTopTenList();
        // create score list 
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