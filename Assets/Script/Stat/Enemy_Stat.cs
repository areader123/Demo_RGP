using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stat : Character_Stat
{
    Enemy_Skeleton enemy_Skeleton;
    ItemDrop itemDropSystem;
    public int _strength;
    public int _damage;


    protected override void Start()
    {
        enemy_Skeleton = GetComponent<Enemy_Skeleton>();
        itemDropSystem = GetComponent<ItemDrop>();
        Modifier();
        base.Start();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        enemy_Skeleton.Damage();
    }
    protected override void Die()
    {
        base.Die();
       // enemy_Skeleton.stateMachine
       enemy_Skeleton.animator.speed = 0;
       enemy_Skeleton.GetComponent<CapsuleCollider2D> ().enabled = false;
       //enemy_Skeleton.SetVelocity()
       enemy_Skeleton.rb.gravityScale = 30;

       itemDropSystem.generateDrop();
    }

    public void Modifier () {
        strength.AddModifiers(_strength);
        damage.AddModifiers(_damage);
    }
   
}
