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

    // When Hit player on serv plate/ Sliced plate veg add to plater
    public void AddSlicesToServePlate(Vegetable sliced, int playerId)
    {
        if (playerId == 1)
        {
            //GameObject servePlate =  Instantiate(_servePlate, _servePlateHolder[0]);
            sliced.transform.SetParent(_servePlateHolder[0]);
            sliced.transform.localPosition = new Vector3(0, 0, -2);
            sliced.transform.localScale = Vector3.one;

        }
        else if (playerId == 2)
        {
            // GameObject servePlate = Instantiate(_servePlate, _servePlateHolder[1]);
            sliced.transform.SetParent(_servePlateHolder[1]);
            sliced.transform.localPosition = new Vector3(0, 0, -2);
            sliced.transform.localScale = Vector3.one;
        }


    }

    // Sliced veg to Player
    public void AddSlicesToPlayer(Transform player, GameObject plate)
    {
        Vegetable[] veg = plate.GetComponentsInChildren<Vegetable>();
        foreach (var item in veg)
        {
            item.GetComponent<Transform>().SetParent(player);
            item.GetComponent<Transform>().localPosition = new Vector3(0,0,-1);


        }
        player.GetComponent<PlayerController>().IsSliceAdded = true;
        //  Vegetable[] obj = this.GetComponentsInChildren<Vegetable>();

    }

    #endregion
    #region protected methods
    #endregion
}
