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
    public partial class VsaOprema : MetroForm
    {
        public Dictionary<string, int> savedKeys = new Dictionary<string, int>();

        public VsaOprema()
        {
            InitializeComponent();
            UpdateListBox();
        }

        private async void UpdateListBox()
        {
            try
            {
                listBox1.Items.Clear();
                savedKeys.Clear();

                HttpClient client = new HttpClient();

                string test = "";

                StringContent queryString = new StringContent(test, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://equipment-rental.herokuapp.com/voprema", queryString);

                response.EnsureSuccessStatusCode();

                string responseString = response.Content.ReadAsStringAsync().Result;

                dynamic process = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);

                foreach (var item in process)
                {
                    listBox1.Items.Add(item["ime"].ToString());
                    savedKeys.Add(item["ime"].ToString(), Convert.ToInt32(item["id"]));
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

        private void MetroButton2_Click(object sender, EventArgs e)
        {
            Console.WriteLine(savedKeys[listBox1.SelectedItem.ToString()]);
            PosodobiOpremo posodobiOpremo = new PosodobiOpremo(savedKeys[listBox1.SelectedItem.ToString()]);
            posodobiOpremo.ShowDialog();
        }
    }
}
