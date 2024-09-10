using System;
using System.Windows.Forms;

namespace Charging_Monitoring
{
    public partial class Form1 : Form
    {
        private Timer batteryCheckTimer;
        public Form1()
        {
            InitializeComponent();
            InitializeBatteryMonitor();
        }

        private void InitializeBatteryMonitor()
        {
            // Timer Configuration
            batteryCheckTimer = new Timer
            {
                Interval = 10000 // Check every 10 seconds
            };
            batteryCheckTimer.Tick += BatteryCheckTimer_Tick;
            batteryCheckTimer.Start();

            // Start with a hidden form
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        private void BatteryCheckTimer_Tick(object sender, EventArgs e)
        {
            // Get battery status
            PowerStatus powerStatus = SystemInformation.PowerStatus;
            float batteryLifePercent = powerStatus.BatteryLifePercent * 100;
            BatteryChargeStatus chargeStatus = powerStatus.BatteryChargeStatus;

            // Check that the battery is charging and has reached 100%.
            if (chargeStatus.HasFlag(BatteryChargeStatus.Charging) && batteryLifePercent >= 50)
            {
                // Display a warning message
                ShowBatteryFullNotification();
            }
        }

        private void ShowBatteryFullNotification()
        {

            // Show notifications using NotifyIcon
            notifyIcon1.BalloonTipTitle = "Battery Full";
            notifyIcon1.BalloonTipText = "Battery is fully charged. Please unplug the charger.";
            notifyIcon1.ShowBalloonTip(5000);

            // MessageBox.Show("Battery is fully charged. Please unplug the charger.", "Battery Full", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
