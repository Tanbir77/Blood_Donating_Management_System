using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;

namespace Blood_Donate_Management_System
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        private System.Windows.Forms.Timer timer1;
        private int counter = 0;
        private String[] imagePaths = new string[10];
        private string gender;
        private int donorInfoPanelCount = 0;
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

            if (counter == 12)
            {
                timer1.Stop();
                counter = 0;
                timer1.Start();

            }
            try
            {
                pictureBox1.Load($"Images/BloodDonation{counter++}.jpg");
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("You delete this image from Images file");
            }


        }
        private void timer1_Tick1(object sender, EventArgs e)
        {
            if (newPasswordTxtBox.Text != "" && confirmPasswordTxtBox.Text != "")
            {
                if (newPasswordTxtBox.Text != confirmPasswordTxtBox.Text)
                    passConfiramtionLabel.Visible = true;
                else
                    passConfiramtionLabel.Visible = false;
            }
            else
                passConfiramtionLabel.Visible = false;

            long number;
            bool flag = Int64.TryParse(mobileNumberTxtBox.Text, out number);

            if (flag == false && mobileNumberTxtBox.Text != "")
                mbNovalidationLabel.Visible = true;
            else
                mbNovalidationLabel.Visible = false;

            if (userNameTxtBox.Text != "")
            {
                using (var context = new BDMSDBEntities())
                {

                    if (context.Users.Any(o => o.userName == userNameTxtBox.Text))
                    {
                        userNameValidityLabel.Visible = true;
                    }
                    else
                        userNameValidityLabel.Visible = false;
                }


            }





        }
        private void signUpComponentsVisibility(bool flag)
        {
            firstNametxtBox.Visible = flag;
            surNametxtBox.Visible = flag;
            userNameTxtBox.Visible = flag;
            confirmPasswordTxtBox.Visible = flag;
            mobileNumberTxtBox.Visible = flag;
            newPasswordTxtBox.Visible = flag;
            districtComboBox.Visible = flag;
            areaTxtBox.Visible = flag;
            birthMonthComboBox.Visible = flag;
            birthDayTxtBox.Visible = flag;
            birthYearTxtBox.Visible = flag;
            bloodGroupComboBox.Visible = flag;
            maleRadioButton.Visible = flag;
            femaleRadioButton.Visible = flag;
            createAccountButton.Visible = flag;
        }






        private void metroButton5_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            signUpComponentsVisibility(false);
            loginUserNameTxtBox.Visible = true;
            passwordTxtBox.Visible = true;
            signInBtn.Visible = true;
            signUpBtn.Visible = true;

        }

        private void signUpBtn_Click_1(object sender, EventArgs e)
        {
            loginUserNameTxtBox.Visible = false;
            passwordTxtBox.Visible = false;
            signInBtn.Visible = false;
            signUpBtn.Visible = false;
            signUpComponentsVisibility(true);
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(timer1_Tick1);
            timer1.Interval = 1; // 1 mili second
            timer1.Start();

        }


        private void createAccountButton_Click(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo("ja-JP");
            string birthDate = birthYearTxtBox.Text + "/" + birthMonthComboBox.Text + "/" + birthDayTxtBox.Text;
            try
            {
                Person newPerson = new Person
                {
                    firstName = firstNametxtBox.Text,
                    surName = surNametxtBox.Text,
                    userName = userNameTxtBox.Text,
                    mobileNo = mobileNumberTxtBox.Text,
                    area = areaTxtBox.Text,
                    bloodGroup = bloodGroupComboBox.Text,
                    dateOfBirth = Convert.ToDateTime(birthDate, culture),
                    sex = this.gender

                };

                User newUser = new User
                {
                    userName = userNameTxtBox.Text,
                    password = newPasswordTxtBox.Text

                };
                Address userAddress = new Address
                {
                    area = areaTxtBox.Text,
                    district = districtComboBox.Text
                };
                using (var context = new BDMSDBEntities())
                {
                    try
                    {
                        context.Addresses.Add(userAddress);
                        context.SaveChanges();
                    }
                    catch (Exception ex) { }
                }
                using (var context = new BDMSDBEntities())
                {

                    context.People.Add(newPerson);
                    context.Users.Add(newUser);
                    context.SaveChanges();
                }
              

            }
            catch (Exception ex)
            {

            }
            finally { }




        }

        private void maleRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.gender = "Male";
        }

        private void femaleRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.gender = "Female";
        }

        private void signInBtn_Click(object sender, EventArgs e)
        {
            using (BDMSDBEntities context = new BDMSDBEntities())
            {
                try
                {

                    string pass = context.Users.FirstOrDefault(x => x.userName == loginUserNameTxtBox.Text).password.ToString();

                    if (pass != passwordTxtBox.Text)
                    {
                        loginErrorMsgLabel.Text = "Password is incorrect!";
                        loginErrorMsgLabel.Visible = true;

                    }
                    else
                        loginErrorMsgLabel.Visible = false;
                }
                catch (Exception ex)
                {
                    loginErrorMsgLabel.Text = "Username is invalid!";
                    loginErrorMsgLabel.Visible = true;
                }
            }
        }

        private void dhakaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var context = new BDMSDBEntities())
            {
               List<string> list = (from user in context.People
                                    where user.Address.district.Equals("Dhaka")
                                    select user.userName).ToList();
                foreach(var v in list)
                {
                    findDonorPanel.Visible = true;
                    initialiseFirstDonorInfoPanel();
                    initialiseDonorInfoPanel();

                }
               
            }
        }

        private void initialiseDonorInfoPanel()
        {

            Panel pnl = new Panel();

            Control c2 = new Control();
            pnl.Location = new Point(8, 37+(donorInfoPanelCount++*118));
            pnl.BorderStyle = panel3.BorderStyle;
            pnl.BackColor = panel3.BackColor;
            pnl.Size = panel3.Size;
            pnl.Visible = true;
            foreach (Control c in panel3.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                    c2 = new TextBox();
                if (c.GetType() == typeof(Button))
                    c2 = new Button();
                if (c.GetType() == typeof(Label))
                    c2 = new Label();
            
                c2.Location = c.Location;
                c2.Size = c.Size;
                c2.Font = c.Font;
                c2.Text = c.Text;
                c2.Name = c.Name;
                pnl.Controls.Add(c2);
                this.findDonorPanel.Controls.Add(pnl);
            }

        }

        private void initialiseFirstDonorInfoPanel()
        {
            if (donorInfoPanelCount == 0)
            {
                donorInfoPanelCount++;
                panel3.Visible = true;
            }
        }
    }
}

