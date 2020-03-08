using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProfile : MonoBehaviour
{
    public Image[] sprites = new Image[4];

    public string name;

    public bool infected = false;

    public int spreadToday = 0;
}
