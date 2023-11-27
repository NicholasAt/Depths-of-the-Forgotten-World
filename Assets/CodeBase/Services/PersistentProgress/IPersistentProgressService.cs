using CodeBase.Data.Progress;

namespace CodeBase.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress PlayerProgress { get; set; }
    }
}