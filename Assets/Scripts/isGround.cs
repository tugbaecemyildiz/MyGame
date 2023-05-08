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
            return; //E�er oyun ba�lamad�ysa alttaki kodlar� �al��t�rma
        }

        RaycastHit2D carpiyorMu = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, layer); //yere de�mek i�in at�lan ���n i�lemleri
        Debug.DrawRay(transform.position, Vector3.down * rayDistance, Color.red); //���n� sahnede g�rmek i�in parametreler; baslang�� nok.,y�n,renk

        if (carpiyorMu.collider != null)
        {
            yerdeMiyiz = true;
        }
        else
        {
            yerdeMiyiz = false;
        }

        if (yerdeMiyiz == true && Input.GetKeyDown(KeyCode.Space)) //E�er yerdeMiyiz true'ysa ve space'e bas�yorsam z�pla
        {
            Jump(jumpSpeed);
        }

       
    }
    private void Animate()
    {
        animator.SetBool("yerdeMiyiz", yerdeMiyiz); //Animatordeki yerdeMiyiz'i de�i�tir
    }

    public void Jump(float jumpForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); //rigitbodynin velocitysini x sabit tutup ysini verilen g�� kadar de�i�tir
    }

}//class
