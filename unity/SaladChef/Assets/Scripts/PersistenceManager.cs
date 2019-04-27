using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class PersistenceManager : MonoBehaviour
{
    private static PersistenceManager s_sharedInstance;
    private readonly string TOP_TEN_LIST = "toptenlist";       
    #region  Unity  Callbacks
    void Awake()
    {
        DontDestroyOnLoad(this);
        s_sharedInstance = this;
    }
    // Use this for initialization
    void Start()
    {
      //PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {

    }  
    #endregion

    #region Public Methods

    public static PersistenceManager GetSharedInstance()
    {
        if (s_sharedInstance == null)
        {
            throw new System.Exception("PersistenceManager not initialized.");
        }
        return s_sharedInstance;
    }

    public void SaveTopTenList(TopTenList topTenList)
    {

        string jsonObject = JsonConvert.SerializeObject(topTenList);
        PlayerPrefs.SetString(TOP_TEN_LIST,jsonObject);
        PlayerPrefs.Save();
    }

    public TopTenList GetTopTenList()
    {
       string topTenString = PlayerPrefs.GetString(TOP_TEN_LIST);
       TopTenList toptenObject = JsonConvert.DeserializeObject<TopTenList>(topTenString);
      

       return toptenObject;
    }
    #endregion
}
