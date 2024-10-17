using System.Windows.Forms;
using CHESS.board;
using CHESS.pieces;

namespace CHESS
{
    partial class chess
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponents()
        {
            InitializeBoard();

            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(500, 600);
            BackColor = Color.LightGray;
            Name = "chess";
            Text = "chess!";
            ResumeLayout(false);
        }

        private void InitializeBoard()
        {


            boardSquares = new ChessSquarePictureBox[BOARDHEIGHT][];
            for (int colIndex = 0; colIndex < BOARDHEIGHT; colIndex++)
            {
                boardSquares[colIndex] = new ChessSquarePictureBox[BOARDWIDTH];
                for (int rowIndex = 0; rowIndex < BOARDWIDTH; rowIndex++)
                {
                    ChessSquarePictureBox Square = new ChessSquarePictureBox(colIndex, rowIndex);
                    chess.boardSquares[colIndex][rowIndex] = Square;

                    ((System.ComponentModel.ISupportInitialize)Square).BeginInit();
                    Controls.Add(Square);
                    ((System.ComponentModel.ISupportInitialize)Square).EndInit();


                }
            }
        }



    }
}