using System.Collections.Generic; // List<> türü için
using BricKartOyunu.Models; // Bid ve Suit'in bulunduğu namespace

namespace BricKartOyunu
{
    public class BiddingSystem
    {
        public List<Bid> Bids { get; } = new List<Bid>();
        public Bid CurrentContract { get; private set; }

        public void MakeBid(Player player, int level, Suit trump)
        {
            var newBid = new Bid(level, trump, player);
            if (IsValidBid(newBid))
            {
                Bids.Add(newBid);
                CurrentContract = newBid;
            }
        }

        private bool IsValidBid(Bid newBid)
        {
            // Basit geçerlilik kontrolü: Yeni teklif öncekinden yüksek olmalı
            if (CurrentContract == null) return true;
            return newBid.Level > CurrentContract.Level ||
                   (newBid.Level == CurrentContract.Level && newBid.Trump > CurrentContract.Trump);
        }
    }
}