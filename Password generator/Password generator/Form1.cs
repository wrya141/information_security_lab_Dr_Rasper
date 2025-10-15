using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Password_generator
{
    public partial class Form1 : Form
    {
        //init the pool of chars
        string capital = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string small = "abcdefghijklmnopqrstuvwxyz";
        string numbers = "0123456789";
        string specialChars = "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; 
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //reset the label
            label1.Text = "";
            Random R=new Random();
            //the length of the password
            int numberofchars=0;
            try
            {
                if (textBox1.Text == "")
                { 
                    //if non provided it will be 8
                    numberofchars = 8;
                }else if (Convert.ToInt32(textBox1.Text)<8)
                {
                    MessageBox.Show("length must be at least  8 chars");
                    return;
                }
                else
                {
                    numberofchars = Convert.ToInt32(textBox1.Text);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          //get 100 line of generated  passwords
            for(int z = 0; z < 100; z++)
            {
                //some flags for later check and it resets every iteration
                bool CAPITAL = false, SMALL = false, NUMBERS = false, SPECIALCHARS = false;
                string x = "";
                //for a random number
                int g;
                for(int i = 0; i < numberofchars; i++)
                {
                    //get a random number between 1 and 4
                    g = R.Next(1,5);
                    switch (g)
                    {
                        case 1: x += capital[R.Next(capital.Length)]; break;
                        case 2: x += small[R.Next(small.Length)]; break;
                        case 3: x += numbers[R.Next(numbers.Length)]; break;
                        case 4: x += specialChars[R.Next(specialChars.Length)]; break;
                    }
                }
                //check for every char if the conditions are met 
                for(int i = 0;i < x.Length; i++)
                {
                    if(x[i] >='A'&&  x[i] <= 'Z')
                    {
                        CAPITAL = true;
                    }
                    if(x[i] >='a'&&  x[i] <= 'z')
                    {
                        SMALL=true;
                    }
                    if (x[i]>='0'&& x[i] <= '9')
                    {
                        NUMBERS = true;
                    }
                    if (!Char.IsLetterOrDigit(x[i]))
                    {
                        SPECIALCHARS= true;
                    }
                }
                //shuffle the password
                string password = "";
                while (x.Length > 0)
                {
                    int y=R.Next(x.Length);
                    password += x[y];
                    x = x.Remove(y, 1);
                }
                password += '\n';
                //add the password to the label
                label1.Text += password;
                //based on the flags we get the result if all are met then it show a message box
                if (CAPITAL && SMALL && SPECIALCHARS && NUMBERS)
                {
                    MessageBox.Show("the conditions are met for the password " + password + " at location : " + Convert.ToString(z+1));
                }
            }
        }
    }
}
