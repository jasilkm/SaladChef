using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class CustomerController : MonoBehaviour
{
    #region Private properties
    [SerializeField] private Customer customer;
    private readonly int _maxCustomerToSpwan = 4;
    private float _minTime = 3f;
    private float _maxTime = 10.0f;
    private PlayerController _playerController { get; set; }
    [SerializeField] private Transform[] spwanPoints;
    private Customer _customer;
    private Action<int, int> _compareCompletedHandler;
    [SerializeField] private HudController hudController;
    private Coroutine _coroutine;
    private List<Customer> _spawnedList;
    private float _bonusScore = 20;
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

        foreach (var item in playerPickedVeg)
        {
            pp.Add((int)item.saladIngredients);
        }

        foreach (var item in consumerRequestedVeg)
        {
            cr.Add((int)item.saladIngredients);
            Debug.Log("item :"+ item.winPoints);
        }
        IEnumerable<int> differenceQuery = pp.Except(cr);

        foreach (int s in differenceQuery)
            Debug.Log("Not Matched" + (SaladIngredients)s);

        if (differenceQuery.Count() > 0)
        {
            _compareCompletedHandler(0,_playerController.PlayerID);
            //completedHandler(false);
            var sum = consumerRequestedVeg.Sum(x => x.winPoints);
            _compareCompletedHandler(-sum, _playerController.PlayerID);

        }
        else
        {
            var sum = consumerRequestedVeg.Sum(x => x.winPoints);
            _compareCompletedHandler(sum, _playerController.PlayerID);
            _customer.Destroy();
            _playerController.RemoveVegetable();
            _playerController._isSliceAdded = false;
            hudController.ClearCollectedVeg(_playerController.PlayerID);
            hudController.UpdateBonusTime(_playerController.PlayerID,_bonusScore);
        }
    }

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
        var list = customerSpwanPonts.Where(x => x._isSpwaned == false).ToList<CustomerSpwan>();
        if (list.Count > 0)
        {
            CustomerSpwan cs = list[UnityEngine.Random.Range(0, list.Count)];
            cs._isSpwaned = true;
           Customer cus = Instantiate(customer, cs.customerSpwanPoint);
            _spawnedList.Add(cus);
            var vegList = vegetablesController.GenerateCustomersSaladIngrediants();
            cus.Init(vegList,cs.customerSpwanPoint,
                ()=> {
                    cs._isSpwaned = false;
                   cus.Destroy();
                },
                   (customerRequested, playerGiven, playerController,cust)=> {
                     _customer = cust;
                    _playerController = playerController;
                   
                    CompareVegetables(customerRequested, playerGiven);
            });
        }
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
            item.Destroy();
        }
    }

    #endregion
}


public class CustomerSpwan {
   public Transform customerSpwanPoint;
   public bool _isSpwaned = false;

}