using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeShop
{
    internal class DBManager
    {
        private static DBManager instance_ = new DBManager();
        string Connection_string = "Server=115.85.181.212;Port=3306;Database=s5727645;Uid=s5727645;Pwd=s5727645;CharSet=utf8;";
        //string Connection_string = DBManager.GetInstance().Get_String();
        public string query = "";

        public static DBManager GetInstance()
        {
            return instance_;
        }

        private DBManager()
        {
            // .. Some initialization for this singleton object
        }

        public string Get_String()
        {
            return "Server=115.85.181.212;Port=3306;Database=s5727645;Uid=s5727645;Pwd=s5727645;CharSet=utf8;";
        }

        public int SELECT_Login(string str_id, string str_pw)
        {
            query = "SELECT * FROM s5727645.User WHERE ID = '" + str_id + "' AND PW = '" + str_pw + "';";
            using (MySqlConnection connection = new MySqlConnection(Connection_string))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read() != false)
                {
                    if (rdr[0].ToString() == "1")
                    {
                        return 9;
                    }
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public void INSERT_sales(string userid, string coffeeid, string date)
        {
            query = "insert into Sales values((SELECT User_id FROM User WHERE ID = '" + userid + "'), (SELECT idCoffee FROM Coffee WHERE name = '" + coffeeid + "'), '" + date + "');";
            using (MySqlConnection connection = new MySqlConnection(Connection_string))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
            }
        }

        public void Search_data_User(DataGridView dataGridView1, DataTable table, string dates)
        {
            string query = "select ID, sum((select price FROM Coffee WHERE idCoffee = COFFEE_ID)) AS \"일일판매액\", count(*) AS \"일일판매량\" from User, Sales where User.User_id = Sales.USER_ID AND Sales.orderdate = '" +dates + "' group by ID";
            

            using (MySqlConnection connection = new MySqlConnection(Connection_string))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                table.Columns.Add("사용자");
                table.Columns.Add("일일판매액");
                table.Columns.Add("일일판매량");
                dataGridView1.DataSource = null;
                while (rdr.Read())
                {
                    table.Rows.Add(rdr[0], rdr[1], rdr[2]);
                    dataGridView1.DataSource = table;
                }
            }
        }


        public void Search_data_Day(DataGridView dataGridView1, DataTable table, string dates)
        {
            string query = "select Coffee.name, sum((select price FROM Coffee WHERE idCoffee = COFFEE_ID)) AS \"일일판매액\", count(*) AS \"일일판매량\" from Coffee, Sales where Coffee.idCoffee = Sales.COFFEE_ID AND Sales.orderdate = '" + dates + "' group by Coffee.name";



            using (MySqlConnection connection = new MySqlConnection(Connection_string))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                table.Columns.Add("커피종류");
                table.Columns.Add("일일판매액");
                table.Columns.Add("일일판매량");
                dataGridView1.DataSource = null;
                while (rdr.Read())
                {
                    table.Rows.Add(rdr[0], rdr[1], rdr[2]);
                    dataGridView1.DataSource = table;
                }
            }
        }

        public void Search_data_Month(DataGridView dataGridView1, DataTable table, string dates)
        {
            string query = "select Coffee.name, sum((select price FROM Coffee WHERE idCoffee = COFFEE_ID)) AS \"일일판매액\", count(*) AS \"일일판매량\" from Coffee, Sales where Coffee.idCoffee = Sales.COFFEE_ID AND Sales.orderdate LIKE'%" + dates + "%' group by Coffee.name";



            using (MySqlConnection connection = new MySqlConnection(Connection_string))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                table.Columns.Add("커피종류");
                table.Columns.Add("월별판매액");
                table.Columns.Add("월별판매량");
                dataGridView1.DataSource = null;
                while (rdr.Read())
                {
                    table.Rows.Add(rdr[0], rdr[1], rdr[2]);
                    dataGridView1.DataSource = table;
                }
            }
        }


    }
}

