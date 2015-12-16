using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_WINForms_Pyatnashki
{
    public partial class Form1 : Form
    {
        public int[] curPos;
        public int[] defPos;
        public Button[] buttonsMas;
        public Form1()
        {
            InitializeComponent();
            buttonsMas = new Button[16];
            curPos = new int[16];
            defPos = new int[16];

            for (int i = 0; i < 16; i++)
            {
                buttonsMas[i] = new Button();
                defPos[i] = 1 + i;
            }

            newGame();
        }

        public void newGame()
        {
            int i, j;
            Random Rand = new Random();
            while (!checkIsSolveble())
            {
                for (i = 0; i < 16; i++)
                {
                XYZ: curPos[i] = 1 + Rand.Next() % 16;
                    for (j = 0; j < i; j++)
                    {
                        if (curPos[i] == curPos[j] && i != j)
                            goto XYZ;
                    }
                }
            }
            writeNumsOnButtons();
            setButtonsPositionsAndStyle();
        }

        public void writeNumsOnButtons()
        {
            for (int i = 0; i<16; i++)
            {
                buttonsMas[i].Text = curPos[i].ToString();
                makeButtonInvizible(buttonsMas[i], curPos[i]);
            }
        }
        public void makeButtonInvizible(Button tmp, int val)
        {
            if (val == 16)
                tmp.Visible = false;
            else
                tmp.Visible = true;
        }

        public bool checkIsSolveble()
        {
            int k = 0, zeroLine = 0, rez;
            for (int i = 0; i < 16; i++)
            {
                for (int j = i; j < 16; j++)
                {
                    if (curPos[i] > curPos[j] && curPos[j] != 16)
                        k++;
                }
                if (curPos[i] == 16)
                {
                    if (i >= 0 && i <= 3)
                        zeroLine = 1; 
                    if (i >= 4 && i <= 7)
                        zeroLine = 2;
                    if (i >= 8 && i <= 11)
                        zeroLine = 3; 
                    if (i >= 12 && i <= 15)
                        zeroLine = 4;
                }
            }
            rez = k + zeroLine;
            if (rez % 2 == 0)
                return true;
            else
                return false;
        }

        public void setButtonsPositionsAndStyle()
        {
            int defX = 8, defY = 33, 
                stepX = 66, stepY = 66;

            for(int i = 0; i < 16; i++)
            {
                buttonsMas[i].Font = new System.Drawing.Font("Colonna MT", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                buttonsMas[i].Size = new System.Drawing.Size(66, 66);
                buttonsMas[i].TabIndex = i;
                buttonsMas[i].UseVisualStyleBackColor = true;
                if (i != 0 && i % 4 == 0)
                    defY += stepY;
                buttonsMas[i].Location = new System.Drawing.Point(defX + stepX * (i % 4), defY);
                buttonsMas[i].Click += new System.EventHandler(this.butClick);
                this.Controls.Add(buttonsMas[i]);
            }
        }

        public void swap(Button btCur, Button btEmpty, int a, int b)
        {
            int tmp = curPos[a];
            curPos[a] = curPos[b];
            curPos[b] = tmp;

            Button tmpBut = btCur;
            btCur = btEmpty;
            btEmpty = tmpBut;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGame();
        }

        private void butClick(object sender, EventArgs e)
        {
            int index = (sender as Button).TabIndex;

            int topNeighbor = Math.Abs(index - 4);
            int leftNeighbor = Math.Abs(index - 1);
            int rightNeighbor = Math.Abs(index + 1);
            int downNeighbor = Math.Abs(index + 4);

            if (index - topNeighbor == 4 && curPos[topNeighbor] == 16)
                swap(buttonsMas[index], buttonsMas[topNeighbor], index, topNeighbor);

            else if(index - leftNeighbor == 1 && curPos[leftNeighbor] == 16 && leftNeighbor != 3 && leftNeighbor != 7 && leftNeighbor != 11)
                swap(buttonsMas[index], buttonsMas[leftNeighbor], index, leftNeighbor);

            else if (rightNeighbor - index == 1 && rightNeighbor < 16 && curPos[rightNeighbor] == 16 && rightNeighbor != 4 && rightNeighbor != 8 && rightNeighbor != 12)
                swap(buttonsMas[index], buttonsMas[rightNeighbor], index, rightNeighbor);

            else if(downNeighbor - index == 4 && downNeighbor < 16 && curPos[downNeighbor] == 16)
                swap(buttonsMas[index], buttonsMas[downNeighbor], index, downNeighbor);

            writeNumsOnButtons();
            checkFinish();
        }

        public void checkFinish()
        {
            for (int i = 0; i < 16; i++)
            {
                if (curPos[i] != defPos[i])
                    break;
                else if (i == 15)
                    MessageBox.Show("Вы выиграли!");
            }
        }
    }
}
