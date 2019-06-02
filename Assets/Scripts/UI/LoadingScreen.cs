using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    private bool isVisibleOnCam;
    private bool anyKeyPressed;
    // Start is called before the first frame update
    void Start()
    {
        isVisibleOnCam = true;
        anyKeyPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForKey();
        DestroyLoadingScreen();
    }

    private void CheckForKey()
    {
        if(Input.anyKey)
        {
            anyKeyPressed = true;
        }
    }

    private void DestroyLoadingScreen()
    {
        if (anyKeyPressed)
        {
            if (this.GetComponent<SpriteRenderer>().color.a > 0)
            {
                this.GetComponent<SpriteRenderer>().color -= Color.black * 0.01f;
            }
            else
            {
                Destroy(this.gameObject, 6.0f);
            }
        }
    }

    public bool returnVisibility()
    {
        return isVisibleOnCam;
    }
}
