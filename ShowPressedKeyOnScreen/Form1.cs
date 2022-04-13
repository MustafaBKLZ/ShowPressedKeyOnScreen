using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ShowPressedKeyOnScreen
{

    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label lblKeyPressed;
        private Timer timer1;
        private IContainer components;

        public Form1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblKeyPressed = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblKeyPressed
            // 
            this.lblKeyPressed.BackColor = System.Drawing.SystemColors.Control;
            this.lblKeyPressed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKeyPressed.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold);
            this.lblKeyPressed.Location = new System.Drawing.Point(0, 0);
            this.lblKeyPressed.Name = "lblKeyPressed";
            this.lblKeyPressed.Size = new System.Drawing.Size(465, 135);
            this.lblKeyPressed.TabIndex = 0;
            this.lblKeyPressed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.Green;
            this.ClientSize = new System.Drawing.Size(465, 135);
            this.Controls.Add(this.lblKeyPressed);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Keyboard Key Listener & Shower";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new Form1());
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {

            //Point loc = new Point(Screen.AllScreens[0].WorkingArea.Width - this.Width, Screen.AllScreens[0].WorkingArea.Height - (this.Height+20));
            //this.Location = loc;


            // bugün obs için klavyeden bastýðým týþlarý gösteren bir eklenti
            // vs bir þey aradým ama düzgün birþey bulamadým.
            // bu yüzden daha önce yaptýðým keyboard listener ile
            // mekanik klavye simulatörü uygulamasý vardý
            // az önce gördükleriniz ile bastýðým tuþlarý gösreren bir uyggulama
            // yapmýþ olmamýzdýr. son bir sorunu kaldý. onu da hemen çözelim.
            // gördüðünüz gibi artýk her zaman yukarýda kalacak
            // böylece bastýðýmýz tuþlarý gösteren bir uygulama oldu.
            // ister bu þekiþde kullanýn, ister obs üzerinden ekran alma 
            // kaynaðý ile kullanýn size kalmýþ. Pencere yakalama
            // aslýnda o daha mantýklý. Þimödiden ona çevirelim.s
            // pencere yakalama olmadý. Þuan bunu ayarlamaya bakýyprým.

            // ben þuanda ikici ekrana ekledim ve obs üzerinden ekran yakalama ile alabildim.
            // ama bu yaklaþým mantýklý gelmedi o yüzden vazgeçtim. Þuan ana ekrana aldým ve bir kenarda kalýyor.
            // topmost özelliði aktif olduðu için her zaman en üstte kalacaktýr.
            // herkeste iki monitör olmadýðýný düþünürsek bu yaklaþým daha doðru olacaktýr.            
            
            // tabi uygulamanýn hala tavlanmasý gerekiyor. yani biraz daha üzerinde uðraþýlmasý ve en iyi haline
            // getirilmesi lazým. Ben þimdilik bu þekilde býrakacaðým. Projeyi de githuba'a yükleyeyim ki sizde ulaþabilin a dostlar :D

            // gerisi artýk sizde.... byy

            // Watch for keyboard activity
            KeyboardListener.s_KeyEventHandler += new EventHandler(KeyboardListener_s_KeyEventHandler);

            timer1.Start();
        }

        bool PressingCTRL = false;
        bool PressingALT = false;
        bool PressingSHIFT = false;
        bool PressingKey = false;

        private void KeyboardListener_s_KeyEventHandler(object sender, EventArgs e)
        {
            KeyboardListener.UniversalKeyEventArgs eventArgs = (KeyboardListener.UniversalKeyEventArgs)e;

            if (eventArgs.m_Msg == 256) // key down and while press
            {
                if (eventArgs.KeyData == Keys.ControlKey)
                {
                    lblKeyPressed.Text += " CTRL ";
                    PressingCTRL = true;
                }
                else if (eventArgs.KeyData == Keys.Menu)
                {
                    lblKeyPressed.Text += " ALT ";
                    PressingALT = true;
                }
                else if (eventArgs.KeyData == Keys.ShiftKey)
                {
                    lblKeyPressed.Text += " SHIFT ";
                    PressingSHIFT = true;
                }
                else
                {
                    lblKeyPressed.Text += " " + eventArgs.KeyData.ToString() + " ";
                    PressingKey = true;
                }
            }

            if (eventArgs.m_Msg == 257) // key up
            {
                if (eventArgs.KeyData == Keys.ControlKey)
                {
                    PressingCTRL = false;
                }
                else if (eventArgs.KeyData == Keys.Menu)
                {
                    PressingALT = false;
                }
                else if (eventArgs.KeyData == Keys.ShiftKey)
                {
                    PressingSHIFT = false;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!PressingCTRL && !PressingALT && !PressingSHIFT)
            {
                lblKeyPressed.Text = "";
            }
        }
    }
}
