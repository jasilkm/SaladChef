using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region private properties
    [SerializeField] private float _speed = 0.4f; // player  
    [SerializeField] private int _playerID; // Player id 
    private VegetablesController vegetablesController;
    private int maxVegeCanCollect = 2; // max vegetables can collect from table 
    private bool _isActive = false; // when this bool is true Player movement restricted
    #endregion

    #region public properties
    public List <Vegetable> PickedVegetables = new List<Vegetable>();
    public HudController hudController;
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

    public void EnablePlayer()
    {
        _isActive = false;
    }

    public void DisablePlayer()
    {
        _isActive = true;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Move(float xMove, float yMove, float xMove1, float yMove1)
    {
        if (_playerID == 1)
        {
            if (!_isActive)
            {
                Vector2 movementPlayer = new Vector2(xMove, yMove);
                movementPlayer *= _speed;
                this.transform.Translate(movementPlayer);
            }
           
        }
        else if (_playerID == 2)
        {
            if (!_isActive)
            {
                Vector2 movementPlayer = new Vector2(xMove1, yMove1);
                movementPlayer *= _speed;
                this.transform.Translate(movementPlayer);
            }
        }

    }
    #endregion

    #region private methods
    /// <summary>
    /// Player Intraction with other objects like vegtable, slicetable, trash and Customer 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "vegbasket":
                {
                    Vegetable veg = other.GetComponentInChildren<Vegetable>();
                    if (PickedVegetables.Count != maxVegeCanCollect)
                    {
                        PickedVegetables.Add(veg);
                        hudController.UpdatePlayersCollectedVeg(veg.VegetableSprite,this._playerID);
                    }
                    else
                    {
                        Debug.Log("Max Veg Collected");
                    }
                    break;
                }
            case "vegcutter":
                {
                    if (PickedVegetables.Count >= 1)
                    {
                        _isActive = true;
                        vegetablesController.SliceVegetable(PickedVegetables, other.transform, () => {
                            _isActive = false;
                            hudController.ClearCollectedVeg(_playerID);
                           // Debug.Log("PickedVegetables :" + PickedVegetables.Count);
                            
                        });
                    }
                    break;
                }
            case "serveplate":
                {
                   // Debug.Log("Serve plate detected"+ other.gameObject.name);
                   // ServePlateController servePlateController = other.gameObject.GetComponent<ServePlateController>();
                    //servePlateController.AddSlicesToPlayer();
                    break;
                }
                default:
                break;
        }


       
    }
    #endregion
    #region protected methods
    #endregion





}

