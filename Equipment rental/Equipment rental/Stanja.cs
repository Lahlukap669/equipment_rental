﻿using System;
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
    public partial class Stanja : MetroForm
    {
        public Stanja()
        {
            InitializeComponent();
        }

        private void MetroButton1_Click(object sender, EventArgs e)
        {
            VsaStanja vsaStanja = new VsaStanja();
            vsaStanja.ShowDialog();
        }

        private void MetroButton2_Click(object sender, EventArgs e)
        {
            UstvariStanje ustvariStanje = new UstvariStanje();
            ustvariStanje.ShowDialog();
        }

        private void MetroButton3_Click(object sender, EventArgs e)
        {
            IzbrisiStanje izbrisiStanje = new IzbrisiStanje();
            izbrisiStanje.ShowDialog();
        }
    }
}