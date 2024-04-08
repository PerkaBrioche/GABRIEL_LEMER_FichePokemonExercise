using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
            
            stats.pv = Random.Range(50, 170);
            stats.atk = Random.Range(10, 30);
            stats.def = Random.Range(5, 99);
            stats.atkSpe = Random.Range(5, 25);
            stats.defSpe = Random.Range(5, 99);
            stats.speed = Random.Range(5, 30);
            return stats;
        }
        
    }
    
    
    
    
    public string Name;
    public float size;
    public float weight;
    [TextArea]public string caption;
    public Texture type;
    public Texture Icon;
    public Texture WeaknessImg;
   [Serializable] public enum Types
    {
        Eletrik,
        water,
        grass,
        fire,
        poison,
    }
   
    public Types MyType;
    public Types MyWeakness;

    public Stats stats;
    public PhysicalAttack[] PhysicalAttacksList;
    public PsychickAttack[] PsychickAttacksList;
    



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

    [System.Serializable]
    public class PhysicalAttack
    {
        public string AttackName;
        [TextArea] public string AttackCaption;
        public int AttackDamage;
        public int AttackID;
        public int AttackPrecision;
    }
    [System.Serializable]
    public class PsychickAttack
    {
        public string AttackName;
        [TextArea] public string AttackCaption;
        public int AttackDamage;
        public int AttackID;
        public int AttackPrecision;
    }


    
    
    

}
