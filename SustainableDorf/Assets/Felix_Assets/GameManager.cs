using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // controls global maximum of stats (and starting condition)
    public float baseStatValue = 10f;
    // how fast do stats decay in general
    // updates 50/s or 3000x per minute
    public float globalDegenerationRate = 0.1f;
    // this multiplicator accelerated decay when dropping below threshold
    // this is calculated in the "Start()"-function of "NeedsManager()"-script
    [Tooltip("the degenerationThreshold is turnt into % relative to the \"baseStatValue\"")]
    public float degenerationThreshold = 50f;
    // this multiplicator accelerated decay when dropping below threshold
    public float accelleratedDegenerationRate = 1.5f;

    // Stats of tiles will be applied contineously
    public bool usingContinuousValues = true;
    public float minInfluencePerTile = .125f;
    public float maxInfluencePerTile = .325f;
    // Multiply tiny values to show in UI-display, without changing the display
    public float statDisplayMultiplicator = 200f;

    // Stats of tiles will be applied only once
    public bool usingOneTimeValues = false;
    public float minStatsPerTile = 3f;
    public float maxStatsPerTile = 6f;

    // Separate, optional display that lets you read out the actual degeneration rates of all stats
    [SerializeField] bool showDegenerationRates = true;
    [SerializeField] TextMeshProUGUI prosperityDegenerationRateField;
    [SerializeField] TextMeshProUGUI happinessDegenerationRateField;
    [SerializeField] TextMeshProUGUI environmentDegenerationRateField;
    // This allows to see NUMERICAL info about current state of the game stats
    [SerializeField] bool numericalValueDisplay = true;
    [SerializeField] TextMeshProUGUI prosperityValueField;
    [SerializeField] TextMeshProUGUI happinessValueField;
    [SerializeField] TextMeshProUGUI environmentValueField;

    // live update of degeneration Bonuses
    [SerializeField] TextMeshProUGUI prosperityDegBonus;
    [SerializeField] TextMeshProUGUI happinessDegBonus;
    [SerializeField] TextMeshProUGUI environmentDegBonus;

    // to adjust camera-zoom ("CameraZoom"-script)
    public float cameraZoomSpeed = 10f;
    public float cameraMaxZoom = 15f;
    public float cameraMinZoom = 70f;
    [SerializeField] TextMeshProUGUI cameraTransformMetrics1;
    [SerializeField] TextMeshProUGUI cameraTransformMetrics2;

    // becomes activated if using numerical degeneration-rates or stat-values (see above)
    [Tooltip("becomes activated if using numerical degeneration-rates or stat-values (see above)")]
    [SerializeField] GameObject developmentStatUI;

    // used to reset the UI-display at the start of the game
    private GameObject StatsDisplay;

    void Start()
    {
        // determines the degeneration Threshold as % of the maximum stat-value
        // is done in NeedsManager as well, here just in case we need the value elsewhere at a later point
        // currently redundant (!!!)
        degenerationThreshold = degenerationThreshold / 100f * baseStatValue;

        // resets all stat-bars in bottom-center UI (otherwise they would be maxed out)
        StatsDisplay = GameObject.Find("Stats_UI");
        StatsDisplay.GetComponent<StatUIDisplay>().ResetStatBars();
        
        // activates numerical stat UI's if activated
        if (showDegenerationRates == true | numericalValueDisplay == true)
        {
            developmentStatUI.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        GameObject SceneManager = GameObject.Find("SceneManager");

        // if activated this displays DEGENERATION-RATES in the UI (optional)
        if (showDegenerationRates == true)
        {
            prosperityDegenerationRateField.text = "prospRate " + SceneManager.GetComponent<NeedsManager>().prosperityDegenerationRate.ToString();
            happinessDegenerationRateField.text = "hapRate " + SceneManager.GetComponent<NeedsManager>().happinessDegenerationRate.ToString();
            environmentDegenerationRateField.text = "envRate " + SceneManager.GetComponent<NeedsManager>().environmentDegenerationRate.ToString();
        }

        // if activated this displays STATS numerically in the UI (optional)
        if (numericalValueDisplay == true)
        {
            prosperityValueField.text = "prosperity " + SceneManager.GetComponent<NeedsManager>().prosperityValue.ToString();
            happinessValueField.text = "happiness " + SceneManager.GetComponent<NeedsManager>().happinessValue.ToString();
            environmentValueField.text = "environment " + SceneManager.GetComponent<NeedsManager>().environmentValue.ToString();
        }

        // live update of camera settings
        cameraTransformMetrics1.text = "camera field Of View: " + Camera.main.fieldOfView.ToString();
        cameraTransformMetrics2.text = "camera orthographicSize: " + Camera.main.orthographicSize.ToString();

        // live update of "degenerationBonus" from "Stats"-script
        prosperityDegBonus.text = "current prospBonus " + SceneManager.GetComponent<PlaceObjectsOnGrid>().curObject.GetComponent<Stats>().prosperityBonus.ToString();
        happinessDegBonus.text = "current hapBonus " + SceneManager.GetComponent<PlaceObjectsOnGrid>().curObject.GetComponent<Stats>().happinessBonus.ToString();
        environmentDegBonus.text = "current envBonus " + SceneManager.GetComponent<PlaceObjectsOnGrid>().curObject.GetComponent<Stats>().environmentBonus.ToString();
    }
}
