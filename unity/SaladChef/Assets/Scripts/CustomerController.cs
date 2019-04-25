using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class CustomerController : MonoBehaviour
{
    #region Private properties
    [SerializeField] private Customer customer; // Customer Object Reference
    private float _minTime = 3f; // min time for spwan
    private float _maxTime = 8.0f; // max time for spwan
    private PlayerController _playerController { get; set; }
    [SerializeField] private Transform[] spwanPoints; // spwan points reference
    private Customer _customer;
    private Action<int, int> _compareCompletedHandler; // comapre  completed handeler 
    [SerializeField] private HudController hudController;
    private Coroutine _coroutine;
    private List<Customer> _spawnedList;
    private float _bonusTime = 20; // When  a correct combination  has give will eligibale to get a Bonus time
    private int _finedScore = -10; // When a wrong combination has given will lead to minus score
    #endregion

    #region Public properties
    public List<CustomerSpwan> customerSpwanPonts = new List<CustomerSpwan>();
    public VegetablesController vegetablesController;
    
    #endregion



    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        AddSpwanPoints();
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region public methods
    /// <summary>
    /// this method with compare user requested veg and Player Choosed
    /// </summary>
    /// <param name="playerPickedVeg">Player picked Vegitable </param>
    /// <param name="consumerRequestedVeg"> consumer requested veg</param>
    private void CompareVegetables(List<Vegetable> playerPickedVeg, List<Vegetable> consumerRequestedVeg)
    {
        List<int> pp = new List<int>();
        List<int> cr = new List<int>();
        // sald ingerdiant converting to int formate will help for easy comparison
        foreach (var item in playerPickedVeg)
        {
            pp.Add((int)item.saladIngredients);
        }

        // sald ingerdiant converting to int formate will help for easy comparison
        foreach (var item in consumerRequestedVeg)
        {
            cr.Add((int)item.saladIngredients);
          //  Debug.Log("item :"+ item.winPoints);
        }
        // Quesry for comparing   customer requested and player provided
        IEnumerable<int> differenceQuery = pp.Except(cr);

        foreach (int s in differenceQuery)
            Debug.Log("Not Matched" + (SaladIngredients)s);

        // if difference > 0 means  requested and given is not matching this will led to minus score
        if (differenceQuery.Count() > 0)
        {
            _compareCompletedHandler(_finedScore, _playerController.PlayerID);

            // wrong combination led to half of life customer
            _customer.UpdateTime();
            //completedHandler(false);
            //   var sum = consumerRequestedVeg.Sum(x => x.winPoints);
            //   _compareCompletedHandler(-sum, _playerController.PlayerID);

        }
        else
        {
            // getting sum of Winpoints want to awareded for player
            var sum = consumerRequestedVeg.Sum(x => x.winPoints);
            //sending compare result to Game controller
            _compareCompletedHandler(sum, _playerController.PlayerID);
            // removing customer from screen
            _customer.Destroy();
            // removing veg from player 
            _playerController.RemoveVegetable();
            // this will make sure player grab next set of veg
            _playerController._isSliceAdded = false;
            // clearing veg list from hud
            hudController.ClearCollectedVeg(_playerController.PlayerID);
            // releasing bonus time to player will add his total time
            hudController.UpdateBonusTime(_playerController.PlayerID,_bonusTime);
        }
    }
    /// <summary>
    /// stop spawing and Removing exsiting customers from screen
    /// </summary>
    public void StopSpawning()
    {

        StopCoroutine(_coroutine);
        DestoryCustomers();
    }
    /// <summary>
    /// Generate customers in random time
    /// </summary>
    /// <param name="compareCompletedHandler"></param>
    public void GenerateCustomers(Action<int,int> compareCompletedHandler)
    {
        _spawnedList = new List<Customer>();
        _compareCompletedHandler = compareCompletedHandler;
      //  _coroutine = SpawnRandomCustomer();
        _coroutine = StartCoroutine(SpawnRandomCustomer());
       
    }

    IEnumerator SpawnRandomCustomer()
    {
        // make sure there is no customer spwned on specified loacation
        var list = customerSpwanPonts.Where(x => x._isSpwaned == false).ToList<CustomerSpwan>();
        if (list.Count > 0)
        {
            // getting spawan point from random position
            CustomerSpwan cs = list[UnityEngine.Random.Range(0, list.Count)];
            // makesure respected spwan point occupaid by a customer so there wont be a overlap
            cs._isSpwaned = true;
            // insatiate customer in screen
           Customer cus = Instantiate(customer, cs.customerSpwanPoint);
            // adding spwaned customer to list this use for delete the objects later
            _spawnedList.Add(cus);
            // creating customer request combination 
            var vegList = vegetablesController.GenerateCustomersSaladIngrediants();
            // initialize  customer and start progressbar also passing his  requeset veg list and his spawing position
           
            cus.Init(vegList,cs.customerSpwanPoint,
                ()=> {
                    // if  time has finished Action call back will trigger and it will delete 
                    cs._isSpwaned = false;
                   cus.Destroy();
                },
                   (customerRequested, playerGiven, playerController,cust)=> {  /// this arguents will recive from customer such as customer requested, player has given to customer 
                     _customer = cust;
                    _playerController = playerController;
                   
                    CompareVegetables(customerRequested, playerGiven);
            });
        }
        // creating customer in random time
        yield return new WaitForSeconds(UnityEngine.Random.Range(_minTime,_maxTime));
        _coroutine = StartCoroutine(SpawnRandomCustomer());
    }

    #endregion
    #region private methods
    /// <summary>
    /// Spwan Points added in to List
    /// </summary>
    private void AddSpwanPoints()
    {
        for (var i = 0; i < spwanPoints.Length; i++)
        {
            CustomerSpwan cs = new CustomerSpwan();
            cs.customerSpwanPoint = spwanPoints[i];
            cs._isSpwaned = false;
            customerSpwanPonts.Add(cs);
            }
    }

    // Destory customer when Game Over
    private void DestoryCustomers()
    {
        foreach (var item in _spawnedList)
        {
            if (item != null)
            {
                item.Destroy();
            }
            
        }
    }

    #endregion
}


public class CustomerSpwan {
   public Transform customerSpwanPoint;
   public bool _isSpwaned = false;

}