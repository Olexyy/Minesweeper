using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mine.Sweeper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            PlayForm Interface          = new PlayForm();
            MineSweeperCore Core        = new MineSweeperCore();
            Interface.SendCoordinates   += Core.GameplayHandler;
            Core.Send_Result            += Interface.CoreResultHandler;
            Interface.SendGameState     += Core.GameStateHandler;
            Application.Run(Interface);
        }
    }
}
