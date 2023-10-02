using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ripasso_estate1
{
    public class Class1
    {
        static public bool Valore(int f, string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                byte[] b = new byte[f];
                UTF8Encoding enc = new UTF8Encoding(true);
                fs.Read(b, 0, f);
                string line = enc.GetString(b).TrimEnd();

                string[] split = line.Split(';');
                for (int i = 0; i < split.Length; i++)
                    if (split[i] == "Valore") return false;

                fs.Position = enc.GetBytes(line).Length;

                byte[] info = enc.GetBytes(";Valore;logic");
                fs.Write(info, 0, info.Length);

                fs.Position = f + 2;
                for (int lin = 2, pos; fs.Read(b, 0, f) > 0; lin++)
                {
                    line = enc.GetString(b);
                    pos = enc.GetBytes(line.TrimEnd()).Length;
                    fs.Position = fs.Position - f + pos;


                    line = $";{new Random(lin * Environment.TickCount).Next(10, 20 + 1)};0";
                    info = enc.GetBytes(line);
                    fs.Write(info, 0, info.Length);

                    fs.Position = (f + 2) * lin;
                }
            }
            return true;
        }

        //funzione

        static public int contacampi(int f, string path)
        {

            using (FileStream sw = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                byte[] b = new byte[f];
                sw.Read(b, 0, f);
                return new UTF8Encoding(true).GetString(b).Split(';').Length;
            }

        }

        //funzione

        static public int lunghezza(int f, int l, string path)
        {
            int nfdi;
            if (l < 100) nfdi = 200; else nfdi = l / 100 * 100 + 200;


            if (f != nfdi)
            {
                string tpath = Path.GetDirectoryName(path) + "\\temp.csv";
                File.Copy(path, tpath, true);
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Write, FileShare.None))
                {
                    using (FileStream temp = new FileStream(tpath, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        UTF8Encoding enc = new UTF8Encoding(true);
                        int b;
                        string line = "";
                        while ((b = temp.ReadByte()) > 0)
                            if ((char)b == '\n')
                            {

                                Byte[] info = enc.GetBytes(line.TrimEnd(' ', '\r').PadRight(nfdi) + "\r\n");
                                fs.Write(info, 0, info.Length);
                                line = "";
                            }
                            else
                                line += (char)b;


                        fs.SetLength(fs.Position);
                    }
                    File.Delete(tpath);
                }
            }
            return nfdi;
        }

        //funzione

        static public void Aggiuntarecord(string coda1, string coda2, string coda3, string path, int f)
        {
            bool[] a = new bool[1000];

            string[] p = new string[1000];

            int dim = 0;
            byte[] bytes = new byte[1000];
            UTF8Encoding e = new UTF8Encoding(true);
            string line = null;

            using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.None))
            {
                line = $"{coda1};{coda2};{coda3};{new Random().Next(10, 21)};0".PadRight(f) + Environment.NewLine;
                bytes = e.GetBytes(line);

                fs.Write(bytes, 0, bytes.Length);
            }
        }

        //funzione

        static public string ricerca(bool checkBox1, bool checkBox2, bool checkBox3, string textBox7, string textBox8, string textBox9, string path, int f)
        {
            byte[] bytes = new byte[1000];
            UTF8Encoding e = new UTF8Encoding(true);
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                while (fs.Read(bytes, 0, f) > 0)
                {
                    string line = e.GetString(bytes);
                    string[] campi = line.Split(';');
                    if (checkBox1)
                    {
                        if (campi[0].ToLower() == textBox7.ToLower())
                        {
                            return line;

                        }
                        if (checkBox2)
                        {
                            if (campi[1].ToLower() == textBox8.ToLower())
                            {
                                return line;

                            }
                        }
                        if (checkBox3)
                        {
                            if (campi[2].ToLower() == textBox9.ToLower())
                            {
                                return line;
                            }
                        }
                    }

                }
            }
            return "";
        }
        //funzione
        static public void modifica(string a1, string a2, string a3, string path)
        {
            string a = a1;

            string[] ele = new string[1000];

            int dim = 0;

            int control = 0;

            using (StreamReader sw = new StreamReader(path))
            {
                string b = sw.ReadLine();

                while (b != null)
                {
                    ele[dim] = b;

                    string[] campi = ele[dim].Split(';');

                    for (int i = 0; i < campi.Length; i++)
                    {
                        if (campi[i] == a)
                        {
                            control = dim;
                        }
                    }

                    dim++;

                    b = sw.ReadLine();
                }
            }

            using (StreamWriter sw = new StreamWriter(path))
            {
                dim = 0;

                string r = "";

                while (ele[dim] != null)
                {
                    if (dim == control)
                    {
                        string[] campi2 = ele[dim].Split(';');

                        if (a2 != null)
                        {
                            r = r + a2;
                        }
                        else
                        {
                            string[] campi3 = ele[dim].Split(';');
                            r = r + campi3[dim];
                        }

                        if (a3 != null)
                        {
                            r = r + ";" + a3;
                        }
                        else
                        {
                            string[] campi4 = ele[dim].Split(';');
                            r = r + ";" + campi4[dim];
                        }



                        sw.WriteLine(r);
                    }
                    else
                    {
                        sw.WriteLine(ele[dim]);
                    }

                    dim++;
                }
            }
        }

        //funzione
        static public void Cancellazionelogica(string a1, string path)
        {
            bool[] a = new bool[1000];

            string[] a2 = new string[1000];

            string c = a1;

            int dim = 0;

            using (StreamReader sw = new StreamReader(path))
            {
                string b = sw.ReadLine();

                while (b != null)
                {
                    a2[dim] = b;

                    string[] campi = b.Split(';');

                    if (campi[0] == c)
                    {
                        a[dim] = false;
                    }
                    else
                    {
                        a[dim] = true;
                    }
                    dim++;

                    b = sw.ReadLine();
                }
            }

            using (StreamWriter sw = new StreamWriter(path))
            {
                dim = 0;

                while (a2[dim] != null)
                {

                    if (a[dim] == true)
                    {
                        sw.WriteLine(a2[dim]);
                    }
                    dim++;
                }

            }
        }
    }
}
