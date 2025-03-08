using UnityEngine;
using TMPro;

public class PlayerHealt : MonoBehaviour
{
    public int currentHealt;
    public int maxHealt;

    public TMP_Text healthText;
    public Animator healtTextAnimator;
    

    private void Start()
    {
        healthText.text = "HP: " + currentHealt + " / " + maxHealt;
    }

    public void CheangeHealt(int amount)
    {
        currentHealt += amount;
        healthText.text = "HP: " + currentHealt + " / " + maxHealt;
        healtTextAnimator.Play("TextAnimation");    
        
        if(currentHealt <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}