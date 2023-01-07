using CodeBase.Data;
using CodeBase.Hero;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services.PersistentProgress;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private IGameFactory _gameFactory;
        private IPersistentProgressService _progressService;
        private const string ProgressKey = "Progress";


        [Inject]
        public void Construct(IPersistentProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
                progressWriter.UpdateProgress(_progressService.Progress);

            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgress()
        {
            return PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
        }
    }
}