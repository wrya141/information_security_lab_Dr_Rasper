using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pin_generator
{
    public partial class Form1 : Form
    {
        string[] appeared = new string[100];
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //number of lines to output
            int n = 100;
            //start and the end of the digitis  
            int start = 1, end = start + 5;
            //empty the label 
            label1.Text = "";
            //to store the digit that we will add to the line 
            int y;
            //get and eqution based on the textbox input
            if (textBox1.Text == "1")
            {
                //the number of rows
                for (int i = 0; i < n; i++)
                {
                    //empty line to add for each iteration
                    string line = "";
                    //get the digits from start to end each iteration 
                    for (int x = start; x <= end; x++)
                    {
                        //equation was provided if the textbox was 1 we use y=x^2
                        y = x * x;
                        //get the digit in ones place
                        y %= 10;
                        //add it to the line 
                        line += Convert.ToString(y);

                    }
                    //insert the line to an array so later we can compare it to a sequence and how many times it appeared 
                    appeared[i] = line;
                    //add a new line to the string 
                    line += "\n";
                    //update both start and the end 
                    start++; end++;
                    //add the line to the label
                    label1.Text += line;
                }


            }
            else if (textBox1.Text == "2")
            {
                //the number of rows
                for (int i = 0; i < n; i++)
                {
                    //empty line to add for each iteration
                    string line = "";
                    //get the digits from start to end each iteration 
                    for (int x = start; x <= end; x++)
                    {
                        //equation was provided if the textbox was 1 we use y=2 * x^2
                        y = 2 * (x * x);
                        //get the digit in ones place
                        y %= 10;
                        //add it to the line 
                        line += Convert.ToString(y);

                    }
                    //insert the line to an array so later we can compare it to a sequence and how many times it appeared 
                    appeared[i] = line;
                    //add a new line to the string 
                    line += "\n";
                    //update both start and the end 
                    start++; end++;
                    //add the line to the label
                    label1.Text += line;
                }
            }
            else if (textBox1.Text == "3")
            {
                //the number of rows
                for (int i = 0; i < n; i++)
                {
                    //empty line to add for each iteration
                    string line = "";
                    //get the digits from start to end each iteration 
                    for (int x = start; x <= end; x++)
                    {
                        //equation was provided if the textbox was 1 we use y=2 * x^2
                        y = 3 * (x * x * x) + 2 * (x * x) + x;
                        //get the digit in ones place
                        y %= 10;
                        //add it to the line 
                        line += Convert.ToString(y);

                    }
                    //insert the line to an array so later we can compare it to a sequence and how many times it appeared 
                    appeared[i] = line;
                    //add a new line to the string 
                    line += "\n";
                    //update both start and the end 
                    start++; end++;
                    //add the line to the label
                    label1.Text += line;
                }

            }else
            {//if we don't get the right number don't do anything
                return;
            }
                //showing the numbers of reappernce by comparing the first output with the rest of the lines or elements
                for (int i = 1; i < appeared.Length; i++)
                {
                    if (appeared[0] == appeared[i])
                    {
                        MessageBox.Show("the sequnce reappeared at " + i);
                    }
                }
        }
    }
}
