using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyperspace
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
    class Obstacle
    {
        public int XCoord { get; set; }
        public int YCoord { get; set; }
        public ConsoleColor Color { get; set; }
        public string Symbol { get; set; }
        public bool IsSpaceRift { get; set; }

        static List<string> ObstacleList = new List<string>() { "*","!", ".",":", ";", "`", "'"};
        static Random rng = new Random();

        public Obstacle(int x, int y)
        {
            this.XCoord = x;
            this.YCoord = y;
            this.Color = ConsoleColor.Cyan;
        }
        public Obstacle(int x, int y, ConsoleColor specColor)
        {
            this.XCoord = x;
            this.YCoord = y;
            this.Color = specColor;
        }
        /// <summary>
        /// Draw the Obstacle object to it's specified place on the screen
        /// </summary>
        public void Draw()
        {
            Console.SetCursorPosition(this.XCoord, this.YCoord);
            Console.ForegroundColor = this.Color;
            Console.Write(this.Symbol);
        }
    }
}
