using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokemonFightController : MonoBehaviour
{
    public Animator PokemonAnim;

    public bool First;
    public int IndexPokemon;
    public int OldLife;
    public DataBaseManager PokemonDataBaseManager;
    public PokemonInfoController PokemonInfoController;

    public RawImage RAWIMAGE_Pokemon;
    public TextMeshProUGUI TMP_LifePokemon;

    public int Life;
    public int MaxLife;
    
    // Start is called before the first frame update
    void Awake()
    {
        PokemonDataBaseManager = FindObjectOfType<DataBaseManager>();
        RAWIMAGE_Pokemon = gameObject.GetComponent<RawImage>();
        TMP_LifePokemon = gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Start()
    {
        RAWIMAGE_Pokemon.color = Color.white;
        if (First)
        {
            IndexPokemon = PokemonInfoController.INT_PokemonChoosed1;
        }
        else
        {
            IndexPokemon = PokemonInfoController.INT_PokemonChoosed2;
        }

        OldLife = PokemonDataBaseManager.PokemonDataBase.datas[IndexPokemon].stats.pv;
        MaxLife = PokemonDataBaseManager.PokemonDataBase.datas[IndexPokemon].stats.pv;
        
        RAWIMAGE_Pokemon.texture = PokemonDataBaseManager.PokemonDataBase.datas[IndexPokemon].Icon;
    }

    public void Update()
    {
        Life = PokemonDataBaseManager.PokemonDataBase.datas[IndexPokemon].stats.pv;
        if (Life <= 0)
        {
            RAWIMAGE_Pokemon.color = Color.gray;
        }


        TMP_LifePokemon.text = Life + "/" + MaxLife;
    }
}
