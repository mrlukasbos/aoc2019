using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;

namespace aoc2017.day_3 {
    public class Program {
        
        public static int solvePartOneForInput(int input) {
            var ring = 0;
            var result = 1;

            // First find the bottom right corner, which is always 1 + (8 * 1) + (8 * 2) + (8 *3) + ... 
            while (result < input) {
                ring++;
                result += ring * 8;
            }
            
            // Prevent division-by-zero issues for small inputs
            if (ring == 0) {
                return 0; 
            }
            
            // At this point we know in what 'ring' our input is. 
            // We use that to calculate the manhattan distance to the bottom right corner
            var manhattanDistanceBottomRightCorner = ring * 2;

            // Determine how many steps back from the bottom right corner the result is. 
            // Then we take to modulo by the lenght of one 'side' of the ring (which is equal to manhattanDistanceBottomRightCorner)
            // This will get the distance to one of the corners.
            // Then we can take manhattanDistanceBottomRightCorner - distance to one of the closest corner, and voila
            var stepsBack = result - input;
            var distanceFromMax = stepsBack % manhattanDistanceBottomRightCorner;
            if (distanceFromMax > ring) distanceFromMax = (ring * 2) - distanceFromMax;
            return manhattanDistanceBottomRightCorner - distanceFromMax;
        }


        enum Direction {
            RIGHT = 0,
            UP = 1,
            LEFT = 2,
            DOWN = 3,
        }
 
        public static int solvePartTwoForInput(int input) {
            var x = 100;
            var y = 100;
            var matrix = new int[200, 200];

            var directions = new(int x, int y)[] {
                (1, 0), // right
                (0, 1), // up
                (-1, 0), // left
                (0, -1), // down
            };
            var currentDirection = Direction.RIGHT;
            matrix[y, x] = 1; // initial element
            var stepSize = 1;
            var stepsOnThisSide = 0;
            var turns = 0;
            while (matrix[x, y] < input) {
                if (stepsOnThisSide == stepSize) {
                    stepsOnThisSide = 0;
                    currentDirection = (Direction) ((int)(currentDirection + 1) % directions.Length); // cycle to the next direction
                    Console.WriteLine("turning to direction: " + currentDirection);
                    turns++;
                    if (turns % 2 == 0) {
                        stepSize++;
                        Console.WriteLine("increased stepsize to" + stepSize);
                    }
                }
                
                stepsOnThisSide++;
                
                // Move our location and calculate the new value
                x += directions[(int)currentDirection].x;
                y += directions[(int)currentDirection].y;

                matrix[x, y] =
                    matrix[x + 1, y] +
                    matrix[x + 1, y + 1] +
                    matrix[x + 1, y - 1] +
                    matrix[x, y + 1] +
                    matrix[x, y - 1] +
                    matrix[x - 1, y + 1] +
                    matrix[x - 1, y] +
                    matrix[x - 1, y - 1];
                Console.WriteLine("writing to location: [" + y + ", " + x + "]: " + matrix[x, y]);

            }
            return matrix[x, y];
        }
    }
}