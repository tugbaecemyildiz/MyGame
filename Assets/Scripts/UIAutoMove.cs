using UnityEngine;

public class UIAutoMove : MonoBehaviour
{
    // EnemyAutoMove'un aynýsý ama sadece transform ile deðil recttransform ile iþlem yapýyor.
    // Çünkü Canvas'taki nesneler için yapýldý ve Canvas objelerinin transform'u deðil RectTransform'u olur
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
