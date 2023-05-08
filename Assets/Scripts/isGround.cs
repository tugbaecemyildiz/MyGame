using UnityEngine;

public class isGround : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] bool yerdeMiyiz = true;
    [SerializeField] private float jumpSpeed = 15f;
    [SerializeField] private float rayDistance;
    [SerializeField] Animator animator;


    private void Update()
    {
        Animate(); 
        if (Player.isStart != true)
        {
            return; //Eðer oyun baþlamadýysa alttaki kodlarý çalýþtýrma
        }

        RaycastHit2D carpiyorMu = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, layer); //yere deðmek için atýlan ýþýn iþlemleri
        Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.red); //ýþýný sahnede görmek için parametreler; baslangýç nok.,yön,renk

        if (carpiyorMu.collider != null)
        {
            yerdeMiyiz = true;
        }
        else
        {
            yerdeMiyiz = false;
        }

        if (yerdeMiyiz == true && Input.GetKeyDown(KeyCode.Space)) //Eðer yerdeMiyiz true'ysa ve space'e basýyorsam zýpla
        {
            Jump(jumpSpeed);
        }

       
    }
    private void Animate()
    {
        animator.SetBool("yerdeMiyiz", yerdeMiyiz); //Animatordeki yerdeMiyiz'i deðiþtir
    }

    public void Jump(float jumpForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); //rigitbodynin velocitysini x sabit tutup ysini verilen güç kadar deðiþtir
    }

}//class
