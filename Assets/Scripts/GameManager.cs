using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool isRestart = false;
    public static bool isGameOver = false;
    public static float volume = 0.5f;
    private AudioSource backgroundMusic;
    [SerializeField] public Text restartPanelScoreText, menuPanelBestScoreText, finishPanelLastScore;
    [SerializeField] private GameObject finishPanel; //Sadece Son sahne i�in

    private void Awake()
    {
        backgroundMusic = GetComponent<AudioSource>();
        backgroundMusic.volume = volume;
    }

    private void Start()
    {
        menuPanelBestScoreText.text = "Best Score " + PlayerPrefs.GetInt("BestScore", 0).ToString(); // Oyun ba�lad���nda PlayerPrefs'teki BestScore'u �a��r
    }

    private void Update()
    {
        if (isGameOver)
        {
            Player.isStart = false;
            finishPanel.SetActive(true);
            finishPanelLastScore.text = "Score: " + Player.score;
        }
    }

    #region Yeniden Oynama ��lemleri 
    public static void RestartGame() //(Try Again)
    {
        Player.isStart = false;
        isRestart = false;
        Player.score = 0;
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("LevelScore", 0);
        isGameOver = false;
        SceneManager.LoadScene(0);
    }

    public static void RestartCurrentLevel() //Can� varken �lmek
    {
        isRestart = true;
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Player.score = 0; //E�er ilk sahneyse scoru s�f�rla
        }
        else
        {
            Player.score = PlayerPrefs.GetInt("LevelScore"); //de�ilse son sahnenin skorunu getir
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion

    #region Quit ��lemleri
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

    #region Level ��lemleri
    public static void LevelUp()
    {
        PlayerPrefs.SetInt("LevelScore", Player.score); //Ge�ilen sahnenin skorunun ayarlanma i�lemleri
        int gectigiSeviye = SceneManager.GetActiveScene().buildIndex + 1; //Sonraki seviye indexini getir ve tut

        if (gectigiSeviye >= SceneManager.sceneCountInBuildSettings) // E�er sonraki seviye indexi build settings'teki sahnelerin say�s�ndan b�y�k ya da e�itse oyunu komple bitir
        {
            isGameOver = true;
            PlayerPrefs.SetInt("MaximumSeviye", gectigiSeviye - 1);//Oyunun son b�l�m�n�n indexini kaydet (yukar�da +1 yapt���m�z i�in -1 yapt�k)
        }
        else
        {
            PlayerPrefs.SetInt("MaximumSeviye", gectigiSeviye); // Sonraki seviyeye ge�ti�inde seviyenin indexini kaydet
            SceneManager.LoadScene(gectigiSeviye); //Sonraki seviyeye ge�
        }
    }

    #endregion

    #region Ses ��lemleri
    public void ChangeVolume(float value)
    {
        volume = value;
        backgroundMusic.volume = volume;
    }

    #endregion
    

}//class
