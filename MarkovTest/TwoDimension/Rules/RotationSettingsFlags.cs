using System;

namespace Markov.MarkovTest.TwoDimension.Rules
{
    [Flags]
    public enum RotationSettingsFlags
    {
        None = 0,
        Rotate = 1,
        FlipX = 2,
        FlipY = 4
    }

    public static class RotationSettingsFlagsExtensions
    {
        public static RotationSettingsFlags All() =>
            RotationSettingsFlags.Rotate | RotationSettingsFlags.FlipX | RotationSettingsFlags.FlipY;
    }
}