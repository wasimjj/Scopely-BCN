using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class GameStructs
    {
    }
    [System.Serializable]
    public struct LayerInfo
    {
        [Tooltip("Add Creep type and number of creep to genrate")]
        public List<CreepsInfo> CreepsInfo;
        [Tooltip("Add time in seconds to generate after that seconds ")]
        public float SpawnAfterTime;
    }
    [System.Serializable]
    public struct CreepsInfo
    {
        [Tooltip("Select creep type for the wave")]
        public CreepType CreepType;
        [Tooltip("Add number of creeps to be spawned")]
        public int Numbers;
        [Tooltip("Movement speed toward player /  target")]
        public float MoveSpped;
        [Tooltip("Health Of the creep")]
        public float Health;
        [Tooltip("Define the value that is going to damage the player")]
        public float AttackValue;
    }
    public enum CreepType
    {
        SmallCreepType,
        BigCreepType
    }
    public enum BulletType
    {
        Noraml,
        Ice
    }
}
