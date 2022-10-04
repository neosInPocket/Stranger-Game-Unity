using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimScriptCut : MonoBehaviour
{
    [SerializeField] float sec;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Destroy(gameObject,sec); 
    }
}
