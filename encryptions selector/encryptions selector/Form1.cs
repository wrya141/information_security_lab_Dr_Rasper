using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace encryptions_selector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string type = comboBox1.Text;



            switch (type) {

                case "Reverse":
                   
                    textBox4.Text = reverse(textBox1.Text);
                    break;

                case "Rail Fence":
                    textBox4.Text= railfence(textBox1.Text,Convert.ToInt32(textBox2.Text));
                    break;

                case "columnar key given":
                    textBox4.Text = columnarkeygiven(textBox1.Text,Convert.ToInt32(textBox2.Text));
                    break;
                case "columnar key number":
                    textBox4.Text= columnarkeynumber(textBox1.Text,textBox2.Text.Replace(" ",""));
                    break;
                case "columnar keyword":
                    textBox4.Text = columnkeyword(textBox1.Text, textBox2.Text.Replace(" ", ""));
                    break;
                case "columnar double":
                    textBox4.Text = columnardouble(textBox1.Text, textBox2.Text, textBox3.Text);
                    break;
                case "row transposition":
                    textBox4.Text = rowstran(textBox1.Text, textBox2.Text);
                    break;
               
            }




        }
        public static string reverse(string str)
        {
            string x="";
            for(int i = str.Length-1; i >= 0; i--)
            {
                x+= str[i];
            }
            return x;
        }
        string railfence(string text, int key)
        {
            if (key == 1) return text;

            string[] row = new string[key];
            int r = 0;

            for (int i = 0; i < text.Length; i++)
            {
                row[r] += text[i];
                r = (r + 1) % key; 
            }

            string result = "";
            for (int i = 0; i < key; i++)
                result += row[i];

            return result;
        }
        string columnarkeygiven(string text,int n)
        {
            string ans = "";
            int rows = text.Length / n + (text.Length % n == 0 ? 0 : 1);
            string[,] col = new string[rows, n];

            // Fill array row by row
            int k = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (k < text.Length)
                        col[i, j] = text[k++].ToString();
                    else
                        col[i, j] = "";
                }
            }

            // Read column by column
            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < rows; i++)
                {
                    if (!string.IsNullOrEmpty(col[i, j]))
                        ans += col[i, j];
                }
            }
            return ans;
        }
        string columnarkeynumber(string text,string n)
        {
            int cols = n.Length;
            int rows = (text.Length + cols - 1) / cols; // ceil division
            int totalCells = rows * cols;

            // Pad remaining cells with 'x'
            if (text.Length < totalCells)
                text = text.PadRight(totalCells, 'x');

            // Fill the grid sequentially (left-to-right)
            char[,] grid = new char[rows, cols];
            int k = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    grid[r, c] = text[k++];
                }
            }

            // Determine column order from key (numeric digits)
            int[] colOrder = new int[cols];
            for (int i = 0; i < cols; i++)
            {
                int digit = n[i] - '0'; // key digit
                colOrder[digit - 1] = i;  // maps smallest digit to column 0, next to column 1, etc.
            }

            // Read columns in key order
            string cipher = "";
            foreach (int c in colOrder)
            {
                for (int r = 0; r < rows; r++)
                    cipher += grid[r, c];
            }


            return cipher;
        }
        string columnkeyword(string text,string n)
        {
            int cols = n.Length;
            int rows = (text.Length + cols - 1) / cols; // ceil division
            int totalCells = rows * cols;

            // Pad with 'x' if needed
            if (text.Length < totalCells)
                text = text.PadRight(totalCells, 'x');

            // Fill grid left-to-right, row by row
            char[,] grid = new char[rows, cols];
            int k = 0;
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    grid[r, c] = text[k++];
                }
            }
            int[] colOrder = new int[cols];
            var keyChars = n.ToCharArray();
            var sorted = keyChars
                .Select((ch, idx) => new { ch, idx })
                .OrderBy(x => x.ch)
                .ToArray();
            for (int i = 0; i < cols; i++)
                colOrder[i] = sorted[i].idx;

            // Read columns in key order
            string cipher = "";
            foreach (int c in colOrder)
            {
                for (int r = 0; r < rows; r++)
                    cipher += grid[r, c];
            }

            return cipher;
        }
        string columnardouble(string text,string key1,string key2)
        {
            string one = columnkeyword(text, key1);
            string two = columnkeyword(one, key2);
            return two;
        }
        string rowstran(string plaintext,string key)
        {

            string text = new string(plaintext.Where(ch => !char.IsWhiteSpace(ch)).ToArray()).ToUpper();

            int cols = key.Length;
            int rows = (text.Length + cols - 1) / cols;
            int total = rows * cols;

            // pad with X to fill the grid
            if (text.Length < total) text = text.PadRight(total, 'X');

            // fill grid row-wise
            char[,] grid = new char[rows, cols];
            int k = 0;
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    grid[r, c] = text[k++];

             int[] keyDigits = key.Select(ch => ch - '0').ToArray();
            int[] order = new int[cols];
            for (int want = 1; want <= cols; want++)
                order[want - 1] = Array.IndexOf(keyDigits, want);

            var sb = new StringBuilder(total);
            for (int r = 0; r < rows; r++)
                for (int oi = 0; oi < cols; oi++)
                    sb.Append(grid[r, order[oi]]);

            return sb.ToString();

        }
        
          
     
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = comboBox1.Text;

            textBox2.Enabled = true;
            textBox3.Enabled = true;
            if (type == "Reverse")
            {
                textBox2.Enabled=false; 
                textBox3.Enabled=false;
            }else if(type == "Rail Fence")
            {
                textBox3.Enabled=false;
            }else if(type == "columnar key given")
            {
                textBox3.Enabled = false;
            }else if( type == "columnar key number")
            {
                textBox3.Enabled = false;
            }else if (type == "columnar keyword"){
                textBox3.Enabled=false;
            }else if(type== "row transposition")
            {
                textBox3.Enabled = false;
            }
        }

    }
}
