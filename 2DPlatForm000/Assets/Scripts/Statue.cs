using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statue : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogBoxText;
    public string signText;
    private bool isPlayerInSign;
    void Start()
    {
        
    }
    void Update()
    {
        if (isPlayerInSign)
        {
            //dialogBoxText.text = signText;
            dialogBox.SetActive(true);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInSign = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isPlayerInSign = false;
            dialogBox.SetActive(false);
        }
    }
}
