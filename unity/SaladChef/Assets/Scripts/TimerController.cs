using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TimerController:MonoBehaviour
{
    #region private properties
    [SerializeField] private float _requiredTime;
    private float elapsedTime;
    #endregion
    #region protected properties
    #endregion
    #region private methods
    #endregion

    #region unity methods

     void Start()
    {
        
    }

     void Update()
    {
        UpdateTimer();
    }
    #endregion


    #region public methods

    public void Init(Action timerCompletedHandler)
    {

    }
   

    private void UpdateTimer()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= _requiredTime)
        {

        }
    }

    #endregion
    #region protected methods
    #endregion

}
