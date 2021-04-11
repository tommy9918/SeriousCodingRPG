using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterManager : MonoBehaviour
{
    public Monster monster;
    public SpriteRenderer sprite;
    public Image HP_Bar;
    public int health;
    public int max_health;
    public int level;
    public int EXP;
    public int ATK;
    public int DEF;
    public int gold;
    public int interval;
    int current = 0;
    // Start is called before the first frame update
    void Start()
    {
        //InitializeMonster();
    }

    // Update is called once per frame
    void Update()
    {
        if (!transform.parent.GetComponent<BattleManager>().paused)
        {
            if (current < interval)
            {
                current++;
            }
            else if (current == interval)
            {
                attack();
                current = 0;
            }
        }
    }

    public void InitializeMonster()
    {
        sprite.sprite = monster.sprite;
        level = monster.level;
        health = max_health = monster.health;
        ATK = monster.ATK;
        DEF = monster.DEF;
        interval = monster.attack_interval;
        GetComponent<FadeControl>().StartFadeIn();
        GetComponent<ScaleChange>().StartAnimate();

        Vector3 pos = transform.localPosition;
        GetComponent<MoveTo>().startPosition = new Vector2(pos.x, pos.y + 3.5f);
        GetComponent<MoveTo>().destination = pos;
        GetComponent<MoveTo>().ReplayStartEndMotion();

        UpdateMonster();
    }

    public void UpdateMonster()
    {
        HP_Bar.fillAmount = (float)health / max_health;
    }

    [ContextMenu("Attack")]
    public void attack()
    {
        //attack animation
        int real_attack = ATK - Player.Instance.DEF;
        if(real_attack <= 5)
        {
            real_attack = Random.Range(1, 6);
        }
        transform.parent.GetComponent<BattleManager>().damage(real_attack);
        //Debug.Log("Monster attacked with " + real_attack.ToString() + " damage!");
    }

    public void damage(int damage_amt)
    {
        health -= damage_amt;
        if (health <= 0)
        {
            health = 0;
            die();
        }
        UpdateMonster();
    }

    public void die()
    {
        Debug.Log("Monster die!");
        GetComponent<FadeControl>().StartFadeOut();
        Destroy(gameObject, 1f);
        //BattleManager.Instance.paused = true;
        BattleManager.Instance.ToNextStage();
    }
}
