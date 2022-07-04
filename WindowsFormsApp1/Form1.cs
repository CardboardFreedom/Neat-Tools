//

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Threading;
using System.Timers;
using System.Globalization;
using System.Security.Cryptography;
using System.IO.Compression;
using System.Reflection;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Color buttons = Color.FromArgb(80, 80, 80);
        byte ssh = 0;
        bool check = false;
        int timeLeft = -1;
        int wrongGuesses = 0;
        string dir = "";
        bool fontWrong = false;
        readonly int iterations = 498996;
        string userPath = "";
        Font RTB4;

        System.Timers.Timer aTimer = new System.Timers.Timer();

        double ratio;
        

        public Form1()
        {
            InitializeComponent();

            button4.Enabled = false;

            pictureBox9.Parent = pictureBox4;

            comboBox1.SelectedIndex = Properties.Settings.Default.theme;
            comboBox1_SelectedIndexChanged(null, null);

            richTextBox4.SelectAll();
            richTextBox4.SelectionAlignment = HorizontalAlignment.Center;
            pictureBox5.Parent = pictureBox3;
            pictureBox5.BackColor = Color.Transparent;

            panel2.Visible = true;
            panel3.Visible = false;
            panel2.BringToFront();
            pictureBox7.Parent = pictureBox3;

            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            label2.Text = "v" + fileVersionInfo.ProductVersion;


            using (Font fontTester = new Font("Bradley Hand ITC", (float)38.25, FontStyle.Bold))
            { if (fontTester.Name != "Bradley Hand ITC") { label11.Font = new Font("Gadugi", (float)38.25); fontWrong = true; } }
            
            using (Font fontTester = new Font("Viner Hand ITC", (float)21.75, FontStyle.Bold))
            { if (fontTester.Name != "Viner Hand ITC") { label3.Font = new Font("Sitka Banner", (float)21.75); fontWrong = true; } }
            
            using (Font fontTester = new Font("Poor Richard", (float)24))
            { if (fontTester.Name != "Poor Richard") { richTextBox4.Font = new Font("Segoe Script", (float)17.25); fontWrong = true; } RTB4 = richTextBox4.Font; }

            using (Font fontTester = new Font("Viner Hand ITC", (float)21.75, FontStyle.Bold))
            { if (fontTester.Name != "Viner Hand ITC") { label1.Font = new Font("Sitka Heading", (float)27.75, FontStyle.Bold); fontWrong = true; } }

            using (Font fontTester = new Font("Cascadia Code", (float)8.25))
            { if (fontTester.Name != "Cascadia Code") fontWrong = true; }

            checkBox1.Checked = Properties.Settings.Default.fontError;
            checkBox2.Checked = !Properties.Settings.Default.link;
            textBox1.Text = Properties.Settings.Default.iterations.ToString();
        }


        private async void Form1_Load(object sender, EventArgs e)
        {
            aTimer.Elapsed += new ElapsedEventHandler(timer1_Tick);
            aTimer.Interval = 1000;

            if (fontWrong && Properties.Settings.Default.fontError)
            {
                await Task.Delay(350);

                Form5 form5 = new Form5();
                int x = Location.X + (Size.Width / 2) - (form5.Size.Width / 2);
                int y = Location.Y + (Size.Height / 2) - (form5.Size.Height / 2);
                form5.Location = new Point(x, y);
                form5.StartPosition = FormStartPosition.Manual;
                form5.ShowDialog(this);

                Properties.Settings.Default.fontError = !form5.dontShow;
                Properties.Settings.Default.Save();
                checkBox1.Checked = Properties.Settings.Default.fontError;
            }

            await Task.Run(() => Thread.Sleep(5000));
            lock (button4) { button4.Enabled = true; }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if(Properties.Settings.Default.theme == 1)
                if(Properties.Settings.Default.link)
                    System.Diagnostics.Process.Start("https://nhentai.net");
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.theme == 1)
                if (Properties.Settings.Default.link)
                    System.Diagnostics.Process.Start("https://nhentai.net");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.SteelBlue;
            button6.BackColor = buttons;
            button2.BackColor = buttons;

            panel2.Visible = true;
            panel3.Visible = false;
            flowLayoutPanel1.Visible = false;
            panel2.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.BackColor = buttons;
            button6.BackColor = buttons;
            button2.BackColor = Color.SteelBlue;

            panel2.Visible = false;
            panel3.Visible = true;
            flowLayoutPanel1.Visible = false;
            panel3.BringToFront();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button7.BackColor = Color.CornflowerBlue;
            button8.BackColor = Color.Transparent;

            button7.FlatAppearance.MouseOverBackColor = Color.DodgerBlue;
            button8.FlatAppearance.MouseOverBackColor = Color.Gray;

            button1.BackColor = buttons;
            button2.BackColor = buttons;
            button6.BackColor = Color.SteelBlue;

            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = true;
            flowLayoutPanel1.Visible = true;
            panel4.BringToFront();
            flowLayoutPanel1.BringToFront();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();

            int x = Location.X + (Size.Width / 2) - (form.Size.Width / 2);
            int y = Location.Y + (Size.Height / 2) - (form.Size.Height / 2);

            form.Location = new Point(x, y);
            form.StartPosition = FormStartPosition.Manual;

            //.ShowDialog(parent) if you want no possible interaction with the parent form
            //.Show(parent) can interact with parent, but will appear above parent
            form.ShowDialog(this);
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            richTextBox4_LostFocus(null, null);
            ssh = 1;
            richTextBox4_LostFocus(null, null);
            if (!check)
            {
                label6.BackColor = Color.Violet;
                label7.BackColor = Color.HotPink;
                label8.BackColor = Color.HotPink;
                roundedButton1.color = Brushes.Violet;
                roundedButton2.color = Brushes.HotPink;
                roundedButton3.color = Brushes.HotPink;
                roundedButton1.Refresh();
                roundedButton2.Refresh();
                roundedButton3.Refresh();
            }
        }

        private void roundedButton2_Click(object sender, EventArgs e)
        {
            richTextBox4_LostFocus(null, null);
            ssh = 2;
            richTextBox4_LostFocus(null, null);
            if (!check)
            {
                label7.BackColor = Color.Violet;
                label6.BackColor = Color.HotPink;
                label8.BackColor = Color.HotPink;
                roundedButton1.color = Brushes.HotPink;
                roundedButton2.color = Brushes.Violet;
                roundedButton3.color = Brushes.HotPink;
                roundedButton1.Refresh();
                roundedButton2.Refresh();
                roundedButton3.Refresh();
            }
        }

        private void roundedButton3_Click(object sender, EventArgs e)
        {
            richTextBox4_LostFocus(null, null);
            ssh = 3;
            richTextBox4_LostFocus(null, null);
            if (!check)
            {
                label8.BackColor = Color.Violet;
                label6.BackColor = Color.HotPink;
                label7.BackColor = Color.HotPink;
                roundedButton1.color = Brushes.HotPink;
                roundedButton2.color = Brushes.HotPink;
                roundedButton3.color = Brushes.Violet;
                roundedButton1.Refresh();
                roundedButton2.Refresh();
                roundedButton3.Refresh();
            }
        }
        
        void richTextBox4_LostFocus(object sender, EventArgs e)
        {
            if (richTextBox4.Text.Equals(string.Empty))
            {
                richTextBox4.Text = "Enter Minutes";
                richTextBox4.ForeColor = Color.Gray;

                richTextBox4.Font = RTB4;

                richTextBox4.SelectAll();
                richTextBox4.SelectionAlignment = HorizontalAlignment.Center;
                richTextBox4.SelectionLength = 0;
                pictureBox3.Focus();
            }
        }

        void richTextBox4_GotFocus(object sender, EventArgs e)
        {
            if (richTextBox4.Text.Equals("Enter Minutes"))
            {
                richTextBox4.Text = string.Empty;
                richTextBox4.ForeColor = Color.Black;

                richTextBox4.Font = new Font("Segoe Script", (float)21);
            }
        }

        private void doIt()
        {
            if (ssh == 1)
            {
                System.Diagnostics.Process.Start("shutdown", "/s /t 0");
                //System.Windows.Forms.Application.Exit();
            }
            else if (ssh == 2)
            {
                Application.SetSuspendState(PowerState.Suspend, true, true);
                //System.Windows.Forms.Application.Exit();
            }
            else if (ssh == 3)
            {
                Application.SetSuspendState(PowerState.Hibernate, true, true);
                //System.Windows.Forms.Application.Exit();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;

                int m = timeLeft / 60;
                int h = m / 60;
                m %= 60;
                int s = timeLeft % 60;

                label10.Text = "";

                label10.Text += h.ToString().PadLeft(2, '0') + ":";
                label10.Text += m.ToString().PadLeft(2, '0') + ":";
                label10.Text += s.ToString().PadLeft(2, '0');
            }
            else
            {
                aTimer.Stop();
                label10.Text = "Time's up!";
                doIt();
            }
        }

        private void timer(int ti)
        {
            timeLeft = ti * 60;
            aTimer.Enabled = true;
        }

        private void label10_Click(object sender, EventArgs e)
        {
            richTextBox4_LostFocus(null, null);
            if (check == false)
            {
                int time = 0;
                bool isNumeric = int.TryParse(richTextBox4.Text, NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out time);

                check = true;

                pictureBox5.Visible = true;
                label11.Visible = true;
                label11.BringToFront();

                string type = "laptop";

                if (SystemInformation.PowerStatus.BatteryChargeStatus == BatteryChargeStatus.NoSystemBattery)
                    type = "computer";


                if (ssh == 0)
                {
                    check = false;
                    label11.Text = "You have to choose a state silly!";
                }

                else if(time >= (int.MaxValue / 60) || (!isNumeric && decimal.TryParse(richTextBox4.Text, out decimal aa)))
                {
                    check = false;
                    label11.Text = "Close it yourself you lazy retard";
                }

                else if (isNumeric == false && richTextBox4.Text != "Enter Minutes" && richTextBox4.Text != "")
                {
                    check = false;
                    label11.Text = "Dyslexic retard.. Baka..";
                }

                else if (time == 0)
                {
                    check = false;
                    label11.Text = "You have to put a number silly!";
                }

                else
                {
                    string state = "shutdown";

                    if (ssh == 2)
                        state = "sleep";
                    if (ssh == 3)
                        state = "hibernate";

                    label11.Text = "Your " + type + " will " + state + " soon ♥";

                    roundedButton6.color = Brushes.Violet;
                    roundedButton5.color = Brushes.Violet;
                    label5.BackColor = Color.Violet;

                    roundedButton5.Refresh();
                    roundedButton6.Refresh();
                    label12.BackColor = Color.Violet;

                    richTextBox4.ReadOnly = true;

                    timer(time);
                }
            }
        }

        private void Encrypt(byte[] key, byte[] IV, byte[] salt, ref string path, int iter)
        {
            long trackProg = 0, divide = 1;
            using (Stream source = File.OpenRead(path))
            {
                long length = new System.IO.FileInfo(path).Length;

                while (length > 10000000)
                {
                    length /= 10;
                    divide *= 10;
                }

                int last = path.LastIndexOf(".");
                path = path.Insert((last >= 0 ? last : path.Length), "Encrypted");

                NextAvailableFilename(ref path);

                byte[] it = BitConverter.GetBytes(iter);
                var total = salt.Concat(IV).ToArray();
                total =  it.Concat(total).ToArray();

                if (userPath != "")
                    path = userPath + "\\" + Path.GetFileName(path);

                using (var write = File.Open(path, FileMode.Create))
                {
                    Stopwatch sw = new Stopwatch();

                    label4.Visible = true;
                    label4.Text = "Encrypting... Please Wait";

                    write.Write(total, 0, total.Length);

                    byte[] buffer = new byte[100000000];
                    int bytesRead;
                    while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        trackProg += (bytesRead / divide);
                        sw.Start();

                        byte[] encrypted = AES.EncryptStringToBytes_Aes(buffer.Take(bytesRead).ToArray(), key, IV);
                        byte[] intBytes = BitConverter.GetBytes(encrypted.Length);
                        write.Write(intBytes, 0, intBytes.Length);
                        write.Write(encrypted, 0, encrypted.Length);
                       
                        sw.Stop();

                        try
                        { 
                            double prog = (double)trackProg / length;
                            progressBar1.Value = (int)(prog * 100);

                            long cls = sw.ElapsedMilliseconds / 1000;

                            long time = (long)(cls / (progressBar1.Value / 100.0)) - cls;

                            long m = time / 60;
                            long h = m / 60;
                            m %= 60;
                            long s = time % 60;
                            long d = h / 24;
                            h %= 24;

                            label19.Text = "";
                            if (d > 0)
                                label19.Text += d.ToString() + " days ";
                            if (h > 0)
                                label19.Text += h.ToString() + " hours ";
                            if (m > 0)
                                label19.Text += m.ToString() + " minutes ";
                            if (s > 0)
                                label19.Text += s.ToString() + " seconds ";

                            label19.Text.TrimEnd();
                        }
                        catch
                        { }
                    }
                }
            }
        }

        private bool Decrypt(ref string path, Form4 form4, Stream file, long length)
        {
            long trackProg = 0, divide = 1;

            while (length > 10000000)
            {
                length /= 10;
                divide *= 10;
            }

            byte[] IV = new byte[16];
            byte[] salt = new byte[24];
            byte[] key;
            byte[] it = new byte[4];

            file.Read(it, 0, it.Length);
            file.Read(salt, 0, salt.Length);
            file.Read(IV, 0, IV.Length);
            
            CreateHash(form4.data, out key, salt, BitConverter.ToInt32(it, 0));
            form4.data = "";

            if (userPath != "")
                path = userPath + "\\" + Path.GetFileName(path);

            using (var write = File.Open(path, FileMode.Create))
            {
                int bytesRead = 0, i = 0;
                byte[] temp = new byte[4];

                Stopwatch sw = new Stopwatch();
                while (true)
                {
                    sw.Start();

                    if (file.Read(temp, 0, 4) <= 0)
                        break;

                    i = BitConverter.ToInt32(temp, 0);

                    byte[] buffer = new byte[i];
                    bytesRead = file.Read(buffer, 0, i);

                    trackProg += (bytesRead / divide);

                    if (bytesRead <= 0)
                        break;

                    try
                    {
                        byte[] decrypted = AES.DecryptStringFromBytes_Aes(buffer.Take(bytesRead).ToArray(), key, IV);
                        write.Write(decrypted, 0, decrypted.Length);
                    }
                    catch
                    {
                        write.Close();
                        File.Delete(path);

                        using (var dummy = new Form() { TopMost = true, TopLevel = true })
                        {
                            MessageBox.Show("Decryption Failed: Check Password", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }


                        wrongGuesses++;
                        if (wrongGuesses > 3)
                        {
                            Properties.Settings.Default.time = DateTime.Now;
                            Properties.Settings.Default.Save();
                        }
                        label4.Visible = true;
                        label4.Text = "Decrypting Failed";

                        return false;
                    }

                    sw.Stop();

                    try
                    {
                        double prog = (double)trackProg / length;
                        progressBar1.Value = (int)(prog * 100);

                        long cls = sw.ElapsedMilliseconds / 1000;

                        long time = (long)(cls / (progressBar1.Value / 100.0)) - cls;

                        long m = time / 60;
                        long h = m / 60;
                        m %= 60;
                        long s = time % 60;
                        long d = h / 24;
                        h %= 24;

                        label19.Text = "";
                        if (d > 0)
                            label19.Text += d.ToString() + " days ";
                        if (h > 0)
                            label19.Text += h.ToString() + " hours ";
                        if (m > 0)
                            label19.Text += m.ToString() + " minutes ";
                        if (s > 0)
                            label19.Text += s.ToString() + " seconds ";

                        label19.Text.TrimEnd();
                    }
                    catch
                    { }
                }
            }

            return true;
        }

        //Regular clicks - ENCRYPT
        private async void button3_Click(object sender, EventArgs e)
        {
            var b = sender as Button;
            lock (button4) { button4.Enabled = false; }
            b.Enabled = false;

            string path = dir;
            dir = "";

            byte[] key = null;
            Aes myAes = Aes.Create();
            byte[] IV = myAes.IV;

            openFileDialog1.FileName = "";
            if (path != "" || openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = (path == "" ? openFileDialog1.FileName : path);
                var fod = fileOrDir(path);

                Form3 form3 = new Form3();
                int x = Location.X + (Size.Width / 2) - (form3.Size.Width / 2);
                int y = Location.Y + (Size.Height / 2) - (form3.Size.Height / 2);
                form3.Location = new Point(x, y);
                form3.StartPosition = FormStartPosition.Manual;
                form3.ShowDialog(this);

                if (!form3.success)
                {
                    b.Enabled = true;
                    await Task.Run(() => Thread.Sleep(5000));
                    lock (button4) { button4.Enabled = true; }
                    return;
                }

                label4.Visible = true;
                label4.Text = "Doing Some Magic..";

                RNGCryptoServiceProvider cryptoProvider = new RNGCryptoServiceProvider();
                byte[] salt = new byte[24];
                cryptoProvider.GetBytes(salt);

                await Task.Run(() => CreateHash(form3.data, out key, salt, iterations + Properties.Settings.Default.iterations));
                form3.data = "";

                if (fod == true)
                {
                    path.TrimEnd('\\');
                    path.TrimEnd('/');

                    string orgPath = path;

                    if (userPath != "")
                        path = userPath + "\\" + Path.GetFileName(path);

                    label4.Visible = true;
                    label4.Text = "Compressing Folder...";

                    try
                    {
                        await Task.Run(() => ZipFile.CreateFromDirectory(orgPath, (path + "Compressed.zip")));
                    }
                    catch (Exception ex)
                    {
                        File.Delete((path + "Compressed.zip"));

                        using (var dummy = new Form() { TopMost = true, TopLevel = true })
                        {
                            MessageBox.Show(ex.Message, "Compress Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    path += "Compressed.zip";
                    File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.Hidden);
                }

                try 
                {
                    progressBar1.Visible = true;
                    progressBar1.Value = 0;
                    label19.Visible = true;
                    label19.Text = "";

                    string delPath = path;
                    await Task.Run(() => Encrypt(key, IV, salt, ref path, iterations + Properties.Settings.Default.iterations));
                    
                    if(fod == true)
                        File.Delete(delPath);
                }
                catch (Exception ex)
                {
                    using (var dummy = new Form() { TopMost = true, TopLevel = true })
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            else
            {
                b.Enabled = true;
                await Task.Run(() => Thread.Sleep(5000));
                lock (button4) { button4.Enabled = true; }
                return;
            }

            progressBar1.Visible = false;
            progressBar1.Value = 0;
            label19.Visible = false;
            label19.Text = "";

            label4.Visible = true;
            label4.Text = "Encryption Completed";

            using (var dummy = new Form() { TopMost = true, TopLevel = true })
            {
                MessageBox.Show("Finished Encrypting", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            b.Enabled = true;
            await Task.Run(() => Thread.Sleep(1500));
            lock (button4) { button4.Enabled = true; }
        }

        //DECRYPT
        private async void button4_Click(object sender, EventArgs e)
        {
            var b = sender as Button;
            button3.Enabled = false;
            b.Enabled = false;

            string path = dir;
            dir = "";

            openFileDialog1.FileName = "";
            if (path != "" || openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = (path == "" ? openFileDialog1.FileName : path);

                if(fileOrDir(path) == true)
                {
                    using (var dummy = new Form() { TopMost = true, TopLevel = true })
                    {
                        MessageBox.Show("Cannot Decrypt A Directory..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    button3.Enabled = true;
                    b.Enabled = true;
                    return;
                }

                Form4 form4 = new Form4();
                int x = Location.X + (Size.Width / 2) - (form4.Size.Width / 2);
                int y = Location.Y + (Size.Height / 2) - (form4.Size.Height / 2);
                form4.Location = new Point(x, y);
                form4.StartPosition = FormStartPosition.Manual;
                form4.ShowDialog(this);

                if (!form4.success || form4.data == "")
                {
                    button3.Enabled = true;
                    b.Enabled = true;
                    return;
                }

                progressBar1.Visible = true;
                progressBar1.Value = 0;
                label19.Visible = true;
                label19.Text = "";

                label4.Visible = true;
                label4.Text = "Decrypting... Please Wait";

                long length = new System.IO.FileInfo(path).Length;
                using (Stream file = File.OpenRead(path))
                {
                    string newPath = path.Replace("Encrypted", "Decrypted");
                    int index = path.LastIndexOf(".");

                    if (newPath == path) newPath = path.Insert((index >= 0 ? index : path.Length), "Decrypted");
                    path = newPath;

                    NextAvailableFilename(ref path);

                    bool t = false;
                    await Task.Run(() => t = Decrypt(ref path, form4, file, length));

                    if (!t)
                    {
                        File.Delete(path);
                        button3.Enabled = true;

                        System.TimeSpan diff1 = DateTime.Now.Subtract(Properties.Settings.Default.time);

                        if (diff1.Days < 1)
                            await Task.Run(() => Thread.Sleep(5000));

                        progressBar1.Visible = false;
                        progressBar1.Value = 0;
                        label19.Visible = false;
                        label19.Text = "";
                        b.Enabled = true;
                        return;
                    }

                    else wrongGuesses = 0;
                }
            }

            else
            {
                button3.Enabled = true;
                b.Enabled = true;
                return;
            }

            progressBar1.Visible = false;
            progressBar1.Value = 0;
            label19.Visible = false;
            label19.Text = "";

            label4.Visible = true;
            label4.Text = "Decrypting Completed";

            using (var dummy = new Form() { TopMost = true, TopLevel = true })
            {
                MessageBox.Show("Finished Decrypting", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            button3.Enabled = true;
            b.Enabled = true;
        }

        //To Decrypt - Get Hash With Known Salt
        public static void CreateHash(string input, out byte[] key, byte[] salt, int it)
        {
            // Generate the hash
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(input, salt, it);
            key = SHA256.Create().ComputeHash(pbkdf2.GetBytes(32));
        }

        //Drag-in-Drops
        private void dragDropStart(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        public static bool? fileOrDir(string path)
        {
            if (Directory.Exists(path)) return true; // is a directory
            else if (File.Exists(path)) return false; // is a file
            else return null; // is a nothing
        }

        private void dragDropEncrypt(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                return;

            string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            for (int i = 0; i < fileList.Length; i++)
                dir = fileList[i];

            button3_Click(button3, null);
        }

        private void dragDropDecrypt(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                return;

            string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            for (int i = 0; i < fileList.Length; i++)
                dir = fileList[i];

            button4_Click(button4, null);
        }

        private void resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                return;

            int x, y, h = this.Height;

            do
            {
                y = h - 165;
                x = (int)(y * ratio);
                h--;
            } while (x > panel3.Width * .9);

            pictureBox4.Location = new Point(pictureBox4.Location.X - (x - pictureBox4.Width),  pictureBox4.Location.Y - (y - pictureBox4.Height));

            pictureBox4.Height = y;
            pictureBox4.Width = x;
        }


        private void reset(object sender, EventArgs e)
        {
            if (check == false)
                return;

            if (MessageBox.Show("Reset Timer?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.None)
                == System.Windows.Forms.DialogResult.Cancel)
                return;

            ssh = 0;
            label11.Visible = true;
            pictureBox5.Visible = true;
            label11.Text = "I reset the timer for you Master ♥";
            label10.Text = "";
            check = false;

            label8.BackColor = Color.HotPink;
            label6.BackColor = Color.HotPink;
            label7.BackColor = Color.HotPink;
            label5.BackColor = Color.HotPink;
            roundedButton1.color = Brushes.HotPink;
            roundedButton2.color = Brushes.HotPink;
            roundedButton3.color = Brushes.HotPink;
            roundedButton1.Refresh();
            roundedButton2.Refresh();
            roundedButton3.Refresh();

            roundedButton6.color = Brushes.HotPink;
            roundedButton5.color = Brushes.HotPink;

            roundedButton5.Refresh();
            roundedButton6.Refresh();
            label12.BackColor = Color.HotPink;

            richTextBox4.Text = "";
            richTextBox4.ReadOnly = false;
            richTextBox4_LostFocus(null, null);

            timeLeft = -1;
            aTimer.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button7.BackColor = Color.CornflowerBlue;
            button8.BackColor = Color.Transparent;

            button7.FlatAppearance.MouseOverBackColor = Color.DodgerBlue;
            button8.FlatAppearance.MouseOverBackColor = Color.Gray;

            panel4.Visible = true;
            panel5.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            button7.BackColor = Color.Transparent;
            button8.BackColor = Color.CornflowerBlue;

            button7.FlatAppearance.MouseOverBackColor = Color.Gray;
            button8.FlatAppearance.MouseOverBackColor = Color.DodgerBlue;

            panel4.Visible = false;
            panel5.Visible = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.fontError = checkBox1.Checked;
            Properties.Settings.Default.Save();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.link = !checkBox2.Checked;
            Properties.Settings.Default.Save();
        }

        public void NextAvailableFilename(ref string path)
        {
            string extension = Path.GetExtension(path);

            int i = 0;
            while (File.Exists(path))
            {
                if (i == 0)
                    path = path.Replace(extension, "(" + ++i + ")" + extension);
                else
                    path = path.Replace("(" + i + ")" + extension, "(" + ++i + ")" + extension);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int x = 0;
            bool success = Int32.TryParse(textBox1.Text, out x);

            if (success)
            {
                int difference = 2000000000 - x;

                if (iterations < difference)
                {
                    Properties.Settings.Default.iterations = x;
                    Properties.Settings.Default.Save();
                }
            }

            if (textBox1.Text == "")
            {
                Properties.Settings.Default.iterations = 0;
                Properties.Settings.Default.Save();
            }

            textBox1.Text = Properties.Settings.Default.iterations.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.theme = (byte)comboBox1.SelectedIndex;
            Properties.Settings.Default.Save();

            if (comboBox1.SelectedIndex == 0)
                theme1();
            if (comboBox1.SelectedIndex == 1)
                theme2();
        }

        private void theme1()
        {
            bool max = false;
            if (WindowState == FormWindowState.Maximized)
            {
                max = true;
                WindowState = FormWindowState.Normal;
            }

            var old = this.Size;
            Size = new System.Drawing.Size(1227, 851);

            pictureBox3.BackgroundImage = Properties.Resources.hello;
            pictureBox3.Size = new System.Drawing.Size(459, 613);
            pictureBox2.BackgroundImage = Properties.Resources.asd;
            pictureBox4.Image = Properties.Resources.light;
            pictureBox4.Size = new System.Drawing.Size(351, 686);
            pictureBox4.Location = new Point(688, 6);
            pictureBox5.Location = new Point(330, 0);

            ratio = (double)pictureBox4.Width / pictureBox4.Height;

            Size = old;
            if (max)
                WindowState = FormWindowState.Maximized;
        }

        private void theme2()
        {
            var resources = new System.Resources.ResourceManager(typeof(Form1));

            bool max = false;
            if(WindowState == FormWindowState.Maximized)
            {
                max = true;
                WindowState = FormWindowState.Normal;
            }

            var old = this.Size;
            Size = new System.Drawing.Size(1227, 851);

            pictureBox3.BackgroundImage = (Bitmap)resources.GetObject("pictureBox3.BackgroundImage");
            pictureBox3.Size = new System.Drawing.Size(459, 607);
            pictureBox2.BackgroundImage = (Bitmap)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox4.Image = (Bitmap)resources.GetObject("pictureBox4.Image");
            pictureBox4.Size = new System.Drawing.Size(1015, 686);
            pictureBox4.Location = new Point(56, 6);
            pictureBox5.Location = new Point(279, 21);

            ratio = (double)pictureBox4.Width / pictureBox4.Height;

            Size = old;
            if (max)
                WindowState = FormWindowState.Maximized;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                userPath = folderBrowserDialog1.SelectedPath;
        }
    }

















    class AES
    {
        public static byte[] EncryptStringToBytes_Aes(byte[] plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                {
                    using (var ms = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(plainText, 0, plainText.Length);
                            cryptoStream.FlushFinalBlock();

                            return ms.ToArray();
                        }
                    }
                }
            }
        }
        public static byte[] DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            var decrypted = new byte[cipherText.Length];
                            var bytesRead = csDecrypt.Read(decrypted, 0, cipherText.Length);

                            return decrypted.Take(bytesRead).ToArray();
                        }
                    }
                }
            }
        }
    }
}


namespace newB
{
    public class RoundedButton : Button
    {
        public Brush color = Brushes.HotPink;

        GraphicsPath GetRoundPath(RectangleF Rect, int radius)
        {
            float r2 = radius / 2f;
            GraphicsPath GraphPath = new GraphicsPath();
            GraphPath.AddArc(Rect.X, Rect.Y, radius, radius, 180, 90);
            GraphPath.AddLine(Rect.X + r2, Rect.Y, Rect.Width - r2, Rect.Y);
            GraphPath.AddArc(Rect.X + Rect.Width - radius, Rect.Y, radius, radius, 270, 90);
            GraphPath.AddLine(Rect.Width, Rect.Y + r2, Rect.Width, Rect.Height - r2);
            GraphPath.AddArc(Rect.X + Rect.Width - radius,
                             Rect.Y + Rect.Height - radius, radius, radius, 0, 90);
            GraphPath.AddLine(Rect.Width - r2, Rect.Height, Rect.X + r2, Rect.Height);
            GraphPath.AddArc(Rect.X, Rect.Y + Rect.Height - radius, radius, radius, 90, 90);
            GraphPath.AddLine(Rect.X, Rect.Height - r2, Rect.X, Rect.Y + r2);
            GraphPath.CloseFigure();
            return GraphPath;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            RectangleF Rect = new RectangleF(0, 0, this.Width, this.Height);
            
            using (GraphicsPath GraphPath = GetRoundPath(Rect, 45))
            {
                this.Region = new Region(GraphPath);
                using (Pen pen = new Pen(Color.CadetBlue, 1.75f))
                {
                    pen.Alignment = PenAlignment.Inset;
                    e.Graphics.DrawPath(pen, GraphPath);
                    e.Graphics.FillPath(color, GraphPath);
                }
            }
        }
    }
}