using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Article02
{
    public partial class Article02 : Form
    {
        string path = Application.StartupPath + @"\form.xml";

        public Article02()
        {
            InitializeComponent();
        }

        // GHI XML
        public void Write(InfoWindows iw)
        {
            XmlSerializer writer = new XmlSerializer(typeof(InfoWindows));
            using (StreamWriter file = new StreamWriter(path))
            {
                writer.Serialize(file, iw);
            }
        }

        // ĐỌC XML
        public InfoWindows Read()
        {
            if (!File.Exists(path))
                return null;

            XmlSerializer reader = new XmlSerializer(typeof(InfoWindows));
            using (StreamReader file = new StreamReader(path))
            {
                return (InfoWindows)reader.Deserialize(file);
            }
        }

        // LOAD → KHÔI PHỤC KÍCH THƯỚC
        private void Article02_Load(object sender, EventArgs e)
        {
            InfoWindows iw = Read();
            if (iw != null)
            {
                this.Width = iw.Width;
                this.Height = iw.Height;
            }
        }

        // RESIZE END → LƯU KÍCH THƯỚC
        private void Article02_ResizeEnd(object sender, EventArgs e)
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
