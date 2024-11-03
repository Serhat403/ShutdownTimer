using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
namespace ShutdownTimer
{
    public partial class Form1 : Form
    {

        TimeSpan timeLeft;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            CheckTxtBoxValues();
            bool isStartable = true;
            try
            {
                timeLeft =
                    new TimeSpan(Convert.ToInt32(txtHours.Text),
                    Convert.ToInt32(txtMinutes.Text),
                    Convert.ToInt32(txtSeconds.Text));
            }
            catch (FormatException ex)
            {
                isStartable = false;
                MessageBox.Show(ex.Message);
            }

            if (isStartable == true)
            {
                Timer.Start();
                lblTimer.Text = timeLeft.ToString(@"hh\:mm\:ss");
            }
        }

        private void CheckTxtBoxValues()
        {
            if (txtHours.Text.Count() == 0)
            {
                txtHours.Text = "0";
            }
            if (txtMinutes.Text.Count() == 0)
            {
                txtMinutes.Text = "0";
            }
            if (txtSeconds.Text.Count() == 0)
            {
                txtSeconds.Text = "0";
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeLeft = timeLeft.Subtract(TimeSpan.FromSeconds(1));
            lblTimer.Text = timeLeft.ToString(@"hh\:mm\:ss");

            if (timeLeft.TotalSeconds <= 0)
            {
                Timer.Stop();
                PerformAction();
            }
        }

        private void PerformAction()
        {
            if (rbShutdown.Checked)
            {
                Process.Start("shutdown", "/s");
            }
            else if (rbRestart.Checked)
            {
                Process.Start("shutdown", "/r");
            }
            else if (rbSavePower.Checked)
            {
                Process.Start("rundll32.exe", "powrprof.dll,SetSuspendState");
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Timer.Stop();
            lblTimer.Text = "00:00:00";
        }
    }
}
