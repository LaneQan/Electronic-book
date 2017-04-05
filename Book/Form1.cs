using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Book
{
    public partial class Form1 : MetroForm
    {
       
        string section = "";
        string topic = "";
        string subtopic = "";
        string dir = System.AppDomain.CurrentDomain.BaseDirectory;
        public Form1()
        {
            InitializeComponent();
            Gnostice.Documents.Framework.ActivateLicense("6B80-A0CD-33FC-F946-4C94-5977-0C48-D844-954F-AA31-8B04-6CC4");
        }

        private void LoadPanels()
        {
            this.Size = new Size(1006, 634);
            Size size = new Size(1005, 595);
            Point point = new Point(5, 30);
            for (int i = 1; i < 9; i++)
            {
                this.Controls["panel" + i].Location = point;
                this.Controls["panel" + i].Size = size;
            }


                VisiblePanel("panel1");
        }

        private void VisiblePanel(string panelName)
        {
            if (panelName=="panel5")
            {
                documentViewer1.Focus();
            }
            string[] panels = new string[] { "panel1", "panel2", "panel3", "panel4", "panel5", "panel6", "panel7", "panel8" };
            for (int i = 0; i < panels.Length; i++)
                this.Controls[panels[i]].Visible = (panels[i] == panelName);
            if (section == "1") linkLabel2.Visible = false;
            else linkLabel2.Visible = true;


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            VisiblePanel("panel2");
        }

        public void RemoveUnderline(LinkLabel link)
        {
            link.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
        }

        public Image SetImageOpacity(Image image, float opacity)
        {
            Bitmap bmp = new Bitmap(image.Width, image.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                ColorMatrix matrix = new ColorMatrix();
                matrix.Matrix33 = opacity;
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default,
                                                  ColorAdjustType.Bitmap);
                g.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height),
                                   0, 0, image.Width, image.Height,
                                   GraphicsUnit.Pixel, attributes);
            }
            return bmp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Transparent;
            Image backImage = Image.FromFile(dir+@"img\background.jpg");
            panel1.BackgroundImage = SetImageOpacity(backImage, 0.17F);
            LoadPanels();
            foreach (LinkLabel lb in panel1.Controls.OfType<LinkLabel>())
            {
                lb.LinkBehavior = LinkBehavior.NeverUnderline;
            }
            foreach (LinkLabel lb in panel2.Controls.OfType<LinkLabel>())
            {
                lb.LinkBehavior = LinkBehavior.NeverUnderline;
            }
            foreach (LinkLabel lb in panel3.Controls.OfType<LinkLabel>())
            {
                lb.LinkBehavior = LinkBehavior.NeverUnderline;
            }
            foreach (LinkLabel lb in panel4.Controls.OfType<LinkLabel>())
            {
                lb.LinkBehavior = LinkBehavior.NeverUnderline;
            }
            foreach (LinkLabel lb in panel6.Controls.OfType<LinkLabel>())
            {
                lb.LinkBehavior = LinkBehavior.NeverUnderline;
            }
            foreach (LinkLabel lb in panel8.Controls.OfType<LinkLabel>())
            {
                lb.LinkBehavior = LinkBehavior.NeverUnderline;
            }
            foreach (LinkLabel lb in metroTabPage1.Controls.OfType<LinkLabel>())
            {
                lb.LinkBehavior = LinkBehavior.NeverUnderline;
            }
            foreach (LinkLabel lb in metroTabPage2.Controls.OfType<LinkLabel>())
            {
                lb.LinkBehavior = LinkBehavior.NeverUnderline;
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            VisiblePanel("panel1");
        }

        private void onClick(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i=1;i<7;i++)
                panel3.Controls["l_" + i.ToString()].Text = "";
            string linkLabelName = ((LinkLabel)sender).Name;
            section = linkLabelName[1].ToString();
            
            List<string> list = new List<string>();

            using (FileStream file = new FileStream(dir+@"\txt\" + section +@"\"+section+".txt", FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(file, Encoding.Default))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Length > 75)
                        {
                            StringBuilder sb = new StringBuilder(line);
                            sb[48] = '\n';
                            line = sb.ToString();
                        }
                        list.Add(line); 
                    }
                }
            }

            int k = 1;
            foreach (string p in list)
            {
                panel3.Controls["l_" + k.ToString()].Text = p;
                    k++;
            }
            VisiblePanel("panel3");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            VisiblePanel("panel2");
        }

        private void onClick2(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 1; i < 5; i++)
                panel4.Controls["l__" + i.ToString()].Text = "";
            string linkLabelName = ((LinkLabel)sender).Name;
            topic = Convert.ToString((int)linkLabelName[2] - 1 - '0');
            List<string> list = new List<string>();

            using (FileStream file = new FileStream(dir + @"\txt\" + section +   @"\"+ section+"."+topic + @"\" + section + "." + topic+ ".txt", FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(file, Encoding.Default))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if ((topic == "2" && section == "1"))
                        {
                            if (line.Length > 79)
                            {
                                StringBuilder sb = new StringBuilder(line);
                                sb[56] = '\n';
                                line = sb.ToString();
                            }
                        }
                        if ((topic == "1" && section == "2"))
                        {
                            if (line.Length > 79)
                            {
                                StringBuilder sb = new StringBuilder(line);
                                sb[55] = '\n';
                                line = sb.ToString();
                            }
                        }
                        if (topic=="3" && section=="2")
                        {
                            StringBuilder sb = new StringBuilder(line);
                            sb[53] = '\n';
                            line = sb.ToString();
                        }
                        if (topic == "3" && section == "3" && line.Length>60 && line[4]=='3')
                        {
                            StringBuilder sb = new StringBuilder(line);
                            sb[64] = '\n';
                            line = sb.ToString();
                        }
                        list.Add(line);
                    }
                }
            }

            int k = 1;
            foreach (string p in list)
            {
                panel4.Controls["l__" + k.ToString()].Text = p;
                k++;
            }
            VisiblePanel("panel4");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            VisiblePanel("panel3");
        }

        private void onClick3(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string linkLabelName = ((LinkLabel)sender).Name;
            subtopic = linkLabelName[3].ToString();
            this.button4.Click += new System.EventHandler(toPanel4);
            documentViewer1.LoadDocument(dir + @"/txt/" + section + @"/" + section + "." + topic + @"/" + section + "." + topic + "." + subtopic + ".docx");
            VisiblePanel("panel5");
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            VisiblePanel("panel4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            VisiblePanel("panel2");
        }

        private void onKurs(object sender, LinkLabelLinkClickedEventArgs e)
        {
            VisiblePanel("panel6");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.button4.Click += new System.EventHandler(toPanel6);
            documentViewer1.LoadDocument(dir + @"/txt/Курсовое проектирование/Рекомендации.docx");
            VisiblePanel("panel5");
        }
        private void toPanel6(object sender, EventArgs e)
        {
            VisiblePanel("panel6");
        }
        private void toPanel4(object sender, EventArgs e)
        {
            VisiblePanel("panel4");
        }
        private void toPanel8(object sender, EventArgs e)
        {
            VisiblePanel("panel8");
        }
        private void toPanel7(object sender, EventArgs e)
        {
            VisiblePanel("panel7");
        }
        private void toPanel3(object sender, EventArgs e)
        {
            VisiblePanel("panel3");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            VisiblePanel("panel6");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            VisiblePanel("panel7");
        }

        private void topicClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.button4.Click += new System.EventHandler(toPanel7);
            documentViewer1.LoadDocument(dir + @"/txt/Курсовое проектирование/Темы/" + ((LinkLabel)sender).Name[1]+".docx");
            VisiblePanel("panel5");
        }

        private void l_10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.button4.Click += new System.EventHandler(toPanel3);
            documentViewer1.LoadDocument(dir + @"/txt/" + section + "/Контроль знаний/Вопросы.docx");
            VisiblePanel("panel5");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            VisiblePanel("panel4");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 1; i < 7; i++)
                panel8.Controls["pr" + i.ToString()].Text = "";

            List<string> list = new List<string>();

            using (FileStream file = new FileStream(dir + @"\txt\" + section + @"\" + section +"."+topic+@"\Практические занятия\Практические.txt", FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(file, Encoding.Default))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                       
                        list.Add(line);
                    }
                }
            }

            int k = 1;
            foreach (string p in list)
            {
                panel8.Controls["pr" + k.ToString()].Text = p;
                k++;
            }
            VisiblePanel("panel8");
        }

        private void onPrClick(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string linkLabelName = ((LinkLabel)sender).Name;
            string prNumber = linkLabelName[2].ToString();
            this.button4.Click += new System.EventHandler(toPanel8);
            documentViewer1.LoadDocument(dir + @"\txt\" + section + @"\" + section + "." + topic + @"\Практические занятия\"+prNumber+".docx");
            VisiblePanel("panel5");
        }



        private void metroButton1_Click(object sender, EventArgs e)
        {
            VisiblePanel("panel2");
        }

        private void topicClicked(object sender, EventArgs e)
        {
            this.button4.Click += new System.EventHandler(toPanel7);
            documentViewer1.LoadDocument(dir + @"/txt/Курсовое проектирование/Темы/" + ((LinkLabel)sender).Name[1] + ".docx");
            VisiblePanel("panel5");
        }
    }
}
