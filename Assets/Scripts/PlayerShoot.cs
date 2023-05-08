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

        //E�er mouse 0a bas�ld�ysa 
        if (Input.GetMouseButtonDown(0))
        {
            GameObject mermi = Instantiate(mermiPrefab, transform.position, Quaternion.identity); //Ate� edilecek mermiyi olu�tur
            gunShotSound.Play();//Ate� sesini �al

            if (transform.parent.localScale.x == -1) //E�er karakter sola bak�yorsa 
            {
                mermi.transform.eulerAngles = new Vector3(0f, 180f, 0f); //ate�in rotasyonunu de�i�tir
            }
        }
    }
}//class
