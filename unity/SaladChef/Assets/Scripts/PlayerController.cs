using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region private properties
    [SerializeField] private float _speed = 0.4f; // player  
    [SerializeField] private float _playerID; // Player id 
    private VegetablesController vegetablesController;
    private int maxVegeCanCollect = 2; // max vegetables can collect from table 
    private bool _isSlicing = false; // when this bool is true Player movement restricted
    #endregion

    #region public properties
    public List <Vegetable> PickedVegetables = new List<Vegetable>();
    #endregion

    #region Events 
    #endregion


    #region unity methods
    // Start is called before the first frame update
    void Start()
    {
        vegetablesController = GameObject.FindObjectOfType<VegetablesController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    #endregion



    #region public methods
    /// <summary>
    /// 
    /// </summary>
    public void Move(float xMove, float yMove, float xMove1, float yMove1)
    {
        if (_playerID == 1)
        {
            if (!_isSlicing)
            {
                Vector2 movementPlayer = new Vector2(xMove, yMove);
                movementPlayer *= _speed;
                this.transform.Translate(movementPlayer);
            }
           
        }
        else if (_playerID == 2)
        {
            if (!_isSlicing)
            {
                Vector2 movementPlayer = new Vector2(xMove1, yMove1);
                movementPlayer *= _speed;
                this.transform.Translate(movementPlayer);
            }
        }

    }
    #endregion

    #region private methods
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "vegbasket")
        {
            Vegetable veg = other.GetComponentInChildren<Vegetable>();

            if (PickedVegetables.Count != maxVegeCanCollect)
            {
                PickedVegetables.Add(veg);
            }
            else
            {
                Debug.Log("Max Veg Collected");

            }
        } else if (other.tag == "vegcutter")
             {
            if (PickedVegetables.Count == maxVegeCanCollect)
            {
                _isSlicing = true;
                vegetablesController.SliceVegetable(PickedVegetables, other.transform, () => {
                    _isSlicing = false;

                });
            }

             }
    }
    #endregion
    #region protected methods
    #endregion





}
