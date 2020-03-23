using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class ScoreWindow : Form
    {
        public string elapsed;
        public int tp;
        public int sc;
        public int acc;
        public int penalty;
        public ScoreWindow()
        {
            InitializeComponent();
        }

        public void UpdateLabels()
        {
            lblTimeVar.Text = elapsed;
            lblAccuracyVar.Text = acc.ToString() + "%";
            lblTilePointsVar.Text = tp.ToString();
            lblTimePenaltyVar.Text = "-" + penalty.ToString();
            lblAccuracyBonusVar.Text = "+" + acc.ToString();
            lblTotalScoreVar.Text = sc.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
