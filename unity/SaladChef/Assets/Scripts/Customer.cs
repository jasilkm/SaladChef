using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class Customer : MonoBehaviour
{
    #region Private properties
    [SerializeField]private SpriteRenderer []vegetables; // available vegitable sprite list
    private GameObject UIRoot; // Ui root canvas referenc for adding Ui elements
    private Action<int> _timeCompletedHandler;
    private Action<List<Vegetable>, List<Vegetable>, PlayerController,Customer > _playerHitted; // if a player given to sald this will trigger and passing requred info to parent controller
    private GameObject _progressBar;
    private float _lifeTime { get; set; }
    private int _gainedScore { get; set; }
    private const float PickUpEligiblePercentage = 30f;
    #endregion
    #region Public properties
    public GameObject ProgressBar;
    //Customer requested vegetables
    public List<Vegetable> CustomerRequestedVeg { get; set; }
    #endregion
   



    #region Unity Methods
    private void Awake()
    {
        // Getting UIRoot for UI
        UIRoot = GameObject.FindGameObjectWithTag("UIRoot");
    }

    #endregion
    #region private Methods
    // calculating  Bonus Percentage
    public bool CalcuclateBonusEligible()
    {
        float getElapsedTimePercentage = _progressBar.GetComponent<ProgressBarController>().CalculateElapsedTimePercentage();
        return getElapsedTimePercentage <= PickUpEligiblePercentage ? true : false;


    }


    /// <summary>
    /// This will trigger when player deliver salad to customer 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // make sure  Player has sliced veg
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player.SlicedVegetables.Count>0) {
                _playerHitted(CustomerRequestedVeg, player.SlicedVegetables, player, this);
            }
         
           
        }
    }

    // customer wating time progressbar
    private void CreateProgressBar(Transform pos)
    {
        _progressBar =  Instantiate(ProgressBar);
       _progressBar.transform.SetParent(UIRoot.transform);
       _progressBar.transform.position = SaladChefHelper.GetScreenPosition(pos.position.x, pos.position.y+1.5f);
        ProgressBarController pc = _progressBar.GetComponent<ProgressBarController>();
        pc.Init(_lifeTime, () => {

            _timeCompletedHandler(_gainedScore);
        });
    }
    #endregion
    #region public  Methods
 /// <summary>
 /// Initialization for Customer. It has call back Action for comapre  Player picked itmes and Customer Request Items. Call back method will recive by customermaincontroller
 /// </summary>
 /// <param name="veg"> List of vegitable </param>
 /// <param name="pos"></param>
 /// <param name="timeCompletedHandler"></param>
 /// <param name="playerHitted"></param>
    public void Init(List<Vegetable> veg,Transform pos, Action<int> timeCompletedHandler, Action<List<Vegetable>, List<Vegetable>,PlayerController,Customer> playerHitted)
    {
        // Waiting period for a customer
        _lifeTime = veg.Sum(x=>x.weightage);
        _gainedScore = veg.Sum(x => x.winPoints);
        _playerHitted = playerHitted;
        CustomerRequestedVeg = veg;
        int i = 0;
        foreach (var item in veg)
        {
            vegetables[i].sprite = item.VegetableSprite;
            i++;
        }
        _timeCompletedHandler = timeCompletedHandler;
        CreateProgressBar(pos);
    }

    /// <summary>
    /// Destory Customer Object
    /// </summary>
    public void Destroy()
    {
        Destroy(_progressBar);
        Destroy(this.gameObject);
    }

    public void UpdateTime()
    {
        ProgressBarController pb = _progressBar.GetComponent<ProgressBarController>();
        pb.TimeIncreaseFactor = 0.05f;
    }

    #endregion

}

