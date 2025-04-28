using Newtonsoft.Json;
using K4os.Compression.LZ4.Encoders;
using MySql.Data.MySqlClient;
using Mysqlx.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
namespace bartaa_keszlet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection kapcs = new MySqlConnection("server = localhost;database = bartaa_keszlet; uid = 'root'; password = ''");
        public MainWindow()
        {
            InitializeComponent();
            Adatok();
        }
        private void Adatok()
        {
            kapcs.Open();
            MySqlCommand leker = new MySqlCommand("SELECT * FROM bartaa_termek", kapcs);
            MySqlDataReader lekerdezes = leker.ExecuteReader();

            while (lekerdezes.Read())
            {
                lbTermekek.Items.Add($"{lekerdezes["id"]} {lekerdezes["cikkszam"]} {lekerdezes["megnevezes"]}");
            }

            lekerdezes.Close();
            kapcs.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            kapcs.Open();
            string cikkszam = txtCikkszam.Text;
            string megnevezes = txtMegnevezes.Text;
            MySqlCommand cmd = new MySqlCommand("INSERT INTO gergelyv_termek (cikkszam, megnevezes) VALUES (@cikkszam, @megnevezes)", kapcs);
            cmd.Parameters.AddWithValue("@cikkszam", cikkszam);
            cmd.Parameters.AddWithValue("@megnevezes", megnevezes);
            cmd.ExecuteNonQuery();
            kapcs.Close();

            lbTermekek.Items.Clear();
            Adatok();
        }

    }
}