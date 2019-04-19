using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetablesController : MonoBehaviour
{



    #region private properties
    [SerializeField] private Vegetable[] _vegitables; // vegitable Object
    [SerializeField] private Transform[] _spwanPoints; // Spwaning points for Vegitable
    #endregion

    #region public properties
    #endregion

    #region Events 
    #endregion


    #region unity methods
    // Start is called before the first frame update
    void Start()
    {
        SpwanVegetable();
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
    public void SpwanVegetable()
    {
        int i = 0;
        foreach (Transform tr in _spwanPoints)
        {
            Instantiate(_vegitables[i], tr);
            i++;
        }
    }
    #endregion

    #region private methods
    #endregion
    #region protected methods
    #endregion



}
