namespace OmegaWarhead.Events
{
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using MEC;
    using System.Collections.Generic;
    internal sealed class WarheadHandler
    {
        public static bool OmegaPhase { get; private set; } = false;
        public List<CoroutineHandle> Coroutines = new List<CoroutineHandle>();
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
            else if (OmegaPhase)
            {
                foreach(Player player in Player.List)
                {
                    if (player != null && player.Role != RoleType.Spectator)
                    {
                        player.Kill("Omega Warhead");
                    }
                }
            }
        }
        public void OnWaitingForPlayers()
        {
            OmegaPhase = false;
            foreach(CoroutineHandle coroutine in Coroutines)
            {
                Timing.KillCoroutines(coroutine);
            }
            Coroutines.Clear();
        }
        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            OmegaPhase = false;
            foreach (CoroutineHandle coroutine in Coroutines)
            {
                Timing.KillCoroutines(coroutine);
            }
            Coroutines.Clear();
        }
        public IEnumerator<float> RadiationCoroutine()
        {
            yield return Timing.WaitForSeconds(Plugin.Singleton.Config.Delay);
            while(Round.IsStarted)
            {
                foreach (Player player in Player.List)
                {
                    player.Hurt("Radiation", Plugin.Singleton.Config.RadiationDamage);
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