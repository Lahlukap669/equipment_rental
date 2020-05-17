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
    public partial class Form1 : MetroForm
    {
        public static int uId;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void Button1_ClickAsync(object sender, EventArgs e)
        {

        }

        private async void  MetroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();

                string test = "{ \"email\": \"" + metroTextBox1.Text + "\", \"geslo\": \"" + metroTextBox2.Text + "\"}";

                StringContent queryString = new StringContent(test, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://equipment-rental.herokuapp.com/login", queryString);

                response.EnsureSuccessStatusCode();

                string responseString = response.Content.ReadAsStringAsync().Result;

                dynamic process = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);

                if (process["bool"].ToString() != "0")
                {
                    MessageBox.Show("Login successful.");
                    MainForm mainForm = new MainForm("lolz");
                    uId = Convert.ToInt32(process["bool"]);
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Login failed!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void MetroButton2_Click(object sender, EventArgs e)
        {
            Register register = new Register();

            register.ShowDialog();
        }
    }
}
