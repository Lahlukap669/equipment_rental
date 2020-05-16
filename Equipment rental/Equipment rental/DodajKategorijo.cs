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
    public partial class DodajKategorijo : MetroForm
    {
        public DodajKategorijo()
        {
            InitializeComponent();
        }

        private async void MetroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();

                string test = "{ \"kategorija\": \"" + metroTextBox1.Text + "\", \"opis\": \"" + metroTextBox2.Text + "\"}";

                StringContent queryString = new StringContent(test, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://equipment-rental.herokuapp.com/ckategorije", queryString);

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
