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
    public partial class YokalmaSistemi : Form
    {
       
        public static string name;
        int sayaç = 0;
        public static string[] dizi=new string [20];
        public static string[] dizi2 = new string[20];
        public static  int artan = -1;
        public static int artan2 = -1;
        public static string yedek;
        static string conString = "Data Source=DESKTOP-GJ184GT\\MSSQLSERVER1;Initial Catalog=DB_OGR;Integrated Security=True";
        SqlConnection baglanti = new SqlConnection(conString);
        SqlDataAdapter da;
        DataSet ds;
        BusinessRecognition recognition = new BusinessRecognition("D:\\", "Yüz", "yuz.xml");
        Classifier_Train train = new Classifier_Train("D:\\", "Yüz", "yuz.xml");
        public YokalmaSistemi()
        {
           
            InitializeComponent();
            listView1.Columns.Add("Adı Soyadı", 100);
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
                        pic_kucuk_res.Image = sadeyuz.ToBitmap();
                        if (train.IsTrained)
                        {
                            name = train.Recognise(sadeyuz);
                            int match_value = (int)train.Get_Eigen_Distance;
                            image.Draw(name + " ", ref font, new Point(yuz.rect.X - 2, yuz.rect.Y - 2), new Bgr(Color.SteelBlue));

                        }
                        image.Draw(yuz.rect, new Bgr(Color.Purple), 2);

                    }
                    pic_kamera.Image = image.ToBitmap();
                };
            }
        }
            private void YokalmaSistemi_Load(object sender, EventArgs e)
            {
                Capture capture1 = new Capture();
                capture1.Start();
            if (capture1== null)
            {
                MessageBox.Show("Kamera Açılamadı");
            }
            else
            {
                capture1.ImageGrabbed += (a, b) =>
                    {
                        var image = capture1.RetrieveBgrFrame();
                        var grayimage1 = image.Convert<Gray, byte>();
                        HaarCascade haaryuz = new HaarCascade("haarcascade_frontalface_default.xml");
                        MCvAvgComp[][] Yuzler = grayimage1.DetectHaarCascade(haaryuz, 1.2, 5, HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(15, 15));
                        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_COMPLEX, 0.5, 0.5);
                        foreach (MCvAvgComp yuz in Yuzler[0])
                        {
                            var sadeyuz = grayimage1.Copy(yuz.rect).Convert<Gray, byte>().Resize(100, 100, INTER.CV_INTER_CUBIC);
                            pic_kucuk_res.Image = sadeyuz.ToBitmap();
                            if (train.IsTrained)
                            {
                                name = train.Recognise(sadeyuz);
                                int match_value = (int)train.Get_Eigen_Distance;
                                image.Draw(name + " ", ref font, new Point(yuz.rect.X - 2, yuz.rect.Y - 2), new Bgr(Color.SteelBlue));
                               
                            }
                            image.Draw(yuz.rect, new Bgr(Color.Purple), 2);
                          //  textBox1.Text = name;
                        }
                        pic_kamera.Image = image.ToBitmap();
                  
                    };
            }
            
          
        }

   
        private void button1_Click(object sender, EventArgs e)
        {
            if (combo_Box_Sinif.Text == "BIL102")
            {
                try
                {
                    baglanti.Open();
                    artan++;
                    SqlCommand cmd = new SqlCommand("Select OGRISIM From BIL_102", baglanti);
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<string> liste = new List<string>();
                    while (dr.Read())
                    {
                        liste.Add(dr["OGRISIM"].ToString());
                    }
                    string[] str = liste.ToArray();
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (name == str[i])
                        {
                            sayaç = 1;
                            /*dizi[artan] = name;
                            listView1.View = View.Details;
                            ListViewItem ögrenci = new ListViewItem(name);
                            listView1.Items.Add(ögrenci);
                            MessageBox.Show("Öğrenci Var Olarak Kaydedildi.");*/
                        }
                

                    }
                    if (sayaç == 1)
                    {

                        dizi[artan] = name;
                        listView1.View = View.Details;
                        ListViewItem ögrenci = new ListViewItem(name);
                        listView1.Items.Add(ögrenci);
                       // MessageBox.Show("Öğrenci Var Olarak Kaydedildi.");
                        int kisi_sayisi = artan + 1;
                        label4.Text = (kisi_sayisi.ToString());
                        name = null;
                        sayaç = 0;
                    }
                    else {
                        MessageBox.Show("Öğrenci Bu Sınıfta Bulunmamaktadır.");
                    }

                    baglanti.Close();
                }
                catch (Exception)
                {

                    MessageBox.Show("Veri Bağlantısı Sağlanamadı.");
                }
            }
            else if (combo_Box_Sinif.Text == "BIL101")
            {

                try
                {
                    artan++;
                    baglanti.Open();
                    SqlCommand cmd = new SqlCommand("Select OGRISIM From BIL_101", baglanti);
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<string> liste = new List<string>();
                    while (dr.Read())
                    {
                        liste.Add(dr["OGRISIM"].ToString());
                    }
                    string[] str2 = liste.ToArray();
                    for (int i = 0; i < str2.Length; i++)
                    {
                        if (name == str2[i].Trim())
                        {
                            sayaç = 1;
                          /*  dizi[artan] = name;
                            listView1.View = View.Details;
                            ListViewItem ögrenci = new ListViewItem(name);
                            listView1.Items.Add(ögrenci);
                            MessageBox.Show("Öğrenci Var Olarak Kaydedildi.");*/
                        }

                    }
                    if (sayaç == 1)
                    {
                        dizi[artan] = name;
                        listView1.View = View.Details;
                        ListViewItem ögrenci = new ListViewItem(name);
                        listView1.Items.Add(ögrenci);
                       // MessageBox.Show("Öğrenci Var Olarak Kaydedildi.");
                        int kisi_sayisi = artan+1;
                        label4.Text=(kisi_sayisi.ToString());
                        name = null;
                        sayaç = 0;
                    }
                    else {
                        MessageBox.Show("Öğrenci Bu Sınıfta Bulunmamaktadır.");
                    }
                    baglanti.Close();
                }
                catch (Exception)
                {

                    MessageBox.Show("Veri Bağlantısı Sağlanamadı.");
                }
            }
            else {

                MessageBox.Show("Lütfen Sınıf Seçiniz.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
          /*  Sinif_Listesi sinif_listesi = new Sinif_Listesi();
            sinif_listesi.dizi[50] = sinif_listesi.Text;
            sinif_listesi.sifre = sinif_listesi.Text;
            sinif_listesi.Show();*/
        }

        private void combo_Box_Sinif_TextChanged(object sender, EventArgs e)
        {
            if (combo_Box_Sinif.Text == "BIL101") {
                dataGridView1.Visible = true;
                dataGridView2.Visible = false;
                baglanti.Open();
                string kayit = "SELECT * from BIL_101";
                SqlCommand komut = new SqlCommand(kayit, baglanti);
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                baglanti.Close();
               // this.t_OBİS2TableAdapter.Fill(this.dBOBİSDataSet2.T_OBİS2);
            }
            if (combo_Box_Sinif.Text == "BIL102") {
                dataGridView1.Visible = false;
                dataGridView2.Visible = true;
                baglanti.Open();
                string kayit = "SELECT * from BIL_102";
                SqlCommand komut = new SqlCommand(kayit, baglanti);
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView2.DataSource = dt;
                baglanti.Close();

            }
        }

      
    }
    }
    
