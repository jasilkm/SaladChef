using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class VegetablesController : MonoBehaviour
{
    #region private properties
    [SerializeField] private Vegetable[] _vegitables; // vegitable Object
    [SerializeField] private Transform[] _spwanPoints; // Spwaning points for Vegitable
    [SerializeField] private GameObject _progressbar;
    [SerializeField] private Transform _uiRoot;
    [SerializeField] private float _sliceDuration = 4;

    private Vegetable SlicedVeg;
    private int[] _randomNumber = new int[] { 0,1,2,3,4,5};
    #endregion

    #region public properties
    public ServePlateController ServePlateController;
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
    public void SliceVegetable(List<Vegetable> veg, Transform cutTable,Action<Vegetable> slicedVeg, Action slicingCompleteHandler)
    {
        //Debug.Log(""+ cutTable.GetComponent<ChopPad>().ChopPadID);
        int vegOrder = 0;
        // placing player collected veg to chop pad for slicing
        PlaceVegetablesOnChopboard(veg[vegOrder], cutTable);
        // creating progress bar for slice time
        GameObject progressbar = Instantiate(_progressbar);
        progressbar.transform.SetParent(_uiRoot);
        // getting scree postion for progress bar
       progressbar.transform.position = SaladChefHelper.GetScreenPosition(cutTable.position.x, cutTable.position.y +.75f);

        ProgressBarController progressBarController = progressbar.GetComponent<ProgressBarController>();
        int totalVegCount = veg.Count - 1;
        // initialize progressbar  args passing slicing vef g and slice time
           progressBarController.Init(veg[vegOrder].VegetableSprite, _sliceDuration, () =>
            {
                // when player completed  slicing
                if (totalVegCount == vegOrder)
                {
                    // vegitable status changing to slice 
                    SlicedVeg._isSliced = true;
                    // this action will give call back to player so he can add own list to slice
                    slicedVeg(SlicedVeg);
                    // adding sliced veg to serveplate so user can make diffrenr combination
                    ServePlateController.AddSlicesToServePlate(SlicedVeg, cutTable.GetComponent<ChopPad>().ChopPadID);
                    vegOrder = 0;
                    // slice completed hander  let know player so he can again start o move
                    slicingCompleteHandler();
                   // remove progressbar
                    progressBarController.RemoveProgressBar();

                }
                else
                {
                   ServePlateController.AddSlicesToServePlate(SlicedVeg, cutTable.GetComponent<ChopPad>().ChopPadID);

                    // slicing Next veg if available
                    vegOrder++;
                    SlicedVeg._isSliced = true;
                    slicedVeg(SlicedVeg);
                    progressBarController.ResetProgress(veg[vegOrder].VegetableSprite);
                    PlaceVegetablesOnChopboard(veg[vegOrder], cutTable);
                }
            });
    }
    /// <summary>
    /// Generating Customer required vegetables
    /// </summary>
    /// <returns> list of vegtables/  two or three</returns>
    public List<Vegetable> GenerateCustomersSaladIngrediants()
    {
        // creating random combination
        _randomNumber = SaladChefHelper.Shuffle(_randomNumber);

        List<Vegetable> cl= new List<Vegetable>();
        for (int i = 0; i < UnityEngine.Random.Range(2,4); i++)
        {
            cl.Add(_vegitables[_randomNumber[i]]);
        }
        return cl;
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
        SlicedVeg =  Instantiate(veg, chopBoard);
        SlicedVeg.transform.localScale = Vector3.one;
        SlicedVeg.transform.localPosition = new Vector3(0,0,-2);
        SlicedVeg.transform.localRotation = Quaternion.identity;
        SlicedVeg.gameObject.SetActive(true);
    }

    
    
    #endregion
    #region protected methods
    #endregion



}
