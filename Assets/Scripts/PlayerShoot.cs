using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject mermiPrefab;
    [SerializeField] private AudioSource gunShotSound;

    void Update()
    {
        if (Player.isStart != true)
        {
            return;
        }

        //Eðer mouse 0a basýldýysa 
        if (Input.GetMouseButtonDown(0))
        {
            GameObject mermi = Instantiate(mermiPrefab, transform.position, Quaternion.identity); //Ateþ edilecek mermiyi oluþtur
            gunShotSound.Play();//Ateþ sesini çal

            if (transform.parent.localScale.x == -1) //Eðer karakter sola bakýyorsa 
            {
                mermi.transform.eulerAngles = new Vector3(0f, 180f, 0f); //ateþin rotasyonunu deðiþtir
            }
        }
    }
}//class
