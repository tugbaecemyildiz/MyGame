using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f; //D��man�n maximum can�n� tutmak i�in (Unity'den daha fazla can vermek isteyebiliriz diye serialize field)
    private float currentHealth; //azalacak can

    private void Start()
    {
        currentHealth = maxHealth; //Oyun ba�lad���nda �uanki can�n� maximum can�na e�itle
    }

    public void HasarAl(float hasar)
    {
        currentHealth -= hasar; // Gelen hasar� �uanki candan ��kart
        if (currentHealth <= 0) // e�er can� 0 yada 0'�n alt�na d��erse �ld�rme fonksyinunu �a��r
            Dead();
    }

    public void Dead() //Karakterin can� azal�nca �ld�rmek ve oyuncuya puan vermek i�in
    {
        Player.score += 5;
        Destroy(gameObject);
    }
}
