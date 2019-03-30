using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Snake {
    public partial class Form1 : Form {

        private List<Circle> Snake = new List<Circle>();
        private Circle food = new Circle();

        public Form1() {
            InitializeComponent();

            new Settings();

            gameTimer.Interval = 1000 / Settings.Speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();

            StartGame();
        }

        private void StartGame() {
            lblGameOver.Visible = false;
            new Settings();

            Snake.Clear();
            Circle head = new Circle { X = 10, Y = 5 };
            Snake.Add(head);

            lblScore.Text = Settings.Score.ToString();
            GenerateFood();
        }

        private void UpdateScreen(object sender, EventArgs e) {
            if (Settings.GameOver == true) {
                if (Input.KeyPressed(Keys.Enter)) {
                    StartGame();
                }
            } else {
                if (Input.KeyPressed(Keys.Right) && Settings.direction != Settings.Direction.Left)
                    Settings.direction = Settings.Direction.Right;
                else if (Input.KeyPressed(Keys.Left) && Settings.direction != Settings.Direction.Right)
                    Settings.direction = Settings.Direction.Left;
                else if (Input.KeyPressed(Keys.Up) && Settings.direction != Settings.Direction.Down)
                    Settings.direction = Settings.Direction.Up;
                else if (Input.KeyPressed(Keys.Down) && Settings.direction != Settings.Direction.Up)
                    Settings.direction = Settings.Direction.Down;

                MovePlayer();
            }

            pbCanvas.Invalidate();
        }

        private void GenerateFood() {
            int maxXPos = pbCanvas.Size.Width / Settings.Width;
            int maxYPos = pbCanvas.Size.Height / Settings.Height;

            Random random = new Random();
            food = new Circle { X = random.Next(0, maxXPos), Y = random.Next(0, maxYPos) };
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e) {
            Graphics canvas = e.Graphics;

            if (!Settings.GameOver) {

                for (int i = 0; i < Snake.Count; ++i) {
                    Brush snakeColor;
                    if (i == 0) {
                        snakeColor = Brushes.Black;
                    } else {
                        snakeColor = Brushes.Green;
                    }
                    canvas.FillEllipse(snakeColor, new Rectangle(Snake[i].X * Settings.Width, Snake[i].Y * Settings.Height, Settings.Width, Settings.Height));
                    canvas.FillEllipse(Brushes.Red, new Rectangle(food.X * Settings.Width, food.Y * Settings.Height, Settings.Width, Settings.Height));
                }
            } else {
                string gameOver = "Press Enter tor try again:";
                lblGameOver.Text = gameOver;
                lblGameOver.Visible = true;
            }
        }

        private void MovePlayer() {
            for(int i = Snake.Count - 1; i >= 0; i--) {
                if(i == 0) {
                    switch (Settings.direction) {
                        case Settings.Direction.Right:
                            Snake[i].X++;
                            break;
                        case Settings.Direction.Left:
                            Snake[i].X--;
                            break;
                        case Settings.Direction.Up:
                            Snake[i].Y--;
                            break;
                        case Settings.Direction.Down:
                            Snake[i].Y++;
                            break;
                    }

                    int maxXPos = pbCanvas.Size.Width / Settings.Width;
                    int maxYPos = pbCanvas.Size.Height / Settings.Height;

                    if (Snake[i].X < 0 || Snake[i]. Y < 0
                        || Snake[i].X >= maxXPos || Snake[i].Y >= maxYPos) {
                        Die();
                    }

                    if (Snake[0].X == food.X && Snake[0].Y == food.Y) {
                        Eat();
                    }
                } else {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        }

        private void Eat() {

            Circle circle = new Circle {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };
            Snake.Add(circle);

            Settings.Score += Settings.Points;
            lblScore.Text = Settings.Score.ToString();

            GenerateFood();
        }

        private void Die() {
            Settings.GameOver = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            Input.ChangeState(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e) {
            Input.ChangeState(e.KeyCode, true);
        }

        private void lblGameOver_Click(object sender, EventArgs e) {

        }
    }
}