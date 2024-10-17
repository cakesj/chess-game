using CHESS.pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHESS
{
    internal class PickPromotionForm : Form
    {
        static readonly int BUTTONSIZE = 75;
        static readonly int MINWINDOWWIDTH = 350;

        private Button[] options;


        internal string UserResponse { get; private set; }

        internal PickPromotionForm()
        {
            this.Text = "Choose your promotion";
        }

        internal void ShowDialog(string[] choices, PieceColor Color)
        {
            int windowWidth = choices.Length * BUTTONSIZE + 15;
            windowWidth = windowWidth < MINWINDOWWIDTH ? MINWINDOWWIDTH : windowWidth;
            this.Size = new System.Drawing.Size(windowWidth + 1, BUTTONSIZE + 40);

            this.options = new Button[choices.Length];
            for (int i = 0; i < choices.Length; i++)
            {
                this.options[i] = new Button();
                this.options[i].BackgroundImage = Image.FromFile($"..\\..\\..\\Resources\\WHT_{choices[i]}.png");
                this.options[i].BackgroundImageLayout = ImageLayout.Stretch;
                this.options[i].Location = new Point(i * BUTTONSIZE, 0);
                this.options[i].Size = new Size(BUTTONSIZE, BUTTONSIZE);
                string option = choices[i];
                this.options[i].Click += (sender, e) => Chosen(option);
                this.Controls.Add(this.options[i]);
            }
            base.ShowDialog();
        }

        private void Chosen(string chosen)
        {
            this.UserResponse = chosen;
            this.Close();
        }
    }
}
