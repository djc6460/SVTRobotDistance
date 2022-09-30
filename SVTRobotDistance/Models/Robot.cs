namespace SVTRobotDistance.Models
{
    public class Robot
    {
        public int robotId { get; set; }
        public int batteryLevel { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        public double distance { get; set; }
    }
}