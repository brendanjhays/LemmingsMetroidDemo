
namespace LemmingsMetroid.DataTypes
{
    public partial class PlatformerValues
    {
        public string Name;
        public float MaxSpeedX;
        public float AccelerationTimeX;
        public float DecelerationTimeX;
        public float Gravity;
        public float MaxFallSpeed;
        public float JumpVelocity;
        public float JumpApplyLength;
        public bool JumpApplyByButtonHold;
        public bool UsesAcceleration;
        public bool CanFallThroughCloudPlatforms;
        public float CloudFallThroughDistance;
        public bool IsUsingCustomDeceleration;
        public float CustomDecelerationValue;
        public bool MoveSameSpeedOnSlopes;
        public System.Decimal UphillFullSpeedSlope;
        public System.Decimal UphillStopSpeedSlope;
        public System.Decimal DownhillFullSpeedSlope;
        public System.Decimal DownhillMaxSpeedSlope;
        public System.Decimal DownhillMaxSpeedBoostPercentage;
        public bool CanClimb;
        public float MaxClimbingSpeed;
        public int InheritOrOverwriteAsInt;
        public const string Ground = "Ground";
        public const string Air = "Air";
        public static System.Collections.Generic.List<System.String> OrderedList = new System.Collections.Generic.List<System.String>
        {
        Ground
        ,Air
        };
        
        
    }
}
