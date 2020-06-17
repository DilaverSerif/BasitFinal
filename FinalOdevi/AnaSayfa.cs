using System;
using System.Linq;
using System.Windows.Forms;


namespace FinalOdevi
{
    public partial class AnaSayfa : Form
    {
        finalProjeEntities database = new finalProjeEntities();

        public AnaSayfa()
        {
            InitializeComponent();
        }

        private void ekle_Click(object sender, EventArgs e)
        {
            users yeniUser = new users();
            yeniUser.sifre = sifre.Text;
            yeniUser.kullaniciadi = kullaniciAdi.Text;
            yeniUser.seviye = seviye.Text;
            database.users.Add(yeniUser);
            database.SaveChanges();
            Kullanici_TabloYenile();
        }

        private void Kullanici_TabloYenile()
        {
            var kullaniciAdiList = database.users.ToList();
            kullaniciList.DataSource = database.users.ToList();
            

            foreach (var id in kullaniciAdiList)
            {
                kullaniciBox.Items.Add(id.kullaniciadi);
            }
            
        }

        private void AnaSayfa_Load(object sender, EventArgs e)
        {
            Kullanici_TabloYenile();
            malzemeList.DataSource = database.items.ToList();
        }

        private void silButton_Click(object sender, EventArgs e)
        {
            var sil = database.users.FirstOrDefault(silicone => silicone.kullaniciadi == silID.Text);

            database.users.Remove(sil ?? throw new InvalidOperationException());
            database.SaveChanges();
            Kullanici_TabloYenile();
        }

        private void kullaniciBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            silID.Text = kullaniciBox.Text;
        }

        private void degistir_Click(object sender, EventArgs e)
        {
            
            bool tamam = Int32.TryParse(idGoster.Text.Replace("ID:", ""), out int idResult);

            if (!tamam)
            {
                MessageBox.Show("HATA");
                return;
            }

            var degistir = database.users.FirstOrDefault(degis => degis.id == idResult);

            degistir.seviye = degisSeviye.Text;
            degistir.eposta = degisEposta.Text;
            degistir.kullaniciadi = degisKadi.Text;
            degistir.sifre = degisSifre.Text;
            degistir.isim = degisAd.Text;
            degistir.telefon = degisTel.Text;
            database.SaveChanges();
        }

        private void kullaniciList_SelectionChanged(object sender, EventArgs e)
        {
            if(kullaniciList.SelectedRows.Count == 0 ) return;
            degisAd.Text = kullaniciList.SelectedRows[0].Cells[2].Value.ToString();
            degisEposta.Text = kullaniciList.SelectedRows[0].Cells[4].Value.ToString();
            degisKadi.Text = kullaniciList.SelectedRows[0].Cells[0].Value.ToString();
            degisSeviye.Text = kullaniciList.SelectedRows[0].Cells[5].Value.ToString();
            degisSifre.Text = kullaniciList.SelectedRows[0].Cells[1].Value.ToString();
            degisTel.Text = kullaniciList.SelectedRows[0].Cells[3].Value.ToString();
            idGoster.Text ="ID:" + kullaniciList.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void malzemeAra_Click(object sender, EventArgs e)
        {
            var ara = database.items.FirstOrDefault(bul => bul.adi == maraText.Text);
            
            if (ara != null)
            {
                for (int i = 0; i < malzemeList.RowCount; i++)
                {
                    if (malzemeList.Rows[i].Cells[0].Value.ToString() == maraText.Text)
                    {
                        malzemeList.Rows[i].Selected = true;
                        malzemeAdEdit.Text = malzemeList.Rows[i].Cells[0].Value.ToString();
                        malzemeDigerEdit.Text = malzemeList.Rows[i].Cells[6].Value.ToString();
                        malzemeDurumuEdit.Text = malzemeList.Rows[i].Cells[4].Value.ToString();
                        malzemeKisiEdit.Text = malzemeList.Rows[i].Cells[3].Value.ToString();
                        malzemeKonumuEdit.Text = malzemeList.Rows[i].Cells[2].Value.ToString();
                        malzemeTarihEdit.Text = malzemeList.Rows[i].Cells[5].Value.ToString();
                        malzemeTuruEdit.Text = malzemeList.Rows[i].Cells[1].Value.ToString();
                        break;
                    } 
                }

                seciliMalzeme.Text = ara.adi;
                malzemeID.Text = "ID:"+ara.id.ToString();
            }
            else
            {
                MessageBox.Show("HATA URUN BULUNAMADI");
            }
        }


        private void malzemeSil_Click(object sender, EventArgs e)
        {
            var sil = database.items.FirstOrDefault(silicone => silicone.adi == seciliMalzeme.Text);

            database.items.Remove(sil ?? throw new InvalidOperationException());
            database.SaveChanges();
            malzemeList.DataSource = database.items.ToList();
        }

        private void malzemeDuzenle_Click(object sender, EventArgs e)
        {
            bool tamam = Int32.TryParse(idGoster.Text.Replace("ID:", ""), out int idResult);
            if (!tamam)
            {
                MessageBox.Show("HATA");
                return;
            }

            var degistir = database.items.FirstOrDefault(degis => degis.id == idResult);

            degistir.adi = malzemeAdEdit.Text;
            degistir.diger = malzemeDigerEdit.Text;
            degistir.durumu = malzemeDurumuEdit.Text;
            degistir.konumu = malzemeKonumuEdit.Text;
            degistir.sorumlu = malzemeKisiEdit.Text;
            degistir.tarih = malzemeTarihEdit.Text;
            degistir.tur = malzemeTuruEdit.Text;
            database.SaveChanges();

        }

        private void malzemeEkle_Click(object sender, EventArgs e)
        {
            items yeniMalzeme = new items();
            yeniMalzeme.adi = malzemeAdi.Text;
            yeniMalzeme.diger = malzemeDiger.Text;
            yeniMalzeme.durumu = malzemeDurumu.Text;
            yeniMalzeme.konumu = malzemeKonumu.Text;
            yeniMalzeme.sorumlu = malzemeKisi.Text;
            yeniMalzeme.tarih = malzemeTarih.Text;
            yeniMalzeme.tur = malzemeTuru.Text;
            database.items.Add(yeniMalzeme);
            database.SaveChanges();
        }

        private void malzemeTemizle_Click(object sender, EventArgs e)
        {
            malzemeAdi.Text = null;
            malzemeDiger.Text = null;
            malzemeDurumu.Text = null;
            malzemeKonumu.Text = null;
            malzemeKisi.Text = null;
            malzemeTarih.Text = null;
            malzemeTuru.Text = null;
        }
    }
}
