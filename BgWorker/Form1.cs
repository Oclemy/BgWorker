using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace BgWorker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            //START BG WORKER
            backgroundWorker1.RunWorkerAsync();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            //REQUEST CANCELLATION
            backgroundWorker1.CancelAsync();
        }

      
        //RUN BG STUFF HERE.NO GUI HERE PLEASE
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                //CHECK FOR CANCELLATION FIRST
                if (backgroundWorker1.CancellationPending)
                {
                    //CANCEL
                    e.Cancel = true;
                }
                else
                {
                    simulateHeavyJob();
                    backgroundWorker1.ReportProgress(i);
                }
            }

        }

        //THIS UPDATES GUI.OUR PROGRESSBAR
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            percentageLabel.Text = e.ProgressPercentage.ToString() + " %";
        }

        //WHEN JOB IS DONE THIS IS CALLED.
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                display("You have Cancelled");
                progressBar1.Value = 0;
                percentageLabel.Text = "0";
            }
            else
            {
                display("Work completed successfully");
            }
        }
        //SIMULATE HEAVY JOB
        private void simulateHeavyJob()
        {
            //SUSPEND THREAD FOR 100 MS
            Thread.Sleep(100);
        }
        //DISPLAY MSG BOX
        private void display(String text)
        {
            MessageBox.Show(text);
        }
        

       
       
    }
}
