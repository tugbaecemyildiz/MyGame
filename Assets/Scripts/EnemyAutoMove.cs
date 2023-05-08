using UnityEngine;

public class EnemyAutoMove : MonoBehaviour
{
    //Unity'den, baþlangýç ve hedef noktasýnýn position'larýný vermek için SerializeField deðiþkenler tanýmlandý
    [SerializeField] private Vector3 baslangicNoktasi;
    [SerializeField] private Vector3 hedefNoktasi;
    [SerializeField] private float speed = 1f; // Hýzýný her nesenede deðiþtirmek istersek diye Unity'den deðiþtirmek için SerializeField tanýmlandý

    [SerializeField] private bool hedefeGidiyorMu = true;

    private void Start()
    {
        transform.localPosition = baslangicNoktasi; //Düþmanýn oyun baþladýðýndaki pozisyonu baþlangýç noktasýna getirildi.
    }

    private void Update()
    {
        if (hedefeGidiyorMu == true) //Eðer hedefe gidiyorsa
        {
            //MoveTowards: þuanki poziyonu verdiðimiz noktaya doðru verdiðmiz hýzda ilerlet
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, hedefNoktasi, speed * Time.deltaTime);
            transform.localScale = new Vector3(-1f, 1f, 1f); // Eðer hedefe gidiyorsa (yani saða) scale'inin x'ini -1 yap
        }
        else //Eðer baþlangýca geri dönüyorsa
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, baslangicNoktasi, speed * Time.deltaTime);
            transform.localScale = new Vector3(1f, 1f, 1f); // sola gidiyorsa 1
        }

        //Distance: iki nokta arasýndaki uzaklýðý verir
        if (Vector3.Distance(transform.localPosition, hedefNoktasi) <= 0.05f) // eðer düþmanýn pozisyonu hedefe çoook yakýnsa hedefeGidiyorMu'yu false yap ve geriye dön
        {
            hedefeGidiyorMu = false;
        }

        if (Vector3.Distance(transform.localPosition, baslangicNoktasi) <= 0.05f)
        {
            hedefeGidiyorMu = true;
        }
    }
}
