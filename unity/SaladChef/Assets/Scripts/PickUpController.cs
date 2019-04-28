using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PickUpController : MonoBehaviour
{
    public Pickup pickup;
    public Transform parentPos;
    public GameObject InfoText;
    public Transform UIRoot;
    public event EventHandler<PickUpCollectedEventArgs> PickupCollected;

    // Creating Pick Ups
    public void GeneratePickUp(Action<Pickup> handler)
    {
        // creating pick up 
        Pickup pickupObj =  Instantiate(pickup);
       // GameObject infoText = Instantiate(InfoText);
       // infoText.transform.SetParent(UIRoot);
        handler(pickupObj);
        pickupObj.PickUpCollected += PickupObj_PickUpCollected;
        pickupObj.Init(() =>
        {
            pickupObj.PickUpCollected -= PickupObj_PickUpCollected;
            // Destroy(infoText);
        });
        pickupObj.transform.SetParent(parentPos);
        Vector2 pos = GetRandomPosition();
        pickupObj.transform.localPosition = pos;
      //  infoText.transform.localScale = Vector3.one;
      //  Debug.Log(SaladChefHelper.GetScreenPosition(pos.x, pos.y));
      ///  infoText.transform.localPosition = SaladChefHelper.GetScreenPosition(pickupObj.transform.localPosition.x, pickupObj.transform.localPosition.y);
    }

    // Get Random Postion
    private Vector2 GetRandomPosition()
    {
        float xPos = UnityEngine.Random.Range(SaladChefConstants.SCREEN_BORDER_LEFT, SaladChefConstants.SCREEN_BORDER_RIGHT);
        float yPos = UnityEngine.Random.Range(SaladChefConstants.SCREEN_BORDER_BOTTOM, SaladChefConstants.SCREEN_BORDER_TOP);
        return new Vector2(xPos, yPos);
    }

    private void PickupObj_PickUpCollected(object sender, PickUpCollectedEventArgs args)
    {
      //Debug.Log("Pickup Collectes"+ args.PickUp);
        OnPickUpCollected(args);
    }

    private void OnPickUpCollected(PickUpCollectedEventArgs args)
    {
        PickupCollected?.Invoke(this, args);
    }
}

public enum PickupsType
{
    Time ,
    Speed,
    score
}

public class PickUpCollectedEventArgs:EventArgs
{
    public PickupsType PickUp { get; set; }
    public Players Player { get; set; }
}