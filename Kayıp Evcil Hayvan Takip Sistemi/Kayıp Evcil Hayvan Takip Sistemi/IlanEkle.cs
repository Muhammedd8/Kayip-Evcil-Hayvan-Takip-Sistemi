using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Kayıp_Evcil_Hayvan_Takip_Sistemi
{
    public partial class FrmIlanEkle: Form
    {
        public FrmIlanEkle()
        {
            InitializeComponent();
        }

        private void BtnIlanEkle_Click(object sender, EventArgs e)
        {
            Hayvan hayvan = new Hayvan
            {
                Adi = TxtHayvanAdi.Text,
                Tur = TxtTur.Text,
                Cins = TxtCins.Text,
                Cinsiyet = CmbCinsiyet.Text,
                Renk = TxtRenk.Text
            };

            Sahip sahip = new Sahip
            {
                Ad = TxtSahipAdi.Text,
                Soyad = TxtSahipSoyadi.Text,
                TelefonNo = TxtTelefonNo.Text,
                Email = TxtEmail.Text
            };

            Ilan ılan = new Ilan
            {
                KaybolmaTarihi = DateTime.Parse(DtpKayipTarihi.Text),
                KaybolmaYeri = RTxtKaybolduguYer.Text,
                //IlanTarihi = DateTime.Parse(DtpIlanTarihi.Text),
                Aciklama = RTxtAciklama.Text
            };


            //string connectionString = ConfigurationManager.ConnectionStrings["Kayip_Evcil_Hayvan_Takip_Sistemi"].ConnectionString;

            string connectionString = "Data Source=NITRO5\\SQLEXPRESS;Initial Catalog=Kayip_Evcil_Hayvan_Takip_Sistemi;Integrated Security=True;";




            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();
                SqlTransaction trans = baglanti.BeginTransaction();

                try
                {
                    //Hayvan
                    SqlCommand cmdHayvan = new SqlCommand(
                    "INSERT INTO Hayvan (Adi, Tur, Cins, Cinsiyet, Renk) OUTPUT INSERTED.HayvanID VALUES (@Adi, @Tur, @Cins, @Cinsiyet, @Renk)",baglanti, trans);
                    cmdHayvan.Parameters.AddWithValue("@Adi", hayvan.Adi);
                    cmdHayvan.Parameters.AddWithValue("@Tur", hayvan.Tur);
                    cmdHayvan.Parameters.AddWithValue("@Cins", hayvan.Cins);
                    cmdHayvan.Parameters.AddWithValue("@Cinsiyet", hayvan.Cinsiyet);
                    cmdHayvan.Parameters.AddWithValue("@Renk", hayvan.Renk);
                    hayvan.HayvanID = (int)cmdHayvan.ExecuteScalar();


                    //Sahip
                    SqlCommand cmdSahip = new SqlCommand(
                    "INSERT INTO Sahip (Ad, Soyad, TelefonNo, Email) OUTPUT INSERTED.SahipID VALUES (@Ad, @Soyad, @TelefonNo, @Email)", baglanti, trans);
                    cmdSahip.Parameters.AddWithValue("@Ad", sahip.Ad);
                    cmdSahip.Parameters.AddWithValue("@Soyad", sahip.Soyad);
                    cmdSahip.Parameters.AddWithValue("@TelefonNo", sahip.TelefonNo);
                    cmdSahip.Parameters.AddWithValue("@Email", sahip.Email);
                    sahip.SahipID = (int)cmdSahip.ExecuteScalar();

                    //İlan
                    SqlCommand cmdIlan = new SqlCommand(
                    "INSERT INTO Ilan (HayvanID, SahipID, KayıpTarihi, KaybolduguYer, İlanTarihi, Aciklama) " +
                    "VALUES (@HayvanID, @SahipID, @KayipTarihi, @KaybolduguYer, @IlanTarihi, @Aciklama)", baglanti, trans);
                    cmdIlan.Parameters.AddWithValue("@HayvanID", hayvan.HayvanID);
                    cmdIlan.Parameters.AddWithValue("@SahipID", sahip.SahipID);
                    cmdIlan.Parameters.AddWithValue("@KayipTarihi", ılan.KaybolmaTarihi);
                    cmdIlan.Parameters.AddWithValue("@KaybolduguYer", ılan.KaybolmaYeri);
                    cmdIlan.Parameters.AddWithValue("@IlanTarihi", DateTime.Now.Date);
                    cmdIlan.Parameters.AddWithValue("@Aciklama", ılan.Aciklama);

                    cmdIlan.ExecuteNonQuery();

                    trans.Commit();

                    MessageBox.Show("Kayıp ilanı başarıyla eklendi!");

                }



                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }

        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            TxtHayvanAdi.Text = "";
            TxtTur.Text = "";
            TxtCins.Text = "";
            CmbCinsiyet.Text = "";
            TxtRenk.Text = "";

            TxtSahipAdi.Text = "";
            TxtSahipSoyadi.Text = "";
            TxtTelefonNo.Text = "";
            TxtEmail.Text = "";

            DtpKayipTarihi.Text = "";
            RTxtKaybolduguYer.Text = "";
            //DtpIlanTarihi.Text = "";
            RTxtAciklama.Text = "";

        }
    }
}
