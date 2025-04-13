using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu]
public class Data : ScriptableObject
{
    public int hpHero;
    public int currentHealth;
    public int countCoins;
    public bool DialogManager;
    public List<Gun> guns;
    public bool itemChachedl;
    public int damage = 1;
    public float destroyBullet = 0.4f;
    public bool YoucantShoot = false;
    public int countFuel = 0;
    public int BatBullet = 1;
    public int GranateCount;
    public int bossHp;
    public float speedHero;
}
