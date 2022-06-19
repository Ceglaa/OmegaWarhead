namespace OmegaWarhead.Events
{
    using System.Collections.Generic;
    using Exiled.API.Features;
    using MEC;
    
    public sealed partial class Handler
    {
        public static bool OmegaPhase { get; private set; }
        
        public List<CoroutineHandle> Coroutines { get; } = new();
        
        public void OnDetonating()
        {
            if (Plugin.Singleton.Config.RadiationMode)
            {
                Coroutines.Add(Timing.RunCoroutine(RadiationCoroutine()));
            }
            else if (!OmegaPhase)
            {
                Coroutines.Add(Timing.RunCoroutine(OmegaWarhead()));
                OmegaPhase = true;
            }
            else
            {
                foreach(Player player in Player.List)
                {
                    if (player is not null && player.Role.Type is not RoleType.Spectator)
                    {
                        player.Kill("Omega Warhead");
                    }
                }
            }
        }

        public IEnumerator<float> RadiationCoroutine()
        {
            yield return Timing.WaitForSeconds(Plugin.Singleton.Config.Delay);
            while(Round.IsStarted)
            {
                foreach (Player player in Player.List)
                {
                    player.Hurt( Plugin.Singleton.Config.RadiationDamage, "Radiation", "Radiation");
                }
                yield return Timing.WaitForSeconds(Plugin.Singleton.Config.DelayBetweenDamage);
            }
        }
        
        public IEnumerator<float> OmegaWarhead()
        {
            yield return Timing.WaitForSeconds(Plugin.Singleton.Config.Delay);
            Warhead.Start();
        }
    }
}