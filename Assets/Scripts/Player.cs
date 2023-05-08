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
        levelManager = optionsPanel.GetComponent<LevelManager>();// Option panel'deki LevelManager'a ula�
    }

    private void Start()
    {
        if (isStart == false) // E�er oyun ba�lamad�ysa
        {
            PlayerPrefs.SetInt("Score", 0); //Score'u 0'la
            startPanel.SetActive(true); // Start paneli g�ster
        }

        if (GameManager.isRestart) // E�er seviyeye restart yaparak (can bitmeden �lerek) geldiyse
        {
            isStart = true; // Oyun oynan�yor demek
            startPanel.SetActive(false);
        }
        else
        {
            currentHealth = maxHealth; //E�er oyun restart ile de�il normal ba�lad�ysa �uanki can� maksimum hale getir
        }

        SetHealths(); //Can g�stergelerini ayarlayan fonksiyonu �a��r
    }

    private void Update()
    {
        if (isStart != true)
        {
            return;
        }

        scoreText.text = score.ToString(); //Her karede score'u g�ncelle
    }

    private void FixedUpdate()
    {
        if (isStart != true)
        {
            return;
        }

        //Sa�a sola hareket etme
        float h = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * h, rb.velocity.y);

        Yon(h);
        Animate(h);
    }
    private void Animate(float h)
    {
        if (h != 0) //E�er horizontal de�eri 0'dan farkl�ysa hareket ediyor demektir
        {
            kosuyorMu = true;
        }
        else
        {
            kosuyorMu = false;
        }
        animator.SetBool("kosuyorMu", kosuyorMu);// Animatordeki kosuyorMu de�i�kenini e�itle
    }

    #region Karakter Y�n ��lemleri
    private void Yon(float h)
    {
        if (h > 0)//Sa�a gidiyorsa
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

        #region Yere d��t���nde �lme
        if (collision.CompareTag("death"))
        {
            Die();
        }
        #endregion

        #region Sonraki Seviye kupas�na de�di mi
        if (collision.CompareTag("Finish"))
        {
            GameManager.LevelUp();
        }
        #endregion

        #region D��man�n kafas�na de�ip �ld�rme
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
        #region D��mana �arp�p �lme
        if (collision.gameObject.CompareTag("Enemies"))
        {
            Die();
        }
        #endregion

    }

    public void Die()
    {
        currentHealth--; //�uanki can� azalt
        SetHealths();// Canlar� g�ncelle

        if (PlayerPrefs.GetInt("Score", 0) < score) // E�er score'um playerPrefsteki score'dan b�y�kse
        {
            PlayerPrefs.SetInt("Score", score); // playerprefse score'u kaydet
        }

        if (PlayerPrefs.GetInt("BestScore", 0) < PlayerPrefs.GetInt("Score", 0))  //E�er �uanki score'um BestScore'dan b�y�kse
        {
            PlayerPrefs.SetInt("BestScore", PlayerPrefs.GetInt("Score", 0)); // BestScore'u score'a e�itle
        }

        //Sahnedeki GameManager'� bul, restarPanel'deki score textini de�i�tir
        FindObjectOfType<GameManager>().restartPanelScoreText.text = "Score: " + PlayerPrefs.GetInt("Score", 0).ToString();

        if (currentHealth <= 0) //E�er can�m 0sa yada 0dan k���kse
        {
            isStart = false;
            GameManager.isRestart = false;
            restartPanel.SetActive(true);// restart panel'i g�ster

            deathAudio.Play();//�lme sesini �al
            animator.Play("death");//�lme animasyonu oynas�n
            PlayerPrefs.SetInt("LevelScore", 0);// geldi�i sahnedeki score'u s�f�rla
            Destroy(gameObject, 1f);// 1 saniye sonra yok et
        }
        else// e�er can 0 olmad�ysa sahneyi yeniden y�kle
        {
            GameManager.RestartCurrentLevel();
        }
    }

    private void SetHealths()
    {
        for (int i = 0; i < canlar.Length; i++)//B�t�n canlar� kapal� yap
        {
            canlar[i].SetActive(false);
        }

        for (int i = 0; i < currentHealth; i++)// Canlar� �uanki can say�s�na gelene kadar aktif hale getir
        {
            canlar[i].SetActive(true);
        }
    }

    public void PlayGame()
    {
        isStart = true;
        startPanel.SetActive(false);
    }

    #region Options Panel Kodlar�
    public void OpenOptions() //Opstion panelini g�ster
    {
        levelManager.SetLevelImages(); //Sahnedeki LevelManager'i bul ve methodu �al��t�r
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
