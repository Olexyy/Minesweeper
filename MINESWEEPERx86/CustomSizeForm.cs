using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mine.Sweeper
{
    public partial class CustomSizeForm : Form
    {
        public CustomSizeForm()
        {
            InitializeComponent();
        }

        public delegate void SendValuesDelegate(int Height, int Width, int MineCount, int MapY, int MapX);

        public event SendValuesDelegate SendValues;

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Height_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == Convert.ToChar(Keys.Back))
                return;
            else
                e.Handled = true;
        }

        private void Width_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == Convert.ToChar(Keys.Back))
                return;
            else
                e.Handled = true;
        }

        private void Mines_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == Convert.ToChar(Keys.Back))
                return;
            else
                e.Handled = true;
        }

        private void Set_Click(object sender, EventArgs e)
        {
            Int32 height, width, minecount;
            bool H = Int32.TryParse(this.MapHeight.Text, out height);
            bool W = Int32.TryParse(this.MapWidth.Text, out width);
            bool M = Int32.TryParse(this.Mines.Text, out minecount);
            if (H == false || W == false || M == false)
                MessageBox.Show("Заповніть усі поля.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (height < 9)
                    height = 9;
                else if (height > 24)
                    height = 24;
                if (width < 9)
                    width = 9;
                else if (width > 30)
                    width = 30;
                if (minecount < 10)
                    minecount = 10;
                else if (minecount > 667)
                    minecount = 667;
                this.SendValues(height, width, minecount, height * 25, width*25);
            }
            this.Close();
        }
    }
}
