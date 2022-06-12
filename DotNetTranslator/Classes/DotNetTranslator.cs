using System;
using System.IO;
using System.Collections.Generic;

using Newtonsoft.Json;

public static class DotNetTranslator {

    #region Static Variables

    private static string _languageFileName = string.Empty;
    private static string _translateFileName = string.Empty;

    #endregion

    #region Lists

    /// <summary>
    /// Languages.json isimli dosyadan okuduğu dilleri tutar.
    /// </summary>
    public static List<Language> Languages { get; set; }

    /// <summary>
    /// Translates.{languageCode}.json isimli dosyadan okuduğu çevirileri tutar.
    /// </summary>
    public static List<Translate> Translates { get; set; }

    #endregion

    #region Models

    public class Language {
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }
    }

    public class Translate {
        public string TranslateLabel { get; set; }
        public string TranslateValue { get; set; }
    }

    #endregion

    /// <summary>
    /// Dilleri, uygulamanın ana dizininde bulunan Languages.json isimli dosyadan almaya çalışır.
    /// Değerleri aldığında, onları Languages isimli listeye aktarır.
    /// Bir terslik olduğunda hata fırlatır.
    /// </summary>
    public static void PrepareLanguages() {
        _languageFileName = "Languages.json";
        try {
            using (StreamReader streamReader = new StreamReader(_languageFileName)) {
                Languages = JsonConvert.DeserializeObject<List<Language>>(streamReader.ReadToEnd());
            }
            if (Languages.Count == 0) {
                throw new Exception($"No languages found in the {_languageFileName} file.");
            }
        } catch (JsonReaderException jsonReaderEx) {
            throw new JsonReaderException($"An error occurred while parsing the {_languageFileName} file.\n\nException details;\n\n{jsonReaderEx.Message}");
        } catch (FileNotFoundException) {
            throw new FileNotFoundException($"The language file {_languageFileName} is not found!");
        } catch (Exception ex) {
            throw ex;
        }
    }

    /// <summary>
    /// Çevirileri, talep edilen dil kodunu içeren Translates.{languageCode}.json isimli dosyadan almaya çalışır.
    /// Değerleri aldığında, onları Translates isimli listeye aktarır.
    /// Bir terslik olduğunda hata fırlatır.
    /// </summary>
    /// <param name="languageCode">Çevirisi talep edilen dilin kodu.</param>
    public static void PrepareTranslates(string languageCode) {
        _translateFileName = $"Translates.{languageCode}.json";
        try {
            if (IsValidLanguageCode(languageCode)) {
                using (StreamReader streamReader = new StreamReader(_translateFileName)) {
                    Translates = JsonConvert.DeserializeObject<List<Translate>>(streamReader.ReadToEnd());
                }
                if (Translates.Count == 0) {
                    throw new Exception($"No translates found in the {_translateFileName} file.");
                }
            } else {
                throw new Exception($"The language code {languageCode} is not supported.");
            }
        } catch (JsonReaderException jsonReaderEx) {
            throw new JsonReaderException($"An error occurred while parsing the {_translateFileName} file.\n\nException details;\n\n{jsonReaderEx.Message}");
        } catch (FileNotFoundException) {
            throw new FileNotFoundException($"The translate file {_translateFileName} is not found!");
        } catch (Exception ex) {
            throw ex;
        }
    }

    /// <summary>
    /// Çeviriyi, talep edilen çeviri etiketini içeren kayıttan almaya çalışır.
    /// Aldığı değeri string olarak döndürür.
    /// Bir terslik olduğunda hata fırlatır.
    /// </summary>
    /// <param name="translateLabel">Çevirisi istenen etiket.</param>
    /// <returns>Çevirisi yapılmış olan dizeyi döndürür.</returns>
    public static string GetTranslatedString(string translateLabel) {
        try {
            if (Translates != null) {
                foreach (Translate translate in Translates) {
                    if (translateLabel == translate.TranslateLabel) {
                        return translate.TranslateValue;
                    }
                }
                return translateLabel;
            } else {
                throw new Exception("The translate list was NULL.");
            }
        } catch (Exception ex) {
            throw ex;
        }
    }

    /// <summary>
    /// Çeviriyi, talep edilen çeviri etiketini içeren kayıttan almaya çalışır.
    /// Aldığı değeri, talep edilen değişkenlerle birlikte string olarak döndürür.
    /// </summary>
    /// <param name="translateLabel">Çevirisi istenen etiket.</param>
    /// <param name="translateVariables">Çevirisi istenen değişkenler.</param>
    /// <returns>Çevirisi yapılmış olan dizeyi, gönderilen değişkenlerle birlikte döndürür.</returns>
    public static string GetTranslatedString(string translateLabel, params object[] translateVariables) {
        string translatedText = GetTranslatedString(translateLabel);
        try {
            return string.Format(translatedText, translateVariables);
        } catch (FormatException) {
            throw new FormatException($"An error occurred while variable translation of the {translateLabel} label. There may be an error in variable definitions. Please check the translation that is made for the {translateLabel} label in the {_translateFileName} file.\n\nHint;\n\nThe {translateLabel} label's variable index values cannot be greater than {translateVariables.Length - 1}.");
        } catch (Exception ex) {
            throw ex;
        }
    }

    /// <summary>
    /// Çevirisi istenen dil kodunun, Languages listesinde olup olmadığını kontrol eder.
    /// Sonucu bool olarak döndürür.
    /// Bir terslik olduğunda hata fırlatır.
    /// </summary>
    /// <param name="languageCode">Çevirisi istenen dil kodu.</param>
    /// <returns>Dil kodu çeviri için destekleniyorsa true, desteklenmiyorsa false döner.</returns>
    private static bool IsValidLanguageCode(string languageCode) {
        try {
            if (Languages != null) {
                foreach (Language language in Languages) {
                    if (languageCode == language.LanguageCode) {
                        return true;
                    }
                }
                return false;
            } else {
                throw new Exception("The language list was NULL.");
            }
        } catch (Exception ex) {
            throw ex;
        }
    }

}
