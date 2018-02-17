using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace MilePost.Web.BusinessEntity
{
    public static class HandleLogging
    {
        /// <summary>
        /// Write to the log file
        /// </summary>
        /// <param name="message">The message to write</param>
        /// /// <param name="WebPage">Name of the Page</param>
        public static void AddtoLogFile(string Message, string WebPage)
        {

            string LogPath = ConfigurationManager.AppSettings[CommonConstants.LogPath].ToString();
            string filename = CommonConstants.Log + DateTime.Now.ToString("dd-MM-yyyy") + CommonConstants.ExtentionLogFile;
            string filepath = LogPath + filename;
            if (File.Exists(filepath))
            {

                using (StreamWriter writer = new StreamWriter(filepath, true))
                {
                    writer.WriteLine(CommonConstants.StartLogging + DateTime.Now);
                    writer.WriteLine(CommonConstants.SourcePage + WebPage);
                    writer.WriteLine(Message);
                    writer.WriteLine(CommonConstants.EndLogging + DateTime.Now);
                }
            }
            else
            {
                StreamWriter writer = File.CreateText(filepath);
                writer.WriteLine(CommonConstants.StartLogging + DateTime.Now);
                writer.WriteLine(CommonConstants.SourcePage + WebPage);
                writer.WriteLine(Message);
                writer.WriteLine(CommonConstants.EndLogging + DateTime.Now);
                writer.Close();
            }
        }
    } 
}
