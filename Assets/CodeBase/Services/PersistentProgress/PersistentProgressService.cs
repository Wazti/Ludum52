using CodeBase.Data;

namespace CodeBase.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress Progress { get; set; }

        public PersistentProgressService()
        {
            Progress = new PlayerProgress("");
            FMODUnity.RuntimeManager.CoreSystem.mixerSuspend();
            FMODUnity.RuntimeManager.CoreSystem.mixerResume();
        }
    }
}