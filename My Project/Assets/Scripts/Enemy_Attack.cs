using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    public int danno = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        collision.gameObject.GetComponent<PlayerHealt>().CheangeHealt(-danno);
    }
}
