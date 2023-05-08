using UnityEngine;

public class BulletFly : MonoBehaviour
{
    [SerializeField] private float hiz = 10f; // Mermi h�z�
    [SerializeField] private float omur = 2f; // Mermi �mr� (saniye)
    private float zaman = 0f;

    private void Update()
    {
        transform.Translate(Vector3.right * hiz * Time.deltaTime); // Mermiyi ileri do�ru hareket ettir

        zaman += Time.deltaTime;
        if (zaman >= omur)
        {
            Destroy(gameObject); // Mermiyi �mr� doldu�unda sil
        }
    }

    //Rigidbody ile kontrol
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemies"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>(); // Enemy Scriptini ula�maya �al��
            if (enemy != null)// e�er null de�ilse ve script enemy'de varsa
            {
                enemy.HasarAl(50f); //hasar ver
            }
            Destroy(gameObject);

        }

        //Destroy(gameObject); //mermi herhangi bir nesneye �arpt�ysa sil
    }
}
