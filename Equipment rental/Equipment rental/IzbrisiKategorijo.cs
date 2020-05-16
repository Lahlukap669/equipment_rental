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
    public partial class IzbrisiKategorijo : MetroForm
    {
        public Dictionary<string, int> savedKeys = new Dictionary<string, int>();

        public IzbrisiKategorijo()
        {
            InitializeComponent();
            DisplayCategories();
        }

        private async void DisplayCategories()
        {
            try
            {
                metroComboBox1.Items.Clear();
                savedKeys.Clear();

                HttpClient client = new HttpClient();

                string test = "";

                StringContent queryString = new StringContent(test, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://equipment-rental.herokuapp.com/vkategorije", queryString);

                response.EnsureSuccessStatusCode();

                string responseString = response.Content.ReadAsStringAsync().Result;

                dynamic process = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);

                foreach (var item in process)
                {
                    metroComboBox1.Items.Add(item["kategorija"].ToString());
                    savedKeys.Add(item["kategorija"].ToString(), Convert.ToInt32(item["id"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void MetroButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void MetroButton2_Click(object sender, EventArgs e)
        {
            string selected = metroComboBox1.SelectedItem.ToString();

            try
            {
                HttpClient client = new HttpClient();

                string test = "{ \"id\": " + savedKeys[selected] + "}";

                StringContent queryString = new StringContent(test, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://equipment-rental.herokuapp.com/dkategorije", queryString);

                response.EnsureSuccessStatusCode();

                string responseString = response.Content.ReadAsStringAsync().Result;

                dynamic process = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);

                MessageBox.Show(process.ToString());
                DisplayCategories();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
