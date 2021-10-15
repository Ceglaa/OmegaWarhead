namespace OmegaWarhead.Events
{
    using System.Collections.Generic;
    using Exiled.Events.EventArgs;
    using Exiled.API.Features;
    using MEC;
    using UnityEngine;
    using Exiled.API.Enums;
    internal sealed class WarheadHandler
    {
        CoroutineHandle coroutine;
        public void OnDetonating()
        {
            if (Plugin.Singleton.OmegaPhase == false && Plugin.Singleton.Config.RadiationMode == false)
            {
                Plugin.Singleton.OmegaPhase = !Plugin.Singleton.OmegaPhase;
                Timing.CallDelayed(Plugin.Singleton.Config.Delay, () => Warhead.Start());
            }
            else if (Plugin.Singleton.OmegaPhase == true && Plugin.Singleton.Config.RadiationMode == false)
            {
                foreach (Player player in Player.List)
                {
                    player.Kill(DamageTypes.Nuke);
                }
                Plugin.Singleton.OmegaPhase = !Plugin.Singleton.OmegaPhase;
            }
            if (Plugin.Singleton.Config.RadiationMode == true)
            {
                Timing.CallDelayed(Plugin.Singleton.Config.Delay, () => Timing.RunCoroutine(DamageCoroutine()));
            }
        }
        public void OnWaitingForPlayers()
        {
            Timing.KillCoroutines(coroutine);
            Plugin.Singleton.OmegaPhase = false;
        }
        public IEnumerator<float> DamageCoroutine()
        {
            for(; ; )
            {
                foreach(Player player in Player.List)
                {
                    player.Hurt(Plugin.Singleton.Config.RadiationDamage, DamageTypes.Lure);
                }
                yield return Timing.WaitForSeconds(Plugin.Singleton.Config.DelayBetweenDamage);
            }
        }
    }
}