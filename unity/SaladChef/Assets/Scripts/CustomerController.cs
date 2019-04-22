using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class CustomerController : MonoBehaviour
{
    #region Private properties
    [SerializeField] private Customer customer;
    private readonly int _maxCustomerToSpwan = 4;
    private float _minTime = 3f;
    private float _maxTime = 10.0f;
    [SerializeField] private Transform[] spwanPoints;
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
    public void GenerateCustomers()
    {
        StartCoroutine("SpawnRandomCustomer");
    }

    IEnumerator SpawnRandomCustomer()
    {
        var list = customerSpwanPonts.Where(x => x._isSpwaned == false).ToList<CustomerSpwan>();
        if (list.Count > 0)
        {
            CustomerSpwan cs = list[Random.Range(0, list.Count)];
            cs._isSpwaned = true;
            Customer cm = Instantiate(customer, cs.customerSpwanPoint);
            var vegList = vegetablesController.GenerateCustomersSaladIngrediants();
            cm.Init(vegList,cs.customerSpwanPoint,()=> {
                cs._isSpwaned = false;
                cm.Destroy();
            });
        }
        yield return new WaitForSeconds(Random.Range(_minTime,_maxTime));
        StartCoroutine("SpawnRandomCustomer");
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

    #endregion
}


public class CustomerSpwan {
   public Transform customerSpwanPoint;
   public bool _isSpwaned = false;

}