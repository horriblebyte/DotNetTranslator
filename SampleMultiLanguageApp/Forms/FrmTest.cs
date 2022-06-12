using System;
using System.Windows.Forms;

namespace SampleMultiLanguageApp.Forms {
    public partial class FrmTest : Form {

        public FrmTest() {
            InitializeComponent();

            try {
                CmbLanguages.ValueMember = "LanguageCode";
                CmbLanguages.DataSource = DotNetTranslator.Languages;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Form component'lerinin initialization işlemleri bittiğinde çeviriyi ayarlıyoruz.
            SetLanguage();
        }

        private void BtnConfirm_Click(object sender, EventArgs e) {
            try {

                if (CmbLanguages.SelectedIndex >= 0) {
                    //Hangi dili seçtiğimizi daha sonra hatırlayabilmek için bir yere kaydediyoruz.
                    Properties.Settings.Default.CurrentLanguage = CmbLanguages.SelectedValue.ToString();
                    Properties.Settings.Default.Save();

                    //Seçtiğimiz dil ile uyuşan çevirileri talep ediyoruz.
                    DotNetTranslator.PrepareTranslates(Properties.Settings.Default.CurrentLanguage);

                    //Component'leri çeviriyoruz.
                    SetLanguage();
                } else {
                    MessageBox.Show("Lütfen dil seçimi yapınız.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Her formda ya da class'ta olması gereken method.
        /// Çeviriler bunun gibi bir methodla derli toplu bir şekilde değişkenlere ya da component'lere atanabilir.
        /// </summary>
        private void SetLanguage() {
            try {
                label1.Text = DotNetTranslator.GetTranslatedString("ANA_SAYFA_LABEL_1", "Mutlu", "FANTASTİK", "YÜZÜKLERİN EFENDİSİ");
                label2.Text = DotNetTranslator.GetTranslatedString("ANA_SAYFA_LABEL_2");
                label3.Text = DotNetTranslator.GetTranslatedString("ANA_SAYFA_LABEL_3");
                label4.Text = DotNetTranslator.GetTranslatedString("ANA_SAYFA_LABEL_4");
                label5.Text = DotNetTranslator.GetTranslatedString("ANA_SAYFA_LABEL_5", "test", "sadasd", "tester", 33, 44, 55);
                label6.Text = DotNetTranslator.GetTranslatedString("ANA_SAYFA_LABEL_6");
                label7.Text = DotNetTranslator.GetTranslatedString("ANA_SAYFA_LABEL_7");
                label8.Text = DotNetTranslator.GetTranslatedString("ANA_SAYFA_LABEL_8");
                label9.Text = DotNetTranslator.GetTranslatedString("ANA_SAYFA_LABEL_9");
                label10.Text = DotNetTranslator.GetTranslatedString("ANA_SAYFA_LABEL_10");
                label11.Text = DotNetTranslator.GetTranslatedString("ANA_SAYFA_LABEL_11");
                label12.Text = DotNetTranslator.GetTranslatedString("ANA_SAYFA_LABEL_12");
                label13.Text = DotNetTranslator.GetTranslatedString("ANA_SAYFA_LABEL_13");
                label14.Text = DotNetTranslator.GetTranslatedString("ANA_SAYFA_LABEL_14");
                label15.Text = DotNetTranslator.GetTranslatedString("ANA_SAYFA_LABEL_15");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}