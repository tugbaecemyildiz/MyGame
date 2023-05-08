using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levels; //GameObject tipinde bir levels liste oluþturuldu
    public static int maximumLevelIndex; //Maksimum geldiði seviyenin indexi tutuldu

    #region Seviye Resimlerini Ayarla
    public void SetLevelImages()
    {
        //Þuanki seviyeyi playerprefs'ten al (her seviye geçtiðmizde güncelleniyor)
        maximumLevelIndex = PlayerPrefs.GetInt("MaximumSeviye");

        //Seviyeler listesini gez, þuanki seviyenin index'inden küçükleri aç büyükleri yani sonrakileri kapat
        //GetComponent ile button'a ulaþ ve interactable özelliðini yani basýlýp basýlamayacaðýný ayarla
        for (int i = 0; i < levels.Length; i++)
        {
            if (i <= maximumLevelIndex)
            {
                levels[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                levels[i].GetComponent<Button>().interactable = false;
            }
        }
    }
    #endregion

    #region Seviye Geçmiþini Sil
    //Eðer kullanýcý isterse seviye geçmiþini temizleyebilir
    //PlayerPrefs'ten CurrentLevel'ý 0'la ve resimleri yeniden ayarla
    public void DeleteLevelHistory()
    {
        PlayerPrefs.SetInt("MaximumSeviye", 0);
        SetLevelImages();
    }
    #endregion

    #region Secili Sahneyi Yukle
    public void SahneYukle(int sceneIndex) // Gonderdiðimiz sahne indexini yükle
    {
        SceneManager.LoadScene(sceneIndex);
    }
    #endregion
}
