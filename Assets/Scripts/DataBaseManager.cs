using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public PokemonDataBase PokemonDataBase;
    public PokemonData PokemonData;

    public PokemonData GetData(int id) => PokemonDataBase.datas[id];
    
}
