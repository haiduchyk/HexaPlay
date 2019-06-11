using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Access : MonoBehaviour
{
    public void Active() => gameObject.SetActive(true);   
    public void Inaccessible() => gameObject.SetActive(false);
}
