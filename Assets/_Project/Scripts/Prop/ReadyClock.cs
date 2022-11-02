﻿using GithubGameOff2022.Player;
using GithubGameOff2022.Translation;
using UnityEngine;

namespace GithubGameOff2022.Prop
{
    // Can be used at the start of a game, allow for a player to say he is ready
    public class ReadyClock : MonoBehaviour, IInteractible
    {
        public bool CanInterract(PlayerController player)
        {
            return !player.IsReady;
        }

        public void DoAction(PlayerController player)
        {
            player.IsReady = true;
            Debug.Log("An user is ready!");
        }

        public string GetInteractionName(PlayerController player) => Translate.Instance.Tr("clock in");
    }
}