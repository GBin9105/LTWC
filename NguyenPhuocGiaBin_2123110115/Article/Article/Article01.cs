using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Article01
{
    public partial class Article01 : Form
    {
        // File XML nằm cùng thư mục exe
        private string path = Application.StartupPath + @"\form.xml";

        public Article01()
        {
            InitializeComponent();
        }

        // GHI XML
        private void Write(InfoWindows iw)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(InfoWindows));
            using (StreamWriter writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, iw);
            }
        }

        // ĐỌC XML
        private InfoWindows Read()
        {
            if (!File.Exists(path))
                return null;

            XmlSerializer serializer = new XmlSerializer(typeof(InfoWindows));
            using (StreamReader reader = new StreamReader(path))
            {
                return (InfoWindows)serializer.Deserialize(reader);
            }
        }

        // FORM LOAD → ĐỌC KÍCH THƯỚC CŨ
        private void Article01_Load(object sender, EventArgs e)
        {
            InfoWindows iw = Read();
            if (iw != null)
            {
                this.Width = iw.Width;
                this.Height = iw.Height;
            }
        }

        // RESIZE XONG → LƯU KÍCH THƯỚC MỚI
        private void Article01_ResizeEnd(object sender, EventArgs e)
        {
            InfoWindows iw = new InfoWindows
            {
                Width = this.Width,
                Height = this.Height
            };
            Write(iw);
        }
    }
}
