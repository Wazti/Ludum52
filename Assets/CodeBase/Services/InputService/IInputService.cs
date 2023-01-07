using UnityEngine;

namespace CodeBase.Services.InputService
{
    public interface IInputService
    {
        Vector2 Axis { get; }

        bool IsIntakeButton();
        bool IsBoostButton();
    }
}