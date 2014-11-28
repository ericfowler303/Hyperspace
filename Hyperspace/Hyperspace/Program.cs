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
    class Obstacle : IEquatable<Obstacle>
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
            this.IsSpaceRift = false;
        }
        public Obstacle(int x, int y, ConsoleColor specColor)
        {
            this.XCoord = x;
            this.YCoord = y;
            this.Color = specColor;
            this.IsSpaceRift = false;
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

        // Implement methods needed for IEquatable interface
        public bool Equals(Obstacle otherObstacle)
        {
            //Check whether the compared object is null.
            if (Object.ReferenceEquals(otherObstacle, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, otherObstacle)) return true;

            //Check whether the products' properties are equal.
            return this.XCoord.Equals(otherObstacle.XCoord) && this.YCoord.Equals(otherObstacle.YCoord);
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.
        public override int GetHashCode()
        {
            //Get hash code for the Name field if it is not null.
            int hashXVal = XCoord.GetHashCode();
            int hashYVal = YCoord.GetHashCode();

            //Calculate the hash code for the Point
            return hashXVal ^ hashYVal;
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
            this.Smashed = false;
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
            // Play the game until the player gets smashed by an obstacle
            while (!this.Smashed)
            {
                if (rng.Next(0, 101) < 10)
                {
                    // 10% chance to make a space rift
                    Obstacle obst = new Obstacle(rng.Next(0, Console.WindowWidth - 2), 5);
                    obst.IsSpaceRift = true;
                    this.ObstacleList.Add(obst);
                }
                else
                {
                    Obstacle obst = new Obstacle(rng.Next(0, Console.WindowWidth - 2), 5);
                }

                MoveShip();
                MoveObstacles();
                DrawGame();

                // Up the speed of the game until it hits 170
                if (this.Speed < 170) { this.Speed++;}
            }
        }
        public void MoveShip()
        {
            ConsoleKeyInfo keyPressed;
            if (Console.KeyAvailable)
            {
                while (Console.KeyAvailable) { Console.ReadKey(true); }
                keyPressed = Console.ReadKey();
            }
            // Future-proof for possible new features
            switch (keyPressed.Key)
            {
                case ConsoleKey.LeftArrow:
                    if (this.SpaceShip.XCoord < 0) { this.SpaceShip.XCoord--; }
                    break;
                case ConsoleKey.RightArrow:
                    if (this.SpaceShip.XCoord < (Console.WindowWidth - 2)) { this.SpaceShip.XCoord++; }
                    break;
            }
        }
        public void MoveObstacles()
        {
            List<Obstacle> newObstacleList = new List<Obstacle>();
            // Check which enemies are still on the screen
            foreach (Obstacle enemy in ObstacleList)
            {
                // Move their coordinate
                enemy.YCoord++;
                // See if this a spacerift collided with the user
                if (enemy.IsSpaceRift && enemy.Equals(this.SpaceShip))
                {
                    // Slow the ship because they hit the rift
                    this.Speed -= 50;
                }
                else if (!enemy.IsSpaceRift && enemy.Equals(this.SpaceShip))
                {
                    // User collided with an enemy, game over
                    this.Smashed = true;
                }

                // See which units are still on the screen, only keep those
                if (enemy.YCoord < Console.WindowHeight) { newObstacleList.Add(enemy); }

                // Since the user has passed this frame without dying, add a point to their score
                this.Score++;

                // Now that the obstacles have been updated, set it to the current list
                this.ObstacleList = newObstacleList;

            }
        }
        public void DrawGame()
        {

        }
    }
}
