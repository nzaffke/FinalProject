using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
    public class SP
    {
        public int i;
        public int j;

        public SP() { }
        public SP(int i, int j)
        {
            this.i = i;
            this.j = j;
        }
    }
    public partial class Form1 : Form
    {
        Timer A, C;
        Panel G, M, D, PSe;
        Label play, t, playa, Se;
        Label[,] L;
        string s;
        int left = 0, top = 0, speed = 70, n = 5, ci, cj, E = 0;
        List<SP> P = new List<SP>();
        SoundPlayer sp = new SoundPlayer(Environment.GetFolderPath(0) + "\\Various-04.wav");
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Snake";
            this.ShowIcon = false;
            this.MaximizeBox = false;
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Size = new Size(716, 789);
            G = new Panel();
            M = new Panel();
            D = new Panel();
            PSe = new Panel();
            FP(G, 716, 739, Color.Green, false);
            FP(M, 716, 789, Color.Green);
            FP(D, 716, 789, Color.Red, false);
            FP(PSe, 716, 50, Color.DarkGreen);
            this.Controls.Add(G);
            this.Controls.Add(M);
            this.Controls.Add(D);
            this.Controls.Add(PSe);
            play = new Label();
            t = new Label();
            playa = new Label();
            Se = new Label();
            F(playa, "Play agian", 30, 500, 125, Color.Transparent);
            F(play, "\u25B6 Play", 80, 500, 125, Color.Transparent);
            F(t, "Game Over", 50, 500, 125, Color.Transparent);
            F(Se, "0", 30, 760, 48, Color.Green);
            t.Top -= 100;
            play.Top -= 100;
            Se.Location = new Point(0, 0);
            PSe.Controls.Add(Se);
            playa.Click += (sender2, e2) =>
            {
                D.Visible = false;
                for (int i = 0; i < 28; i++) for (int j = 0; j < 28; j++) { L[i, j].BackColor = Color.Green; }
                G.Visible = true;
                n = 5;
                Se.Text = "0";
                E = 0;
                P.Clear();
                for (int i = 0; i < n; i++) P.Add(new SP(0, i));
                s = "T R";
                A.Enabled = true;
                R();
            };
            play.Click += (sender2, e2) =>
            {
                G.Visible = true;
                M.Visible = false;
                A.Enabled = true;
            };
            M.Controls.Add(play);
            D.Controls.Add(t);
            D.Controls.Add(playa);
            A = new Timer();
            C = new Timer();
            A.Interval = speed;
            C.Interval = 250;
            A.Tick += A_Tick;
            C.Tick += C_Tick;
            L = new Label[28, 28];
            for(int i = 0; i < 28; i++)
            {
                for(int j = 0; j < 28; j++)
                {
                    L[i, j] = new Label();
                    L[i, j].AutoSize = false;
                    L[i, j].Size = new Size(25, 25);
                    L[i, j].Location = new Point(left, top);
                    L[i, j].BackColor = Color.Green;
                    left += L[0, j].Width;
                    G.Controls.Add(L[i, j]);
                }
                left = 0;
                top += L[0, 0].Height;
            }
            s = "T R";
            for (int i = 0; i < n; i++) P.Add(new SP(0, i));
            R();
            C.Enabled = true;
        }
        private void C_Tick(object sender, EventArgs e)
        {
            if (D.Visible)
            {
                if (playa.ForeColor == Color.White) playa.ForeColor = Color.DarkRed;
                else playa.ForeColor = Color.White;
            }
            else
                if (M.Visible)
            {
                if(play.ForeColor == Color.White)
                {
                    play.ForeColor = Color.DarkGreen;
                    play.Text = '\u25B6' + "Play";
                }
                else
                {
                    play.ForeColor = Color.White;
                    play.Text = '\u25B7' + "Play";
                }
            }
        }
        public void R()
        {
            Random r = new Random();
            int ri, rj;
            do
            {
                ri = r.Next(0, 26);
                rj = r.Next(0, 26);
            } while (L[ri, rj].BackColor == Color.Black);
            if (E % 20 == 0) L[ri, rj].BackColor = Color.Gold;
            else
                L[ri, rj].BackColor = Color.DarkGreen;
            if (E % 8 == 0 && speed > E)
                A.Interval = speed - E;
        }
        public void F(object c, string text, float fsize, int w, int h, Color color)
        {
            if(c is Label)
            {
                Label l = c as Label;
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.AutoSize = false;
                l.Size = new Size(w, h);
                l.ForeColor = Color.White;
                l.BackColor = color;
                l.Text = text;
                l.Location = new Point((this.Width / 2) - (l.Width / 2), (this.Height / 2) - (l.Height / 2));
                l.Font = new Font("Tahoma", fsize, FontStyle.Bold);
            }
        }
        public void FP(object c, int w, int h, Color color, bool b = true)
        {
            if (c is Panel)
            {
                Panel p = c as Panel; G.Location = new Point(0, 50);
                if (p.Name == "G")
                    p.Location = new Point(0, 50);
                else
                    p.Location = new Point(0, 0);
                p.Size = new Size(w, h);
                p.BackColor = color;
                p.Visible = b;
            }
        }
        private void A_Tick (object sender, EventArgs e)
        {
            ci = P[n - 1].i;
            cj = P[n - 1].j;
            if(s == "B L" || s == "T L")
            {
                L[P[0].i, P[0].j].BackColor = Color.Green;
                if((cj - (1)) >= 0)
                {
                    if (L[ci, cj - (1)].BackColor == Color.DarkGreen)
                    {
                        P.Add(new SP(P[n - 1].i, P[n - 1].j - 1));
                        n++;
                        Se.Text = ++E + "";
                        R();
                    }
                    else
                        if (L[ci, cj - (1)].BackColor == Color.Gold)
                    {
                        P.Add(new SP(P[n - 1].i, P[n - 1].j - 1));
                        P.Add(new SP(P[n - 1].i, P[n - 1].j - 2));
                        n += 2;
                        E += 2;
                        Se.Text = E + "";
                        R();
                    }
                    else if (L[ci, cj - (1)].BackColor == Color.Black) { S();}
                    L[ci, cj - (1)].BackColor = Color.Black;
                    for(int i = 0; i < n - 1; i++)
                    {
                        P[i].i = P[i + 1].i;
                        P[i].j = P[i + 1].j;
                    }
                    P[n - 1].i = ci;
                    P[n - 1].j = cj - (1);
                }
                else { S(); }
            }
            else
                if(s == "T R" || s == "B R")
            {
                L[P[0].i, P[0].j].BackColor = Color.Green;
                if((cj + (1)) <= 27)
                {
                    if(L[ci, cj + (1)].BackColor == Color.DarkGreen)
                    {
                        P.Add(new SP(P[n - 1].i, P[n - 1].j + 1));
                        n++;
                        Se.Text = ++E + "";
                        R();
                    }
                    else if (L[ci, cj + (1)].BackColor == Color.Gold)
                    {
                        P.Add(new SP(P[n - 1].i, P[n - 1].j + 1));
                        P.Add(new SP(P[n - 1].i, P[n - 1].j + 2));
                        n += 2;
                        E += 2;
                        Se.Text = E + "";
                        R();
                    }
                    else if (L[ci, cj + (1)].BackColor == Color.Black) { S(); }
                    L[ci, cj + (1)].BackColor = Color.Black;
                    for (int i = 0; i < n - 1; i++)
                    {
                        P[i].i = P[i + 1].i;
                        P[i].j = P[i + 1].j;
                    }
                    P[n - 1].i = ci;
                    P[n - 1].j = cj + (1);
                }
                else { S(); }
            }
            else
                if(s == "L B" || s == "R B")
            {
                L[P[0].i, P[0].j].BackColor = Color.Green;
                if((ci + (1)) <= 27)
                {
                    if(L[ci + (1), cj].BackColor == Color.DarkGreen)
                    {
                        P.Add(new SP(P[n - 1].i + 1, P[n - 1].j));
                        n++;
                        Se.Text = ++E + "";
                        R();
                    }
                    else 
                        if(L[ci + (1), cj].BackColor == Color.Gold)
                    {
                        P.Add(new SP(P[n - 1].i + 1, P[n - 1].j));
                        P.Add(new SP(P[n - 1].i + 2, P[n - 1].j));
                        n += 2;
                        E += 2;
                        Se.Text = E + "";
                        R();
                    }
                    else if (L[ci + (1), cj].BackColor == Color.Black) { S(); }
                    L[ci + (1), cj].BackColor = Color.Black;
                    for (int i = 0; i < n - 1; i++)
                    {
                        P[i].i = P[i + 1].i;
                        P[i].j = P[i + 1].j;
                    }
                    P[n - 1].i = ci + (1);
                    P[n - 1].j = cj;
                }
                else { S(); }
            }
            else
                if(s == "L T" || s == "R T")
            {
                L[P[0].i, P[0].j].BackColor = Color.Green;
                if ((ci - (1)) >= 0)
                {
                    if(L[ci - (1), cj].BackColor == Color.DarkGreen)
                    {
                        P.Add(new SP(P[n - 1].i - 1, P[n - 1].j));
                        n++;
                        Se.Text = ++E + "";
                        R();
                    }
                    else 
                        if (L[ci - (1), cj]. BackColor == Color.Gold)
                    {
                        P.Add(new SP(P[n - 1].i - 1, P[n - 1].j));
                        P.Add(new SP(P[n - 1].i - 2, P[n - 1].j));
                        n += 2;
                        E += 2;
                        Se.Text = E + "";
                        R();
                    }
                    else if (L[ci - (1), cj].BackColor == Color.Black) { S(); }
                    L[ci - (1), cj].BackColor = Color.Black;
                    for (int i = 0; i < n - 1; i++)
                    {
                        P[i].i = P[i + 1].i;
                        P[i].j = P[i + 1].j;
                    }
                    P[n - 1].i = ci - (1);
                    P[n - 1].j = cj;
                }
                else { S(); }
            }
        }
        public void S() { sp.Play(); A.Enabled = false; System.Threading.Thread.Sleep(1000); G.Visible = false; D.Visible = true; }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Down && s != "L B" && s != "R B" && s != "L T" && s != "R T")
            {
                s = s.Split(' ')[1] + " B";
                return true;
            }
            else 
                if(keyData == Keys.Up && s != "L B" && s != "R B" && s != "L T" && s != "R T")
            {
                s = s.Split(' ')[1] + " T";
                return true;
            }
            else
                if (keyData == Keys.Left && s != "T R" && s != "B R" && s != "T L" && s != "B L")
            {
                s = s.Split(' ')[1] + " L";
                return true;
            }
            else
                if (keyData == Keys.Right && s != "T R" && s != "B R" && s != "T L" && s != "B L")
            {
                 s = s.Split(' ')[1] + " R";
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
