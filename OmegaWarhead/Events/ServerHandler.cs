namespace OmegaWarhead.Events
{
    using Exiled.Events.EventArgs;
    using MEC;
    public sealed partial class Handler
    {
        public void OnWaitingForPlayers()
        {
            OmegaPhase = false;
            
            Timing.KillCoroutines(Coroutines.ToArray());
            Coroutines.Clear();
        }
        
        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            OmegaPhase = false;
            
            Timing.KillCoroutines(Coroutines.ToArray());
            Coroutines.Clear();
        }
    }
}