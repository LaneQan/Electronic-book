using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace Book
{
    public partial class MainForm : MetroForm
    {
        private int _section;
        private readonly string _dir;

        public MainForm()
        {
            InitializeComponent();
            Gnostice.Documents.Framework.ActivateLicense("6B80-A0CD-33FC-F946-4C94-5977-0C48-D844-954F-AA31-8B04-6CC4");
            _dir = AppDomain.CurrentDomain.BaseDirectory;
            _section = 0;
        }

        private void LoadPanels()
        {
            Size = new Size(1006, 634);
            var size = new Size(1005, 595);
            var point = new Point(5, 30);
            for (var i = 1; i < 6; i++)
            {
                Controls["panel" + i].Location = point;
                Controls["panel" + i].Size = size;
            }
            VisiblePanel("panel1");
        }

        private void VisiblePanel(string panelName)
        {
            if (panelName == "panel5")
                documentViewer1.Focus();
            string[] panels = { "panel1", "panel2", "panel3", "panel4", "panel5" };
            foreach (string t in panels)
                Controls[t].Visible = t == panelName;
        }

        public void RemoveUnderline(LinkLabel link)
        {
            link.LinkBehavior = LinkBehavior.NeverUnderline;
        }

        public Image SetImageOpacity(Image image, float opacity)
        {
            var bmp = new Bitmap(image.Width, image.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                var matrix = new ColorMatrix
                {
                    Matrix33 = opacity
                };
                var attributes = new ImageAttributes();
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
            var backImage = Image.FromFile(_dir + @"img\background.jpg");
            panel1.BackgroundImage = SetImageOpacity(backImage, 0.28F);
            LoadPanels();
            var linkLabels = GetAllControls(this, typeof(LinkLabel));
            foreach (var control in linkLabels)
            {
                var k = (LinkLabel)control;
                k.LinkBehavior = LinkBehavior.NeverUnderline;
            }

            var btnToolTip = new System.Windows.Forms.ToolTip();
            var buttons = GetAllControls(this, typeof(Button));
            foreach (var control in buttons)
            {
                var k = (Button)control;
                btnToolTip.SetToolTip(k, control.Name.Contains("content") ? "Содержание" : "Назад");
            }
        }

        public IEnumerable<Control> GetAllControls(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrl => GetAllControls(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VisiblePanel("panel1");
        }

        private void onClick(object sender, LinkLabelLinkClickedEventArgs e)
        {
            VisiblePanel("panel3");
            _section = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            VisiblePanel("panel2");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            VisiblePanel("panel2");
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            VisiblePanel("panel4");
        }

        private void FinalTest_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(_dir + @"src\Итоговый тест\Тест.exe");
            }
            catch (Win32Exception)
            {
                MessageBox.Show(@"Запуск теста отменен!");
                this.Focus();
            }
        }

        private void r2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            VisiblePanel("panel4");
            _section = 2;
        }

        private void onLesClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var linkLabelName = ((LinkLabel)sender).Name;
            documentViewer1.LoadDocument(_dir + @"src\Раздел " + _section + @"\Лекции\Тема_" + linkLabelName[7] + ".docx");
            VisiblePanel("panel5");
            if (_section == 1)
                backButton4.Click += delegate { VisiblePanel("panel3"); };
            else
                backButton4.Click += delegate { VisiblePanel("panel4"); };
        }

        private void onPrClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var linkLabelName = ((LinkLabel)sender).Name;
            var prNumber = linkLabelName.Substring(5, linkLabelName.Length - 5);
            Process.Start(_dir + @"src\Раздел " + _section + @"\Презентации\Презентация_" + prNumber + ".pptx");
        }

        private void toPanel(object sender, EventArgs e, int panelId)
        {
            VisiblePanel("panel" + panelId);
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            VisiblePanel("panel2");
        }

        private void literature_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            backButton4.Click += delegate { toPanel(sender, e, 2); };
            documentViewer1.LoadDocument(_dir + @"/src/Рекомендуемая литература/Литература.docx");
            VisiblePanel("panel5");
        }

        private void goToContent(object sender, EventArgs e)
        {
            VisiblePanel("panel2");
        }

        private void onTestClicked(object sender, EventArgs e)
        {
            var linkLabelName = ((LinkLabel)sender).Name;
            try
            {
                Process.Start(_dir + @"src\Раздел " + _section + @"\Тесты\Тест_" + linkLabelName[4] + ".exe");
            }
            catch (Win32Exception)
            {
                MessageBox.Show(@"Запуск теста отменен!");
                this.Focus();
            }
        }

        private void Introduction_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            backButton4.Click += delegate { VisiblePanel("panel2"); };
            documentViewer1.LoadDocument(_dir + @"src\Введение.docx");
            VisiblePanel("panel5");
        }
    }
}