using System;
using System.Collections.Generic;
using System.Text;
//using System.Linq; // Framework 2.0
//using System.Threading.Tasks; // Framework 2.0

namespace Mine.Sweeper
{
    enum Defs {to_show_map, mine_map, mines, not_mines};

    class MineSweeperCore
    {
        private const int Hardware_Height = 26;

        private const int Hardware_Width = 32;

        private int Real_Height;

        private int Real_Width;

        bool Mines_Are_Set;

        private int Count_Of_Mines;
        
        private char [,] Mine_Map;

        private char [,] To_Show_Map;

        private char [,] Buffer;

        public MineSweeperCore()
        {
            this.Count_Of_Mines  = 11;
            this.Real_Height = 11;
            this.Real_Width = 11;
            this.Mine_Map = new char[Hardware_Height, Hardware_Width];
            this.To_Show_Map = new char[Hardware_Height, Hardware_Width];
            this.Buffer = new char[Hardware_Height-2, Hardware_Width-2];
            this.Earse_Map(Defs.mine_map);
            this.Earse_Map(Defs.to_show_map);
            this.Mines_Are_Set = false;
        }

        public void Earse_Map (Defs mode )
        {
            for (int i = 0; i < this.Real_Height; i++)
                for (int j = 0; j < this.Real_Width; j++)
                {
                    if (i == 0 || i == Real_Height - 1 || j == 0 || j == Real_Width - 1)
                        if (mode == Defs.mine_map)
                            this.Mine_Map [i, j] = '*';
                        else
                            this.To_Show_Map[i, j] = '*';
                    else
                        if (mode == Defs.mine_map)
                            this.Mine_Map[i, j] = '.';
                        else
                            this.To_Show_Map[i, j] = ' ';
                }
        }

        public void Set_Mines (int i, int j)
        {
            Random Rand = new Random();
            int I, J, counter = 0;
            bool again = false;
            this.Earse_Map(Defs.mine_map);
            while (counter < this.Count_Of_Mines)
            {
                I = Rand.Next (1, this.Real_Height);
                J = Rand.Next(1, this.Real_Width);
                if (this.Mine_Map [I, J] == '.')
                {
                    for (int y = -1; y <= 1 && i + y < this.Real_Height; y++)
                        for (int x = -1; x <= 1 && i + x < this.Real_Width; x++)
                            if (i + y == I && j + x == J)
                                again = true;
                    if (again == false)
                    {
                        this.Mine_Map[I, J] = '@';
                        counter++;
                    }
                    else
                        again = false;
                }
            }
            this.Set_Mines_Bounds();
        }

        public void Set_Mines_Bounds ()
        {
            int counter = 0;
            for (int i = 0; i < this.Real_Height; i++)
                for (int j = 0; j < this.Real_Width; j++)
                {
                    counter = 0;
                    if (this.Mine_Map[i, j] == '.')
                    {
                        for (int y = -1; y <= 1; y++)
                            for (int x = -1; x <=1; x++)
                                if (this.Mine_Map[i+y, j+x] == '@')
                                        counter++;
                        if (counter > 0)
                            this.Mine_Map [i,j] = Convert.ToChar(String.Format ("{0}", counter));
                    }
                }
        }

        public void Copy_Map_To_Buffer (Defs mode) 
        {
            for (int i = 1; i < this.Real_Height-1; i++)
            {
                for (int j = 1; j < this.Real_Width-1; j++)
                {
                    if (mode == Defs.mine_map)
                        this.Buffer[i-1, j-1] = this.Mine_Map[i, j];
                    else
                        this.Buffer[i-1, j-1] = this.To_Show_Map[i, j];
                }
            }
        }

        public void Define_Mine(int i, int j)
        {
            if (this.To_Show_Map[i, j] == ' ')
                this.To_Show_Map[i, j] = 'X';
            else if (this.To_Show_Map[i, j] == 'X')
                this.To_Show_Map[i, j] = ' ';
        }

        public void Disclose_Around(int i, int j)
        {
        for (int y = -1; y <= 1 ; y++)
            for (int x = -1; x <= 1; x++)
            {
                if (this.Mine_Map[i + y, j + x] == '.' && this.To_Show_Map[i + y, j + x] == '.')
                    continue;
                else if (this.Mine_Map[i + y, j + x] == '.' && this.To_Show_Map[i+y, j+x] != '.')
                {
                    this.To_Show_Map[i + y, j + x] = '.';
                    this.Disclose_Around(i + y, j + x);
                }
                else
                    this.To_Show_Map[i + y, j + x] = this.Mine_Map[i + y, j + x];
            }
        }

        public bool Disclose_Around_Blind(int i, int j) 
        {
            for (int y = -1; y <= 1; y++)
                for (int x = -1; x <= 1; x++)
                {
                    if (this.To_Show_Map[y + i, x + j] == ' ' && (this.Mine_Map[y + i, x + j] >= '1' && this.Mine_Map[y + i, x + j] <= '8'))
                        this.To_Show_Map[y + i, x + j] = this.Mine_Map[y + i, x + j];
                    else if (this.To_Show_Map[y + i, x + j] == ' ' && this.Mine_Map[y + i, x + j] == '.')
                        this.Disclose_Around (y + i, x + j);
                    else if (this.To_Show_Map[y + i, x + j] == ' ' && this.Mine_Map[y + i, x + j] == '@')
                        return false;
                    else
                        continue;
                }
            return true;
        }

        public void Disclose_All (Defs var) 
        {
            for (int i = 1; i < this.Real_Height - 1; i++)
            {
                for (int j = 1; j < this.Real_Width - 1; j++)
                {
                    if (var == Defs.not_mines && this.To_Show_Map[i, j] == ' ')
                        this.To_Show_Map[i, j] = this.Mine_Map[i, j];
                    if (var == Defs.mines && this.Mine_Map[i, j] == '@')
                        this.To_Show_Map[i, j] = this.Mine_Map[i, j];
                }
            }
        }

        public bool Check_Win()
        {
            int counter = 0;
            for (int i = 1; i < this.Real_Height - 1; i++)
            {
                for (int j = 1; j < this.Real_Width - 1; j++)
                {
                    if (this.To_Show_Map[i, j] == 'X' && this.Mine_Map[i, j] == '@')
                        counter++;
                }
            }
            if (counter == this.Count_Of_Mines)
                return true;
            return false;
        }

        public int Count_User_Mines()
        {
            int counter = 0;
            for (int i = 1; i < this.Real_Height - 1; i++)
            {
                for (int j = 1; j < this.Real_Width - 1; j++)
                {
                    if (this.To_Show_Map[i, j] == 'X')
                        counter++;
                }
            }
            return this.Count_Of_Mines - counter;
        }

        public void GameplayHandler(int y, int x, String button)
        {
            y++;
            x++;
            if (button.Equals("Left"))
            {
                if (this.Mines_Are_Set == false)
                {
                    this.Set_Mines(y, x);
                    this.Mines_Are_Set = true;
                }
                if (this.To_Show_Map[y, x] == 'X')
                    return;
                else if (this.Mine_Map[y, x] == '@')
                {
                    this.Disclose_All(Defs.mines);
                    this.Copy_Map_To_Buffer(Defs.to_show_map);
                    this.Mines_Are_Set = false;
                    this.Send_Result(this.Buffer, this.Count_User_Mines(), "Loose");
                }
                else if (this.Mine_Map[y, x] != '.')
                {
                    this.To_Show_Map[y, x] = this.Mine_Map[y, x];
                    this.Copy_Map_To_Buffer(Defs.to_show_map);
                    this.Send_Result(this.Buffer, this.Count_User_Mines(), "Continue");
                }
                else // == '0'('.');
                {
                    this.Disclose_Around(y, x);
                    this.Copy_Map_To_Buffer(Defs.to_show_map);
                    this.Send_Result(this.Buffer, this.Count_User_Mines(), "Continue");
                }
            }
            else if (button.Equals("Right"))
            {
                this.Define_Mine(y, x);
                if (this.Check_Win() == true)
                {
                    this.Disclose_All(Defs.not_mines);
                    this.Copy_Map_To_Buffer(Defs.to_show_map);
                    this.Send_Result(this.Buffer, this.Count_User_Mines(), "Win");
                }
                else
                {
                    this.Copy_Map_To_Buffer(Defs.to_show_map);
                    this.Send_Result(this.Buffer, this.Count_User_Mines(), "Continue");
                }
            }
            else
            {
                if (this.To_Show_Map[y, x] >= '1' && this.To_Show_Map[y, x] <= '8')
                {
                    if (this.Disclose_Around_Blind(y, x) == true)
                    {
                        this.Copy_Map_To_Buffer(Defs.to_show_map);
                        this.Send_Result(this.Buffer, this.Count_User_Mines(), "Continue");
                    }
                    else
                    {
                        this.Disclose_All(Defs.mines);
                        this.Copy_Map_To_Buffer(Defs.to_show_map);
                        this.Mines_Are_Set = false;
                        this.Send_Result(this.Buffer, this.Count_User_Mines(), "Loose");
                    }
                }
            }
        }

        public void GameStateHandler (int MapHeight, int MapWidth, int MineCount)
        {
            this.Real_Height = MapHeight + 2;
            this.Real_Width = MapWidth + 2;
            this.Count_Of_Mines = MineCount;
            this.Earse_Map(Defs.mine_map);
            this.Earse_Map(Defs.to_show_map);
            this.Copy_Map_To_Buffer(Defs.to_show_map);
            this.Mines_Are_Set = false;
            this.Send_Result(this.Buffer, this.Count_Of_Mines, "Generated");
        }
        
        public delegate void Send_Result_Delegate(char[,] Map, int Mines, String Result);
                                                          
        public event Send_Result_Delegate Send_Result; 
    }
}
