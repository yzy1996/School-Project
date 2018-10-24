
using System;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;
using System.Drawing.Drawing2D;
using Microsoft.VisualBasic.FileIO;
using System.IO;

namespace MyImageView
{
    public partial class Form1 : Form
    {
        #region private var
        int[] newfolder = new int[25];
        private Image img;
        private int similarcount;
        private int i;
        private int hangju;
        private int lieju;
        private string copyPath;    //copied destination path
        private string imagePath;
        private bool start = false;     //if load files succeed, start will be set true

        private string[] fileList;          //All Files will be displayed
        private int fileCount = 0;  //total of the files
        private int currentIndex = 0;       //index of the current files

        private int FirstHeight;      //第一次载入图片的高度
        private int FirstWidth;       //第一次载入图片的宽度


        private int formHeight;      //windows Height
        private int formWidth;      //windows Width
        private int workHeight;    //Screen Height - taskBar Height

        private int imageHeight;      //the displayed image Height
        private int imageWidth;       //the displayed image width
        private Bitmap bitmap;
        public static Bitmap bitmap1;      //全局图片变量 

        private Rectangle formRect;      //display region in form
        private Rectangle imageRect;     //display region in image

        private Point oldMousePoint;
        private int mouseMoveStep;
        private int copyCount = 0;         //how many photos copy to favor directory

        public bool reduce = false;//wheather reduce the picture
        private int page = 0;//totle number of pages
        private int currentpage = 0;//current pages
        private int hang = 5;
        private int lie = 5;



        private bool similar = false;
        #endregion
        #region Inital&Closed Function
        public Form1()
        {
            InitializeComponent();
            //when the program start, it will run in full screen
            formHeight = Screen.PrimaryScreen.Bounds.Height;
            formWidth = Screen.PrimaryScreen.Bounds.Width;
            //the workHeight actually not using, maybe will be used in the future
            workHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.Height = formHeight;      //full screen mode
            this.Width = formWidth;
            toolStrip1.Items[1].Enabled = false;
            toolStrip1.Items[2].Enabled = false;
            toolStrip1.Location = new Point(formWidth - toolStrip1.Width - 5, formHeight - toolStrip1.Height - 5);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseWheel);
            timer1.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] commandLine = Environment.GetCommandLineArgs();
            if (commandLine.Length > 1)
            {
                string fileName = commandLine[1];
                imagePath = System.IO.Path.GetDirectoryName(fileName);
                if (start = LoadImageFiles())
                {
                    for (var i = 0; i < fileList.Length; i++)
                    {
                        if (fileName == fileList[i])
                        {
                            currentIndex = i;
                            //ChangeStauts();
                            break;
                        }
                    }
                    LoadImage();
                }
            }
            else
            {
                RegistryKey SoftwareKey = Registry.LocalMachine.OpenSubKey("Software");
                RegistryKey MyName = SoftwareKey.OpenSubKey("Cuishuning");
                if (MyName != null)
                {
                    RegistryKey imageView = MyName.OpenSubKey("ImageView");
                    if (imageView != null)
                    {
                        imagePath = (string)imageView.GetValue("ImagePath");
                    }
                    else
                    {
                        imagePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                    }
                }
                else
                {
                    imagePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                }

                if (start = LoadImageFiles())
                {
                    LoadImage();
                }

            }
            RegistryKey SoftwareKey2 = Registry.LocalMachine.OpenSubKey("Software");
            RegistryKey MyName2 = SoftwareKey2.OpenSubKey("Cuishuning");
            if (MyName2 != null)
            {
                RegistryKey imageView2 = MyName2.OpenSubKey("ImageView");
                if (imageView2 != null)
                {
                    copyPath = (string)imageView2.GetValue("CopyPath");
                }
                else
                {
                    copyPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                }
            }
            else
            {
                copyPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                RegistryKey SoftwareKey = Registry.LocalMachine.OpenSubKey("Software", true);
                RegistryKey myNameKey = SoftwareKey.CreateSubKey("Cuishuning");
                RegistryKey imageViewKey = myNameKey.CreateSubKey("ImageView");
                imageViewKey.SetValue("ImagePath", (object)imagePath);
                imageViewKey.SetValue("CopyPath", (object)copyPath);
            }
            catch
            {
            }
        }

        #endregion
        private string GetProperty(PropertyItem[] pt, ref int orientation)
        {
            string property = String.Empty;
            for (int i = 0; i < pt.Length; i++)
            {
                PropertyItem p = pt[i];
                switch (pt[i].Id)
                {
                    case 0x010F:  // 设备制造商
                        property += "#厂商" + System.Text.ASCIIEncoding.ASCII.GetString(pt[i].Value, 0, pt[i].Value.Length - 1).Trim() + "#";
                        break;
                    case 0x0110: // 设备型号  
                        property += "型号" + System.Text.ASCIIEncoding.ASCII.GetString(pt[i].Value, 0, pt[i].Value.Length - 1).Trim() + "#";
                        break;
                    case 0x9003: // 拍照时间
                        property += "时间" + System.Text.ASCIIEncoding.ASCII.GetString(pt[i].Value, 0, pt[i].Value.Length - 1).Trim() + "#";
                        break;
                    case 0x829A: // 曝光时间  
                        property += "快门" + GetValueOfType5(p.Value) + "#";
                        break;
                    case 0x8827: // ISO  
                        property += "ISO" + GetValueOfType3(p.Value) + "#";
                        break;
                    //case 0x010E: // 图像说明info.description
                    //    this.textBox6.Text = GetValueOfType2(p.Value);
                    //    break;
                    case 0x920a: //焦距
                        property += "焦距" + GetValueOfType5A(p.Value) + "#";
                        break;
                    case 0x829D: //光圈
                        property += "光圈" + GetValueOfType5A(p.Value) + "#";
                        break;
                    case 0xA433:
                        property += "镜头" + System.Text.ASCIIEncoding.ASCII.GetString(pt[i].Value, 0, pt[i].Value.Length - 1).Trim() + "#";
                        break;
                    case 0xA434:
                        property += System.Text.ASCIIEncoding.ASCII.GetString(pt[i].Value, 0, pt[i].Value.Length - 1).Trim() + "#";
                        break;
                    case 0x112:
                        orientation = Convert.ToUInt16(pt[i].Value[1] << 8 | pt[i].Value[0]);
                        break;
                    default:
                        break;

                }

            }
            return property;
        }   //exif标准规范文件
        private string GetValueOfType3(byte[] b) //对type=3 的value值进行读取
        {
            if (b.Length != 2) return "";
            return Convert.ToUInt16(b[1] << 8 | b[0]).ToString();
        }
        private string GetValueOfType5(byte[] b) //对type=5 的value值进行读取
        {
            if (b.Length != 8) return "";
            UInt32 fm, fz;
            fm = 0;
            fz = 0;
            fz = Convert.ToUInt32(b[7] << 24 | b[6] << 16 | b[5] << 8 | b[4]);
            fm = Convert.ToUInt32(b[3] << 24 | b[2] << 16 | b[1] << 8 | b[0]);
            fz = fz / fm;
            fm = 1;
            return fm.ToString() + "/" + fz.ToString();
        }
        private string GetValueOfType5A(byte[] b)//获取光圈的值
        {
            if (b.Length != 8) return "";
            UInt32 fm, fz;
            fm = 0;
            fz = 0;
            fz = Convert.ToUInt32(b[7] << 24 | b[6] << 16 | b[5] << 8 | b[4]);
            fm = Convert.ToUInt32(b[3] << 24 | b[2] << 16 | b[1] << 8 | b[0]);
            double temp = (double)fm / fz;
            return (temp).ToString();

        }
        /// <summary>
        /// loaded a new image from fileList
        /// Note: not spy any change from file system
        /// </summary>
        private void LoadImage()
        {
            ChangeStauts();
            if (bitmap != null)
            {
                bitmap.Dispose();
            }
            bitmap = new Bitmap(fileList[currentIndex]);
            PropertyItem[] pt = bitmap.PropertyItems;
            //IEnumerable<PropertyItem> propertyItems = pt.Where(ep => ep.Id == 0x112);
            int orientation = 0;
            //if(propertyItems.Count()>0)
            //{
            //    PropertyItem orient = propertyItems.First();
            //    orientation = Convert.ToUInt16(orient.Value[1] << 8 | orient.Value[0]);
            //}
            label3.Text = GetProperty(pt, ref orientation);
            switch (orientation)
            {
                case 2:
                    bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);//horizontal flip
                    break;
                case 3:
                    bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);//right-top
                    break;
                case 4:
                    bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);//vertical flip
                    break;
                case 5:
                    bitmap.RotateFlip(RotateFlipType.Rotate90FlipX);
                    break;
                case 6:
                    bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);//right-top
                    break;
                case 7:
                    bitmap.RotateFlip(RotateFlipType.Rotate270FlipX);
                    break;
                case 8:
                    bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);//left-bottom
                    break;
                default:
                    break;
            }
            imageWidth = bitmap.Width;
            imageHeight = bitmap.Height;
            //image information display
            label1.Text = "W:" + imageWidth.ToString() + " H:" + imageHeight.ToString()
                                 + " Pixel:" + ((imageHeight * imageWidth) / 10000).ToString();
            label2.Text = GetFileNameWithoutPath();
            ShowFull();

        }
        private string GetFileNameWithoutPath()
        {
            Regex re = new Regex(@"\\[^\\]*$");
            Match ma = re.Match(fileList[currentIndex]);
            if (ma.Success)
                return ma.Value.Substring(1);
            else
                return "NewFile.jpg";
        }
        /// <summary>
        /// base on image size, show a full image on screen
        /// </summary>
        private void ShowFull()
        {
            //show full image
            imageRect = new Rectangle(0, 0, imageWidth, imageHeight);
            if (imageWidth <= this.Width && imageHeight <= this.Height)
            {
                int x = (this.Width - imageWidth) / 2;
                int y = (this.Height - imageHeight) / 2;
                formRect = new Rectangle(x, y, imageWidth, imageHeight);
            }
            else
            {
                double ratio = Math.Min((double)this.Height / imageHeight, (double)this.Width / imageWidth);
                int x = (int)(this.Width - imageWidth * ratio) / 2;
                int y = (int)(this.Height - imageHeight * ratio) / 2;
                formRect = new Rectangle(x, y, (int)(imageWidth * ratio), (int)(imageHeight * ratio));
            }
            Invalidate();
        }
        /// <summary>
        /// Load all jpg file form a directory
        /// </summary>
        /// <returns>true if succeed, or false</returns>
        private bool LoadImageFiles()
        {
            try
            {
                //load all jpg files include subdirectory
                fileList = System.IO.Directory.GetFiles(imagePath, "*.jpg", System.IO.SearchOption.AllDirectories);
                fileCount = fileList.GetUpperBound(0);
                page = (fileCount) / (lie * hang);//
            }
            catch
            {
                //load jpg files only top directory
                try
                {
                    fileList = System.IO.Directory.GetFiles(imagePath, "*.jpg", System.IO.SearchOption.TopDirectoryOnly);
                    fileCount = fileList.GetUpperBound(0);
                    page = (fileCount) / (lie * hang);
                }
                catch
                {
                    return false;
                }

            }
            if (fileCount < 0)
            {
                return false;
            }
            currentIndex = 0;
            currentpage = 0;
            //ChangeStauts();
            return true;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (start)
            {
                e.Graphics.DrawImage(bitmap, formRect, imageRect, System.Drawing.GraphicsUnit.Pixel);

            }
        }
        private void Form1_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (similar)
                return;
            if (start && reduce == false)
            {
                currentIndex = (e.Delta > 0 ? currentIndex - 1 : currentIndex + 1);
                currentIndex = (currentIndex < 0 ? 0 : (currentIndex > fileCount ? fileCount : currentIndex));
                LoadImage();

            }
            if (start && reduce == true)
            {
                if (currentpage != 0 && currentpage != page)
                {
                    currentpage = (e.Delta > 0 ? currentpage - 1 : currentpage + 1);
                    currentpage = (currentpage < 0 ? 0 : (currentpage > page ? page : currentpage));
                    showpage();
                }
                else if (currentpage == 0 && currentpage != page)
                {
                    if (e.Delta > 0) { }
                    else
                    {
                        currentpage += 1;
                        showpage();
                    }
                }
                else if (currentpage == page && currentpage != 0)
                {
                    if (e.Delta > 0)
                    {
                        currentpage -= 1;
                        showpage();
                    }
                }
            }
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (similar)
                return;
            if (start && reduce == false)
            {
                if (e.Button == MouseButtons.Left)
                {
                    this.Capture = true;
                    oldMousePoint = e.Location;
                    mouseMoveStep = 0;
                    System.Windows.Forms.Cursor.Hide();
                    NewRegion(e.Location, 1);      //zoom to original
                }
                if (e.Button == MouseButtons.Right)
                {

                    this.Capture = true;
                    oldMousePoint = e.Location;
                    mouseMoveStep = 0;
                    System.Windows.Forms.Cursor.Hide();
                    NewRegion(e.Location, 2);     //zoom to 2X
                }

            }

            //copy to favor directory
            if (e.Button == MouseButtons.Middle)
            {
                string fileName = GetFileNameWithoutPath();
                if (!System.IO.File.Exists(copyPath + "\\" + fileName))
                {
                    System.IO.File.Copy(fileList[currentIndex], copyPath + "\\" + fileName);
                    label2.Text = (++copyCount).ToString() + " files copied";
                }
            }
        }
        /// <summary>
        /// when zoom or drag, get rectangles of form and image
        /// </summary>
        /// <param name="mousePoint">mouse click point</param>
        /// <param name="zoom">zoom</param>
        private void NewRegion(Point mousePoint, int zoom)
        {
            //注意在代码30行加入以下内容
            //用于记录初始载入图片时图片的数据信息
            // private int FirstHeight;      //第一次载入图片的高度
            // private int FirstWidth;       //第一次载入图片的宽度

            //FirstHeight以及FirstWidth指的是一开始载入图片的大小；ratio为一开始的放缩比例，如果不放缩设置此比例为1
            //将图片除以ratio，还原出原图像的尺寸，再乘以zoom（放大比率）为鼠标左击或右击时图片的大小
            //修改的思路为坐标化，开始记录一开始呈现在屏幕上的图像四个点的坐标，
            //在计算出鼠标点击之后，点击点到图像四条边的距离（下面有图说明 Z Y S X （左 右 上 下））
            //通过将Z Y S X乘以zoom再除以ratio，得到图像按照点击点延拓的长度，计算出新的（放大的）图像坐标
            //坐标原点设置为左上角，向右为X正方向，向下为Y正方向
            //通过判断放大图像的坐标，可以求得图像那一部分在显示窗口区域中得到两个矩形的尺寸及坐标
            double ratio = 1;
            double zoom_TRUE = zoom;
            if (imageWidth <= this.Width && imageHeight <= this.Height)
            {
                FirstHeight = imageHeight;
                FirstWidth = imageWidth;
            }
            else
            {
                ratio = Math.Min((double)this.Height / imageHeight, (double)this.Width / imageWidth);
                FirstHeight = (int)(imageHeight * ratio);
                FirstWidth = (int)(imageWidth * ratio);
                zoom_TRUE = zoom * ratio;
            }
            //规定XY后的 zs-左上 zx-左下 ys-右上 yx-右下
            double Xzs = formWidth / 2 - FirstWidth / 2;
            double Xzx = formWidth / 2 - FirstWidth / 2;
            double Xys = formWidth / 2 + FirstWidth / 2;
            double Xyx = formWidth / 2 + FirstWidth / 2;
            double Yzs = formHeight / 2 - FirstHeight / 2;
            double Yzx = formHeight / 2 + FirstHeight / 2;
            double Yys = formHeight / 2 - FirstHeight / 2;
            double Yyx = formHeight / 2 + FirstHeight / 2;
            if (mousePoint.X <= Xys && mousePoint.X >= Xzs && mousePoint.Y >= Yzs && mousePoint.Y <= Yzx)
            //---------------------------------
            //|          S|                    |
            //|     Z     |         Y          |                            
            //|--------------------------------|
            //|           |                    |
            //|          X|                    |
            //|           |                    |
            //|           |                    |
            //|           |                    |
            //----------------------------------
            // 在计算出鼠标点击之后，点击点到图像四条边的距离（上面有图说明 Z Y S X （左 右 上 下））
            {
                double S = mousePoint.Y - Yzs;
                double X = Yzx - mousePoint.Y;
                double Z = mousePoint.X - Xzs;
                double Y = Xyx - mousePoint.X;

                //通过将Z Y S X乘以zoom再除以ratio，得到图像按照点击点延拓的长度，计算出新的（放大的）图像坐标
                double S_Xzs = mousePoint.X - zoom * Z / ratio;
                double S_Xzx = mousePoint.X - zoom * Z / ratio;
                double S_Xys = mousePoint.X + zoom * Y / ratio;
                double S_Xyx = mousePoint.X + zoom * Y / ratio;
                double S_Yzs = mousePoint.Y - zoom * S / ratio;
                double S_Yzx = mousePoint.Y + zoom * X / ratio;
                double S_Yys = mousePoint.Y - zoom * S / ratio;
                double S_Yyx = mousePoint.Y + zoom * X / ratio;

                //计算出窗口的坐标，长度
                double formX = (S_Xzs < 0 ? 0 : S_Xzs);
                double formY = (S_Yzs < 0 ? 0 : S_Yzs);
                double S_formWidth = (S_Xys > this.formWidth ? this.formWidth : S_Xys);
                double S_formHeigth = (S_Yyx > this.formHeight ? this.formHeight : S_Yyx);
                formRect = new Rectangle((int)formX, (int)formY, (int)S_formWidth, (int)S_formHeigth);

                //计算出图像的坐标长度
                double deltX = (S_Xzs < 0 ? -S_Xzs : 0);
                double deltY = (S_Yzs < 0 ? -S_Yzs : 0);
                double imageW = (S_Xys > this.formWidth ? this.formWidth : S_Xys);
                double imageH = (S_Yyx > this.formHeight ? this.formHeight : S_Yyx);
                imageRect = new Rectangle((int)(deltX / zoom), (int)(deltY / zoom), (int)(imageW / zoom), (int)(imageH / zoom));
                Invalidate(formRect);
            }
            else { return; }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (similar)
                return;
            if (start && reduce == false)
            {
                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
                {
                    this.Capture = false;
                    System.Windows.Forms.Cursor.Show();
                    ShowFull();
                }
            }
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (similar)
                return;
            int zoom = 1;
            if (e.Button == MouseButtons.Right)
            { zoom = 2; }
            if (start && this.Capture)
            {
                mouseMoveStep++;
                if (mouseMoveStep <= 2)
                {
                    return;
                }
                else
                {
                    mouseMoveStep = 0;
                    var deltX = (e.Location.X - oldMousePoint.X) * 7 / zoom / zoom;
                    var deltY = (e.Location.Y - oldMousePoint.Y) * 7 / zoom / zoom;
                    int imageX = imageRect.X;
                    int imageY = imageRect.Y;
                    /*EXPLANATION
                    If you want the picture to move as if you drag it with the
                    mouse, the image rectangle(ImageRect) should move in 
                    the opposite direction.
                    DO NOT UNDERSTAND? You can think in this way: now 
                    suppose the position of image rectangle(ImageRect) is
                    fixed and you drag the picture back and forth to show
                    different part of it. For example if you drag the picture
                    to your left, then the right part is displayed. Now the 
                    picture is moving in the same direction as your mouse.
                    But in fact the picture is fixed. It is the image 
                    rectangle(ImageRect) that can move so it has to move 
                    in exactly the opposite direction to achieve the same 
                    effect.                      
                     */

                    if (deltX > 0)//mouse right
                    {
                        imageX = (imageRect.X - deltX < 0 ? 0 : imageRect.X - deltX);
                    }
                    else//mouse left
                    {
                        imageX =
                            ((imageRect.X - deltX + imageRect.Width) > imageWidth ?
                            imageWidth - imageRect.Width : imageRect.X - deltX);
                    }
                    if (deltY > 0)//mouse down
                    {
                        imageY = (imageRect.Y - deltY < 0 ? 0 : imageRect.Y - deltY);
                    }
                    else//mouse up
                    {
                        imageY = ((imageRect.Y - deltY + imageRect.Height) >= imageHeight ?
                            imageHeight - imageRect.Height : imageRect.Y - deltY);
                    }
                    imageRect = new Rectangle(imageX, imageY, imageRect.Width, imageRect.Height);
                    oldMousePoint = e.Location;
                    Invalidate(formRect);
                }
            }
        }
        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = imagePath;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                imagePath = fbd.SelectedPath;
            }

            start = LoadImageFiles();
            if (start && reduce == false)
            {
                LoadImage();
            }
            else
            {
                showpage();
            }
        }
        private void toolStripButtonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void toolStripButtonRotate_Click(object sender, EventArgs e)
        {
            if (bitmap != null)
            {
                bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                imageWidth = bitmap.Width;
                imageHeight = bitmap.Height;
                ShowFull();
            }
        }
        /// <summary>
        /// cut the white edge below the toolstrip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStrip1_Paint(object sender, PaintEventArgs e)
        {
            if ((sender as ToolStrip).RenderMode == ToolStripRenderMode.System)
            {
                Rectangle rect = new Rectangle(0, 0, this.toolStrip1.Width, this.toolStrip1.Height - 2);
                e.Graphics.SetClip(rect);
            }

        }
        private void ChangeStauts()
        {
            if (reduce == true)
            {
                toolStrip1.Items[0].Enabled = true;
                toolStrip1.Items[1].Enabled = false;
                toolStrip1.Items[2].Enabled = false;
                toolStrip1.Items[3].Enabled = false;
                toolStrip1.Items[4].Enabled = false;
                toolStrip1.Items[5].Enabled = true;
                toolStrip1.Items[6].Enabled = false;
                toolStrip1.Items[7].Enabled = false;
                toolStrip1.Items[8].Enabled = true;
                toolStrip1.Items[9].Enabled = false;
                if (page >= 1 && start)
                {
                    if (currentpage == 0)
                    {
                        toolStrip1.Items[1].Enabled = false;
                        toolStrip1.Items[2].Enabled = true;
                        return;
                    }
                    if (currentpage == page)
                    {
                        toolStrip1.Items[2].Enabled = false;
                        toolStrip1.Items[1].Enabled = true;
                        return;
                    }
                    toolStrip1.Items[1].Enabled = true;
                    toolStrip1.Items[2].Enabled = true;
                }

            }
            else
            {
                toolStrip1.Items[0].Enabled = true;
                toolStrip1.Items[1].Enabled = true;
                toolStrip1.Items[2].Enabled = true;
                toolStrip1.Items[3].Enabled = true;
                toolStrip1.Items[4].Enabled = true;
                toolStrip1.Items[5].Enabled = true;
                toolStrip1.Items[6].Enabled = true;
                toolStrip1.Items[7].Enabled = true;
                toolStrip1.Items[8].Enabled = true;
                toolStrip1.Items[9].Enabled = true;
                if (fileCount < 1 || !start)
                {
                    toolStrip1.Items[1].Enabled = false;
                    toolStrip1.Items[2].Enabled = false;
                }
                else
                {
                    if (currentIndex == 0)
                    {
                        toolStrip1.Items[1].Enabled = false;
                        toolStrip1.Items[2].Enabled = true;
                        return;
                    }
                    if (currentIndex == fileCount)
                    {
                        toolStrip1.Items[2].Enabled = false;
                        toolStrip1.Items[1].Enabled = true;
                        return;
                    }
                    toolStrip1.Items[1].Enabled = true;
                    toolStrip1.Items[2].Enabled = true;
                }
            }
        }
        private void toolStripButtonBack_Click(object sender, EventArgs e)
        {
            if (reduce == false)
            {
                if (start)
                {
                    currentIndex = currentIndex - 1;
                    //ChangeStauts();
                    //currentIndex = (currentIndex < 0 ? 0 : currentIndex );
                    LoadImage();
                }
            }
            else
            {
                if (start)
                {
                    currentpage = currentpage - 1;
                    //ChangeStauts();
                    //currentIndex = (currentIndex < 0 ? 0 : currentIndex );
                    showpage();
                }
            }
        }
        private void toolStripButtonNext_Click(object sender, EventArgs e)
        {
            if (reduce == false)
            {
                if (start)
                {
                    currentIndex = currentIndex + 1;
                    //ChangeStauts();
                    //currentIndex = (currentIndex < 0 ? 0 : currentIndex );
                    LoadImage();
                }
            }
            else
            {
                if (start)
                {
                    currentpage = currentpage + 1;
                    //ChangeStauts();
                    //currentIndex = (currentIndex < 0 ? 0 : currentIndex );
                    showpage();
                }
            }
        }
        private void toolStripButtonSaveFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = copyPath;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                copyPath = fbd.SelectedPath;
            }

        }
        /// <summary>
        /// <修改>修改了相关删除的BUG如连续删除的乱序，
        /// 删除最后一张时的错误，
        /// 以及删除第一张和最后一张时的前后按钮不变灰的错误
        /// 增添了有关删除到回收站的操作
        /// </summary>
        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            if (fileCount >= 1)
            {
                if (currentIndex == fileCount)
                {
                    int index = currentIndex;
                    currentIndex = 0;
                    LoadImage();
                    //System.IO.File.Delete(fileList[index]);
                    FileSystem.DeleteFile(fileList[index], UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                    ChangeStauts();
                    fileCount--;
                    ChangeStauts();
                }
                else
                {
                    int index = currentIndex;
                    currentIndex = currentIndex + 1;
                    currentIndex = (currentIndex > fileCount ? 0 : currentIndex);
                    LoadImage();
                    //System.IO.File.Delete(fileList[index]);
                    FileSystem.DeleteFile(fileList[index], UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                    currentIndex = index;
                    ChangeStauts();
                    while (index < fileCount)
                    {
                        fileList[index] = fileList[index + 1];
                        index++;
                    }
                    fileCount--;
                    ChangeStauts();
                }
            }
            else
            {
                int index = currentIndex;
                fileCount--;
                Bitmap bmp = new Bitmap(formWidth, formHeight);
                Graphics g = Graphics.FromImage(bmp);
                SolidBrush b = new SolidBrush(Color.Black);         //这里修改颜色
                if (bitmap != null)
                {
                    bitmap.Dispose();
                }
                g.FillRectangle(b, 0, 0, formWidth, formHeight);
                bitmap = new Bitmap(bmp);
                FileSystem.DeleteFile(fileList[index], UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                ChangeStauts();
                toolStrip1.Items[3].Enabled = false;
                toolStrip1.Items[4].Enabled = false;
                toolStrip1.Items[5].Enabled = false;
                toolStrip1.Items[6].Enabled = false;
                toolStrip1.Items[7].Enabled = false;
                Invalidate();
            }
        }
        private void toolStripButtonLeft_Click(object sender, EventArgs e)
        {
            if (bitmap != null)
            {
                bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                imageWidth = bitmap.Width;
                imageHeight = bitmap.Height;
                ShowFull();
            }
        }
        private void toolStripButtonSaveAs_Click(object sender, EventArgs e)
        {
            if (reduce == false)
            {
                if (bitmap != null)
                {
                    bitmap1 = bitmap;
                    Form2 mySecondForm = new Form2();
                    mySecondForm.Show();
                }
            }
            else
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.AddExtension = true;
                saveFile.CheckFileExists = false;
                saveFile.DefaultExt = "jpg";
                saveFile.Filter = "JPG Files(*.JPG)|*.JPG|All files (*.*)|*.*";
                if (bitmap != null)
                {
                    bitmap.Dispose();
                }
                formHeight = Screen.PrimaryScreen.Bounds.Height;
                formWidth = Screen.PrimaryScreen.Bounds.Width;
                bitmap = new Bitmap(formWidth, formHeight - (int)((double)63 / 1080 * formHeight));
                Graphics g = Graphics.FromImage(bitmap);
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.CopyFromScreen(0, 0, 0, 0, new Size(formWidth, formHeight - (int)((double)63 / 1080 * formHeight)));
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    bitmap.Save(saveFile.FileName);
                }
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 27:
                    Application.Exit();
                    break;
                case 113:
                    label1.Visible = false;
                    label2.Visible = false;
                    label3.Visible = false;
                    break;
                case 114:
                    label1.Visible = true;
                    label2.Visible = true;
                    label3.Visible = true;
                    break;
            }

        }
        private void showpage()
        {
            formRect = new Rectangle(0, 0, formWidth, formHeight);
            Invalidate();
            ChangeStauts();
            i = 0;
            hangju = (int)(5.0 / 1920 * formWidth);
            lieju = (int)(5.0 / 1080 * formHeight);
            for (i = 0; i < hang * lie; i++)
            {

                if (currentpage * hang * lie + i > fileCount)
                {
                    break;
                }
                if (bitmap != null)
                {
                    bitmap.Dispose();
                }
                img = Image.FromFile(fileList[currentpage * hang * lie + i]);
                bitmap = new Bitmap(img, (formWidth - (lie + 1) * lieju) / lie, (formHeight - (int)(50.0 / 1080 * formHeight) - (hang + 1) * hangju) / hang);
                img.Dispose();
                imageWidth = bitmap.Width;
                imageHeight = bitmap.Height;
                imageRect = new Rectangle(0, 0, imageWidth, imageHeight);
                formRect = new Rectangle(
                                         lieju + (i % lie) * (formWidth - lieju) / lie,
                                         hangju + (i / lie) * ((formHeight - (int)(50.0 / 1080 * formHeight)) - hangju) / hang,
                                         (formWidth - (lie + 1) * lieju) / lie,
                                         (formHeight - (int)(50.0 / 1080 * formHeight) - (hang + 1) * hangju) / hang
                                         );
                Invalidate(formRect);
                Update();
            }
        }
        private void showreduce()
        {
            reduce = (reduce == true) ? false : true;
            ChangeStauts();
            if (reduce == false)
            {
                if (start)
                {
                    LoadImage();
                    label1.Visible = true;
                    label2.Visible = true;
                    label3.Visible = true;
                    toolStrip1.ShowItemToolTips = true;
                }
            }
            else
            {
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                toolStrip1.ShowItemToolTips = false;
                showpage();
            }

        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if(start)
            showreduce();
        }
        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (similar == false)
            {
                if (start && reduce == true)
                {
                    if (((e.X - lieju) / ((formWidth - lieju) / lie)) + ((e.Y - hangju) / ((formHeight - (int)(50.0 / 1080 * formHeight)) / hang)) * lie + currentpage * hang * lie <= fileCount)
                    {
                        currentIndex = ((e.X - lieju) / ((formWidth - lieju) / lie)) +
                            ((e.Y - hangju) / ((formHeight - (int)(50.0 / 1080 * formHeight)) / hang)) * lie + currentpage * hang * lie;
                        reduce = false;
                        ChangeStauts();
                        LoadImage();
                    }
                }
            }
            else
            {
                if(start&&!reduce)
                {
                   if (((e.X - lieju) / ((formWidth - lieju) / lie)) +
                       ((e.Y - hangju) / ((formHeight - (int)(50.0 / 1080 * formHeight)) / hang)) * lie<= similarcount)
                    {
                        currentIndex = newfolder[((e.X - lieju) / ((formWidth - lieju) / lie)) +
                             ((e.Y - hangju) / ((formHeight - (int)(50.0 / 1080 * formHeight)) / hang)) * lie];
                        similar = false;
                        ChangeStauts();
                        LoadImage();
                    }
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            fileList = System.IO.Directory.GetFiles(imagePath, "*.jpg", System.IO.SearchOption.AllDirectories);
            fileCount = fileList.GetUpperBound(0);
            page = fileCount / (hang * lie);
        }
        private void ChangeStauts1()
        {
            if (similar)
            {
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                toolStrip1.Items[0].Enabled = false;
                toolStrip1.Items[1].Enabled = false;
                toolStrip1.Items[2].Enabled = false;
                toolStrip1.Items[3].Enabled = false;
                toolStrip1.Items[4].Enabled = false;
                toolStrip1.Items[5].Enabled = false;
                toolStrip1.Items[6].Enabled = false;
                toolStrip1.Items[7].Enabled = false;
                toolStrip1.Items[8].Enabled = false;
            }
            else
            {
                ChangeStauts();
            }
        }
        void showsimi()
        {
            similar = (similar == true) ? false : true;
            if (similar)
            {
                showsimilar();
            }
            else
            {
                LoadImage();
            }

        }
        private void showsimilar()
        {   
            long origin;
            origin = GetHisogram(myResize(fileList[currentIndex]));
            similarcount = 0;
            long current;
            for (int i = 0; i <= fileCount; i++)
            {
                if (i == currentIndex)
                    continue;

                current = Math.Abs( GetHisogram(myResize(fileList[i])));
                
                current =hamming(origin, current);

                
                if (current<10)
                {
                    newfolder[similarcount] = i;
                    similarcount++;
                }
                if (similarcount == 25)
                    break;
            }
            if (similarcount > 0)
            {
                formRect = new Rectangle(0, 0, formWidth, formHeight);
                Invalidate();
                ChangeStauts1();
                hangju = (int)(5 / 1920 * formWidth);
                lieju = (int)(5 / 1080 * formHeight);
                for (int i = 0; i < similarcount; i++)
                {
                    bitmap = new Bitmap(fileList[newfolder[i]]);
                    imageWidth = bitmap.Width;
                    imageHeight = bitmap.Height;
                    imageRect = new Rectangle(0, 0, imageWidth, imageHeight);
                    formRect = new Rectangle(
                                             lieju + (i % lie) * (formWidth - lieju) / lie,
                                             hangju + (i / lie) * ((formHeight - (int)(50.0 / 1080 * formHeight)) - hangju) / hang,
                                             (formWidth - (lie + 1) * lieju) / lie,
                                             (formHeight - (int)(50.0 / 1080 * formHeight) - (hang + 1) * hangju) / hang
                                             );
                    Invalidate(formRect);
                    Update();
                }
            }
            else
            {
                MessageBox.Show("没有相似图！！");    //弹出窗口          
                similar = false;
                LoadImage();
            }

        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if(start)
            showsimi();
        }
        //步骤1， 将图像转化成相同大小，我们暂且转化成256 X 256吧。
        public Bitmap myResize(string imageFile)
        {
            img = Image.FromFile(imageFile);
            Bitmap imgOutput = new Bitmap(img, 8, 8);
            
            //imgOutput.Dispose();
            img.Dispose();

            return imgOutput;

        }
        //        这部分代码很好懂，imageFile为原始图片的完整路径，
        //newImageFile为强转大小后的256 X 256图片的路径，
        //为了“赛”后可以看到我们转化出来的图片长啥样，
        //所以我就把它保存到了本地了，以至于有了上面略显丑陋的代码。

        //步骤2，计算图像的直方图
        public long GetHisogram(Bitmap img)
        {
            var data = new long[64];
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    Color c = img.GetPixel(x, y);
                    var l = c.R * 299 / 1000 + c.G * 587 / 1000 + c.B * 114 / 1000;
                    data[y * 8 + x] = l;

                }
            }
            var avg = data.Sum() / 64;      
            return data.Select((i, y) => new
            { y, z = i < avg ? 0 : 1 }).Aggregate(0L, (x, a) => x | ((long)a.z << a.y));

        }
        public long hamming(long h1, long h2)
        {
            long h = 0, d =  h1 ^ h2;
            
            if (d < 0)
                d = -d;
            while (d> 0)
            {
                h ++;
               
                d &=d-1;
                if (h >= 20)
                    break;
            }
            return h;
        }
    }
}


