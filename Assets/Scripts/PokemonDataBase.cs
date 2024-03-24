using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "Pokemon", menuName = "DataBase", order = 0)]
public class PokemonDataBase : ScriptableObject
{
    public List<PokemonData> datas = new();

    public void CreateData()
    {
        
    }
}
