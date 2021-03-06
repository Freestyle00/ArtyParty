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
        void OnPlayerInstanceVsArmor_Piercing_roundListCollisionOccurredTunnel (ArtyParty.Entities.Player player, Entities.Projectiles.Armor_Piercing_round armor_Piercing_round) 
        {
            if (this.PlayerInstanceVsArmor_Piercing_roundListCollisionOccurred != null)
            {
                PlayerInstanceVsArmor_Piercing_roundListCollisionOccurred(player, armor_Piercing_round);
            }
        }
    }
}
