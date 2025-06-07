using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kayıp_Evcil_Hayvan_Takip_Sistemi
{
    public partial class FrmAnaSayfa: Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmIlanEkle fr = new FrmIlanEkle();
            fr.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmIlanlar fr = new FrmIlanlar();
            fr.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmIlanDuzenleme fr = new FrmIlanDuzenleme();
            fr.Show();
        }
    }
}
