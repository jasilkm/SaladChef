using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region private properties
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _playerID;
    #endregion

    #region public properties
    #endregion

    #region Events 
    #endregion


    #region unity methods
    // Start is called before the first frame update
    void Start()
    {
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
            Vector2 movementPlayer = new Vector2(xMove, yMove);
            movementPlayer *= _speed;
            this.transform.Translate(movementPlayer);
        }
        else if (_playerID == 2)
        {
            Vector2 movementPlayer = new Vector2(xMove1, yMove1);
            movementPlayer *= _speed;
            this.transform.Translate(movementPlayer);
        }

    }
    #endregion

    #region private methods
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
    }
    #endregion
    #region protected methods
    #endregion





}
