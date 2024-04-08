using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokemonFightController : PokemonFightBaseData
{
    

    public bool First;
    public int IndexPokemon;
    public PokemonInfoController PokemonInfoController;
    public RawImage RAWIMAGE_Pokemon;
    public TextMeshProUGUI TMP_LifePokemon;
    public TextMeshProUGUI TMP_NamePokemon;
    public int Life;
    public int maxLife;


    void Awake()
    {
        RAWIMAGE_Pokemon = gameObject.GetComponent<RawImage>();
        TMP_LifePokemon = gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {

        if (First)
        {
            IndexPokemon = PokemonInfoController.INT_PokemonChoosed1;
        }
        else
        {
            IndexPokemon = PokemonInfoController.INT_PokemonChoosed2;
        }
        
        RAWIMAGE_Pokemon.color = Color.white;
       Initialize(IndexPokemon); 
       TMP_NamePokemon.text = Name;
       RAWIMAGE_Pokemon.texture = ImagePokemon;
       maxLife = MaxLife;
    }
    
    
    public void Update()
    {
        GetPv(IndexPokemon);
        Life = MaxLife;
        TMP_LifePokemon.text = Life + "/" + maxLife + "Hp";
        if (Life > maxLife)
        {
            Life -= (Life - maxLife);

        }
        if (Life <= 0)
        {
            RAWIMAGE_Pokemon.color = Color.gray;
        }
    }
    
}
