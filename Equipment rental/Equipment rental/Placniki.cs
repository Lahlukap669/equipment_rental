using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace Equipment_rental
{
    public partial class Placniki : MetroForm
    {
        public Placniki()
        {
            InitializeComponent();
        }

        private void MetroButton1_Click(object sender, EventArgs e)
        {
            VsiPlacniki vsiPlacniki = new VsiPlacniki();
            vsiPlacniki.ShowDialog();
        }

        private void MetroButton2_Click(object sender, EventArgs e)
        {
            UstvariPlacnika ustvari = new UstvariPlacnika();
            ustvari.ShowDialog();
        }

        private void Placniki_Load(object sender, EventArgs e)
        {

        }
    }
}
