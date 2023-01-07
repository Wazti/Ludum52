using CodeBase.Data;

namespace CodeBase.Services.PersistentProgress
{
    public interface IPersistentProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}