using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PokemonData
{
    [System.Serializable]
    public struct Stats
    {
        public int pv;
        public int atk;
        public int def;
        public int atkSpe;
        public int defSpe;
        public int speed;


        public Stats InitRandomStat()
        {
            Stats stats = new();
            
            stats.pv = Random.Range(20, 99);
            stats.atk = Random.Range(10, 40);
            stats.def = Random.Range(5, 99);
            stats.atkSpe = Random.Range(10, 40);
            stats.defSpe = Random.Range(5, 99);
            stats.speed = Random.Range(5, 30);
            return stats;
        }
        
    }
    
    
    
    
    public string Name;
    public float size;
    public float weight;
    public string caption;
    public Texture type;
    public Texture Icon;
    public Texture Sexe;
    public Stats stats;
    



    public PokemonData(string name, int number, float size, float weight, Texture icon, string caption, Texture sexe, Texture type,
        Stats stats)
    {
        this.Name = name;
        this.size = size;
        this.weight = weight;
        this.Icon = icon;
        this.caption = caption;
        this.stats = stats;
    }


    
    
    

}
