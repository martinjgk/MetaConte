using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public int itemPrice;

    // 추가 속성
    public int defenseBonus;
    public int attackBonus;
    public int speedBonus;
    public int healthRestore;
    public int manaRestore;

    // 아이템 사용 메서드
    public virtual void Use(Player player)
    {
        // 각 아이템의 효과를 여기서 구현
        if (defenseBonus > 0)
        {
            player.IncreaseDefense(defenseBonus);
        }
        if (attackBonus > 0)
        {
            player.IncreaseAttack(attackBonus);
        }
        if (speedBonus > 0)
        {
            player.IncreaseSpeed(speedBonus);
        }
        if (healthRestore > 0)
        {
            player.RestoreHealth(healthRestore);
        }
        if (manaRestore > 0)
        {
            player.RestoreMana(manaRestore);
        }
    }
}
