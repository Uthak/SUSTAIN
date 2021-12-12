using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionStats : MonoBehaviour
{
    #region variables
    [Header("Tile Settings:")]
    [SerializeField] GameObject TierII;
    [SerializeField] GameObject TierIII;

    [Header("Tile Sounds:")]
    [SerializeField] AudioSource build_Sound;
    [SerializeField] AudioSource upgrade_Sound;

    [Header("(handshakes - don't touch)")]
    public bool wasPlaced = false;
    public int tierLevel = 0;
    private GameObject _sceneManager;
    private float _productionValue = 0f;
    private float _constructionCost;
    private float _productionCostPerMinute;
    #endregion

    void Awake()
    {
        _sceneManager = GameObject.Find("SceneManager");
        _productionValue = _sceneManager.GetComponent<NewGameManager>().baseProductionValuePerMinute / 50f / 60f;
        _constructionCost = _sceneManager.GetComponent<NewGameManager>().baseConstructionCost;
        _productionCostPerMinute = _sceneManager.GetComponent<NewGameManager>().baseProductionCostPerMinute / 50 / 60;
    }

    // called when Upgrade-Tile is placed onto this built tile:
    public void Upgrade()
    {
        if(tierLevel == 1)
        {
            tierLevel++; // now: tier II
            _productionValue += _sceneManager.GetComponent<NewGameManager>().tier2ProductionValuePerMinute / 50f / 60f;
            _sceneManager.GetComponent<StatsManager>().upkeep += _sceneManager.GetComponent<NewGameManager>().tier2ProductionCostPerMinute / 50 / 60;
            TierII.GetComponent<MeshRenderer>().enabled = true;
            upgrade_Sound.Play();
            Debug.Log("upkeep shall now be " + _sceneManager.GetComponent<StatsManager>().upkeep);
        }
        else if (tierLevel == 2)
        {
            tierLevel++; // now: tier III
            _productionValue += _sceneManager.GetComponent<NewGameManager>().tier3ProductionValuePerMinute / 50f / 60f;
            _sceneManager.GetComponent<StatsManager>().upkeep += _sceneManager.GetComponent<NewGameManager>().tier3ProductionCostPerMinute / 50 / 60;
            TierIII.GetComponent<MeshRenderer>().enabled = true;
            upgrade_Sound.Play();
            Debug.Log("upkeep shall now be " + _sceneManager.GetComponent<StatsManager>().upkeep);
        }
    }

    // called when being placed on grid:
    public void Build()
    {
        Debug.Log("i want to build something");

        if (_sceneManager.GetComponent<StatsManager>().availableMoney >= _constructionCost && !wasPlaced) // do you have enough money?
        {
            Debug.Log("available money " + _sceneManager.GetComponent<StatsManager>().availableMoney + " construction cost " + _constructionCost + ", so I have enough money!");

            wasPlaced = true;
            build_Sound.Play();
            _sceneManager.GetComponent<StatsManager>().availableMoney -= _constructionCost;
            Debug.Log("construction cost were " + _constructionCost);
            _sceneManager.GetComponent<StatsManager>().upkeep += _productionCostPerMinute;
            Debug.Log("upkeep shall now be " + _sceneManager.GetComponent<StatsManager>().upkeep);
            if (CompareTag("energy"))
            {
                _sceneManager.GetComponent<StatsManager>().UpdateEnergyProduction(_productionValue);
            }
            if (CompareTag("happiness"))
            {
                _sceneManager.GetComponent<StatsManager>().UpdateHappinessProduction(_productionValue);
            }
            if (CompareTag("environment"))
            {
                _sceneManager.GetComponent<StatsManager>().UpdateEnvironmentProduction(_productionValue);
            }
            tierLevel++; // now: tier I
        }
    }
}
