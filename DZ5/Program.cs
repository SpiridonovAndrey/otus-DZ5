using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DZ5
{
    class Program
    {
        static void Main(string[] args)
        {
            Quadcopter quadcopter = new Quadcopter();
            string components = "";
            foreach (string component in quadcopter.GetComponents())
                components += "'" + component + "', ";
            Console.WriteLine(quadcopter.GetRobotType());
            Console.WriteLine(components);
            quadcopter.Charge();
            Console.WriteLine(quadcopter.GetInfo());
            Thread.Sleep(8000);
            Console.WriteLine(quadcopter.GetInfo());
            quadcopter.Charge();
            Console.WriteLine(quadcopter.GetInfo());
            Console.ReadKey();
        }
        public interface IRobot
        {
            string GetInfo();
            List<string> GetComponents();
            string GetRobotType() => "I am a simple robot.";
        }
        public interface IChargeable
        {
            string Charge();
            string GetInfo();
        }
        public interface IFlyingRobot : IRobot
        {
            new string GetRobotType() => "I am a flying robot.";
        }
        public class Quadcopter : IFlyingRobot, IChargeable
        {
            List<string> Components = new List<string> { "rotor1", "rotor2", "rotor3", "rotor4" };
            string Info { get; set; } = "Квадрокоптер K-700FGB";
            DateTime WhenCharged { get; set; }
            public string GetInfo()
            {
                var seconds = (DateTime.Now.Subtract(WhenCharged)).Ticks / TimeSpan.TicksPerSecond;
                var Charge = Math.Round(100 - (double)seconds) > 0 ? Math.Round(100 - (double)seconds) : 0;
                return Info + ". Заряд: " + Math.Round(100 - (double)seconds) + "%. С последней зарядки прошло: " + seconds;
            }
            public List<string> GetComponents()
            {
                return Components;
            }
            public string GetRobotType() => "I am a simple flying robot.";
            public string Charge()
            {
                Console.Write("Charging...");
                Thread.Sleep(3000);
                Console.Write("Charged!");
                Console.WriteLine();
                WhenCharged = DateTime.Now;
                return "";
            }
        }
    }
}