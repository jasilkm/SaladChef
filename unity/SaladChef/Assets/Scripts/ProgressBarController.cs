using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ProgressBarController : MonoBehaviour
{

    #region private properties
    [SerializeField] private Image _progressImage;
    private float _elapsedTime;
    private float _requiredTime;
    private Action _progressCompletedhandler;
    private bool _isProgressable = false;

    #endregion

    #region public properties
    public float scaleFactor { get; set; }
    public Image VegSprite;
    #endregion
    #region protected properties
    #endregion


    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_isProgressable)
            UpdateProgressBar();
    }
    #endregion

    #region public methods
    public void Init(Sprite vegSprite,float requiredTime,Action progressCompletedhandler)   {
        _progressImage.fillAmount = 0;
        _elapsedTime = 0;
        _requiredTime = requiredTime;
        _isProgressable = true;
        VegSprite.sprite = vegSprite;
        _progressCompletedhandler = progressCompletedhandler;
    }

    public void Init(float requiredTime, Action progressCompletedhandler)
    {
        _progressImage.fillAmount = 0;
        _isProgressable = true;
        _requiredTime = requiredTime;
        _elapsedTime = 0.0f;
        scaleFactor = 0.0001f;
        _progressCompletedhandler = progressCompletedhandler;
    }
    public void RemoveProgressBar()
    {
        Destroy(this.gameObject);
        
    }


    public void ResetProgress(Sprite vegSprite)
    {
        _progressImage.fillAmount = 0;
        _elapsedTime = 0;
        VegSprite.sprite = vegSprite;
    }
    #endregion

    #region Private methods

    private void UpdateProgressBar()
    {
        _elapsedTime += Time.deltaTime;
        _progressImage.fillAmount = SaladChefHelper.NormalizedValue(_elapsedTime, 0, _requiredTime, 0, 1);

        if (_progressImage.fillAmount >= 1) {
            _progressCompletedhandler();
        }
    }


    #endregion

}
