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
    public partial class MainForm : MetroForm
    {
        public MainForm(string _email)
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MetroButton1_Click(object sender, EventArgs e)
        {
            Kategorije kategorije = new Kategorije();
            kategorije.ShowDialog();
        }

        private void MetroButton2_Click(object sender, EventArgs e)
        {
            Stanja stanja = new Stanja();
            stanja.ShowDialog();
        }

        private void MetroButton4_Click(object sender, EventArgs e)
        {
            Oprema oprema = new Oprema();
            oprema.ShowDialog();
        }
    }
}
