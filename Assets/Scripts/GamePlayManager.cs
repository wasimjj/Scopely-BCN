using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
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
    public GameObject IceTurret;
    [Tooltip("Ice Turret preset to create in the workd")]
    public GameObject NormalTurret;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCreepsWithDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            Plane plane = new Plane(Vector3.up, 0);

            float distance;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
                Vector3 worldPosition = ray.GetPoint(distance);
                Debug.Log("World position::"+ worldPosition);
                Instantiate(IceTurret,worldPosition,Quaternion.identity);
            }


        }

    }
    void SpawnCreeps()
    { 
    }
    IEnumerator SpawnCreepsWithDelay()
    {
        WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
        WaitForSeconds WaitForSecondsOneSec = new WaitForSeconds(SpawnDelayInSameWave);
        CreepBase creepBase = null;
        foreach (LayerInfo LayerInfo in LayersInfo)
        {
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
                    creepBase.Setup(CreepInfo.MoveSpped, CreepInfo.Health, PlayerBase.transform);
                    creepBase.ResetSafe(CreepsSpawnPoints[Random.Range(0, CreepsSpawnPoints.Count)].position);
                    yield return WaitForSecondsOneSec;
                }
                yield return WaitForEndOfFrame;            
            }

            yield return new WaitForSeconds(LayerInfo.SpawnAfterTime);
            
        }

    }
    [SerializeField]
    GamePlayPoolManager GamePlayPoolManager;
    [SerializeField]
    PlayerBase PlayerBase;
}
