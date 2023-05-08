using UnityEngine;

public class UIAutoMove : MonoBehaviour
{
    // EnemyAutoMove'un ayn�s� ama sadece transform ile de�il recttransform ile i�lem yap�yor.
    // ��nk� Canvas'taki nesneler i�in yap�ld� ve Canvas objelerinin transform'u de�il RectTransform'u olur
    [SerializeField] private Vector3 baslangicNoktasi;
    [SerializeField] private Vector3 hedefNoktasi;
    [SerializeField] private float speed = 1f;

    [SerializeField] private bool hedefeGidiyorMu = true;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = baslangicNoktasi;
    }

    private void Update()
    {
        if (hedefeGidiyorMu == true)
        {
            rectTransform.localPosition = Vector3.MoveTowards(rectTransform.localPosition, hedefNoktasi, speed * Time.deltaTime);
            rectTransform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            rectTransform.localPosition = Vector3.MoveTowards(rectTransform.localPosition, baslangicNoktasi, speed * Time.deltaTime);
            rectTransform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (Vector3.Distance(rectTransform.localPosition, hedefNoktasi) == 0)
        {
            hedefeGidiyorMu = false;
        }

        if (Vector3.Distance(rectTransform.localPosition, baslangicNoktasi) == 0)
        {
            hedefeGidiyorMu = true;
        }
    }
}
