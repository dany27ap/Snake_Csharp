using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake {
    class Settings {

        public enum Direction {
            Right,
            Left,
            Up,
            Down
        };

        public static int Width { set; get; }
        public static int Height { set; get; }
        public static int Speed { set; get; }
        public static int Score { set; get; }
        public static int Points { set; get; }
        public static bool GameOver { set; get; }
        public static Direction direction { set; get; }

        public Settings() {
            Width = 16;
            Height = 16;
            Speed = 15;
            Score = 0;
            Points = 1;
            GameOver = false;
            direction = Direction.Right;
        }
    }
}
