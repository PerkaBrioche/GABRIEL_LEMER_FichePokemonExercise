using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutoDespawn : MonoBehaviour
{
    public float LifeDuration;
    public float Timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer < LifeDuration)
        {
            Timer += Time.deltaTime;
            if (Timer >= 1.5f)
            {
                float ColorAlpha = LifeDuration - Timer;
                gameObject.GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, ColorAlpha / 5);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
