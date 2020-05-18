﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace Equipment_rental
{
    public partial class VseIzposoje : MetroForm
    {
        public VseIzposoje()
        {
            InitializeComponent();
            UpdateData();
        }

       private async void UpdateData()
       {
            try
            {
                dataGridView1.Rows.Clear();

                HttpClient client = new HttpClient();

                string test = "";

                StringContent queryString = new StringContent(test, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://equipment-rental.herokuapp.com/vizposoje", queryString);

                response.EnsureSuccessStatusCode();

                string responseString = response.Content.ReadAsStringAsync().Result;

                dynamic process = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);

                foreach (var item in process)
                {
                    dataGridView1.Rows.Add(item["id"].ToString(), item["nadzornik"].ToString(), item["opis"].ToString(), item["oprema"].ToString(), item["placnik"].ToString(), item["datum_do"].ToString(), item["datum_od"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
       }

        private async void MetroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells["id"].Value);

                HttpClient client = new HttpClient();

                string test = "{ \"id\" : " + id + "}";

                StringContent queryString = new StringContent(test, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://equipment-rental.herokuapp.com/dizposoje", queryString);

                response.EnsureSuccessStatusCode();

                string responseString = response.Content.ReadAsStringAsync().Result;

                dynamic process = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            UpdateData();
        }

        private async void MetroButton2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.SelectedRows[0]);

            try
            {
                HttpClient client = new HttpClient();

                string test = "{ \"id\" : " + id + "}";

                StringContent queryString = new StringContent(test, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://equipment-rental.herokuapp.com/uizposoje", queryString);

                response.EnsureSuccessStatusCode();

                string responseString = response.Content.ReadAsStringAsync().Result;

                dynamic process = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void MetroButton2_Click_1(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells["id"].Value);

            PosodobiIzposojo posodobiIzposojo = new PosodobiIzposojo(id);
            posodobiIzposojo.ShowDialog();
        }
    }
}
