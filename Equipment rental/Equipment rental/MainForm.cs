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
    public partial class MainForm : MetroForm
    {
        public string uName;

        public MainForm(string _email)
        {
            InitializeComponent();
           // UpdateData();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MetroButton1_Click(object sender, EventArgs e)
        {
            Kategorije kategorije = new Kategorije();
            kategorije.ShowDialog();
        }

        private void MetroButton2_Click(object sender, EventArgs e)
        {
            Stanja stanja = new Stanja();
            stanja.ShowDialog();
        }

        private void MetroButton4_Click(object sender, EventArgs e)
        {
            Oprema oprema = new Oprema();
            oprema.ShowDialog();
        }

        private void MetroButton3_Click(object sender, EventArgs e)
        {
            Izposoje izposoje = new Izposoje();
            izposoje.ShowDialog();
        }

        private void MetroButton5_Click(object sender, EventArgs e)
        {
            Placniki placniki = new Placniki();
            placniki.ShowDialog();
        }

        public async void UpdateData()
        {
            dataGridView1.DataSource = null;

            try
            {
                dataGridView1.DataSource = null;

                HttpClient client = new HttpClient();

                string test = "{ \"id\": " + Form1.uId.ToString() + "}";

                StringContent queryString = new StringContent(test, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://equipment-rental.herokuapp.com/userinfo", queryString);

                response.EnsureSuccessStatusCode();

                string responseString = response.Content.ReadAsStringAsync().Result;

                dynamic process = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);

                uName = process["ime"].ToString() + " " + process["priimek"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            try
            {

                HttpClient client = new HttpClient();

                string test = "";

                StringContent queryString = new StringContent(test, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://equipment-rental.herokuapp.com/vizposoje", queryString);

                response.EnsureSuccessStatusCode();

                string responseString = response.Content.ReadAsStringAsync().Result;

                dynamic process = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);

                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();

                foreach (var item in process)
                {
                    Console.WriteLine("test1");
                    if (item["nadzornik"].ToString() == uName)
                    {
                        dataGridView1.Rows.Add(item["id"].ToString(), item["nadzornik"].ToString(), item["opis"].ToString(), item["oprema"].ToString(), item["placnik"].ToString(), item["datum_do"].ToString(), item["datum_od"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void MetroButton6_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = true;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Exported from gridview";
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            }
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }
        }

        private void MainForm_Enter(object sender, EventArgs e)
        {
            //UpdateData();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            UpdateData();
        }
    }
}
