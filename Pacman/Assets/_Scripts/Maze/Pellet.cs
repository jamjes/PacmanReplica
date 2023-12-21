using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom.Variables;

public class Pellet : MonoBehaviour
{
    [SerializeField] private Sprite _pelletSprite, _powerPelletSprite;
    public PelletType _type;
    private SpriteRenderer _spr;

    private void Start()
    {
        _spr = GetComponent<SpriteRenderer>();
        _spr.sprite = _type == PelletType.PELLET ? _pelletSprite : _powerPelletSprite;
    }
}
