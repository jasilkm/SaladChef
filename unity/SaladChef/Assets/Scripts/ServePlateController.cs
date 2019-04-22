using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServePlateController : MonoBehaviour
{
    #region private properties
    [SerializeField] private GameObject _servePlate;
    [SerializeField] private Transform[] _servePlateHolder;
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
    }
    #endregion


    #region public methods
    public void AddSlicesToServePlate(Vegetable sliced, int playerId)
    {
        if (playerId == 1)
        {
            //GameObject servePlate =  Instantiate(_servePlate, _servePlateHolder[0]);
            sliced.transform.SetParent(_servePlateHolder[0]);
            sliced.transform.localPosition = new Vector3(0, -2, 0);
            sliced.transform.localScale = Vector3.one;

        }
        else if (playerId == 2)
        {
            // GameObject servePlate = Instantiate(_servePlate, _servePlateHolder[1]);
            sliced.transform.SetParent(_servePlateHolder[1]);
            sliced.transform.localPosition = new Vector3(0, -2, 0);
            sliced.transform.localScale = Vector3.one;
        }


    }

    public void AddSlicesToPlayer()
    {

       
        Vegetable[] obj = this.GetComponentsInChildren<Vegetable>();
        Debug.Log("count" + obj.Length);


    }

    #endregion
    #region protected methods
    #endregion
}
