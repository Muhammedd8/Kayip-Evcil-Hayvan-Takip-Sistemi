using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kayıp_Evcil_Hayvan_Takip_Sistemi
{
    public partial class FrmIlanlar: Form
    {
        public FrmIlanlar()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            string baglantiYolu = "Data Source=NITRO5\\SQLEXPRESS;Initial Catalog=Kayip_Evcil_Hayvan_Takip_Sistemi;Integrated Security=True;";

            using (SqlConnection baglanti = new SqlConnection(baglantiYolu))
            {
                string sorgu = @"SELECT 
                            i.ILANID,
                            h.Adi,
                            h.Tur,
                            h.Cins,
                            h.Cinsiyet,
                            h.Renk,
                            s.Ad,
                            s.Soyad,
                            s.TelefonNo,
                            s.Email,
                            i.KayıpTarihi,
                            i.KaybolduguYer,
                            i.İlanTarihi,
                            i.Aciklama
                        FROM ILAN i
                        JOIN Hayvan h ON i.HayvanID = h.HayvanID
                        JOIN Sahip s ON i.SahipID = s.SahipID";

                SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

    }
}

