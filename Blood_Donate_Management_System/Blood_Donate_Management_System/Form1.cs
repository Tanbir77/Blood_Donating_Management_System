using System;
using System.Drawing;

using System.Windows.Forms;

namespace Blood_Donate_Management_System
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        private System.Windows.Forms.Timer timer1;
        private int counter = 0;
        private String [] imagePaths = new string[10];
        public Form1()
        {
            InitializeComponent();
            ShowCoverImages();
        }

        private void ShowCoverImages()
        {
             timer1 = new System.Windows.Forms.Timer();
             timer1.Tick += new EventHandler(timer1_Tick);
             pictureBox1.Load($"Images/BloodDonation{counter++}.jpg");
             pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
             timer1.Interval = 5000; // 5 second
             timer1.Start();
            
            
            
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
           
            if (counter ==12)
            {
                timer1.Stop();
                counter = 0;
                timer1.Start();
                
            }
            try
            {
                pictureBox1.Load($"Images/BloodDonation{counter++}.jpg");
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }catch(Exception ex)
            {
                MessageBox.Show("You delete this image from Images file");
            }
          
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            
        }
        private void menuStrip1_MouseEnter(object sender, EventArgs e)

        {


            toolStripMenuItem1.BackColor = Color.Red;

        }

        private void metroContextMenu1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void sylhetToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            userIdTxtBox.Visible = true;
            passwordTxtBox.Visible = true;
            signInBtn.Visible = true;
            signUpBtn.Visible = true;

        }


        private void signInBtn_Click(object sender, EventArgs e)
        {

        }

        private void signUpBtn_Click_1(object sender, EventArgs e)
        {
            userIdTxtBox.Visible = false;
            passwordTxtBox.Visible = false;
            signInBtn.Visible = false;
            signUpBtn.Visible = false;

            firstNametxtBox.Visible = true;
            surNametxtBox.Visible = true;
            mobileNumberTxtBox.Visible = true;
            newPasswordTxtBox.Visible = true;
        }

    }




}

