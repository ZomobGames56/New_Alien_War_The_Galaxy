using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Weapon
{
    public enum WeaponCategory
    {
        Melee,
        Pistol,
        Shotgun,
        MachineGun,
        Rifle
    }
    public int index;
    public Sprite image;
    public string name;
    public AudioClip audioClip;
    public WeaponCategory category;
    public int damage;
    public int range;
    public int fireRate;
    public int mazgine;
    public bool isAvailable;
    public int level;

    public Weapon(int index, Sprite image, AudioClip audioClip, string name , WeaponCategory category , int damage , int range , int fireRate , int mazgine , bool isAvailable , int level)
    {
        this.index = index;
        this.image = image;
        this.audioClip = audioClip;
        this.name = name;
        this.category = category;
        this.damage = damage;
        this.range = range;
        this.fireRate = fireRate;
        this.mazgine = mazgine;
        this.isAvailable = isAvailable;
        this.level = level;

    }
}
