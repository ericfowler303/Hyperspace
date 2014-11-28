﻿using System;
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
            Hyperspace game = new Hyperspace();
            game.PlayGame();
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

    class Hyperspace
    {
        public int Score { get; set; }
        public int Speed { get; set; }
        public List<Obstacle> ObstacleList { get; set; }
        public Obstacle SpaceShip { get; set; }
        public bool Smashed { get; set; }

        private Random rng = new Random();

        public Hyperspace()
        {
            this.Score = 0;
            this.Speed = 1;
            this.ObstacleList = new List<Obstacle>();
            // Setup the Console Window
            Console.BufferHeight = 30;
            Console.WindowHeight = 30;
            Console.BufferWidth = 60;
            Console.WindowWidth = 60;

            // Place the user ship at the bottom center of the window
            this.SpaceShip = new Obstacle((Console.WindowWidth/2),(Console.WindowHeight-1),ConsoleColor.Red);
        }

        public void PlayGame()
        {

        }
    }
}
