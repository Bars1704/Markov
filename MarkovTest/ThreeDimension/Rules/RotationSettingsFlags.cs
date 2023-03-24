namespace MarkovTest.ThreeDimension.Rules
{
    [Flags]
    public enum RotationSettingsFlags
    {
        None = 0,
        RotateX = 1,
        RotateY = 2,
        RotateZ = 4,
        FlipX = 8,
        FlipY = 16,
        FlipZ = 32,
    }

    public static class RotationSettingsFlagsExtensions
    {
        public static RotationSettingsFlags All() =>
            RotationSettingsFlags.RotateX |
            RotationSettingsFlags.RotateY |
            RotationSettingsFlags.RotateZ |
            RotationSettingsFlags.FlipX |
            RotationSettingsFlags.FlipY |
            RotationSettingsFlags.FlipZ;
    }
}