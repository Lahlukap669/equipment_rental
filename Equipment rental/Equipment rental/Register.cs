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
    public partial class Register : MetroForm
    {
        public Register()
        {
            InitializeComponent();
        }

        private async void MetroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();

                string test = "{ \"ime\": \"" + metroTextBox1.Text + "\", \"priimek\": \"" + metroTextBox2.Text + "\", \"email\": \"" + metroTextBox3.Text + "\", \"geslo\": \"" + metroTextBox4.Text + "\", \"tel\": \"" + metroTextBox5.Text + "\" }";

                StringContent queryString = new StringContent(test, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://equipment-rental.herokuapp.com/create_user", queryString);

                response.EnsureSuccessStatusCode();

                string responseString = response.Content.ReadAsStringAsync().Result;

                dynamic process = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);

                if(process["bool"].ToString() == "True")
                {
                    MessageBox.Show("Registration successful.");
                }
                else
                {
                    MessageBox.Show("Registration failed, this user might already exist!");
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
