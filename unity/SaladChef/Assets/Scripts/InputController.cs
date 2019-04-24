using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private PlayerController playerController;
   

    // Start is called before the first frame update
    void Start()
    {
        playerController = this.GetComponent<PlayerController>();

          }

    // Update is called once per frame
    void FixedUpdate()
    {
        float movementX = Input.GetAxis("MovementX");
        float movementY = Input.GetAxis("MovementY");
        float movementX1 = Input.GetAxis("MovementX1");
        float movementY1 = Input.GetAxis("MovementY1");

        //Movement for player
        playerController.Move(movementX, movementY, movementX1, movementY1);
    }



}
