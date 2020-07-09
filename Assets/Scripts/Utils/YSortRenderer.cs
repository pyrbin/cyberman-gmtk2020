using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class YSortRenderer : MonoBehaviour
{
    [SerializeField] Transform Reference = null;
    [Space]
    [Header("Settings")]
    [SerializeField] int sortingOrderBase = 5000;
    [SerializeField] float offset = 0;
    [SerializeField] bool isStatic = false;

    private float timer;
    private float timerMax = .1f;
    private Renderer re;

    // Start is called before the first frame update
    void Awake()
    {
        if (!Reference) Reference = this.transform;
        re = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = timerMax;
            re.sortingOrder = (int)(sortingOrderBase - transform.position.y - offset);
            if (isStatic) Destroy(this);
        }
    }
}
