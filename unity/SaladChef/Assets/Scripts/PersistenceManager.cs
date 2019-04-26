using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistenceManager : MonoBehaviour
{
    private static PersistenceManager s_sharedInstance;

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
            throw new System.Exception("PersistenceManager not initialized. Please check if it is available within the scene.");
        }
        return s_sharedInstance;
    }
    #endregion
}
