using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppSearch
{
    public partial class Form1 : Form
    {
        int V;
        DataTable dtMatrix;
        public Form1()
        {
            InitializeComponent();
        }

        void SetGraph()
        {
            radioButton1.Checked = true;
            textBox1.Text = "0";
            radioButton2.Checked = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Text = "";
        }

        void ReadMatrixArr(string filename)
        {
            dtMatrix = new DataTable();
            string[] lines = File.ReadAllLines(filename);
            int flag = 0;
            foreach (string s in lines)
            {
                if (flag == 0)
                    V = Convert.ToInt16(s);
                else
                {
                    string[] st = s.Split('\t');
                    if (dtMatrix.Columns.Count == 0)
                        for (int i = 0; i < st.Length; i++)
                            dtMatrix.Columns.Add(i.ToString());
                    dtMatrix.Rows.Add(st);
                }
                flag = 1;
            }
            dataGridView1.DataSource = dtMatrix;
        }

        int Dequeue(int[] Queue, int countQ)
        {
            for (int i = 0; i <= countQ; i++)
            {
                Queue[i] = Queue[i + 1];
            }
            countQ--;
            return countQ;
        }

        string BFSAll(int Start)
        {
            string Result = "";
            int[] Queue = new int[20];
            int[] Close = new int[20];
            int countQ = 0;
            Queue[countQ] = Start;
            Result = Queue[countQ].ToString();
            while (countQ >= 0)
            {
                int i = Queue[0];
                countQ = Dequeue(Queue, countQ);
                for (int j = 0; j < V; j++)
                {
                    if (Convert.ToInt16(dtMatrix.Rows[i][j].ToString()) > 0 && Close[j] != 1)
                    {
                        Result = Result + " --> " + j.ToString();
                        countQ++;
                        Queue[countQ] = j;
                        Close[j] = 1;
                    }
                }
                Close[i] = 1;
            }
            return Result;
        }

        string BFSFromTo(int Start, int End)
        {
            string Result = "";
            int[] Queue = new int[20];
            int[] Close = new int[20];
            int countQ = 0;
            Queue[countQ] = Start;
            Result = Queue[countQ].ToString();
            while (countQ >= 0)
            {
                int i = Queue[0];
                countQ = Dequeue(Queue, countQ);
                for (int j = 0; j < V; j++)
                {
                    if (Convert.ToInt16(dtMatrix.Rows[i][j].ToString()) > 0 && Close[j] != 1)
                    {
                        Result = Result + " --> " + j.ToString();
                        countQ++;
                        Queue[countQ] = j;
                        Close[j] = 1;
                        if (j == End)
                        {
                            return Result;
                        }
                    }
                }
                Close[i] = 1;
            }
            return Result;
        }

        string DFSAll(int Start)
        {
            string Result = "";
            int[] Stack = new int[20];
            int[] Close = new int[20];
            int top = 0;
            Stack[top] = Start;
            Result = Stack[top].ToString();
            while (top >= 0)
            {
                int flag = 0;
                int i = Stack[top];
                Close[i] = 1;
                for (int j = 0; j < V; j++)
                {
                    if (Convert.ToInt16(dtMatrix.Rows[i][j].ToString()) > 0 && (Close[j] != 1))
                    {
                        Result = Result + " --> " + j.ToString();
                        flag = 1;
                        top++;
                        Stack[top] = j;
                        Close[j] = 1;
                        break;
                    }
                }
                if (flag == 0)
                {
                    top--;
                }
            }
            return Result;
        }

        string DFSFromTo(int Start, int End)
        {
            string Result = "";
            int[] Stack = new int[20];
            int[] Close = new int[20];
            int top = 0;
            Stack[top] = Start;
            Result = Stack[top].ToString();
            while (top >= 0)
            {
                int flag = 0;
                int i = Stack[top];
                Close[i] = 1;
                for (int j = 0; j < V; j++)
                {
                    if (Convert.ToInt16(dtMatrix.Rows[i][j].ToString()) > 0 && Close[j] != 1)
                    {
                        Result = Result + " --> " + j.ToString();
                        flag = 1;
                        top++;
                        Stack[top] = j;
                        Close[j] = 1;
                        if (j == End)
                        {
                            return Result;
                        }
                        break;
                    }
                }
                if (flag == 0)
                {
                    top--;
                }
            }
            return Result;
        }

        private void graph1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetGraph();
            label7.Text = "Graph 1";
            pictureBox1.Image = global::AppSearch.Properties.Resources.G1;
            ReadMatrixArr(@"G1.txt");
        }

        private void graph2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetGraph();
            label7.Text = "Graph 2";
            pictureBox1.Image = global::AppSearch.Properties.Resources.G2;
            ReadMatrixArr(@"G2.txt");
        }

        private void graph3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetGraph();
            label7.Text = "Graph 3";
            pictureBox1.Image = global::AppSearch.Properties.Resources.G3;
            ReadMatrixArr(@"G3.txt");
        }

        private void graph4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetGraph();
            label7.Text = "Graph 4";
            pictureBox1.Image = global::AppSearch.Properties.Resources.G4;
            ReadMatrixArr(@"G4.txt");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton2.Checked == true)
            {
                textBox2.Text = "0";
                textBox3.Enabled = true;
                radioButton1.Checked = false;
                textBox1.Enabled = false;
                textBox4.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                label5.Text = "Result BFS All:";
                textBox4.Text = BFSAll(Convert.ToInt16(textBox1.Text));
            }
            else
            {
                label5.Text = "Result BFS from Start to End:";
                textBox4.Text = BFSFromTo(Convert.ToInt16(textBox2.Text), Convert.ToInt16(textBox3.Text));
            }  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                label5.Text = "Result DFS All:";
                textBox4.Text = DFSAll(Convert.ToInt16(textBox1.Text));
            }
            else 
            {
            label5.Text = "Result DFS from Start to End:";
            textBox4.Text = DFSFromTo(Convert.ToInt16(textBox2.Text), Convert.ToInt16(textBox3.Text)); 
            }
        }
    }
}
