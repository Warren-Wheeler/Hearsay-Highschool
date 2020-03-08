using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    public SpriteRenderer pupils = null, skin= null, top= null, bottom= null;
    public TextMesh textMesh;

    int myID;

    public void Die()
    {
        Destroy(this.gameObject);
    }

    public void SetID(int ID)
    {
        myID = ID;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Interaction"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().StartDialogue(myID);
        }
    }
}
