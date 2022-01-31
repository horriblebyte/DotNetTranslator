using System;
using System.Windows.Forms;

namespace DotNetTranslator {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Dilleri hazırlayan fonksiyon.
            LanguageInit();

            Application.Run(new FrmTest());
        }

        private static void LanguageInit() {
            try {

                //Dilleri hazırlıyor.
                Translation.PrepareLanguages();

                //Dilleri sakladığımız değişken boş mu dolu mu kontrol ediyor (%localappdata%/DotNetTranslator).
                if (!string.IsNullOrEmpty(Properties.Settings.Default.CurrentLanguage)) {
                    //Boş değilse, çevirileri hazırlıyor.
                    Translation.PrepareTranslates(Properties.Settings.Default.CurrentLanguage);
                } else {
                    //Boş ise, dil seçimi formunu açıyor.
                    using (FrmLanguageSelector frmLanguageSelector = new FrmLanguageSelector()) {
                        if (frmLanguageSelector.ShowDialog() == DialogResult.OK && frmLanguageSelector.languageCode != "") {

                            //Seçilen kod ile çevirileri istiyoruz.
                            Translation.PrepareTranslates(frmLanguageSelector.languageCode);

                            //Hangi dili seçtiğimizi daha sonra hatırlayabilmek için config'e (%localappdata%/DotNetTranslator) kaydediyoruz.
                            Properties.Settings.Default.CurrentLanguage = frmLanguageSelector.languageCode;
                            Properties.Settings.Default.Save();
                        } else {
                            return;
                        }
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}