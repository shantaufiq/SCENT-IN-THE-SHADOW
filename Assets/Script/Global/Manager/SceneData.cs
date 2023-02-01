using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScentInTheShadow.Global.Manager
{
    [CreateAssetMenu]
    [System.Serializable]
    public class SceneData : ScriptableObject
    {
        public string TargetScene;
    }
}