using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageStats : MonoBehaviour
{
    #region variables
    [Header("Tile Settings:")]
    [SerializeField] GameObject TierII;
    [SerializeField] GameObject TierIII;
    [SerializeField] GameObject TierIV;
    [SerializeField] GameObject TierV;
    [SerializeField] GameObject incomeDisplay;

    [Header("Tile Sounds:")]
    [SerializeField] AudioSource build_Sound;
    [SerializeField] AudioSource upgrade_Sound; // happy kids? people laughing or talking?
    [SerializeField] AudioSource cashRegister_Sound; // Ka-Ching!

    [Header("(handshakes - don't touch)")]
    public bool wasPlaced = false;
    public bool influencedByEnergy = false;
    private bool _energyAccounted = false; // prevents double-counts
    public bool influencedByHappiness = false;
    private bool _happinessAccounted = false; // prevents double-counts
    public bool influencedByNature = false;
    private bool _natureAccounted = false; // prevents double-counts
    public bool influencedByNeighbors = false;
    private bool _neighborsAccounted = false;
    private GameObject _sceneManager;
    private float _costOfLiving;
    private float _taxesToPay;
    private float _bonusTaxes;
    private float _frequencyToPay;
    private int _tierLevel;
    private float _constructionCost;
    #endregion

    void Awake()
    {
        _sceneManager = GameObject.Find("SceneManager");
        _costOfLiving = _sceneManager.GetComponent<NewGameManager>().baseCostOfLivingPerMinute / 50f / 60f;
        _taxesToPay = _sceneManager.GetComponent<NewGameManager>().baseTaxesGeneratedPerMinute;
        _bonusTaxes = _sceneManager.GetComponent<NewGameManager>().bonusTaxes;
        _frequencyToPay = _sceneManager.GetComponent<NewGameManager>().taxationFrequency;
        _constructionCost = _sceneManager.GetComponent<NewGameManager>().baseVillageConstructionCost;
    }

    void Update()
    {
        if(_tierLevel < 5)
        {
            if (influencedByEnergy && !_energyAccounted)
            {
                _energyAccounted = true; // stops counting it again
                Upgrade();
            }
            if (influencedByHappiness && !_happinessAccounted)
            {
                _happinessAccounted = true; // stops counting it again
                Upgrade();
            }
            if (influencedByNature && !_natureAccounted)
            {
                _natureAccounted = true; // stops counting it again
                Upgrade();
            }
            if (influencedByNeighbors && !_neighborsAccounted)
            {
                _neighborsAccounted = true; // stops counting it again
                Upgrade();
            }
        }
    }

    // AUTOMATICALY upgrades (once), when new influence is detected:
    public void Upgrade()
    {
        if (_tierLevel == 1)
        {
            _tierLevel++; // now: tier II
            _taxesToPay += _bonusTaxes;
            TierII.GetComponent<MeshRenderer>().enabled = true;
            upgrade_Sound.Play();
        }else if (_tierLevel == 2)
        {
            _tierLevel++; // now: tier III
            _taxesToPay += _bonusTaxes;
            TierIII.GetComponent<MeshRenderer>().enabled = true;
            upgrade_Sound.Play();
        }else if (_tierLevel == 3)
        {
            _tierLevel++; // now: tier IV
            _taxesToPay += _bonusTaxes;
            TierIV.GetComponent<MeshRenderer>().enabled = true;
            upgrade_Sound.Play();
        }else if (_tierLevel == 4)
        {
            _tierLevel++; // now: tier V
            _taxesToPay += _bonusTaxes;
            TierV.GetComponent<MeshRenderer>().enabled = true;
            upgrade_Sound.Play();
        }
    }

    // generate income:
    IEnumerator GenerateIncome()
    {
        yield return new WaitForSeconds(_frequencyToPay);
        _sceneManager.GetComponent<StatsManager>().availableMoney += _taxesToPay;
        _sceneManager.GetComponent<StatsManager>().efficientlyPlaced += _tierLevel;
        StartCoroutine("DisplayIncome");
        StartCoroutine("GenerateIncome"); // restart money-generation-timer
    }

    // audiovisual feedback to player (about income):
    IEnumerator DisplayIncome()
    {
        cashRegister_Sound.Play();
        incomeDisplay.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        incomeDisplay.SetActive(false);
    }

    // called when being placed on grid:
    public void Build()
    {
        Debug.Log("smallest resource value is: " + _sceneManager.GetComponent<StatsManager>().smallestAvailableValue);
        if (_sceneManager.GetComponent<StatsManager>().smallestAvailableValue >= _constructionCost && !wasPlaced) // do you have enough resources?
        {
            _tierLevel++; // now: TierI
            wasPlaced = true;
            _sceneManager.GetComponent<StatsManager>().energyValue -= _constructionCost;
            _sceneManager.GetComponent<StatsManager>().happinessValue -= _constructionCost;
            _sceneManager.GetComponent<StatsManager>().environmentValue -= _constructionCost;
            // we could consider houses also costing MONEY instead or in addition to resources!
            build_Sound.Play();
            StartCoroutine("GenerateIncome"); // starts money generation
            _sceneManager.GetComponent<StatsManager>().UpdateCostOfLiving(_costOfLiving);
        }
    }
}
