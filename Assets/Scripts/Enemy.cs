using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f; //Düþmanýn maximum canýný tutmak için (Unity'den daha fazla can vermek isteyebiliriz diye serialize field)
    private float currentHealth; //azalacak can

    private void Start()
    {
        currentHealth = maxHealth; //Oyun baþladýðýnda þuanki canýný maximum canýna eþitle
    }

    public void HasarAl(float hasar)
    {
        currentHealth -= hasar; // Gelen hasarý þuanki candan çýkart
        if (currentHealth <= 0) // eðer caný 0 yada 0'ýn altýna düþerse öldürme fonksyinunu çaðýr
            Dead();
    }

    public void Dead() //Karakterin caný azalýnca öldürmek ve oyuncuya puan vermek için
    {
        Player.score += 5;
        Destroy(gameObject);
    }
}
