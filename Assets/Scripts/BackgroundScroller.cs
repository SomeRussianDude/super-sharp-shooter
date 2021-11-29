using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    //Config params
    [SerializeField] private float scrollSpeed = 0.5f;
    
    //Cached references
    private Material myMaterial;
    private Vector2 offSet;
    
    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offSet = new Vector2(0f, scrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        ScrollBackground();
    }

    private void ScrollBackground()
    {
        myMaterial.mainTextureOffset += Time.deltaTime * offSet;
    }
}
