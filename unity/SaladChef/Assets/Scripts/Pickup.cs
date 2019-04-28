using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Pickup : MonoBehaviour
{
    private Action _completeHandler;
    public Players Player { get; set; }
    private  PickupsType PickupType { get; set; }
    public event EventHandler<PickUpCollectedEventArgs> PickUpCollected;


    public void Init(Action completeHandler)
    {
        _completeHandler = completeHandler;
        SetTypeOfPickUp();
    }

    private void SetTypeOfPickUp()
    {
        PickupType = (PickupsType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(PickupsType)).Length);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerController hitPlayer = other.gameObject.GetComponent<PlayerController>();

            if (hitPlayer.Player == Player)
            {
                _completeHandler();
                hitPlayer.NumberOfPickUpCollected += 1;
                OnPickUpCollected();
                OnDestory();
            }

        }
    }

    private void OnDestory()
    {
        Destroy(this.gameObject);
    }

    protected void OnPickUpCollected()
    {
        PickUpCollectedEventArgs arg = new PickUpCollectedEventArgs();
        arg.PickUp = this.PickupType;
        arg.Player = this.Player;
       PickUpCollected?.Invoke(this, arg);
    }
}
