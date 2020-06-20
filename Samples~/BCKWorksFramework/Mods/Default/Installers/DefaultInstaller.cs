using System;
using UnityEngine;
using Zenject;

namespace Mods.Default.Installer
{
    [CreateAssetMenu(fileName = "DefaultSettings", menuName = "BCKWorks/Mods/Default/Settings")]
    public class DefaultInstaller : ScriptableObjectInstaller<DefaultInstaller>
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
            public string Name = "Default Installer";
        }
    }
}