using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Security.Cryptography;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Formun kontrol düğmelerini (simgeleştirme ve kapatma) ayarla
            this.ControlBox = true; // Simgeleştirme ve kapatma düğmeleri etkinleştirildi
            this.MaximizeBox = false; // Maksimize düğmesi devre dışı bırakıldı
            //this.FormBorderStyle = FormBorderStyle.FixedToolWindow; // Kenarlık ve başlık kaldırıldı

            // KeyPreview özelliğini true olarak ayarla
            this.KeyPreview = true;

            // KeyDown olayını ele al
            this.KeyDown += Form1_KeyDown;
            //axWindowsMediaPlayer1.URL = "C:\\Users\\Zehra Bağçe\\Documents\\alarm.mp3";

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.HorizontalScrollbar = true;
            timer1.Interval = 5000; // 1000 milisaniye = 1 saniye
            timer1.Tick += timer1_Tick;
            timer1.Start();

            this.MaximizeBox = false;//tam ekran işaretini devre dışı bıraktık
            this.KeyPreview = true;// bu sayede tuş atamasına hazır hale getiriyoruz

            axWindowsMediaPlayer1.Visible = false;

            // cbhatirlat.Checked = false;
            //dateTimePicker1.Visible = false;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            //label4.Text = "0";

            pictureBox2.Visible = true;
            pictureBox1.Visible = false;

            //timer1.Interval = 1000;
            timer1.Enabled = true;
            timer2.Enabled = true;

            timer1.Start();
            timer2.Start();
            btnDurdur.Visible = false;

            try
            {
                // Liste.txt dosyasının yolunu belirtin
                string dosyaYolu = "Liste.txt";

                // Dosyayı oku ve listBox1'e ekle
                using (StreamReader sr = new StreamReader(dosyaYolu))
                {
                    string satir;
                    while ((satir = sr.ReadLine()) != null)
                    {
                        listBox1.Items.Add(satir);
                        kayitsayisi();
                    }
                    sr.Close();
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata: " + hata.Message);
            }

        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Tam ekran tuşunun basılmasını kontrol et
            if (e.KeyCode == Keys.F11)
            {
                // Tuş işlenmemesini sağla
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            // Birinci tuş 'Alt' ve ikinci tuş 'Y' ise
            if (e.Alt && e.KeyCode == Keys.Y)
            {
                btnYeniKayit.PerformClick();
            }
            // Birinci tuş 'Alt' ve ikinci tuş 'K' ise
            else if (e.Alt && e.KeyCode == Keys.K)
            {
                btnKaydet.PerformClick();
            }
            // Birinci tuş 'Alt' ve ikinci tuş 'Z' ise
            else if (e.Alt && e.KeyCode == Keys.Z)
            {
                btnDuzelt.PerformClick();
            }
            // Birinci tuş 'Alt' ve ikinci tuş 'S' ise
            else if (e.Alt && e.KeyCode == Keys.S)
            {
                btnSecilenSil.PerformClick();
            }
            // Birinci tuş 'Alt' ve ikinci tuş 'H' ise
            else if (e.Alt && e.KeyCode == Keys.H)
            {
                btnHepsiniSil.PerformClick();
            }
            // Birinci tuş 'Alt' ve ikinci tuş 'D' ise
            else if (e.Alt && e.KeyCode == Keys.D)
            {
                btnDosyala.PerformClick();
            }
        }
        private void txtIsAdi_MouseEnter(object sender, EventArgs e)
        {
            txtIsAdi.BackColor = Color.LightYellow;
        }
        private void txtIsAdi_MouseLeave(object sender, EventArgs e)
        {
            txtIsAdi.BackColor = Color.White;
        }
        private void txtAciklama_MouseEnter(object sender, EventArgs e)
        {
            txtAciklama.BackColor = Color.LightYellow;
        }
        private void txtAciklama_MouseLeave(object sender, EventArgs e)
        {
            txtAciklama.BackColor = Color.White;
        }
        private void button1_Click(object sender, EventArgs e)//yeni kayıt butonu
        {
            txtAciklama.Text = "";
            txtIsAdi.Text = "";
            checkBox1.Checked = false;
            //dateTimePicker1.Visible = false;

        }
        private void kayitsayisi()
        {
            int kayitsayisi = listBox1.Items.Count;
            label5.Text = Convert.ToString(kayitsayisi);
        }
        private void btnKaydet_Click(object sender, EventArgs e)//kaydet butonu
        {
            string isadi, aciklama;
            isadi = txtIsAdi.Text;
            aciklama = txtAciklama.Text;


            //listBox1.Items.Add(isadi + " - " +  aciklama + " - " + dateTimePicker1.Text);

            if (string.IsNullOrWhiteSpace(txtIsAdi.Text) || string.IsNullOrWhiteSpace(txtAciklama.Text))
            {
                DialogResult cevap = MessageBox.Show("Lütfen iş adını ve açıklamasını boş geçmeyiniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                if (txtIsAdi.Text != " " && txtAciklama.Text != " " && checkBox1.Checked == true)
                {
                    listBox1.Items.Add(isadi + " - " + aciklama + " - " + dateTimePicker1.Text);
                }
                else if (txtIsAdi.Text != " " && txtAciklama.Text != " " && checkBox1.Checked == false)
                {
                    listBox1.Items.Add(isadi + " - " + aciklama);
                }
                kayitsayisi();
            }
        }
        private void button3_Click(object sender, EventArgs e)//düzelt butonu
        {

           if (listBox1.SelectedItems.Count == 0)
            {
                DialogResult cevap = MessageBox.Show("Seçilen kayıt değiştirilsin mi?", "Uyarı",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (cevap == DialogResult.No)
                {
                    // Kullanıcı "Hayır"ı seçtiyse, işlemi sonlandır
                    return;
                }
            }

            string guncelisadi, guncelaciklama;
            guncelisadi = txtIsAdi.Text;
            guncelaciklama = txtAciklama.Text;

            // Yeni içerik oluşturma
            string newItem = txtIsAdi.Text.Trim() + " - " + txtAciklama.Text.Trim();

            if (checkBox1.Checked && dateTimePicker1.Enabled)
            {
                // Eğer hatırlatma işaretliyse ve datetimepicker aktifse, tarih-zaman bilgisini ekle
                newItem += " - " + dateTimePicker1.Value.ToString("dd/MM/yyyy HH:mm:ss");
            }
            else
            {
                newItem += " - ";
            }
            // ListBox'ta seçili öğenin indeksini al
            int selectedIndex = listBox1.SelectedIndex;

            // Yeni içeriği seçili öğenin yerine koy
            listBox1.Items[selectedIndex] = newItem;


        }

        

        private void label3_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            // Tarih, gün ve saat bilgisini bir string olarak oluşturun
            string dateTimeString = string.Format("{0:dd/MM/yyyy} {1:HH:mm:ss} {2}", now, now.DayOfWeek, now.ToString("tt"));
            // Label kontrolüne yazın
            //labelDateTime.Text = dateTimeString;
            if (btnDurdur.Enabled)
            {
                stopMp3();
                btnDurdur.Text = "Başlat";
                label3.Text = txtIsAdi.Text + " " + txtAciklama.Text;

                //alanDurdur = true;


            }
            else //if(btnDurdur.Enabled)
            {
                playMp3();
                btnDurdur.Text = "Durdur";
                label3.Text = txtIsAdi.Text + " " + txtAciklama.Text;
                //alanDurdur = false;
            }
        }

        

        private void button4_Click(object sender, EventArgs e)//seçileni sil butonu
        {
            DialogResult cevap = MessageBox.Show("Silmek istediğinize emin misiniz?","Uyarı",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(cevap == DialogResult.Yes)
            {
                if(listBox1.Items.Count > 0)
                {
                    if(listBox1.SelectedIndex >= 0)
                    {
                        listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                        label5.Text=listBox1.Items.Count.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Lütfen bir eleman seçiniz.","Uyarı",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                else
                {
                    MessageBox.Show("Listede silinecek eleman yok.","Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)//hepsini sil butonu
        {
            DialogResult cevap = MessageBox.Show("Hepsini silmek istediğinize emin misiniz?", 
                "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

           
            if (cevap == DialogResult.Yes)
            {
                listBox1.Items.Clear();
                label5.Text = "0";
            }

        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dateTimePicker1.Enabled = true;
            }
            else
                dateTimePicker1.Enabled = false;
        }

        private void btnDosyala_Click(object sender, EventArgs e)//dosyaya kaydet butonu
        {

            StreamWriter yaz = new StreamWriter("Liste.txt");
            foreach (string bilgi in listBox1.Items)
            {
                yaz.WriteLine(bilgi);
            }
            yaz.Close();
            MessageBox.Show("Başarıyla dosyaya kaydedildi", "Kayıt Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void button7_Click(object sender, EventArgs e)//zilin butonu
        {
            //btnDurdur.Visible = false;
            alanDurdur = btnDurdur.Enabled;

            if(btnDurdur.Enabled)
            {
                stopMp3();
                btnDurdur.Text = "Başlat";
                label3.Text = txtIsAdi.Text + " " + txtAciklama.Text;

                //alanDurdur = true;


            }
            else //if(btnDurdur.Enabled)
            {
                playMp3();
                btnDurdur.Text = "Durdur";
                label3.Text = txtIsAdi.Text + " " + txtAciklama.Text;
                //alanDurdur = false;
            }

        }
        bool goster;
        bool alanDurdur = false;
        private void playMp3()
        {
            alanDurdur = false;
            axWindowsMediaPlayer1.settings.volume = 100;
            axWindowsMediaPlayer1.URL = "alarmm.mp3";
            btnDurdur.Text = "Durdur"; // Butonun metnini değiştiriyoruz
            btnDurdur.Enabled = true; // Butonu etkin hale getiriyoruz
           // label3.Text = txtIsAdi.Text + " " + txtAciklama.Text;
        }

        private void stopMp3()
        {
            alanDurdur = true;
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            axWindowsMediaPlayer1.Ctlcontrols.pause();
            btnDurdur.Text = "Başlat"; // Butonun metnini değiştiriyoruz
            btnDurdur.Enabled = true; // Butonu etkisiz hale getiriyoruz
           // label3.Text = txtIsAdi.Text + " " + txtAciklama.Text;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            // timer1.Interval = 30000;
            // label3.Text = "Bugün:" + DateTime.Now.ToLongDateString() + " " + "  Saat:" + DateTime.Now.ToString("hh:mm:ss");
            //  label3.Text = dateTimePicker1.Value.ToString("dd.MM.yyyy   dddd") + " " + DateTime.Now.ToString("h:mm:ss");
            DateTime suan = DateTime.Now;
            string satir, mesaj;
            panel();
            goster = false;

            mesaj = "Bugün: " + suan.ToLongDateString() + " Saat: " + suan.ToString("HH:mm:ss");
            resimdegisme(goster, mesaj);


            label3.Text = dateTimePicker1.Value.ToString("dd.MM.yyyy   dddd") + " " + DateTime.Now.ToString("HH:mm: ss");
            //label3.Text = dateTimePicker1.Value.ToString("dd.MM.yyyy   dddd") + " " + dateTimePicker1.Value.ToString("HH:mm: ss");

            resimdegisme(goster, mesaj);

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                satir = listBox1.Items[i].ToString();
                string[] dizi = satir.Split('-');
                if (dizi.Length < 2)
                {
                    continue;
                }
                else if (dizi.Length > 2)
                {

                    if (DateTime.Now.ToString("dd.MM.yyyy dddd HH:mm").Trim() == DateTime.Parse(dizi[2]).ToString("dd.MM.yyyy dddd HH:mm"))
                    {
                        //label6.Text=
                        label6.Text = dizi[0] + " - " + dizi[1];

                        //label3.Text = dizi[0] + " " + dizi[1]; // label3.Text'i txtIsAdi.Text + " " + txtAciklama.Text olarak ayarla
                        goster = true;
                        listBox1.SelectedIndex = i;
                        btnDurdur.Enabled = true; // Butonu etkinleştir
                        resimdegisme(goster, mesaj);

                        // Buton etkinse işlemleri gerçekleştir
                        if (btnDurdur.Enabled)
                        {
                            playMp3();
                            label3.BackColor = Color.Navy;
                            label3.ForeColor = Color.White;
                            notifyIcon1.Visible = true;
                            notifyIcon1.Text = "İş Hatırlatma Programı";
                            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                            notifyIcon1.BalloonTipTitle = "İş Takibi 1.0 Çalışıyor...";
                            notifyIcon1.BalloonTipText = "İş Takibi ile işlerinizi atlamayın";
                            notifyIcon1.ShowBalloonTip(30000);
                        }
                        else
                        {
                            stopMp3();
                        }
                    


                }
            }

            }



        label3.Text = dateTimePicker1.Value.ToString("dd.MM.yyyy   dddd") + " " + DateTime.Now.ToString("h:mm:ss");

        }
        private void panel()
        {
            label3.BackColor = Color.White;
            label3.ForeColor = Color.Black;
            panel1.BackColor = Color.White;
            panel1.ForeColor = Color.Black;
        }

        private void resimdegisme(bool goster, string mesaj)
        {
            label3.Text = goster.ToString() + mesaj;
            if (goster == false)
            {
                label6.Visible = false;
                label3.Visible = true;
                pictureBox1.Visible = false;
                pictureBox2.Visible = true;
                btnDurdur.Visible = false;
                label3.Text = mesaj;
                panel1.BackColor = Color.White;

            }
            else if (goster == true)
            {
                label6.Visible = true;
                label3.Visible = false;
                pictureBox1.Visible = true;
                pictureBox2.Visible = false;
                btnDurdur.Visible = true;
                label3.Text = mesaj;
                panel1.BackColor = Color.Navy;

            }

        }

     /*
        private void bildirigoster()
        {
            if (FormWindowState.Minimized == WindowState)
            {
                Hide();
                notifyIcon1.Visible = true;

                notifyIcon1.Text = "İş Hatırlatma Programı";
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon1.BalloonTipTitle = "İş Takibi 1.0 Çalışıyor...";
                notifyIcon1.BalloonTipText = "İş Takibi ile işlerinizi atlamayın";
                notifyIcon1.ShowBalloonTip(3000);
                notifyIcon1.MouseDoubleClick += new MouseEventHandler(notifyIcon1_MouseDoubleClick);
            }
        }*/

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                Hide();
                notifyIcon1.Visible = true;
                notifyIcon1.Text = "İş Hatırlatma Programı";
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon1.BalloonTipTitle = "İş Takibi 1.0 Çalışıyor...";
                notifyIcon1.BalloonTipText = "İş Takibi ile işlerinizi atlamayın";
                notifyIcon1.ShowBalloonTip(30000);
                notifyIcon1.MouseDoubleClick += new MouseEventHandler(notifyIcon1_MouseDoubleClick);
            }
        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                Show();
                WindowState = FormWindowState.Normal;
                notifyIcon1.Visible = false;
            }
            //this.Show();
            //notifyIcon1.Visible = false;



        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Show();
        }

        /*
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                Hide();
               notifyIcon1.Visible = true;
                notifyIcon1.Text = "İş Hatırlatma Programı";
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon1.BalloonTipTitle = "İş Takibi 1.0 Çalışıyor...";
                notifyIcon1.BalloonTipText = "İş Takibi ile işlerinizi atlamayın";
                notifyIcon1.ShowBalloonTip(100);
                notifyIcon1.MouseDoubleClick += new MouseEventHandler(notifyIcon1_MouseDoubleClick);
            }

        }*/

       

        private void Form1_FormClosing(object sender, EventArgs e)
        {
            //btnKaydet(sender, e);
            StreamWriter yaz = new StreamWriter("Liste.txt");
            // StreamWriter sw = new StreamWriter("Liste.txt");//bizden içine bir dosya adı yazmamızı ister
            foreach (string bilgi in listBox1.Items)
            {
                yaz.WriteLine(bilgi);
            }
            yaz.Close();
            //MessageBox.Show("Başarıyla dosyaya kaydedildi", "Kayıt Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label3.Text = "Bugün: " + DateTime.Now.ToString("dd.MM.yyyy  dddd") + "  " + "Saat: " + DateTime.Now.ToString("HH:mm: ss ");

        }


        /*private void ikonDegistir(bool goster,string alarmMesaj)
        {
           // labdebug.Text = "ikon değiş çağırıldı" + goster.ToString() + alarmMesaj;
            if(goster==false)
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = true;
                btnDurdur.Visible = false;
                label3.Text = alarmMesaj;
            }
            else if(goster==true)
            {
                pictureBox1.Visible = true;
                pictureBox2.Visible = false;
                btnDurdur.Visible = true;
                label3.Text = alarmMesaj;
            }
        }*/


    }
}
