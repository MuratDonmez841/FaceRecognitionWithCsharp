using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using Capture = Emgu.CV.Capture;
using Emgu.CV.CvEnum;
using System.Threading;
using deneme1211;
using System.Data.SqlClient;
        
namespace deneme12211
{
    public partial class OgrKayit : Form
    {
        static string conString = "Data Source=DESKTOP-GJ184GT\\MSSQLSERVER1;Initial Catalog=DB_OGR;Integrated Security=True";
        SqlConnection baglanti = new SqlConnection(conString);
        SqlDataAdapter da;
        DataSet ds;
        int fotosayisi = 0;
        BusinessRecognition recognition = new BusinessRecognition("D:\\", "Yüz", "yuz.xml");
        Classifier_Train train = new Classifier_Train("D:\\", "Yüz", "yuz.xml");
        public OgrKayit()
        {

            InitializeComponent();
            Capture capture = new Capture();
            capture.Start();
            if (capture == null)
            {
                MessageBox.Show("Kamera Açılamadı");
            }
            else
            {
                capture.ImageGrabbed += (a, b) =>
                {
                    var image = capture.RetrieveBgrFrame();
                    var grayimage = image.Convert<Gray, byte>();
                    HaarCascade haaryuz = new HaarCascade("haarcascade_frontalface_default.xml");
                    MCvAvgComp[][] Yuzler = grayimage.DetectHaarCascade(haaryuz, 1.2, 5, HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(15, 15));
                    MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_COMPLEX, 0.5, 0.5);
                    foreach (MCvAvgComp yuz in Yuzler[0])
                    {
                        var sadeyuz = grayimage.Copy(yuz.rect).Convert<Gray, byte>().Resize(100, 100, INTER.CV_INTER_CUBIC);
                        image.Draw(yuz.rect, new Bgr(Color.Red), 2);
                        pic_kucuk_res.Image = sadeyuz.ToBitmap();
                    }
                    pic_box_kamera.Image = image.ToBitmap();
                };
            }
        }
        private async void btn_kayit_Click(object sender, EventArgs e)
        {
            /*  if (TekrarKontrolu(txt_box_Okul_No.Text)==false)
              {
                  MessageBox.Show(txt_box_Okul_No.Text + "Öğrenci Zaten Kayıtlı.");
                  return;
              }*/
            if (ogr_ad_soyad.Text != "" && txt_box_Tc.Text != "" && txt_box_Okul_No.Text != "" && txt_box_Fakülte.Text != "" && txt_box_Bölüm.Text != "")
            {
                if (combo_Box_Sinif.Text == "BIL102") {
                    try
                    {
                        baglanti.Open();
                        string kayit = "INSERT INTO  BIL_102 (OGRNU,OGR_TCNU,OGRISIM,OGRDOGUMTARIHI,OGRBOLUM,OGRFAK,OGRCIN) values(@OGRNU,@OGR_TCNU,@OGRISIM,@OGRDOGUMTARIHI,@OGRBOLUM,@OGRFAK,@OGRCIN) ";
                        SqlCommand komut = new SqlCommand(kayit, baglanti);
                        komut.Parameters.AddWithValue("@OGRNU", txt_box_Okul_No.Text);
                        komut.Parameters.AddWithValue("@OGR_TCNU", txt_box_Tc.Text);
                        komut.Parameters.AddWithValue("@OGRISIM", ogr_ad_soyad.Text);
                        komut.Parameters.AddWithValue("@OGRDOGUMTARIHI", dt_time_Dogum_t.Text);
                        komut.Parameters.AddWithValue("@OGRBOLUM", txt_box_Bölüm.Text);
                        komut.Parameters.AddWithValue("@OGRFAK", txt_box_Fakülte.Text);
                        komut.Parameters.AddWithValue("@OGRCIN", combo_box_Cinsiyet.Text);
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                    }

                            catch (Exception)
                    {
                        MessageBox.Show("Kayıt yapılamadı!");
                    }
                        for (int i = 0; i < 10; i++)
                    {
                        fotosayisi++;
                        if (!recognition.SaveTrainingData(pic_kucuk_res.Image, ogr_ad_soyad.Text))
                        {
                            MessageBox.Show("Öğrenci Kaydı Yapılamadı");
                            
                        }
                        label11.Text = (fotosayisi.ToString());
                    }
                    recognition = null;
                    train = null;
                    recognition = new BusinessRecognition("D:\\", "Yüz", "yuz.xml");
                    train = new Classifier_Train("D:\\", "Yüz", "yuz.xml");
                    MessageBox.Show("Kayıt Başarılı Bir Şekilde Tamamlandı!");
                    fotosayisi = 0;
                    label11.Text = (fotosayisi.ToString());
                    txt_box_Okul_No.Clear();
                    txt_box_Tc.Clear();
                    ogr_ad_soyad.Clear();
                    txt_box_Bölüm.Clear();
                    txt_box_Fakülte.Clear();
                    

                }
                if (combo_Box_Sinif.Text == "BIL101") {

                    try
                    {
                        baglanti.Open();
                        string kayit = "INSERT INTO  BIL_101 (OGRNU,OGR_TCNU,OGRISIM,OGRDOGUMTARIHI,OGRBOLUM,OGRFAK,OGRCIN) values(@OGRNU,@OGR_TCNU,@OGRISIM,@OGRDOGUMTARIHI,@OGRBOLUM,@OGRFAK,@OGRCIN) ";
                        SqlCommand komut = new SqlCommand(kayit, baglanti);
                        komut.Parameters.AddWithValue("@OGRNU", txt_box_Okul_No.Text);
                        komut.Parameters.AddWithValue("@OGR_TCNU", txt_box_Tc.Text);
                        komut.Parameters.AddWithValue("@OGRISIM", ogr_ad_soyad.Text);
                        komut.Parameters.AddWithValue("@OGRDOGUMTARIHI", dt_time_Dogum_t.Text);
                        komut.Parameters.AddWithValue("@OGRBOLUM", txt_box_Bölüm.Text);
                        komut.Parameters.AddWithValue("@OGRFAK", txt_box_Fakülte.Text);
                        komut.Parameters.AddWithValue("@OGRCIN", combo_box_Cinsiyet.Text);
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                    }

                    catch (Exception)
                    {
                        MessageBox.Show("Kayıt yapılamadı!");
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        fotosayisi++;
                        if (!recognition.SaveTrainingData(pic_kucuk_res.Image, ogr_ad_soyad.Text))
                        {
                            MessageBox.Show("Öğrenci Kaydı Yapılamadı");
                            
                        }

                        label11.Text = (fotosayisi.ToString());
                    }
                    recognition = null;
                    train = null;
                    recognition = new BusinessRecognition("D:\\", "Yüz", "yuz.xml");
                    train = new Classifier_Train("D:\\", "Yüz", "yuz.xml");
                    MessageBox.Show("Kayıt Başarılı Bir Şekilde Tamamlandı!");
                    fotosayisi = 0;
                    label11.Text = (fotosayisi.ToString());
                    txt_box_Okul_No.Clear();
                    txt_box_Tc.Clear();
                    ogr_ad_soyad.Clear();
                    txt_box_Bölüm.Clear();
                    txt_box_Fakülte.Clear();

                }

            } 
                else
                {
                    MessageBox.Show("Alanlardan hiç birini boş bırakmayınız!");
                }
            
        }
  

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Kaydı Veritabanından silmek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            
                if (secenek == DialogResult.Yes)
                { 
                if (combo_Box_Sinif.Text == "BIL102")
                    {
                        string OGRNU = txt_box_Okul_No.Text;
                        string sql = "DELETE FROM T_OBİS WHERE OGRNU=@OGRNU";
                        SqlCommand komut = new SqlCommand(sql, baglanti);
                        komut.Parameters.AddWithValue("@OGRNU", OGRNU);
                        baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Kayıt başarıyla silindi");
                    }
                   else if (combo_Box_Sinif.Text == "BIL101")
                    {
                        
                        string OGRNU = txt_box_Okul_No.Text;
                        string sql = "DELETE FROM T_OBİS2 WHERE OGRNU=@OGRNU";
                        SqlCommand komut = new SqlCommand(sql, baglanti);
                        komut.Parameters.AddWithValue("@OGRNU", OGRNU);
                        baglanti.Open();
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Kayıt başarıyla silindi");

                    }
                    else
                    {
                        MessageBox.Show("Lütfen Sınıf Seçiniz!");

                    }

                }
                else if (secenek == DialogResult.No)
                {

                }
            }
 
        

        private void OgrKayit_Load(object sender, EventArgs e)
        {
           // this.t_OBİSTableAdapter.Fill(this.dBOBİSDataSet.T_OBİS);
        }

        private void ogr_ad_soyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                        && !char.IsSeparator(e.KeyChar);
        }

        private void txt_box_Fakülte_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                      && !char.IsSeparator(e.KeyChar);
        }

        private void txt_box_Tc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txt_box_Okul_No_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void ogr_ad_soyad_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }


        /*private void OgrKayit_Load(object sender, EventArgs e)
        {
            Capture capture = new Capture();
            capture.Start();
            if (capture == null)
            {
                MessageBox.Show("Kamera Açılamadı");
            }
            else
            {
                capture.ImageGrabbed += (a, b) =>
                {
                    var image = capture.RetrieveBgrFrame();
                    var grayimage = image.Convert<Gray, byte>();
                    HaarCascade haaryuz = new HaarCascade("haarcascade_frontalface_default.xml");
                    MCvAvgComp[][] Yuzler = grayimage.DetectHaarCascade(haaryuz, 1.2, 5, HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(15, 15));
                    MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_COMPLEX, 0.5, 0.5);
                    foreach (MCvAvgComp yuz in Yuzler[0])
                    {
                        var sadeyuz = grayimage.Copy(yuz.rect).Convert<Gray, byte>().Resize(100, 100, INTER.CV_INTER_CUBIC);
                        image.Draw(yuz.rect, new Bgr(Color.Red), 2);
                        pic_kucuk_res.Image = sadeyuz.ToBitmap();
                    }
                    pic_box_kamera.Image = image.ToBitmap();
                };
            }*/
    }





}


