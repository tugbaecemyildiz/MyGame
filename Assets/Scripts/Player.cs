using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private isGround _isGround;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform foot;
    [SerializeField] private LayerMask layer;
    [SerializeField] Animator animator;
    public static int score = 0;
    [SerializeField] Text scoreText;
    [SerializeField] private GameObject restartPanel, startPanel, optionsPanel;
    [SerializeField] private GameObject[] canlar;
    [SerializeField] Text lastScoreText;
    public static bool isStart = false;
    private bool kosuyorMu = true;
    [SerializeField] private AudioSource fruitAudio, deathAudio;
    private LevelManager levelManager;


    [SerializeField] private float speed;
    [SerializeField] private float jump;
    [SerializeField] private bool yerdeMi;

    public static int maxHealth = 3;
    public static int currentHealth;

    private void Awake()
    {
        levelManager = optionsPanel.GetComponent<LevelManager>();// Option panel'deki LevelManager'a ulaþ
    }

    private void Start()
    {
        if (isStart == false) // Eðer oyun baþlamadýysa
        {
            PlayerPrefs.SetInt("Score", 0); //Score'u 0'la
            startPanel.SetActive(true); // Start paneli göster
        }

        if (GameManager.isRestart) // Eðer seviyeye restart yaparak (can bitmeden ölerek) geldiyse
        {
            isStart = true; // Oyun oynanýyor demek
            startPanel.SetActive(false);
        }
        else
        {
            currentHealth = maxHealth; //Eðer oyun restart ile deðil normal baþladýysa þuanki caný maksimum hale getir
        }

        SetHealths(); //Can göstergelerini ayarlayan fonksiyonu çaðýr
    }

    private void Update()
    {
        if (isStart != true)
        {
            return;
        }

        scoreText.text = score.ToString(); //Her karede score'u güncelle
    }

    private void FixedUpdate()
    {
        if (isStart != true)
        {
            return;
        }

        //Saða sola hareket etme
        float h = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * h, rb.velocity.y);

        Yon(h);
        Animate(h);
    }
    private void Animate(float h)
    {
        if (h != 0) //Eðer horizontal deðeri 0'dan farklýysa hareket ediyor demektir
        {
            kosuyorMu = true;
        }
        else
        {
            kosuyorMu = false;
        }
        animator.SetBool("kosuyorMu", kosuyorMu);// Animatordeki kosuyorMu deðiþkenini eþitle
    }

    #region Karakter Yön Ýþlemleri
    private void Yon(float h)
    {
        if (h > 0)//Saða gidiyorsa
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (h < 0)//sola gidiyorsa
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        #region Meyve Toplama
        if (collision.CompareTag("Fruits"))
        {
            score += 5;
            scoreText.text = score.ToString();
            Destroy(collision.gameObject);
            fruitAudio.Play();
        }
        #endregion

        #region Yere düþtüðünde ölme
        if (collision.CompareTag("death"))
        {
            Die();
        }
        #endregion

        #region Sonraki Seviye kupasýna deðdi mi
        if (collision.CompareTag("Finish"))
        {
            GameManager.LevelUp();
        }
        #endregion

        #region Düþmanýn kafasýna deðip öldürme
        if (collision.CompareTag("Enemies"))
        {
            Collider2D[] colliders = collision.GetComponents<Collider2D>();
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].enabled = false;

            }
            score += 10;
            scoreText.text = score.ToString();
            _isGround.Jump(5f);
            Destroy(collision.gameObject, 3f);
        }
        #endregion
    }

    private void OnCollisionEnter2D(Collision2D collision) //RigidBody2D
    {
        #region Düþmana çarpýp ölme
        if (collision.gameObject.CompareTag("Enemies"))
        {
            Die();
        }
        #endregion

    }

    public void Die()
    {
        currentHealth--; //Þuanki caný azalt
        SetHealths();// Canlarý güncelle

        if (PlayerPrefs.GetInt("Score", 0) < score) // Eðer score'um playerPrefsteki score'dan büyükse
        {
            PlayerPrefs.SetInt("Score", score); // playerprefse score'u kaydet
        }

        if (PlayerPrefs.GetInt("BestScore", 0) < PlayerPrefs.GetInt("Score", 0))  //Eðer þuanki score'um BestScore'dan büyükse
        {
            PlayerPrefs.SetInt("BestScore", PlayerPrefs.GetInt("Score", 0)); // BestScore'u score'a eþitle
        }

        //Sahnedeki GameManager'ý bul, restarPanel'deki score textini deðiþtir
        FindObjectOfType<GameManager>().restartPanelScoreText.text = "Score: " + PlayerPrefs.GetInt("Score", 0).ToString();

        if (currentHealth <= 0) //Eðer caným 0sa yada 0dan küçükse
        {
            isStart = false;
            GameManager.isRestart = false;
            restartPanel.SetActive(true);// restart panel'i göster

            deathAudio.Play();//ölme sesini çal
            animator.Play("death");//ölme animasyonu oynasýn
            PlayerPrefs.SetInt("LevelScore", 0);// geldiði sahnedeki score'u sýfýrla
            Destroy(gameObject, 1f);// 1 saniye sonra yok et
        }
        else// eðer can 0 olmadýysa sahneyi yeniden yükle
        {
            GameManager.RestartCurrentLevel();
        }
    }

    private void SetHealths()
    {
        for (int i = 0; i < canlar.Length; i++)//Bütün canlarý kapalý yap
        {
            canlar[i].SetActive(false);
        }

        for (int i = 0; i < currentHealth; i++)// Canlarý þuanki can sayýsýna gelene kadar aktif hale getir
        {
            canlar[i].SetActive(true);
        }
    }

    public void PlayGame()
    {
        isStart = true;
        startPanel.SetActive(false);
    }

    #region Options Panel Kodlarý
    public void OpenOptions() //Opstion panelini göster
    {
        levelManager.SetLevelImages(); //Sahnedeki LevelManager'i bul ve methodu çalýþtýr
        startPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions() //options panelini gizle
    {
        startPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }
    #endregion

}//class
