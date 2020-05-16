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
    public partial class PosodobiOpremo : MetroForm
    {
        public Dictionary<string, int> savedKategorije = new Dictionary<string, int>();
        public Dictionary<string, int> savedStanje = new Dictionary<string, int>();
        int id;

        public PosodobiOpremo(int _id)
        {
            InitializeComponent();
            id = _id;
            UpdateAll();
            GetInfo();
        }

        public async void GetInfo()
        {
            int kategorija;
            int stanje;

            try
            {
                HttpClient client = new HttpClient();

                string test = "{ \"id\":" + id + "}";
                MessageBox.Show(test);

                StringContent queryString = new StringContent(test, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://equipment-rental.herokuapp.com/ioprema", queryString);

                response.EnsureSuccessStatusCode();

                string responseString = response.Content.ReadAsStringAsync().Result;

                dynamic process = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);

                kategorija = Convert.ToInt32(process["kategorija_id"]);
                stanje = Convert.ToInt32(process["stanje_id"]);
                metroTextBox1.Text = process["ime"].ToString();
                metroTextBox2.Text = process["opis"].ToString();

                metroComboBox1.SelectedIndex = metroComboBox1.Items.IndexOf(savedKategorije.FirstOrDefault(x => x.Value == kategorija).Key);
                metroComboBox2.SelectedIndex = metroComboBox2.Items.IndexOf(savedStanje.FirstOrDefault(x => x.Value == stanje).Key);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
            try
            {
                HttpClient client = new HttpClient();

                string test = "{ \"id\": " + id + ", \"kategorija_id\": " + savedKategorije[metroComboBox1.SelectedItem.ToString()] + ", \"stanje_id\" : " + savedStanje[metroComboBox2.SelectedItem.ToString()]  + ", \"ime\" : \"" + metroTextBox1.Text +"\", \"opis\" : \"" + metroTextBox2.Text + "\"}";
                MessageBox.Show(test);

                StringContent queryString = new StringContent(test, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://equipment-rental.herokuapp.com/uoprema", queryString);

                response.EnsureSuccessStatusCode();

                string responseString = response.Content.ReadAsStringAsync().Result;

                dynamic process = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
