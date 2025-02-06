using System;
using System.Collections.Generic; // List<> türü için
using System.Linq;
using BricKartOyunu.Models; // Player ve Suit'in bulunduğu namespace

namespace BricKartOyunu
{
    public class Game
    {
        public List<Player> Players { get; }
        public Suit Trump { get; }
        public Player Declarer { get; }

        public Game(Bid contract)
        {
            Trump = contract.Trump;
            Declarer = contract.Declarer;
            Players = new List<Player>(); // Oyuncuları buraya ata
        }

        public void PlayTrick()
        {
            // Her el için kart toplama mantığı
            List<Card> currentTrick = new List<Card>();
            foreach (var player in Players)
            {
                Card playedCard = player.PlayCard();
                currentTrick.Add(playedCard);
            }
            DetermineTrickWinner(currentTrick);
        }

        private void DetermineTrickWinner(List<Card> trick)
        {
            // En yüksek trump veya lider renk kartını bul
            var winningCard = trick.OrderByDescending(c => c.Suit == Trump ? 1 : 0)
                                   .ThenByDescending(c => c.Rank)
                                   .First();
            Console.WriteLine($"Kazanan kart: {winningCard.Rank} of {winningCard.Suit}");
        }
    }
}