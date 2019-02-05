using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using DYMO.Label.Framework;

namespace PrintLabelSystemService {
    public partial class PrintLabelSystemService : ServiceBase {
        System.Timers.Timer timer;
        int count = 1;
        BOXX_V2Entities dbConnection;
        static string PATH = @"myServices\PrintLabelSystemService\";
        string fullPath;
        public PrintLabelSystemService() {
            InitializeComponent();
            timer = new System.Timers.Timer();
            timer.Interval = 60000; // 60 seconds
            timer.Elapsed += new System.Timers.ElapsedEventHandler(WorkProcess);
        }
        public void WorkProcess(object sender, System.Timers.ElapsedEventArgs e) {
            string process = "60 seconds passed " + count;
            LogService(process);
            count++;

            try {
                var labelsToPrint = dbConnection.tbl_LabelToPrint.Where(x => x.LabelPrinted == 0).ToList();
                // foreach label not printed, attempt to print
                // if sucessful, change "labelPrinted" to 1 (printed)
                // else, change "labelPrinted" to 2 (error) and update "error" with the reason for not printing
                foreach (tbl_LabelToPrint row in labelsToPrint) {
                    if (row.LabelPrinted == 0) {
                        try {
                            // send to printer specified in row
                            ILabel label = Label.OpenXml(row.CompletedLabelXML);
                            label.Print(row.PrinterName);
                            row.LabelPrinted = 2;
                            row.DateTimePrinted = DateTime.Now;
                            LogService(row.ID + " " + row.LabelText + " " + row.DateTimePrinted + " " + "Sucess");
                        } catch (Exception f) {
                            // add error to "error" column in database
                            LogService(DateTime.Now + " Error Sending to Printer: " + f);
                            try {
                                row.LabelPrinted = 3;
                                row.ErrorInformation = DateTime.Now + " " + f.ToString();
                            } catch (Exception g) {
                                // display error message
                                LogService(DateTime.Now + " Error updating database: " + g);
                            }
                        }
                        dbConnection.SaveChanges();
                    }
                }
            } catch (Exception ex) {
                LogService(DateTime.Now + " Error connecting to database: " + ex + "\r\n" +
                    "Inner Exception: " + ex.InnerException);
            }
        }
        protected override void OnStart(string[] args) {
            LogService("Service is Started " + DateTime.Now);
            timer.Enabled = true;

            dbConnection = new BOXX_V2Entities();
        }
        protected override void OnStop() {
            LogService("Service Stoped " + DateTime.Now);
            timer.Enabled = false;
        }
        private void LogService(string content) {
            string rootDirectory = Path.GetPathRoot(Directory.GetCurrentDirectory());
            fullPath = rootDirectory + PATH;
            if (!Directory.Exists(fullPath)) {
                Directory.CreateDirectory(fullPath);
            }
            FileStream fs = new FileStream(fullPath + @"\PrintLabelSystemServiceLog.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(content);
            sw.Flush();
            sw.Close();
        }
    }
}
