using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace deneme12211
{
    public partial class AnaSayfa : Form
    {
        public AnaSayfa()
        {
            InitializeComponent();
        }



        private void btn_ogr_kyt_Click(object sender, EventArgs e)
        {
            txtKullanici.Text.ToLower();
            txtSifre.Text.ToLower();   
            if (txtKullanici.Text == "admin" && txtSifre.Text == "admin123")
            {

                OgrKayit ogr_kyt = new OgrKayit();
                ogr_kyt.Show();
                txtKullanici.Clear();
                txtSifre.Clear();
            }
            else if (txtKullanici.Text == "görevli" && txtSifre.Text == "görevli123")
            {

                YokalmaSistemi yoklama_sis = new YokalmaSistemi();
                yoklama_sis.Show();
                txtKullanici.Clear();
                txtSifre.Clear();

            }
            else {
                MessageBox.Show("Geçersiz Kullanıcı Adı Veya Şifre!");
            }
            

        }

        private void txtSifre_TextChanged(object sender, EventArgs e)
        {
            txtSifre.PasswordChar = '*';
        }
    }
}
