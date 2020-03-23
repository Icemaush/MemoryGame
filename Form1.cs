using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace MemoryGame
{
    public partial class MemoryGame : Form
    {
        readonly Stopwatch timer = new Stopwatch();
        string elapsedTime;
        int tilePoints;
        int score;
        int accuracy;
        int timePenalty;
        int revealedTiles = 0;
        int guesses;

        public IList<Color> colorList = new List<Color>();
        IList<Button> selectedTiles = new List<Button>();

        Color[] colors =
        {
            Color.White, Color.Black, Color.Cyan, Color.Blue,
            Color.DarkRed, Color.Red, Color.Green, Color.Lime,
            Color.Yellow, Color.Goldenrod, Color.DarkOrange, Color.DeepPink,
            Color.Purple, Color.SaddleBrown, Color.DarkBlue
        };

        public MemoryGame()
        {
            InitializeComponent();
            MessageBox.Show("Reveal tiles of matching colours until all tiles are revealed!");
            NewGame();
        }

        public void NewGame()
        {
            StartTimer();
            tilePoints = 0;
            score = 0;
            guesses = 0;
            revealedTiles = 0;
            lblScoreCount.Text = score.ToString();
            colorList.Clear();
            foreach (var color in colors)
            {
                colorList.Add(color);
                colorList.Add(color);
            }

            Random rnd = new Random();
            int colorIndex;
            int colorNum = 30;
            foreach (var button in this.Controls.OfType<Button>())
            {
                if (button.Text != "New Game" && colorNum >= 0)
                {
                    button.BackColor = Color.Gray;
                    button.Enabled = true;
                    colorIndex = rnd.Next(0, colorNum);
                    button.ForeColor = colorList[colorIndex];
                    colorList.RemoveAt(colorIndex);
                }
                colorNum--;
            }
        }

        public void OnButtonClick(object sender, EventArgs e)
        {
            if (selectedTiles.Count == 2)
            {
                foreach (var button in selectedTiles)
                {
                    button.BackColor = Color.Gray;
                    button.Enabled = true;
                }
                selectedTiles.Clear();
            }

            ((Button)sender).BackColor = ((Button)sender).ForeColor;
            ((Button)sender).Enabled = false;
            selectedTiles.Add((Button)sender);

            if (selectedTiles.Count == 2)
            {
                guesses++;
                if (selectedTiles[0].BackColor == selectedTiles[1].BackColor)
                {
                    tilePoints += 20;
                    UpdateScore();
                    revealedTiles += 2;
                    selectedTiles.Clear();

                    if (revealedTiles == 30)
                    {
                        StopTimer();
                        CalculateScore();

                        ScoreWindow scorewindow = new ScoreWindow
                        {
                            elapsed = elapsedTime,
                            tp = tilePoints,
                            sc = score,
                            acc = accuracy,
                            penalty = timePenalty
                        };
                        scorewindow.UpdateLabels();
                        scorewindow.ShowDialog();
                    }
                }
                else
                {
                    tilePoints -= 1;
                    UpdateScore();
                }
            }
        }

        private void StartTimer()
        {
            timer.Reset();
            timer.Start();
        }

        private void StopTimer()
        {
            timer.Stop();
            if (timer.Elapsed.Seconds < 10)
            {
                elapsedTime = timer.Elapsed.Minutes + ":0" + timer.Elapsed.Seconds;
            }
            else
            {
                elapsedTime = timer.Elapsed.Minutes + ":" + timer.Elapsed.Seconds;
            }
        }

        private void CalculateScore()
        {
            timePenalty = (timer.Elapsed.Minutes * 60) + timer.Elapsed.Seconds;
            score = tilePoints - timePenalty;
            accuracy = (int)Math.Floor((double)15 / guesses * 100);
            score += accuracy;
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void UpdateScore()
        {
            lblScoreCount.Text = tilePoints.ToString();
        }

        private void GuessingGame_Load(object sender, EventArgs e)
        {

        }
    }
}
