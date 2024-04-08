using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonFightBaseData : MonoBehaviour
{

  [SerializeField]  protected PokemonDataBase PokemonDataBase;
    protected string Name;
    protected int MaxLife;
    protected int PhysicalDamage;
    protected int PsychicDamage;
    protected Texture ImagePokemon;
    
    public void Awake()
    {
        print(PokemonDataBase);
    }
    public void Initialize(int INDEX)
    {
        InitInfos(INDEX);
        print(PokemonDataBase);
    }

    public void GetPv(int MyIndex)
    {
        MaxLife = PokemonDataBase.datas[MyIndex].stats.pv;
    }
    protected virtual void InitInfos(int MyIndex)
    {
        print("My Index : " +  MyIndex);
        if (PokemonDataBase != null)
        {
            Name = PokemonDataBase.datas[MyIndex].Name;
            MaxLife = PokemonDataBase.datas[MyIndex].stats.pv;
            PhysicalDamage = PokemonDataBase.datas[MyIndex].stats.atk;
            PsychicDamage = PokemonDataBase.datas[MyIndex].stats.atkSpe;
            ImagePokemon = PokemonDataBase.datas[MyIndex].Icon;
        }
        else
        {
            Debug.LogError("PokemonDataBase = null");
        }
    }
    



}
