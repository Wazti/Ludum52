using UnityEngine;

namespace CodeBase.Services.InputService
{
    public class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        private const string Intake = "Intake";
        private const string Boost = "Boost";


        public bool IsIntakeButtonUp() => Input.GetButtonUp(Intake);

        public bool IsIntakeButtonDown() => Input.GetButtonDown(Intake);

        public bool IsBoostButton() => Input.GetButton(Boost);

        public Vector2 Axis
        {
            get => UnityAxis();
        }

        private static Vector2 UnityAxis()
        {
            return new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
        }
    }
}