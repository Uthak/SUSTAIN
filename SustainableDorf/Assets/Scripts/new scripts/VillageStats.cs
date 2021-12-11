using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageStats : MonoBehaviour
{
    #region variables
    //[Header("Tile Settings:")]
    /*public string livesInHouse1;
    [SerializeField] GameObject house_01;
    public string livesInHouse2;
    public string livesInHouse3;
    public string livesInHouse4;
    public string livesInHouse5;*/

    [Header("Tile Sounds:")]
    [SerializeField] AudioSource villageBuilt_Sound;
    [SerializeField] AudioSource cashRegister_Sound; // Ka-Ching!

    [Header("(handshakes - don't touch)")]
    public bool wasPlaced = false;
    public bool influencedByEnergy = false;
    public bool influencedByHappiness = false;
    public bool influencedByNature = false;
    public bool influencedByNeighbors = false;
    private GameObject _sceneManager;
    private float _costOfLiving;
    private int _taxesToPay;
    private int _bonusTaxes;
    private float _frequencyToPay;
    //private bool _isInCoroutine = false;
    #endregion

    void Awake()
    {
        _sceneManager = GameObject.Find("SceneManager");
        _costOfLiving = _sceneManager.GetComponent<NewGameManager>().baseCostOfLivingPerMinute / 50f / 60f;
        _taxesToPay = _sceneManager.GetComponent<NewGameManager>().baseTaxesGeneratedPerMinute;
        _bonusTaxes = _sceneManager.GetComponent<NewGameManager>().bonusTaxes;
        _frequencyToPay = _sceneManager.GetComponent<NewGameManager>().taxationFrequency;
    }

    // subtract costs of living 50times per second
    /*private void FixedUpdate()
    {
        if (wasPlaced && !_isInCoroutine)
        {
            _sceneManager.GetComponent<StatsManager>().energyValue -= _costOfLiving;
            _sceneManager.GetComponent<StatsManager>().happinessValue -= _costOfLiving;
            _sceneManager.GetComponent<StatsManager>().environmentValue -= _costOfLiving;
            StartCoroutine("GenerateIncome"); // starts coRoutine only if tile is placed
        }
    }*/

    // generate income:
    IEnumerator GenerateIncome()
    {
        //_isInCoroutine = true;
        yield return new WaitForSeconds(_frequencyToPay);
        
        // add bonus-income based on influences:
        if (influencedByEnergy)
        {
            _taxesToPay += _bonusTaxes;
            _sceneManager.GetComponent<StatsManager>().efficientlyPlaced++;
        }
        if (influencedByHappiness)
        {
            _taxesToPay += _bonusTaxes;
            _sceneManager.GetComponent<StatsManager>().efficientlyPlaced++;
        }
        if (influencedByNature)
        {
            _taxesToPay += _bonusTaxes;
            _sceneManager.GetComponent<StatsManager>().efficientlyPlaced++;
        }
        if (influencedByNeighbors)
        {
            _taxesToPay += _bonusTaxes;
            _sceneManager.GetComponent<StatsManager>().efficientlyPlaced++;
        }

        _sceneManager.GetComponent<StatsManager>().availableMoney += _taxesToPay;
        ShowGeneratedTaxes(_taxesToPay);
        _bonusTaxes = 0; // reset to prevent bonus climbing endlessly
        
        StartCoroutine("GenerateIncome"); // start over

        //_isInCoroutine = false; // starts coRoutine inside Update again
    }

    // (juice) audiovisual feedback to player:
    private void ShowGeneratedTaxes(int _taxesToPay)
    {
        cashRegister_Sound.Play();
        // show taxAmt popup using _taxesToPay
    }

    // called when being placed on grid:
    public void Build()
    {
        wasPlaced = true;
        villageBuilt_Sound.Play();
        StartCoroutine("GenerateIncome");
        _sceneManager.GetComponent<StatsManager>().UpdateCostOfLiving(_costOfLiving);
    }
}
