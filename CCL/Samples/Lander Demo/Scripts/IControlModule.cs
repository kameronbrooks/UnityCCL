namespace LanderDemo
{
    public interface IControlModule
    {
        Accelerometer accelerometer { get; }

        Thruster centerThruster { get; }

        Thruster leftThruster { get; }

        RangeFinder rangeFinder { get; }

        Thruster rightThruster { get; }

        bool Override(string overrideCode);

        void SetControlMode(int mode);
    }
}