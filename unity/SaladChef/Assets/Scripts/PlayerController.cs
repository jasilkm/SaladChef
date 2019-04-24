using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class PlayerController : MonoBehaviour
{
    #region private properties
      
    private VegetablesController vegetablesController;
    private int maxVegeCanCollect = 2; // max vegetables can collect from table 
    private bool _isActive = false; // when this bool is true Player movement restricted
    private int _trashMinusScore = -10;

    #endregion

    #region public properties
    public List <Vegetable> PickedVegetables = new List<Vegetable>();
    public List<Vegetable> SlicedVegetables = new List<Vegetable>();
    public HudController hudController;
    public int PlayerID; // Player id 
    public float Speed = 12f; // Player Movement SPeed
    public bool _isSliceAdded = false;

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
        //Debug.Log(String.Format("{000000:0000.00000}", xMove));
        if (PlayerID == 1)
        {
            if (!_isActive)
            {
                // Vector2 movementPlayer = new Vector2(xMove, yMove);
                // movementPlayer *= _speed;
                //this.transform.Translate(movementPlayer);

                xMove = xMove * Speed * Time.deltaTime;
                yMove = yMove * Speed * Time.deltaTime;
                Vector3 pos = transform.localPosition;
                pos.x = Mathf.Clamp(pos.x + xMove, -5.7f, 5.7f);
                pos.y = Mathf.Clamp(pos.y + yMove, -2.87f, 2.87f);
                transform.localPosition = pos;
            }
           
        }
        else if (PlayerID == 2)
        {
            if (!_isActive)
            {
                xMove1 = xMove1 * Speed * Time.deltaTime;
                yMove1 = yMove1 * Speed * Time.deltaTime;
                Vector3 pos = transform.localPosition;
                pos.x = Mathf.Clamp(pos.x + xMove1, -5.7f, 5.7f);
                pos.y = Mathf.Clamp(pos.y + yMove1, -2.87f, 2.87f);

                //Debug.Log(pos);
                transform.localPosition = pos;
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
                    if (_isSliceAdded) return;
                    Vegetable veg = other.GetComponentInChildren<Vegetable>();
                    if (PickedVegetables.Count != maxVegeCanCollect)
                    {
                        PickedVegetables.Add(veg);
                        hudController.UpdatePlayersCollectedVeg(veg.VegetableSprite,this.PlayerID);
                    }
                    else
                    {
                        Debug.Log("Max Veg Collected");
                    }
                    break;
                }
            case "vegcutter":
                {

                    ChopPad chopPad = other.GetComponent<ChopPad>();
                    if (chopPad.ChopPadID != PlayerID) return;
                    if (PickedVegetables.Count > 0)
                    {
                        _isActive = true;
                        vegetablesController.SliceVegetable(PickedVegetables, other.transform,
                            (slice) => {
                                SlicedVegetables.Add(slice);
                            },
                            
                            () => {
                            _isActive = false;
                            PickedVegetables.Clear();

                            });
                    }
                    break;
                }
            case "serveplate":
                {
                    SlicePlate slicePlate = other.GetComponent<SlicePlate>();
                    if (slicePlate.SlicePlateId != PlayerID) return;
                    //Debug.Log("Serve plate detected"+ this);
                    ServePlateController servePlateController = GameObject.FindObjectOfType<ServePlateController>();
                    servePlateController.AddSlicesToPlayer(this.transform,other.gameObject);
                    break;
                }
            case "trash":
                {
                    if (SlicedVegetables.Count > 0)
                    {
                        foreach (var item in SlicedVegetables)
                        {
                            Destroy(item.gameObject);
                        }
                        SlicedVegetables.Clear();
                        hudController.ClearCollectedVeg(this.PlayerID);
                        hudController.UpdatePlayerScore(_trashMinusScore,this.PlayerID);
                        _isSliceAdded = false;
                    }
                   
                    break;
                }
            case "waitplate":
                {
                    var waitplate = other.gameObject.GetComponent<WaitPlateController>();
                    if (PickedVegetables.Count > 0)
                    {
                       
                        if (!waitplate._isFilled)
                        {
                            waitplate._isFilled = true;
                            waitplate.CreateVege(PickedVegetables[0]);
                            PickedVegetables.RemoveAt(0);
                        }
                        else
                        {
                            Debug.Log("Vegitable availabel");
                        }
                    }
                    else
                    {
                        if (waitplate._isFilled)
                        {
                            if (PickedVegetables.Count == 0)
                            {
                                PickedVegetables.Clear();
                                PickedVegetables.Add(waitplate.vegetable);
                                waitplate.RemoveVegetable();
                            }
                        }
                    }
                    break;  
                } 
            default:  
                break;
        }


       
    }

    public void RemoveVegetable()
    {
        Vegetable[] veg = this.gameObject.GetComponentsInChildren<Vegetable>();
        foreach (var item in veg)
        {
           // Debug.Log(item.saladIngredients.ToString());
            Destroy(item.gameObject);
            PickedVegetables.Clear();
            //item.GetComponent<Transform>().SetParent(player);
        }
    }
    #endregion
    #region protected methods
    #endregion
    

}

public enum Players
{
    player1 = 1,
    player2 = 2
}