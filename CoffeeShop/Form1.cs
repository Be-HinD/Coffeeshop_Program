using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using System.Data;

namespace CoffeeShop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void Hide_Login()
        {
            label1.Visible = false;
            label2.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            button1.Visible = false;
        }

        public void Open_Login()
        {
            label1.Visible = true;
            label2.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            button1.Visible = true;
            label3.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int Login_num = DBManager.GetInstance().SELECT_Login(textBox1.Text, textBox2.Text);
            if(Login_num == 1)
            {
                groupBox1.Visible = true;
                string date = DateTime.Now.ToString("yyyyMMdd");
                string dates = DateTime.ParseExact(date, "yyyyMMdd", null).ToString("yyyy-MM-dd");
                label3.Text = textBox1.Text + "님 어서오세요";
                Hide_Login();
            }
            else if(Login_num == 9)
            {
                groupBox2.Visible = true;
                label3.Text = textBox1.Text + "님 어서오세요";
                Hide_Login();
            }
            else
            {
                MessageBox.Show("실패");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = false;

            Open_Login();
        }

        public void INSERT_sales(string user, string coffee)
        {
            string date = dateTimePicker1.Value.ToString("yyyyMMdd");
            string dates = DateTime.ParseExact(date, "yyyyMMdd", null).ToString("yyyy-MM-dd");
            DBManager.GetInstance().INSERT_sales(user, coffee, dates);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            INSERT_sales(textBox1.Text, button3.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            INSERT_sales(textBox1.Text, button4.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            INSERT_sales(textBox1.Text, button5.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string date = dateTimePicker1.Value.ToString("yyyyMMdd");
            string dates = DateTime.ParseExact(date, "yyyyMMdd", null).ToString("yyyy-MM-dd");
            DataTable tb = new DataTable();
            DBManager.GetInstance().Search_data_User(dataGridView1, tb, dates);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string date = dateTimePicker1.Value.ToString("yyyyMMdd");
            string dates = DateTime.ParseExact(date, "yyyyMMdd", null).ToString("yyyy-MM-dd");
            DataTable tb = new DataTable();
            DBManager.GetInstance().Search_data_Day(dataGridView1, tb, dates);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string date = dateTimePicker1.Value.ToString("yyyyMMdd");
            string dates = DateTime.ParseExact(date, "yyyyMMdd", null).ToString("yyyy-MM");
            MessageBox.Show(dates);
            DataTable tb = new DataTable();
            DBManager.GetInstance().Search_data_Month(dataGridView1, tb, dates);
        }
    }
}