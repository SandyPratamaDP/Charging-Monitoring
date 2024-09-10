using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            // Konfigurasi Timer
            batteryCheckTimer = new Timer();
            batteryCheckTimer.Interval = 10000; // Cek setiap 10 detik
            batteryCheckTimer.Tick += BatteryCheckTimer_Tick;
            batteryCheckTimer.Start();

            // Mulai dengan form tersembunyi
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        private void BatteryCheckTimer_Tick(object sender, EventArgs e)
        {
            // Mendapatkan status baterai
            PowerStatus powerStatus = SystemInformation.PowerStatus;
            float batteryLifePercent = powerStatus.BatteryLifePercent * 100;
            BatteryChargeStatus chargeStatus = powerStatus.BatteryChargeStatus;

            // Cek apakah baterai sedang di-charge dan telah mencapai 100%
            if (chargeStatus.HasFlag(BatteryChargeStatus.Charging) && batteryLifePercent >= 50)
            {
                // Menampilkan pesan peringatan
                ShowBatteryFullNotification();
            }
        }

        private void ShowBatteryFullNotification()
        {
            // Tampilkan notifikasi menggunakan NotifyIcon
            //MessageBox.Show("Battery is fully charged. Please unplug the charger.", "Battery Full", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            notifyIcon1.BalloonTipTitle = "Battery Full";
            notifyIcon1.BalloonTipText = "Battery is fully charged. Please unplug the charger.";
            notifyIcon1.ShowBalloonTip(5000);

        }
    }
}
