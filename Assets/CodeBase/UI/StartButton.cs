using CodeBase.Infrastructure;
using CodeBase.Services;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class StartButton : MonoBehaviour
    {
        [Inject] private ISceneLoader _sceneLoader;

        [Inject] private ICurtainService _curtainService;

        public void StartLevel()
        {
            _curtainService.DOFade(1, .5f, () =>
            {
                _sceneLoader.Load("Main", () =>
                {
                    _curtainService.DOFade(0, .5f, ()=> { });
                });
            });
        }
    }
}