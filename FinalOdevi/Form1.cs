using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalOdevi
{
    public partial class Giris : Form
    {
        finalProjeEntities database = new finalProjeEntities();
        public Giris()
        {
            InitializeComponent();
        }

        private void gir_Click(object sender, EventArgs e)
        {
            var varMi = database.users.FirstOrDefault(ara =>
                ara.kullaniciadi == idBox.Text && ara.sifre == sifreBox.Text);

            if (varMi != null)
            {
                new AnaSayfa().Show();
            }
            else
            {
                uyari.Text = "HATALI GIRIŞ";
            }
        }
    }
}
