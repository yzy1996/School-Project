using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace MyImageView
{
    public partial class Form2 : Form
    {
        long a;
        public Form2()
        {
            InitializeComponent();
            label1.Text = "100";           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            a = trackBar1.Value;
            ImageCodecInfo myImageCodecInfo;
            Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;
            myImageCodecInfo = GetEncoderInfo("image/jpeg");
            myEncoder = Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);
            myEncoderParameter = new EncoderParameter(myEncoder, (long)a);
            myEncoderParameters.Param[0] = myEncoderParameter;

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.AddExtension = true;
            saveFile.CheckFileExists = false;
            saveFile.DefaultExt = "jpg";
            saveFile.Filter = "JPG Files(*.JPG)|*.JPG|All files (*.*)|*.*";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                Form1.bitmap1.Save(saveFile.FileName, myImageCodecInfo, myEncoderParameters);
            }            
        }
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromHbitmap(Form1.bitmap1.GetHbitmap());
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = string.Format("{0}%", trackBar1.Value);
        }
    }
}
