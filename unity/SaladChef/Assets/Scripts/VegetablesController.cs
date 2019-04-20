using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class VegetablesController : MonoBehaviour
{



    #region private properties
    [SerializeField] private Vegetable[] _vegitables; // vegitable Object
    [SerializeField] private Transform[] _spwanPoints; // Spwaning points for Vegitable
    [SerializeField] private GameObject _progressbar;
    #endregion

    #region public properties
    #endregion

    #region Events 
    #endregion


    #region unity methods
    // Start is called before the first frame update
    void Start()
    {
        SpwanVegetables();
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion



    #region public methods
    /// <summary>
    /// Spwaning Vegitables to table basket
    /// </summary>
    public void SpwanVegetables()
    {
        int i = 0;
        foreach (Transform tr in _spwanPoints)
        {
            Instantiate(_vegitables[i], tr);
            i++;
        }
    }
    /// <summary>
    /// slicing vegitables in chopboard
    /// </summary>
    /// <param name="veg">collected vegitables</param>
    /// <param name="cutTable"> chop board transform for placing veg and Progressbar</param>
    /// <param name="slicingCompleteHandler"> this handler will callback when slicing has completed</param>
    public void SliceVegetable(List<Vegetable> veg, Transform cutTable, Action slicingCompleteHandler)
    {
        int vegOrder = 0;
        PlaceVegetablesOnChopboard(veg[vegOrder], cutTable);
        GameObject progressbar = Instantiate(_progressbar, cutTable);
        ProgressBarController progressBarController = progressbar.GetComponent<ProgressBarController>();
        int totalVegCount = veg.Count - 1;

           progressBarController.Init(veg[vegOrder].VegetableSprite, 5, () =>
            {
                if (totalVegCount == vegOrder)
                {
                    slicingCompleteHandler();
                    progressBarController.RemoveProgressBar();

                }
                else
                {
                    vegOrder++;
                    progressBarController.ResetProgress(veg[vegOrder].VegetableSprite);
                    PlaceVegetablesOnChopboard(veg[vegOrder], cutTable);
                }
                
              

            });

       
       


        
       
    }


    #endregion

    #region private methods
    /// <summary>
    /// showing veg image on chopbard
    /// </summary>
    /// <param name="veg"></param>
    /// <param name="chopBoard"></param>
    private void PlaceVegetablesOnChopboard(Vegetable veg, Transform chopBoard)
    {
        Instantiate(veg, chopBoard);
    }
    
    #endregion
    #region protected methods
    #endregion



}
