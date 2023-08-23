using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlGainPointsText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textPoints;
    private float alpha = 1;
    // Start is called before the first frame update
    void Start()
    {
        textPoints.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        alpha -= 0.075f;

        transform.Translate(0, 20, 0);
        textPoints.color = new Color(textPoints.color.r, textPoints.color.g,
                                textPoints.color.b, alpha);

        if (alpha < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
