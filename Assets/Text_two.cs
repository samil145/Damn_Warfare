using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_two : MonoBehaviour
{
    //goal gl;
    public Text txt;
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Revolver.additionalCount == 0)
        {
            txt.text = "";
        } else
        {
            txt.text = "" + Revolver.additionalCount;
        }
    }
}
