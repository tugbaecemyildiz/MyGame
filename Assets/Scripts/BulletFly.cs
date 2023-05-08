using UnityEngine;

public class BulletFly : MonoBehaviour
{
    [SerializeField] private float hiz = 10f; // Mermi hýzý
    [SerializeField] private float omur = 2f; // Mermi ömrü (saniye)
    private float zaman = 0f;

    private void Update()
    {
        transform.Translate(Vector3.right * hiz * Time.deltaTime); // Mermiyi ileri doðru hareket ettir

        zaman += Time.deltaTime;
        if (zaman >= omur)
        {
            Destroy(gameObject); // Mermiyi ömrü dolduðunda sil
        }
    }

    //Rigidbody ile kontrol
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemies"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>(); // Enemy Scriptini ulaþmaya çalýþ
            if (enemy != null)// eðer null deðilse ve script enemy'de varsa
            {
                enemy.HasarAl(50f); //hasar ver
            }
            Destroy(gameObject);

        }

        //Destroy(gameObject); //mermi herhangi bir nesneye çarptýysa sil
    }
}
