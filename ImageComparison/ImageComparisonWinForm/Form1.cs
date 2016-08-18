using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using XnaFan.ImageComparison;

namespace ImageComparisonWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string ConnectionString
        {
            get { return "Data Source=.;Initial Catalog=Test;Persist Security Info=True;User ID=sa;Password=B@z1nga;"; }
        }

        public void PerformShapeDetection()
        {
            if (fileNameTextBox.Text != String.Empty)
            {
                Bitmap img = (Bitmap)Image.FromFile(fileNameTextBox.Text);
                this.pictureBox1.Image = img;
                axWindowsMediaPlayer1.URL = GetFromDataBase(fileNameTextBox.Text);
                axWindowsMediaPlayer1.settings.autoStart = true;
            }
        }

        private string GetFromDataBase(string image1)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(ConnectionString);
                String Query1 = "SELECT * FROM [Test].[dbo].[MyPlay] a, [Test].[dbo].[MyImage] b where a.FileId = b.MyPlayId";
                SqlDataAdapter adapter = new SqlDataAdapter(Query1, ConnectionString);
                DataSet Ds = new DataSet();
                adapter.Fill(Ds, "MyPlay");
                if (Ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("No data Found");
                    return string.Empty;
                }

                byte[] fileData = null;

                for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                {
                    string image2 = (string)Ds.Tables[0].Rows[i]["LocalImage"];

                    Bitmap firstBmp = (Bitmap)Image.FromFile(image1);
                    Bitmap secondBmp = (Bitmap)Image.FromFile(image2);

                    firstBmp.GetDifferenceImage(secondBmp, true)
                        .Save(Assembly.GetExecutingAssembly().Location + "_diff.png");

                    if ((firstBmp.PercentageDifference(secondBmp, 3) * 100) < 50)
                        fileData = (byte[])Ds.Tables[0].Rows[i]["FileData"];
                }

                if (fileData != null)
                    return ConvertByteDataToFile((string)Ds.Tables[0].Rows[0]["FileName"], GetUnCompressedData(fileData));
                else
                    return string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
            return string.Empty;
        }

        private string ConvertByteDataToFile(string targetFileName, byte[] value)
        {
            // ReSharper disable EmptyGeneralCatchClause
            var str = string.Empty;
            try
            {
                try
                {
                    var path = Path.GetTempPath();
                    str = path + "\\" + targetFileName;
                    if (File.Exists(str))
                        File.Delete(str);
                }
                catch (Exception) { }

                var file = (new BinaryWriter(new FileStream(str, FileMode.OpenOrCreate, FileAccess.Write)));
                file.Write(value);
                file.Close();
                return str;
            }
            catch (Exception) { }
            // ReSharper restore EmptyGeneralCatchClause
            return string.Empty;
        }

        private void loadImageButton_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK || result == DialogResult.Yes)
            {
                fileNameTextBox.Text = openFileDialog1.FileName;
            }
        }

        private void fileNameTextBox_TextChanged(object sender, EventArgs e)
        {
            PerformShapeDetection();
        }

        public static byte[] GetUnCompressedData(byte[] value)
        {
            try
            {
                if (value != null)
                    using (var zipInputStream = new ZipInputStream(new MemoryStream(value)))
                    {
                        while ((zipInputStream.GetNextEntry()) != null)
                        {
                            using (var zippedInMemoryStream = new MemoryStream())
                            {
                                var data = new byte[2048];
                                while (true)
                                {
                                    var size = zipInputStream.Read(data, 0, data.Length);
                                    if (size <= 0)
                                        break;

                                    zippedInMemoryStream.Write(data, 0, size);
                                }
                                zippedInMemoryStream.Close();

                                return zippedInMemoryStream.ToArray();
                            }
                        }
                    }
                return null;
            }
            catch (Exception)
            {
                return value;
            }
        }
    }
}
