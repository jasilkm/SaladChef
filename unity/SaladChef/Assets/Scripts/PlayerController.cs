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
    public List<Vegetable> PickedVegetables = new List<Vegetable>();
    public List<Vegetable> SlicedVegetables = new List<Vegetable>();
    public HudController hudController;
    public int PlayerID; // Player id 
    public float Speed = 12f; // Player Movement SPeed
    public bool IsSliceAdded = false;
    public Players Player;
    // Number of Pickups Collected 
    public int NumberOfPickUpCollected { get; set; }

    #endregion

    #region Events 
    #endregion


    #region unity methods
    // Start is called before the first frame update
    void Start()
    {
        vegetablesController = GameObject.FindObjectOfType<VegetablesController>();
        Speed = SaladChefConstants.PLAYER_SPEED;

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
    /// Player movement value will recive from Input controller
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
                //clamping position to desired area 
                pos.x = Mathf.Clamp(pos.x + xMove, SaladChefConstants.SCREEN_BORDER_LEFT, SaladChefConstants.SCREEN_BORDER_RIGHT);
                pos.y = Mathf.Clamp(pos.y + yMove, SaladChefConstants.SCREEN_BORDER_BOTTOM, SaladChefConstants.SCREEN_BORDER_TOP);
                transform.localPosition = pos;
            }

        }
        else if (PlayerID == 2)
        {
            if (!_isActive)
            {
                // calculating postion 
                xMove1 = xMove1 * Speed * Time.deltaTime;
                yMove1 = yMove1 * Speed * Time.deltaTime;
                Vector3 pos = transform.localPosition;
                //clamping position to desired area 
                pos.x = Mathf.Clamp(pos.x + xMove1, SaladChefConstants.SCREEN_BORDER_LEFT, SaladChefConstants.SCREEN_BORDER_RIGHT);
                pos.y = Mathf.Clamp(pos.y + yMove1, SaladChefConstants.SCREEN_BORDER_BOTTOM, SaladChefConstants.SCREEN_BORDER_TOP);

                //Debug.Log(pos);
                transform.localPosition = pos;
            }
        }

    }
    #endregion

    #region private methods
    /// <summary>
    /// Player Intraction with other objects like vegtable, slicetable, trash and Customer  based on tag
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "vegbasket":
                {
                    OnHitVegetableBasket(other);
                    break;
                }
            case "vegcutter":
                {
                    OnHitvegetableCutter(other);
                    break;
                }
            case "serveplate":
                {
                    OnHitServePlate(other);

                    break;
                }
            case "trash":
                {
                    OnHitTrashCan(other); 

                    break;
                }
            case "waitplate":
                {
                    OnHitWaitPlate(other);
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

            //item.GetComponent<Transform>().SetParent(player);
        }
        PickedVegetables.Clear();
    }

    public void RemoveSlicedVegetables()
    {
        foreach (var item in SlicedVegetables)
        {
            if (item != null)
            {
                Destroy(item.gameObject);
            }

        }
        SlicedVegetables.Clear();

    }

    public void ClearWaitPlate()
    {


    }
    #endregion
    #region private  methods

    private void OnHitWaitPlate(Collider other)
    {

        // adding vege to wait plate so player can collect later for combination
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
    }

    private void OnHitTrashCan(Collider other)
    {
        // combination move to trach
        if (SlicedVegetables.Count > 0)
        {
            IsSliceAdded = false;
            foreach (var item in SlicedVegetables)
            {
                if (item != null)
                {
                    Destroy(item.gameObject);
                }
            }
            SlicedVegetables.Clear();
            hudController.ClearCollectedVeg(this.PlayerID);
            hudController.UpdatePlayerScore(_trashMinusScore, this.PlayerID);

        }
    }

    private void OnHitServePlate(Collider other)
    {
        SlicePlate slicePlate = other.GetComponent<SlicePlate>();
        if (slicePlate.SlicePlateId != PlayerID) return;
        if (SlicedVegetables.Count == 0) return;
        //Debug.Log("Serve plate detected"+ this);
        ServePlateController servePlateController = GameObject.FindObjectOfType<ServePlateController>();
        servePlateController.AddSlicesToPlayer(this.transform, other.gameObject);
       
    }

    private void OnHitvegetableCutter(Collider other)
    {
        ChopPad chopPad = other.GetComponent<ChopPad>();
        //chop board restcted based on the user
        if (chopPad.ChopPadID != PlayerID) return;
        // can collect veg from chop pad
        if (PickedVegetables.Count > 0)
        {
            _isActive = true;
            vegetablesController.SliceVegetable(PickedVegetables, other.transform,
                (slice) => {
                                // this call back will recived when a vege slice has complted
                                SlicedVegetables.Add(slice);
                },

                () => {
                    _isActive = false;
                    PickedVegetables.Clear();

                });
        }
    }

    private void OnHitVegetableBasket(Collider other)
        {
             // if sliced object carring with player will restrict to collect veg from basket
             if (IsSliceAdded) return;
                Vegetable veg = other.GetComponentInChildren<Vegetable>();
                    //
                   
                    if (PickedVegetables.Count != maxVegeCanCollect)
                    {
                        // list updating picked vege
                        PickedVegetables.Add(veg);
                        // updating image to hud
                        hudController.UpdatePlayersCollectedVeg(veg.VegetableSprite,this.PlayerID);
    }
                    else
                    {
                        Debug.Log("Max Veg Collected");
                    }
}
    #endregion
    

}

public enum Players
{
    player1 = 1,
    player2 = 2
}