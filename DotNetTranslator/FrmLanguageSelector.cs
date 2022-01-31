using System;
using System.Windows.Forms;

namespace DotNetTranslator {
    public partial class FrmLanguageSelector : Form {

        public string languageCode = string.Empty;

        public FrmLanguageSelector() {
            InitializeComponent();

            try {
                CmbLanguages.ValueMember = "LanguageCode";
                CmbLanguages.DataSource = Translation.Languages;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnConfirm_Click(object sender, EventArgs e) {
            languageCode = CmbLanguages.SelectedValue.ToString();
            DialogResult = DialogResult.OK;
        }

    }
}