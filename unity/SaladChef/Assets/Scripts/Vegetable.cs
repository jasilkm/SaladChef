﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : MonoBehaviour
{
    #region private properties
    [SerializeField] private float _point;
    [SerializeField] private string _name;
    #endregion

    #region public properties
    public Sprite VegetableSprite;
    public Sprite SlicedVegitable;
    #endregion

    #region Events 
    #endregion

    #region unity methods
    void Start()
    {
        SpriteRenderer Sprite = GetComponent<SpriteRenderer>();
        Sprite.sprite = VegetableSprite;
    }

    void Update()
    {

    }
    #endregion

}
