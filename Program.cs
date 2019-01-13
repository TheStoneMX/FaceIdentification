using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BioHuellas.Forms
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
            Application.Run(new BioTechSys.Face.Enroll.Forms.MainForm());
        }

    }
}