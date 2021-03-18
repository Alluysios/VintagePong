using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GDIDrawer;

namespace VintagePong
{
    class Program
    {
        static void Main(string[] args)
        {
            bool gameOver = false;      // Exit the Game
            bool gameExit = false;      // Exit the Program
            bool start = false;         // Start the Game

            // Instance of random.
            Random rand = new Random();

            //x ball position
            int iX = 3;
            //y ball position
            int iY = rand.Next(2, 115);

            //amount ball moves in x direction for every loop
            int iXVelocity = 3;

            //amount ball moves in y direction for every loop
            int iYVelocity = 3;

            // Game score
            int score = 0;

            // Ball speed delay
            int ballSpeed = 120;

            //create a drawer window
            CDrawer Canvas = new CDrawer();
            Canvas.Scale = 5;

            // coordinates.
            Point coord;

            // Mouse position in Canvas
            int mouseYPos = 70;


            //loop until the ball leaves the visible window
            while (((iX < 160) || (iX > 0)) && !gameExit)
            {
                while (gameOver == false)
                {
                    // Erase the old ball and paddle.
                    Canvas.Clear();

                    // If left mouse click start the game.
                    if (Canvas.GetLastMouseLeftClick(out coord))
                        start = true;

                    // if start is true start the game.
                    if (start)
                    {
                        // Get the mouse position in the canva and scale it to 5.
                        if (Canvas.GetLastMousePosition(out coord))
                            mouseYPos = (coord.Y / 5) - 5;

                        // Add the paddle.
                        Canvas.AddRectangle(0, mouseYPos, 1, 10, Color.Red);

                        //draw the new ball
                        Canvas.AddEllipse(iX, iY, 2, 2);

                        //time delay to slow down the ball
                        System.Threading.Thread.Sleep(ballSpeed);

                        // If mouse is on paddle position bounce back.
                        if ((iY - 9) <= mouseYPos && (iY + 1) >= mouseYPos && iX <= 2)
                        {
                            // Reverse speed position (goes opposite way).
                            iXVelocity = -iXVelocity;
                            iYVelocity = -iYVelocity;

                            // increment the ball speed.
                            ballSpeed = ballSpeed <= 20 ? 1 : ballSpeed - 20;

                            // increment score.
                            score++;
                        }

                        // Increment the position with the velocity.
                        iX += iXVelocity;
                        iY += iYVelocity;

                        //check for bouncing off of the lower edge of the window
                        if ((iY > 118) || (iY < 0))
                            //reverse the y velocity (ball goes up)
                            iYVelocity = -iYVelocity;

                        // If ball position is greater than 160 reverse the X velocity (ball goes left).
                        if (iX > 160)
                            iXVelocity = -iXVelocity;

                        // If ball position is less than 0 game is over.
                        if (iX < 0)
                            gameOver = true;
                    }
                }

                // Erase the old ball and paddle.
                Canvas.Clear();

                // Display the final score and option buttons to play again and quit the game.
                Canvas.AddText($"Final Score: {score}", 36, Color.Gray);
                Canvas.AddText("Play Again", 12, 85, 84, 30, 45, Color.Green);
                Canvas.AddText("Quit", 12, 120, 84, 30, 45, Color.Gray);
                Canvas.AddRectangle(87, 103, 25, 8, Color.Empty, 1, Color.Green);
                Canvas.AddRectangle(122, 103, 25, 8, Color.Empty, 1, Color.Gray);
                while (gameOver == true)
                {
                    if (Canvas.GetLastMouseLeftRelease(out coord))
                    {
                        Console.WriteLine("Click!");
                    }
                    // If play again button is click start the game.
                    if (Canvas.GetLastMouseLeftClick(out coord) && (coord.X >= 435 && coord.X <= 560) && (coord.Y >= 515 && coord.Y <= 555))
                    {
                        // Initialize ball position and speed.
                        iX = 3;
                        iY = rand.Next(2, 115);
                        iXVelocity = 3;
                        iYVelocity = 3;
                        ballSpeed = 120;
                        score = 0;

                        // Start the game.
                        gameOver = false;
                        start = true;
                    }

                    // If quit button is click quit the game.
                    if (Canvas.GetLastMouseLeftClick(out coord) && (coord.X >= 610 && coord.X <= 735) && (coord.Y >= 515 && coord.Y <= 555))
                    {
                        // Exit the game.
                        gameExit = true;
                        gameOver = false;
                    }
                }
            }
        }
    }
}
