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
    [SerializeField] private GameObject finishPanel; //Sadece Son sahne için

    private void Awake()
    {
        backgroundMusic = GetComponent<AudioSource>();
        backgroundMusic.volume = volume;
    }

    private void Start()
    {
        menuPanelBestScoreText.text = "Best Score " + PlayerPrefs.GetInt("BestScore", 0).ToString(); // Oyun baþladýðýnda PlayerPrefs'teki BestScore'u çaðýr
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

    #region Yeniden Oynama Ýþlemleri 
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

    public static void RestartCurrentLevel() //Caný varken ölmek
    {
        isRestart = true;
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Player.score = 0; //Eðer ilk sahneyse scoru sýfýrla
        }
        else
        {
            Player.score = PlayerPrefs.GetInt("LevelScore"); //deðilse son sahnenin skorunu getir
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion

    #region Quit Ýþlemleri
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

    #region Level Ýþlemleri
    public static void LevelUp()
    {
        PlayerPrefs.SetInt("LevelScore", Player.score); //Geçilen sahnenin skorunun ayarlanma iþlemleri
        int gectigiSeviye = SceneManager.GetActiveScene().buildIndex + 1; //Sonraki seviye indexini getir ve tut

        if (gectigiSeviye >= SceneManager.sceneCountInBuildSettings) // Eðer sonraki seviye indexi build settings'teki sahnelerin sayýsýndan büyük ya da eþitse oyunu komple bitir
        {
            isGameOver = true;
            PlayerPrefs.SetInt("MaximumSeviye", gectigiSeviye - 1);//Oyunun son bölümünün indexini kaydet (yukarýda +1 yaptýðýmýz için -1 yaptýk)
        }
        else
        {
            PlayerPrefs.SetInt("MaximumSeviye", gectigiSeviye); // Sonraki seviyeye geçtiðinde seviyenin indexini kaydet
            SceneManager.LoadScene(gectigiSeviye); //Sonraki seviyeye geç
        }
    }

    #endregion

    #region Ses Ýþlemleri
    public void ChangeVolume(float value)
    {
        volume = value;
        backgroundMusic.volume = volume;
    }

    #endregion
    

}//class
