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
    public partial class Kategorije : MetroForm
    {
        public Kategorije()
        {
            InitializeComponent();
        }

        private void Kategorije_Load(object sender, EventArgs e)
        {

        }

        private void MetroButton2_Click(object sender, EventArgs e)
        {
            DodajKategorijo dodajKategorijo = new DodajKategorijo();
            dodajKategorijo.ShowDialog();
        }

        private void MetroButton3_Click(object sender, EventArgs e)
        {
            IzbrisiKategorijo izbrisiKategorijo = new IzbrisiKategorijo();
            izbrisiKategorijo.ShowDialog();
        }

        private void MetroButton1_Click(object sender, EventArgs e)
        {
            VseKategorije vseKategorije = new VseKategorije();
            vseKategorije.ShowDialog();
        }
    }
}
