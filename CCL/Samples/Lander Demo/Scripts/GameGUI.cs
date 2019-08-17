using UnityEngine;
using UnityEngine.UI;

namespace LanderDemo
{
    public class GameGUI : MonoBehaviour
    {
        [SerializeField]
        private Text _accelerationText;

        [SerializeField]
        private Text _centerThrusterText;

        [SerializeField]
        private Text _detailsText;

        [SerializeField]
        private Text _leftThrusterText;

        [SerializeField]
        private Text _rightThrusterText;

        [SerializeField]
        private Text _velocityText;

        public void OnCenterThrustChange(Thruster thruster)
        {
            _centerThrusterText.text = "FUEL CNTR: " + thruster.fuel + " / " + thruster.maxFuel;
        }

        public void OnLeftThrustChange(Thruster thruster)
        {
            _leftThrusterText.text = "FUEL LEFT: " + thruster.fuel + " / " + thruster.maxFuel;
        }

        public void OnRightThrustChange(Thruster thruster)
        {
            _rightThrusterText.text = "FUEL RGHT: " + thruster.fuel + " / " + thruster.maxFuel;
        }

        public void OnUpdateAccelerometer(Accelerometer acc)
        {
            _accelerationText.text = "ACC: " + acc.acceleration.ToString();
            _velocityText.text = "VEL: " + acc.velocity + " " + acc.angularVelocity.ToString("0.00") + " DEG/S";
        }

        public void OnUpdateGroundRange(RangeFinder rangeFinder)
        {
            _detailsText.text = "RNG: " + rangeFinder.range.ToString("0.00") + " M FROM SURFACE";
        }
    }
}