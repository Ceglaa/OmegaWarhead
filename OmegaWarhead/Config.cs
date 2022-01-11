namespace OmegaWarhead
{
    using Exiled.API.Interfaces;
    using System.ComponentModel;

    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        [Description("How much time after Alpha Warhead Detonation, should Omega Warhead be started or Radiation Should Occur")]
        public int Delay { get; private set; } = 180;
        [Description("Damages players after Delay time, Enabling Radiation Mode will disable Omega Warhead")]
        public bool RadiationMode { get; private set; } = false;
        public float RadiationDamage { get; private set; } = 10f;
        public int DelayBetweenDamage { get; private set; } = 10;
    }
}