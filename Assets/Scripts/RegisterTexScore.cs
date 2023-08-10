using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RegisterTexScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameData.singleton.textScore = GetComponent<TextMeshProUGUI>();
    }

}
