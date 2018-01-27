using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackPlace.WPF.Logger
{
    /// <summary>
    /// Class for makeing all logic for logger
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Method, which wont override everything
        /// </summary>
        /// <param name="message">Text, what will be writte</param>
        /// <param name="txtname">.txt file name</param>
        private static void AppendTextExisting(string message, string txtname)
        {
            using (StreamWriter str = File.AppendText(txtname))
            {
                str.WriteLine(message);
                str.Close();
            }
        }

        /// <summary>
        /// Method to create new file and overrite everyting
        /// </summary>
        /// <param name="message">Text what will be written</param>
        /// <param name="txtname">txtfile name</param>
        private static void CreateNewFileAndOverrideExisting(string message, string txtname)
        {
            using (StreamWriter strWriter = new StreamWriter(txtname))
            {
                strWriter.WriteLine(message);
                strWriter.Flush();
            }
        }

        /// <summary>
        /// Loggger main method
        /// </summary>
        /// <param name="message">What will be printed</param>
        /// <param name="txtname">-txt file name</param>
        public static void log(string message, string txtname)
        {
            string folder = "logger";
            txtname = folder + "/" + txtname + ".txt";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            if (!File.Exists(txtname))
                {
                    CreateNewFileAndOverrideExisting(message, txtname);
                }
                else
                {
                    AppendTextExisting(message, txtname);
                }
            }
    }
}