using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SVTRobotDistance.Models;
using System.Drawing;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SVTRobotDistance.Controllers
{

    [ApiController]
    [Route("api/robots/closest")]
    public class RobotController : ControllerBase
    {

        static string robotURL = "https://60c8ed887dafc90017ffbd56.mockapi.io/robots";
        static HttpClient client = new HttpClient();

        private readonly ILogger<RobotController> _logger;

        public RobotController(ILogger<RobotController> logger)
        {
            _logger = logger;
        }
        private async Task<List<Robot>> getRobots(CancellationToken cts = default)
        {
            List<Robot> robots = new List<Robot>();
            var response = await client.GetAsync(robotURL);
            response.EnsureSuccessStatusCode();
            string data = await response.Content.ReadAsStringAsync();
            robots = JsonSerializer.Deserialize<List<Robot>>(data, new JsonSerializerOptions
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            });
            return robots;
        }

        [HttpPost]
        public async Task<RobotDistance> GetClosestRobot([FromBody] RobotRequest request)
        {
            List<Robot> robots = (List<Robot>) await getRobots();

            RobotDistance correctRobot = new RobotDistance
            {
                distanceToGoal = -1,
                robotId = -1,
                batteryLevel = -1
            };

            //figure out closest robot
            foreach (Robot r in robots)
            {
                r.distance = Math.Pow(r.x - request.x, 2) + Math.Pow(r.y - request.y, 2);
                if(correctRobot.distanceToGoal == -1 || r.distance < correctRobot.distanceToGoal)
                {
                    correctRobot = new RobotDistance
                    {
                        batteryLevel = r.batteryLevel,
                        robotId = r.robotId,
                        distanceToGoal = r.distance
                    };
                }
            }

            //reduce calculations, do this once.
            if(correctRobot.distanceToGoal >= 0)
                correctRobot.distanceToGoal = Math.Sqrt(correctRobot.distanceToGoal);

            return correctRobot;
        }
    }
}