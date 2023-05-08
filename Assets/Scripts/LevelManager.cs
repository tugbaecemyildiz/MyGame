using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levels; //GameObject tipinde bir levels liste olu�turuldu
    public static int maximumLevelIndex; //Maksimum geldi�i seviyenin indexi tutuldu

    #region Seviye Resimlerini Ayarla
    public void SetLevelImages()
    {
        //�uanki seviyeyi playerprefs'ten al (her seviye ge�ti�mizde g�ncelleniyor)
        maximumLevelIndex = PlayerPrefs.GetInt("MaximumSeviye");

        //Seviyeler listesini gez, �uanki seviyenin index'inden k���kleri a� b�y�kleri yani sonrakileri kapat
        //GetComponent ile button'a ula� ve interactable �zelli�ini yani bas�l�p bas�lamayaca��n� ayarla
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

    #region Seviye Ge�mi�ini Sil
    //E�er kullan�c� isterse seviye ge�mi�ini temizleyebilir
    //PlayerPrefs'ten CurrentLevel'� 0'la ve resimleri yeniden ayarla
    public void DeleteLevelHistory()
    {
        PlayerPrefs.SetInt("MaximumSeviye", 0);
        SetLevelImages();
    }
    #endregion

    #region Secili Sahneyi Yukle
    public void SahneYukle(int sceneIndex) // Gonderdi�imiz sahne indexini y�kle
    {
        SceneManager.LoadScene(sceneIndex);
    }
    #endregion
}
