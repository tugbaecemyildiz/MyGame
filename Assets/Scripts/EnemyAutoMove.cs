using UnityEngine;

public class EnemyAutoMove : MonoBehaviour
{
    //Unity'den, ba�lang�� ve hedef noktas�n�n position'lar�n� vermek i�in SerializeField de�i�kenler tan�mland�
    [SerializeField] private Vector3 baslangicNoktasi;
    [SerializeField] private Vector3 hedefNoktasi;
    [SerializeField] private float speed = 1f; // H�z�n� her nesenede de�i�tirmek istersek diye Unity'den de�i�tirmek i�in SerializeField tan�mland�

    [SerializeField] private bool hedefeGidiyorMu = true;

    private void Start()
    {
        transform.localPosition = baslangicNoktasi; //D��man�n oyun ba�lad���ndaki pozisyonu ba�lang�� noktas�na getirildi.
    }

    private void Update()
    {
        if (hedefeGidiyorMu == true) //E�er hedefe gidiyorsa
        {
            //MoveTowards: �uanki poziyonu verdi�imiz noktaya do�ru verdi�miz h�zda ilerlet
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, hedefNoktasi, speed * Time.deltaTime);
            transform.localScale = new Vector3(-1f, 1f, 1f); // E�er hedefe gidiyorsa (yani sa�a) scale'inin x'ini -1 yap
        }
        else //E�er ba�lang�ca geri d�n�yorsa
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, baslangicNoktasi, speed * Time.deltaTime);
            transform.localScale = new Vector3(1f, 1f, 1f); // sola gidiyorsa 1
        }

        //Distance: iki nokta aras�ndaki uzakl��� verir
        if (Vector3.Distance(transform.localPosition, hedefNoktasi) <= 0.05f) // e�er d��man�n pozisyonu hedefe �oook yak�nsa hedefeGidiyorMu'yu false yap ve geriye d�n
        {
            hedefeGidiyorMu = false;
        }

        if (Vector3.Distance(transform.localPosition, baslangicNoktasi) <= 0.05f)
        {
            hedefeGidiyorMu = true;
        }
    }
}
