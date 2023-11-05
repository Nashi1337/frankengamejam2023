using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZSorter : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Vector3 _worldPosition;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _worldPosition = transform.position;
        _spriteRenderer.sortingOrder = (int)(transform.position.y * -100f);
    }
}
