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
using Class1;

namespace ripasso_estate1
{
    public partial class Form1 : Form
    {
        public string path = @"../../Sciusco.csv";
        public int l = 0;
        public int f = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button3_Click(sender, e);
            button4_Click(sender, e);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Valore(f, path);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            contacampi(f, path);

            int nc = contacampi(f, path);

            MessageBox.Show("Il numero dei campi è:" + nc);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (StreamReader sw = new StreamReader(path))
            {
                int d = 0;

                string a = sw.ReadLine();

                string[] campi = a.Split(';');

                int[] arr = new int[(campi.Length) + 1];

                for (int i = 0; i < campi.Length; i++)
                {
                    arr[d] = campi[i].Length;
                    d++;
                }
                arr[(arr.Length) - 1] = a.Length;

                while (a != null)
                {
                    d = 0;

                    string[] campi2 = a.Split(';');

                    for (int i = 0; i < campi2.Length; i++)
                    {
                        if (arr[d] < campi2[i].Length)
                        {
                            arr[d] = campi2[i].Length;
                        }

                        d++;
                    }

                    if (arr[(arr.Length) - 1] < a.Length)
                    {
                        arr[(arr.Length) - 1] = a.Length;
                    }

                    a = sw.ReadLine();

                }

                d = 0;



                for (int i = 0; i < arr.Length; i++)
                {
                    if (i != arr.Length - 1)
                    {
                        MessageBox.Show("Lunghezza campo " + d.ToString() + ": " + arr[i]);
                    }
                    else
                    {
                        MessageBox.Show("Lunghezza record " + d.ToString() + ": " + (arr[arr.Length - 1] + 1));
                    }
                    d++;
                }

                l = arr[arr.Length - 1];
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (l == 0)
            {

                MessageBox.Show("Calcolare prima lunghezza del record più lungo ");
            }
            else
            {

                f = lunghezza(f, l, path);
                MessageBox.Show("Tutti i record hanno la stessa lunghezza");
            }


        }
        private void button5_Click(object sender, EventArgs e)
        {
            Aggiuntarecord(textBox1.Text, textBox2.Text, textBox3.Text, path, f);
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Visualizza();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ricerca(checkBox1.Checked, checkBox2.Checked, checkBox3.Checked, textBox7.Text, textBox8.Text, textBox9.Text, path, f);
            MessageBox.Show("L'elemento ricercato si trova nella posizione:");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            modifica(textBox10.Text, textBox11.Text, textBox12.Text, path);

        }
        private void button9_Click(object sender, EventArgs e)
        {
            Cancellazionelogica(textBox13.Text, path);

        }

        public void Visualizza()
        {
            string[] lines = File.ReadAllLines(path);
            dataGridView1.Rows.Clear();
            string y = textBox4.Text;
            string x = textBox5.Text;
            string z = textBox6.Text;

            int y1 = 0;
            int x1 = 0;
            int z1 = 0;

            bool y2 = false;
            bool x2 = false;
            bool z2 = false;


            byte[] bytes = new byte[f];
            UTF8Encoding e = new UTF8Encoding(true);
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                int dim = 0;
                fs.Read(bytes, 0, f);
                fs.Position += 2;
                string line = e.GetString(bytes);
                string[] campi = line.Split(';');

                for (; dim < campi.Length; dim++)
                {


                    if (y == campi[dim])
                    {
                        y1 = dim;
                    }
                    if (x == campi[dim])
                    {
                        x1 = dim;
                    }
                    if (z == campi[dim])
                    {
                        z1 = dim;
                    }

                    if (y == "")
                    {
                        y2 = true;
                    }
                    if (x == "")
                    {
                        x2 = true;
                    }
                    if (z == "")
                    {
                        z2 = true;
                    }
                }


                while (fs.Read(bytes, 0, f) > 0)
                {
                    fs.Position += 2;
                    line = e.GetString(bytes);
                    campi = line.Split(';');
                    string[] row = new string[3];

                    if (!y2)
                    {
                        row[0] = campi[y1];
                    }


                    if (!x2)
                    {
                        row[1] = campi[x1];
                    }


                    if (!z2)
                    {
                        row[2] = campi[z1];
                    }


                    dataGridView1.Rows.Add(row[0], row[1], row[2]);
                }
            }
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

    }
}

