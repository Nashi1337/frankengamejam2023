using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZSorter : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -1000f);
    }
}
