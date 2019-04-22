using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Customer : MonoBehaviour
{
    #region Private properties
    [SerializeField]private SpriteRenderer []vegetables;
    private GameObject UIRoot;
    private Action _timeCompletedHandler;
    private GameObject _progressBar;
    #endregion
    #region Public properties
    public GameObject ProgressBar;
    #endregion
    #region protected properties
    #endregion



    #region Unity Methods
    private void Awake()
    {
        UIRoot = GameObject.FindGameObjectWithTag("UIRoot");
    }
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region private Methods

    private void CreateProgressBar(Transform pos)
    {
        _progressBar =  Instantiate(ProgressBar);
       _progressBar.transform.SetParent(UIRoot.transform);
       _progressBar.transform.position = SaladChefHelper.GetScreenPosition(pos.position.x, pos.position.y+1.5f);
        ProgressBarController pc = _progressBar.GetComponent<ProgressBarController>();
        pc.Init(10, () => {

            _timeCompletedHandler();
        });
    }
    #endregion
    #region public  Methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="veg"></param>
    /// <param name="pos"></param>
    /// <param name="timeCompletedHandler"></param>
    public void Init(List<Vegetable> veg,Transform pos, Action timeCompletedHandler)
    {
        int i = 0;
        foreach (var item in veg)
        {
            vegetables[i].sprite = item.VegetableSprite;
            i++;
        }
        _timeCompletedHandler = timeCompletedHandler;
        CreateProgressBar(pos);
    }
    /// <summary>
    /// Destory Customer Object
    /// </summary>
    public void Destroy()
    {
        Destroy(_progressBar);
        Destroy(this.gameObject);
    }

    #endregion

}
