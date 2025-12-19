using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Article03
{
    public partial class Article03 : Form
    {
        string path = Application.StartupPath + @"\form.xml";
        InfoWindows iw = new InfoWindows();

        public Article03()
        {
            InitializeComponent();
        }

        // GHI XML (CHỈ GỌI KHI ĐÓNG FORM)
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

        // LOAD → KHÔI PHỤC SIZE + LOCATION
        private void Article03_Load(object sender, EventArgs e)
        {
            InfoWindows data = Read();
            if (data != null)
            {
                this.Width = data.Width;
                this.Height = data.Height;
                this.Location = data.Location;
            }
        }

        // RESIZE → CẬP NHẬT BIẾN (KHÔNG GHI FILE)
        private void Article03_ResizeEnd(object sender, EventArgs e)
        {
            iw.Width = this.Width;
            iw.Height = this.Height;
        }

        // DI CHUYỂN → CẬP NHẬT BIẾN (KHÔNG GHI FILE)
        private void Article03_LocationChanged(object sender, EventArgs e)
        {
            iw.Location = this.Location;
        }

        // ĐÓNG FORM → GHI XML 1 LẦN
        private void Article03_FormClosing(object sender, FormClosingEventArgs e)
        {
            Write(iw);
        }
    }
}
