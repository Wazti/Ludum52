using System;

namespace CodeBase.Services
{
    public interface ICurtainService
    {
        public void DOFade(int value, float duration, Action callback, bool ignoreTimeScale = false);
        public void SetFade(int value);
    }
}