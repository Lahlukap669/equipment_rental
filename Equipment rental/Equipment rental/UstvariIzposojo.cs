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

        private async void MetroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();

                string test = "{ \"user_id\": " + Form1.uId + ", \"oprema_id\": " + savedOprema[metroComboBox1.SelectedItem.ToString()] + ", \"placnik_id\": " + savedPlacniki[metroComboBox2.SelectedItem.ToString()] + ", \"stanje_id\": " + savedStanje[metroComboBox3.SelectedItem.ToString()] + ", \"datum_od\" :\"" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "\", \"datum_do\" : \"" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "\", \"opis\" : \"" + metroTextBox1.Text +"\"}";

                StringContent queryString = new StringContent(test, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://equipment-rental.herokuapp.com/cizposoje", queryString);

                response.EnsureSuccessStatusCode();

                string responseString = response.Content.ReadAsStringAsync().Result;

                dynamic process = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //Form1.uId;
        }
    }
}
