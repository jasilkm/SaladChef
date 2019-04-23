using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitPlateController : MonoBehaviour
{
    public bool _isFilled { get; set; }

    public Transform _position;
    public Vegetable vegetable { get; set; }

    public void CreateVege(Vegetable veg)
    {
        if (veg != null)
        {
            Vegetable vg = Instantiate(veg);
            vg.transform.SetParent(_position);
            vg.transform.localPosition = new Vector3(0, -2f, 0);
            vegetable = vg;
        }
     
    }

    public void RemoveVegetable()
    {
        vegetable.gameObject.SetActive(false);
        _isFilled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        _isFilled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
} 
