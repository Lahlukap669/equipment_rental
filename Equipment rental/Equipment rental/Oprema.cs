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
    public partial class Oprema : MetroForm
    {
        public Oprema()
        {
            InitializeComponent();
        }

        private void MetroButton1_Click(object sender, EventArgs e)
        {
            VsaOprema vsaOprema = new VsaOprema();
            vsaOprema.ShowDialog();
        }

        private void MetroButton2_Click(object sender, EventArgs e)
        {
            UstvariOpremo ustvariOpremo = new UstvariOpremo();
            ustvariOpremo.ShowDialog();
        }

        private void MetroButton3_Click(object sender, EventArgs e)
        {
            IzbrisiOpremo izbrisiOpremo = new IzbrisiOpremo();
            izbrisiOpremo.ShowDialog();
        }
    }
}
