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
using System.Data.SqlClient;

namespace Kayıp_Evcil_Hayvan_Takip_Sistemi
{
    public partial class FrmIlanDuzenleme : Form
    {
        public FrmIlanDuzenleme()
        {
            InitializeComponent();
        }


        private void VerileriYenile()
        {
            string baglantiYolu = "Data Source=NITRO5\\SQLEXPRESS;Initial Catalog=Kayip_Evcil_Hayvan_Takip_Sistemi;Integrated Security=True;";

            using (SqlConnection baglanti = new SqlConnection(baglantiYolu))
            {
                string sorgu = @"SELECT 
                            i.ILANID, h.Adi AS HayvanAdi, h.Tur, h.Cins, h.Cinsiyet, h.Renk,
                            s.Ad AS SahipAdi,s.Soyad AS Soyad, i.KayıpTarihi, i.KaybolduguYer, i.Aciklama
                        FROM ILAN i
                        JOIN Hayvan h ON i.HayvanID = h.HayvanID
                        JOIN Sahip s ON i.SahipID = s.SahipID";

                SqlDataAdapter da = new SqlDataAdapter(sorgu, baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                TxtHayvanAdi.Text = row.Cells["Adi"].Value.ToString();
                TxtTur.Text = row.Cells["Tur"].Value.ToString();
                TxtCins.Text = row.Cells["Cins"].Value.ToString();
                CmbCinsiyet.Text = row.Cells["Cinsiyet"].Value.ToString();
                TxtRenk.Text = row.Cells["Renk"].Value.ToString();


                TxtSahipAdi.Text = row.Cells["Ad"].Value.ToString();
                TxtSahipSoyadi.Text = row.Cells["Soyad"].Value.ToString();
                TxtTelefonNo.Text = row.Cells["TelefonNo"].Value.ToString();
                TxtEmail.Text = row.Cells["Email"].Value.ToString();

                RTxtKaybolduguYer.Text = row.Cells["KaybolduguYer"].Value.ToString();
                RTxtAciklama.Text = row.Cells["Aciklama"].Value.ToString();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string baglantiYolu = "Data Source=NITRO5\\SQLEXPRESS;Initial Catalog=Kayip_Evcil_Hayvan_Takip_Sistemi;Integrated Security=True;";

            using (SqlConnection baglanti = new SqlConnection(baglantiYolu))
            {
                string sorgu = @"SELECT 
                            i.ILANID,
                            h.HayvanID,
                            s.SahipID,
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


                dataGridView1.Columns["HayvanID"].Visible = false;
                dataGridView1.Columns["SahipID"].Visible = false; //tabloda göstermedim.
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {

            int hayvanID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["HayvanID"].Value);
            int sahipID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["SahipID"].Value);


            string baglantiYolu = "Data Source=NITRO5\\SQLEXPRESS;Initial Catalog=Kayip_Evcil_Hayvan_Takip_Sistemi;Integrated Security=True;";

            using (SqlConnection baglanti = new SqlConnection(baglantiYolu))
            {
                baglanti.Open();
                SqlTransaction trans = baglanti.BeginTransaction();

                try
                {
                    // Hayvan Güncelleme
                    SqlCommand cmdHayvan = new SqlCommand(@"UPDATE Hayvan 
                    SET Adi = @Adi, Tur = @Tur, Cins = @Cins, Cinsiyet = @Cinsiyet, Renk = @Renk 
                    WHERE HayvanID = @HayvanID", baglanti, trans);

                    cmdHayvan.Parameters.AddWithValue("@Adi", TxtHayvanAdi.Text);
                    cmdHayvan.Parameters.AddWithValue("@Tur", TxtTur.Text);
                    cmdHayvan.Parameters.AddWithValue("@Cins", TxtCins.Text);
                    cmdHayvan.Parameters.AddWithValue("@Cinsiyet", CmbCinsiyet.Text);
                    cmdHayvan.Parameters.AddWithValue("@Renk", TxtRenk.Text);
                    cmdHayvan.Parameters.AddWithValue("@HayvanID", hayvanID);

                    cmdHayvan.ExecuteNonQuery();


                    //sahip
                    SqlCommand cmdSahip = new SqlCommand(@"UPDATE Sahip 
                    SET Ad = @Ad,Soyad = @Soyad, TelefonNo = @TelefonNo, Email = @Email 
                    WHERE SahipID = @SahipID", baglanti, trans);

                    cmdSahip.Parameters.AddWithValue("@Ad", TxtSahipAdi.Text);
                    cmdSahip.Parameters.AddWithValue("@Soyad", TxtSahipSoyadi.Text);
                    cmdSahip.Parameters.AddWithValue("@TelefonNo", TxtTelefonNo.Text);
                    cmdSahip.Parameters.AddWithValue("@Email", TxtEmail.Text);
                    cmdSahip.Parameters.AddWithValue("@SahipID", sahipID);



                    cmdSahip.ExecuteNonQuery();


                    //ilan
                    SqlCommand cmdIlan = new SqlCommand(@"UPDATE ILAN 
                    SET KayıpTarihi = @KayipTarihi, KaybolduguYer = @KaybolduguYer, Aciklama = @Aciklama 
                    WHERE ILANID = @IlanID", baglanti, trans);

                    cmdIlan.Parameters.AddWithValue("@KayipTarihi", DateTime.Parse(DtpKayipTarihi.Text));
                    cmdIlan.Parameters.AddWithValue("@KaybolduguYer", RTxtKaybolduguYer.Text);
                    //cmdIlan.Parameters.AddWithValue("@IlanTarihi", DateTime.Now.Date);
                    cmdIlan.Parameters.AddWithValue("@Aciklama", RTxtAciklama.Text);

                    int ilanID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ILANID"].Value);
                    cmdIlan.Parameters.AddWithValue("@IlanID", ilanID);
                    cmdIlan.ExecuteNonQuery();



                    
                    trans.Commit();
                    MessageBox.Show("Güncelleme başarılı!");
                    VerileriYenile();



                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message);
                }
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Lütfen silinecek bir kayıt seçin.");
                return;
            }

            int ilanID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ILANID"].Value);
            int hayvanID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["HayvanID"].Value);  
            int sahipID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["SahipID"].Value);

            string baglantiYolu = "Data Source=NITRO5\\SQLEXPRESS;Initial Catalog=Kayip_Evcil_Hayvan_Takip_Sistemi;Integrated Security=True;";

            using (SqlConnection baglanti = new SqlConnection(baglantiYolu))
            {
                baglanti.Open();
                SqlTransaction trans = baglanti.BeginTransaction();

                try
                {
                    
                    SqlCommand cmdIlan = new SqlCommand("DELETE FROM ILAN WHERE ILANID = @ILANID", baglanti, trans);
                    cmdIlan.Parameters.AddWithValue("@ILANID", ilanID);
                    cmdIlan.ExecuteNonQuery();

                    
                    SqlCommand cmdHayvan = new SqlCommand("DELETE FROM Hayvan WHERE HayvanID = @HayvanID", baglanti, trans);
                    cmdHayvan.Parameters.AddWithValue("@HayvanID", hayvanID);
                    cmdHayvan.ExecuteNonQuery();

                    
                    SqlCommand cmdSahip = new SqlCommand("DELETE FROM Sahip WHERE SahipID = @SahipID", baglanti, trans);
                    cmdSahip.Parameters.AddWithValue("@SahipID", sahipID);
                    cmdSahip.ExecuteNonQuery();

                    trans.Commit();
                    MessageBox.Show("Kayıt başarıyla silindi.");
                    VerileriYenile();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Silme işlemi başarısız oldu: " + ex.Message);
                }
            }
        }


    }
    }
    



