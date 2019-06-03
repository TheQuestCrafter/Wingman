using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectSound : MonoBehaviour
{

    [SerializeField]
    private EventSystem eventSystem;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private GameObject firstSelected;

    private GameObject gameObject;

    // Start is called before the first frame update
    void Start()
    {
        gameObject = eventSystem.firstSelectedGameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(eventSystem.currentSelectedGameObject != gameObject)
        {
            audioSource.Play();
        }
        gameObject = eventSystem.currentSelectedGameObject;
    }

    public void ReEnableMenu()
    {
        eventSystem.SetSelectedGameObject(firstSelected);
    }
}
