using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    [Tooltip("Add creeps layers information")]
    public List<LayerInfo> LayersInfo;

    [Tooltip("Add creeps spawnning points")]
    public List<Transform> CreepsSpawnPoints;
    [SerializeField, Tooltip("If creeps spawn ata same point there must be a differnce")]
    float SpawnDelayInSameWave = 1.0f;
    [SerializeField, Tooltip("Current wave number")]
    private int CurrentWave = 0;
    [Tooltip("Ice Turret preset to create in the workd")]
    public TurretBase IceTurretPreset;
    [Tooltip("Ice Turret preset to create in the workd")]
    public TurretBase NormalTurretPreset;

    // Related to game play at runtime 

    [ReadOnly]
    [SerializeField]
    [Tooltip("Total creeps in current wave this is readonly property for editor")]
    int TotlalCreepsInWave;
    [ReadOnly]
    [SerializeField]
    [Tooltip("Total creeps Killed in current wave this is readonly property for editor")]
    int TotlalCreepsKilledInAWave;
    [SerializeField]
    [Tooltip("Total creeps reached at player base and damge player this is readonly property for editor")]
    public int CreepsReachedAtBaseInAWave;
    /// <summary>
    /// Delegates for get information all over the game
    /// </summary>
    /// 
    public delegate void OnNotifyDelegate();
    public delegate void OneIntParamDelegate(int Coins);
    public delegate void OneFloatParamDelegate(float Coins);

    public OneIntParamDelegate OnCreepKilledDelegate;
    public OneIntParamDelegate OnCoinsUpdateDelegate;
    public OneFloatParamDelegate OnHealthUpdateDelegate;
    public OnNotifyDelegate OnPlayerDeathDelegate;
    public OnNotifyDelegate OnWinDelegate;
    public OnNotifyDelegate OnLoseDelegate;
    // Start is called before the first frame update

    void Awake()
    {
        EconomyManager = FindObjectOfType<EconomyManager>();
        UIManager = FindObjectOfType<UIManager>();
    }
    void Start()
    {
        OnCreepKilledDelegate = OnCreepKilledCallback;
        OnHealthUpdateDelegate += OnPlayerHealthUpdateaCallback;
        SetNewWave();
    }
    public void SetTotalCreepsInWave(LayerInfo LayerInfo)
    {
        foreach (CreepsInfo CreepInfo in LayerInfo.CreepsInfo)
        {
            TotlalCreepsInWave += CreepInfo.Numbers;
        }
    }
    void OnPlayerHealthUpdateaCallback(float Health)
    {
        CreepsReachedAtBaseInAWave++;
        if (Health > 0)
        {
            CheckForNewWave();
        }
    }
    private void OnCreepKilledCallback(int Coins)
    {
        EconomyManager.AddCoins(Coins);
        TotlalCreepsKilledInAWave++;
        CheckForNewWave();
    }
    void CheckForNewWave()
    {
        if ((CreepsReachedAtBaseInAWave + TotlalCreepsKilledInAWave) >= TotlalCreepsInWave)
        {
            SetNewWave();
        }
    }
    void SetNewWave()
    {
        if (CurrentWave < LayersInfo.Count)
        {
            TotlalCreepsKilledInAWave = 0;
            TotlalCreepsInWave = 0;
            CreepsReachedAtBaseInAWave = 0;
            SetTotalCreepsInWave(LayersInfo[CurrentWave]);
            StartCoroutine(SpawnCreepsWithDelay(LayersInfo[CurrentWave]));
            CurrentWave++;
            return;
        }
        OnWinDelegate();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (IceTurretPreset.CostInCoins <= EconomyManager.Coins)
            {
                CreatTurret(IceTurretPreset);
                EconomyManager.DeductCoins(IceTurretPreset.CostInCoins);
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (NormalTurretPreset.CostInCoins <= EconomyManager.Coins)
            {
                CreatTurret(NormalTurretPreset);
                EconomyManager.DeductCoins(IceTurretPreset.CostInCoins);
            }
        }

    }
    void CreatTurret(TurretBase TurretPreset) 
    {
        Plane plane = new Plane(Vector3.up, 0);

        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            Vector3 worldPosition = ray.GetPoint(distance);
            Instantiate(TurretPreset, worldPosition, Quaternion.identity);
        }
    }
  
    IEnumerator SpawnCreepsWithDelay(LayerInfo LayerInfo)
    {
        WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
        WaitForSeconds WaitForSecondsOneSec = new WaitForSeconds(SpawnDelayInSameWave);
        CreepBase creepBase = null;
       
        foreach (CreepsInfo CreepInfo in LayerInfo.CreepsInfo)
        {
            for(int index = 0; index < CreepInfo.Numbers; index++)
            {
                switch (CreepInfo.CreepType)
                {
                    case CreepType.SmallCreepType:
                        creepBase = GamePlayPoolManager.GetCreep(CreepType.SmallCreepType);
                        break;
                    case CreepType.BigCreepType:
                        creepBase = GamePlayPoolManager.GetCreep(CreepType.BigCreepType);
                        break;
                }
                creepBase.Setup(CreepInfo, PlayerBase.transform);
                creepBase.ResetSafe(CreepsSpawnPoints[Random.Range(0, CreepsSpawnPoints.Count)].position);
                yield return WaitForSecondsOneSec;
            }
            yield return WaitForEndOfFrame;            
        }
    }


    [SerializeField]
    GamePlayPoolManager GamePlayPoolManager;
    [SerializeField]
    EconomyManager EconomyManager;
    [SerializeField]
    UIManager UIManager;
    [SerializeField]
    PlayerBase PlayerBase;
}
