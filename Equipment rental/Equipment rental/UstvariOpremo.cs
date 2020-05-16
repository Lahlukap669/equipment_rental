using System;
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
    public partial class UstvariOpremo : MetroForm
    {
        public Dictionary<string, int> savedKategorije = new Dictionary<string, int>();
        public Dictionary<string, int> savedStanje = new Dictionary<string, int>();

        public UstvariOpremo()
        {
            InitializeComponent();
            UpdateAll();
        }

        public async void UpdateAll()
        {
            try
            {
                metroComboBox1.Items.Clear();

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
                    savedKategorije.Add(item["kategorija"].ToString(), Convert.ToInt32(item["id"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            try
            {
                metroComboBox2.Items.Clear();

                HttpClient client = new HttpClient();

                string test = "";

                StringContent queryString = new StringContent(test, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://equipment-rental.herokuapp.com/vstanja", queryString);

                response.EnsureSuccessStatusCode();

                string responseString = response.Content.ReadAsStringAsync().Result;

                dynamic process = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);

                foreach (var item in process)
                {
                    metroComboBox2.Items.Add(item["stanje"].ToString());
                    savedStanje.Add(item["stanje"].ToString(), Convert.ToInt32(item["id"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private async void MetroButton1_Click(object sender, EventArgs e)
        {
            string kategorija = metroComboBox1.SelectedItem.ToString();
            string stanje = metroComboBox2.SelectedItem.ToString();

            try
            {
                HttpClient client = new HttpClient();

                string test = "{ \"kategorija_id\": " + savedKategorije[kategorija] + ", \"stanje_id\": " + savedStanje[stanje] + ", \"ime\": \"" + metroTextBox1.Text + "\", \"opis\": \"" + metroTextBox2.Text + "\"}";

                MessageBox.Show(test);

                StringContent queryString = new StringContent(test, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://equipment-rental.herokuapp.com/coprema", queryString);

                response.EnsureSuccessStatusCode();

                string responseString = response.Content.ReadAsStringAsync().Result;

                dynamic process = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);

                if (process["bool"].ToString() == "True")
                {
                    MessageBox.Show("Creation successful.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Creation failed!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
