using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kayıp_Evcil_Hayvan_Takip_Sistemi
{
    public  class Ilan
    {
        public int IlanID { get; set; }
        public Hayvan KayipHayvan { get; set; } //composition
        public Sahip IletisimBilgileri { get; set; } //composition
        public string KaybolmaYeri { get; set; }
        public DateTime KaybolmaTarihi { get; set; }
        public DateTime IlanTarihi { get; set; }
        public string Aciklama { get; set; }
    }

}
