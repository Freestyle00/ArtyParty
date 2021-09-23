using System;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Specialized;
using FlatRedBall.Audio;
using FlatRedBall.Screens;
using ArtyParty.Entities;
using ArtyParty.Entities.Projectiles;
using ArtyParty.Screens;
namespace ArtyParty.Screens
{
    public partial class GameScreen
    {
        void OnPlayerInstanceVsArmor_Piercing_roundListCollisionOccurred (ArtyParty.Entities.Player player, Entities.Projectiles.Armor_Piercing_round armor_Piercing_round) 
        {
            if(armor_Piercing_round.Collision.CollideAgainst(player.GunHolder))
            {
                
			}
        }
    }
}
