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
    public partial class Sinif_Listesi : Form
    {
        public Sinif_Listesi()
        {
            InitializeComponent();

            Label[] lDizi = new Label[10];

            for (int i = 0; i < lDizi.Length; i++)

            {

                lDizi[i] = new Label();

                lDizi[i].Text = "Label" + i.ToString();

                this.Controls.Add(lDizi[i]);

                lDizi[i].Top = i * 30;

                lDizi[i].Left = 20;

                lDizi[i].Width = 100;

            }

        }

    }
}
    

