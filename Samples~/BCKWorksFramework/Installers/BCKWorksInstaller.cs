using System;
using UnityEngine;
using Zenject;

namespace BCKWorks
{
    [CreateAssetMenu(fileName = "BCKWorksSettings", menuName = "BCKWorks/Settings")]
    public class BCKWorksInstaller : ScriptableObjectInstaller<BCKWorksInstaller>
    {
#pragma warning disable 0649
        [SerializeField]
        Settings settings;
#pragma warning restore 0649

        public override void InstallBindings()
        {
            Container.BindInstance(settings).IfNotBound();
        }

        [Serializable]
        public class Settings
        {
            public bool UseLoading;
            public GameObject Loading;
            public GameObject Bootstrap;
            public string IntroSceneName = "Lobby";
        }
    }
}