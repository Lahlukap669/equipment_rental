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
    public partial class UstvariIzposojo : MetroForm
    {
        public Dictionary<string, int> savedOprema = new Dictionary<string, int>();
        public Dictionary<string, int> savedPlacniki = new Dictionary<string, int>();
        public Dictionary<string, int> savedStanje = new Dictionary<string, int>();

        public UstvariIzposojo()
        {
            InitializeComponent();
            UpdateComboBoxes();
        }

        public async void UpdateComboBoxes()
        {
            try
            {
                metroComboBox1.Items.Clear();
                savedOprema.Clear();

                HttpClient client = new HttpClient();

                string test = "";

                StringContent queryString = new StringContent(test, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://equipment-rental.herokuapp.com/voprema", queryString);

                response.EnsureSuccessStatusCode();

                string responseString = response.Content.ReadAsStringAsync().Result;

                dynamic process = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);

                foreach (var item in process)
                {
                    metroComboBox1.Items.Add(item["ime"].ToString());
                    savedOprema.Add(item["ime"].ToString(), Convert.ToInt32(item["id"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            try
            {
                metroComboBox2.Items.Clear();
                savedPlacniki.Clear();

                HttpClient client = new HttpClient();

                string test = "";

                StringContent queryString = new StringContent(test, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://equipment-rental.herokuapp.com/vplacniki", queryString);

                response.EnsureSuccessStatusCode();

                string responseString = response.Content.ReadAsStringAsync().Result;

                dynamic process = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);

                foreach (var item in process)
                {
                    metroComboBox2.Items.Add(item["ime"].ToString() + " " + item["priimek"].ToString());
                    savedPlacniki.Add(item["ime"].ToString() + " " + item["priimek"].ToString(), Convert.ToInt32(item["id"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            try
            {
                metroComboBox3.Items.Clear();
                savedStanje.Clear();

                HttpClient client = new HttpClient();

                string test = "";

                StringContent queryString = new StringContent(test, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://equipment-rental.herokuapp.com/vstanja", queryString);

                response.EnsureSuccessStatusCode();

                string responseString = response.Content.ReadAsStringAsync().Result;

                dynamic process = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);

                foreach (var item in process)
                {
                    metroComboBox3.Items.Add(item["stanje"].ToString());
                    savedStanje.Add(item["stanje"].ToString(), Convert.ToInt32(item["id"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
