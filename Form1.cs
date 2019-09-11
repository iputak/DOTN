using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DOTN_projekt
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            prikaziPodatke();
        }
        
        Image Slika;
        string displayimg, filePath;
        string folderpath = @"C:\\Dotn";
        //Unos podataka
        private void button1_Click(object sender, EventArgs e)
        {
                
                string imeSlike = textBox1.Text + ".jpg";
                string mapa = "C:\\Dotn";
                string pathstring = System.IO.Path.Combine(mapa, imeSlike); 

                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Users VALUES (@BrojIskaznice,@Ime,@Prezime,@ZadnjiUpis, @Slika)", con);
                cmd.Parameters.AddWithValue("@BrojIskaznice", int.Parse(textBox1.Text));
                cmd.Parameters.AddWithValue("@Ime", textBox2.Text);
                cmd.Parameters.AddWithValue("@Prezime", textBox3.Text);
                cmd.Parameters.AddWithValue("@ZadnjiUpis", dateTimePicker1.Text);
                Slika.Save(pathstring);
                cmd.Parameters.AddWithValue("@Slika", pathstring);
               // Slika.Save(pathstring);
            
                //Slika.Copy(filePath, pathstring, true);
                //cmd.Parameters.AddWithValue("@Slika", textBox2.Text);

            cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("Uspjesno spremljeno!");
                
                prikaziPodatke();
                makniPodatke();
        }
        

        //Ažuriranje korisnika
        private void azuriraj_Click(object sender, EventArgs e)
        {
            string imeSlike = textBox1.Text + ".jpg";
            string mapa = "C:\\Dotn";
            string pathstring = System.IO.Path.Combine(mapa, imeSlike);

            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Users SET Ime=@Ime,Prezime=@Prezime,ZadnjiUpis=@ZadnjiUpis,Slika=@Slika WHERE BrojIskaznice=@BrojIskaznice", con);
                cmd.Parameters.AddWithValue("@BrojIskaznice", int.Parse(textBox1.Text));
                cmd.Parameters.AddWithValue("@Ime", textBox2.Text);
                cmd.Parameters.AddWithValue("@Prezime", textBox3.Text);
                cmd.Parameters.AddWithValue("@ZadnjiUpis", dateTimePicker1.Text);
                Slika.Save(pathstring);
                cmd.Parameters.AddWithValue("@Slika", pathstring);

                cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("Uspjesno spremljeno!");

                prikaziPodatke();
                makniPodatke();
            }
            else
            {
                MessageBox.Show("Označite korisnika za ažuriranje");
            }
        }
        //Prikaz podataka
        private void prikaziPodatke()
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Users", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        //Micanje podataka iz forme za unos  
        private void makniPodatke()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        
        //Brisanje korisnika
        private void izbrisi_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE Users WHERE BrojIskaznice=@BrojIskaznice", con);

                cmd.Parameters.AddWithValue("@BrojIskaznice", int.Parse(textBox1.Text));
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Korisnik je izbrisan!");
                prikaziPodatke();
                makniPodatke();
            }
            else
            {
                MessageBox.Show("Označite korisnika za brisanje");
            }
        }

        //Prikaz odabranog iz tablice
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
            Slika = Image.FromFile(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
            pictureBox2.Image = Slika;
        }
        //Odabir slike
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Odaberite sliku";
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp;)|*.jpg; *.jpeg; *.gif; *.bmp;";
            if(open.ShowDialog() == DialogResult.OK)
            {
                
                Slika = Image.FromFile(open.FileName);
                pictureBox2.Image = Slika;
                // Putanja slike
                filePath = open.FileName;
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void godine_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
