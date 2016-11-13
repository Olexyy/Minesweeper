using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
//using System.Threading.Tasks;
using System.Text;
using System.Windows.Forms;

namespace Mine.Sweeper
{
    public partial class PlayForm : Form
    {
        private Timer Counter;

        private Timer ButtonTimer;

        private int ClockTime;

        private int MapHeight;

        private int MapWidth;

        private int MineCount;

        private bool MouseRightDown;

        private bool MouseLeftDown;

        public delegate void SendCoordinatesDelegate(int y, int x, String button);

        public event SendCoordinatesDelegate SendCoordinates;

        public delegate void SendGameStateDelegate(int MapHeight, int MapWidth, int MineCount);

        public event SendGameStateDelegate SendGameState;

        public PlayForm()
        {
            InitializeComponent();
            this.Map_Mines.Rows.Add(24);
            this.Messages.Text = "Початок гри/Обрати рівень складності";
            this.MapHeight = 9;
            this.MapWidth = 9;
            this.MineCount = 11;
            this.Counter = new Timer();
            this.Counter.Interval = 1000;
            this.ClockTime = 0;
            this.Counter.Tick += new System.EventHandler (this.Clock);
            this.Clock_.Text = "00 : 00";
            this.Mines.Text = Convert.ToString(this.MineCount);
            MouseRightDown = MouseLeftDown = false;
            this.Smile.Text = String.Format("{0}", Convert.ToChar(0x004B));
            ButtonTimer = new Timer();
            ButtonTimer.Interval = 500;
            ButtonTimer.Tick += new EventHandler(LeftMouseUp);
            ButtonTimer.Tick += new EventHandler(RightMouseUp);
        }

        private void маленькийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Map_Mines.Height = 225;
            this.Map_Mines.Width = 225;
            this.MapHeight = 9;
            this.MapWidth = 9;
            this.MineCount = 11;
            this.SendGameState(this.MapHeight, this.MapWidth, this.MineCount);
            this.Messages.Text = "Низький рівень складності";
            this.середнійToolStripMenuItem.Checked = false;
            this.великийToolStripMenuItem.Checked = false;
            this.користувацькийToolStripMenuItem.Checked = false;
            this.Counter.Stop();
            this.ClockTime = 0;
            this.Clock_.Text = "00 : 00";
            this.Map_Mines.Enabled = true;
            MouseRightDown = MouseLeftDown = false;
            this.Smile.Text = String.Format("{0}", Convert.ToChar(0x004B));
        }

        private void середнійToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Map_Mines.Height = 400;
            this.Map_Mines.Width = 400;
            this.MapHeight = 16;
            this.MapWidth = 16;
            this.MineCount = 40;
            this.SendGameState(this.MapHeight, this.MapWidth, this.MineCount);
            this.Messages.Text = "Середній рівень складності";
            this.великийToolStripMenuItem.Checked = false;
            this.маленькийToolStripMenuItem.Checked = false;
            this.користувацькийToolStripMenuItem.Checked = false;
            this.Counter.Stop();
            this.ClockTime = 0;
            this.Clock_.Text = "00 : 00";
            this.Map_Mines.Enabled = true;
            MouseRightDown = MouseLeftDown = false;
            this.Smile.Text = String.Format("{0}", Convert.ToChar(0x004B));
        }

        private void великийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Map_Mines.Height = 400;
            this.Map_Mines.Width = 750;
            this.MapHeight = 16;
            this.MapWidth = 30;
            this.MineCount = 99;
            this.SendGameState(this.MapHeight, this.MapWidth, this.MineCount);
            this.Messages.Text = "Високий рівень складності";
            this.середнійToolStripMenuItem.Checked = false;
            this.маленькийToolStripMenuItem.Checked = false;
            this.користувацькийToolStripMenuItem.Checked = false;
            this.Counter.Stop();
            this.ClockTime = 0;
            this.Clock_.Text = "00 : 00";
            this.Map_Mines.Enabled = true;
            MouseRightDown = MouseLeftDown = false;
            this.Smile.Text = String.Format("{0}", Convert.ToChar(0x004B));
        }

        private void розпочатиНовуГруToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SendGameState(this.MapHeight, this.MapWidth, this.MineCount);
            this.Messages.Text = "Початок гри...";
            this.Counter.Stop();
            this.ClockTime = 0;
            this.Clock_.Text = "00 : 00";
            this.Map_Mines.Enabled = true;
            MouseRightDown = MouseLeftDown = false;
            this.Smile.Text = String.Format("{0}", Convert.ToChar(0x004B));
        }

        private void користувацькийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomSizeForm Options = new CustomSizeForm();
            Options.SendValues += CoordinatesValuesHandler;
            Options.ShowDialog();      
            this.SendGameState(this.MapHeight, this.MapWidth, this.MineCount);
            this.Messages.Text = "Користувацький рівень складності";
            this.Counter.Stop();
            this.ClockTime = 0;
            this.Clock_.Text = "00 : 00";
            this.Map_Mines.Enabled = true;
            MouseRightDown = MouseLeftDown = false;
            this.Smile.Text = String.Format("{0}", Convert.ToChar(0x004B));
        }

        public void CoordinatesValuesHandler(int Height, int Width, int MineCount, int MapY, int MapX)
        {
            this.MapHeight = Height;
            this.MapWidth = Width;
            this.MineCount = MineCount;
            this.Map_Mines.Height = MapY;
            this.Map_Mines.Width = MapX;
            SendGameState(this.MapHeight, this.MapWidth, this.MineCount);
            this.середнійToolStripMenuItem.Checked = false;
            this.маленькийToolStripMenuItem.Checked = false;
            this.великийToolStripMenuItem.Checked = false;
            this.користувацькийToolStripMenuItem.Checked = true;
        }

        private void вихідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Map_Mines_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button.ToString().Equals("Left"))
                this.MouseLeftDown = true;
            if (e.Button.ToString().Equals("Right"))
                this.MouseRightDown = true;
            if (e.Button.ToString().Equals("Left") && this.MouseRightDown == true)
                this.SendCoordinates(e.RowIndex, e.ColumnIndex, "Both");
            else if (e.Button.ToString().Equals("Right") && this.MouseLeftDown == true)
                this.SendCoordinates(e.RowIndex, e.ColumnIndex, "Both");
            else
                this.SendCoordinates(e.RowIndex, e.ColumnIndex, e.Button.ToString());
            if (this.ClockTime == 0 && this.Map_Mines.Enabled == true)
                this.Counter.Start();
        }

        private void LeftMouseUp(object sender, EventArgs e)
        {
            this.MouseLeftDown = false;
            ButtonTimer.Stop();
        }

        private void RightMouseUp(object sender, EventArgs e)
        {
            this.MouseRightDown = false;
            ButtonTimer.Stop();
        }

        private void Map_Mines_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button.ToString().Equals("Left"))
                ButtonTimer.Start();
            if (e.Button.ToString().Equals("Right"))
                ButtonTimer.Start();
        }
        
        public void CoreResultHandler (char[,] Map, int Mines, String Result)
        {
            this.AcceptMap(Map);
            if (Result.Equals("Win"))
            {
                this.Messages.Text = "Ви перемогли!";
                this.Map_Mines.Enabled = false;
                this.Map_Mines.Refresh();
                this.Counter.Stop();
                this.ClockTime = 0;
                this.Smile.Text = String.Format("{0}", Convert.ToChar(0x004A));
            }
            else if (Result.Equals("Loose"))
            {
                this.Messages.Text = "Ви програли!";
                this.Map_Mines.Enabled = false;
                this.Map_Mines.Refresh();
                this.Counter.Stop();
                this.ClockTime = 0;
                this.Smile.Text = String.Format("{0}", Convert.ToChar(0x004C));
            }
            else
                this.Messages.Text = "";
            this.Mines.Text = Convert.ToString (Mines);
        }

        private void AcceptMap(char[,] Map)
        {
            for (int i = 0; i < MapHeight; i++)
                for (int j = 0; j < MapWidth; j++)
                {
                    this.Map_Mines.Rows[i].Cells[j].Value = Convert.ToString(Map[i, j]);
                    if (Map[i, j] == ' ')
                    {
                        this.Map_Mines.Rows[i].Cells[j].Style.BackColor = Color.RoyalBlue;
                        this.Map_Mines.Rows[i].Cells[j].Style.SelectionBackColor = Color.RoyalBlue;
                        this.Map_Mines.Rows[i].Cells[j].Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
                    }
                    if (Map[i, j] >= '1' && Map[i, j] <= '8' || Map[i, j] == '.')
                    {
                        this.Map_Mines.Rows[i].Cells[j].Style.BackColor = Color.LightBlue;
                        this.Map_Mines.Rows[i].Cells[j].Style.SelectionBackColor = Color.LightBlue;
                        this.Map_Mines.Rows[i].Cells[j].Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
                        if (Map[i, j] == '1')
                        {
                            this.Map_Mines.Rows[i].Cells[j].Style.ForeColor = Color.Blue;
                            this.Map_Mines.Rows[i].Cells[j].Style.SelectionForeColor = Color.Blue;
                        }
                        else if (Map[i, j] == '2')
                        {
                            this.Map_Mines.Rows[i].Cells[j].Style.ForeColor = Color.Green;
                            this.Map_Mines.Rows[i].Cells[j].Style.SelectionForeColor = Color.Green;
                        }
                        else if (Map[i, j] == '3')
                        {
                            this.Map_Mines.Rows[i].Cells[j].Style.ForeColor = Color.Red;
                            this.Map_Mines.Rows[i].Cells[j].Style.SelectionForeColor = Color.Red;
                        }
                        else if (Map[i, j] == '4')
                        {
                            this.Map_Mines.Rows[i].Cells[j].Style.ForeColor = Color.DarkBlue;
                            this.Map_Mines.Rows[i].Cells[j].Style.SelectionForeColor = Color.DarkBlue;
                        }
                        else if (Map[i, j] == '5')
                        {
                            this.Map_Mines.Rows[i].Cells[j].Style.ForeColor = Color.DarkRed;
                            this.Map_Mines.Rows[i].Cells[j].Style.SelectionForeColor = Color.DarkRed;
                        }
                        else if (Map[i, j] == '6')
                        {
                            this.Map_Mines.Rows[i].Cells[j].Style.ForeColor = Color.DarkGreen;
                            this.Map_Mines.Rows[i].Cells[j].Style.SelectionForeColor = Color.DarkGreen;
                        }
                    }
                    if (Map[i, j] == 'X')
                    {
                        this.Map_Mines.Rows[i].Cells[j].Style.BackColor = Color.LightBlue;
                        this.Map_Mines.Rows[i].Cells[j].Style.ForeColor = Color.DarkRed;
                        this.Map_Mines.Rows[i].Cells[j].Style.SelectionBackColor = Color.LightBlue;
                        this.Map_Mines.Rows[i].Cells[j].Style.SelectionForeColor = Color.DarkRed;
                        this.Map_Mines.Rows[i].Cells[j].Style.Font = new System.Drawing.Font("Wingdings", 10F);
                        this.Map_Mines.Rows[i].Cells[j].Value = Convert.ToChar(0x004D);
                    }
                    if (Map[i, j] == '@')
                    {
                        this.Map_Mines.Rows[i].Cells[j].Style.BackColor = Color.Red;
                        this.Map_Mines.Rows[i].Cells[j].Style.ForeColor = Color.LightPink;
                        this.Map_Mines.Rows[i].Cells[j].Style.SelectionBackColor = Color.Red;
                        this.Map_Mines.Rows[i].Cells[j].Style.SelectionForeColor = Color.LightPink;
                        this.Map_Mines.Rows[i].Cells[j].Style.Font = new System.Drawing.Font("Wingdings", 10F);
                        this.Map_Mines.Rows[i].Cells[j].Value = Convert.ToChar (0x004D);
                    }
                }
        }

        private void Version_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Версія: 1.1f", "Сапер", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Clock(object obj, EventArgs e)
        {
            if (ClockTime <= 3600)
                ClockTime++;
            this.Clock_.Text = String.Format("{0}{1} : {2}{3}", ClockTime / 60 / 10, ClockTime / 60 % 10, ClockTime % 60 / 10, ClockTime % 60 % 10);
        }

        private void Smile_Click(object sender, EventArgs e)
        {
            this.розпочатиНовуГруToolStripMenuItem_Click(sender, e);
        }
    }
}
