using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PlayerBlackHoleState : PlayerState
{
    private bool canUseSkill;
    private float flyTime = .4f;

    private float defaultGravityScale;
    public PlayerBlackHoleState(string _animboolname, PlayStateMachine _stateMachine, Player _player) : base(_animboolname, _stateMachine, _player)
    {
    }
    

     public override void Enter(){
        base.Enter();
        defaultGravityScale = player.rb.gravityScale;
        canUseSkill = true;
        StateTimer = flyTime;
        player.rb.gravityScale = 0;
     }

      public override void Exit(){
        base.Exit();
        player.rb.gravityScale = defaultGravityScale;
        player.MakeTransparent(false);
        
      }

      public override void Update(){
        base.Update();
        if(StateTimer > 0){
            player.rb.velocity = new Vector2(0,15);
        }
        if(StateTimer < 0 ){
            player.rb.velocity = new Vector2(0,-0.5f);
        }



        if(canUseSkill){
            if( SkillManger.Instance.blackHole.CanUseSkill())
            {            
                canUseSkill = false;
            }
        }

        if(SkillManger.Instance.blackHole.BlackHoleFinished()){
            stateMachine.ChangeState(player.AirState);
        }
      }

      private void BlackHoleUseSkill(){
        SkillManger.Instance.blackHole.CanUseSkill();
      }

      
}
