using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Versioning;

using BricKartOyunu.Models;
using System.Linq;
using System.Collections.Generic;
using System.IO; // Dosya kontrolü için gerekli

namespace BricKartOyunu
{
    [SupportedOSPlatform("windows")]
    public partial class Form1 : Form
    {
        private Deck deck;

        public Form1()
        {
            InitializeComponent();
            InitializeUI(); // UI kontrollerini başlat
        }

        private void InitializeUI()
        {
            Button btnDeal = new()
            {
                Text = "Kartları Dağıt",
                Location = new Point(10, 10),
                Size = new Size(100, 30)
            };
            btnDeal.Click += BtnDeal_Click;
            Controls.Add(btnDeal);
        }

        private void BtnDeal_Click(object sender, EventArgs e)
        {
            deck = new Deck();

            // Kartları dağıt
            var southHand = deck.DealCards(13);
            var westHand = deck.DealCards(13);
            var northHand = deck.DealCards(13);
            var eastHand = deck.DealCards(13);

            // Kart sayısı doğrulama
            if (southHand.Count < 13 || westHand.Count < 13 || northHand.Count < 13 || eastHand.Count < 13)
            {
                MessageBox.Show("Kartlar eksik dağıtılıyor! Deck sınıfını kontrol et.");
                return;
            }

            // Kartları sırala
            var sortedSouthHand = SortHand(southHand);
            var sortedWestHand = SortHand(westHand);
            var sortedNorthHand = SortHand(northHand);
            var sortedEastHand = SortHand(eastHand);

            // Önceki kartları temizle
            ClearPreviousCards();

            // Kartları ekrana yerleştir
            PlaceHand(sortedSouthHand, 100, ClientSize.Height - 150, true);
            PlaceHand(sortedWestHand, 10, 100, false);
            PlaceHand(sortedNorthHand, 100, 10, true);
            PlaceHand(sortedEastHand, ClientSize.Width - 200, 100, false);
        }

        private List<Card> SortHand(List<Card> hand)
        {
            return hand
                .OrderBy(card => card.Suit == Suit.Spades ? 0 :
                                 card.Suit == Suit.Hearts ? 1 :
                                 card.Suit == Suit.Clubs ? 2 : 3)
                .ThenByDescending(card => card.Rank)
                .ToList();
        }

        private void PlaceHand(List<Card> hand, int startX, int startY, bool isHorizontal)
        {
            int x = startX;
            int y = startY;
            int spacing = 25; // Daha sıkı yerleşim için 30 yerine 25

            foreach (var card in hand)
            {
                if (!File.Exists(card.ImagePath))
                {
                    MessageBox.Show($"Kart resmi bulunamadı: {card.ImagePath}");
                    continue;
                }

                PictureBox pb = new PictureBox
                {
                    ImageLocation = card.ImagePath,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Width = 80,
                    Height = 120,
                    Location = new Point(x, y)
                };
                Controls.Add(pb);

                if (isHorizontal)
                {
                    x += spacing;
                    if (x > this.ClientSize.Width - 100) x = startX; // Ekran dışına taşmamak için
                }
                else
                {
                    y += spacing;
                    if (y > this.ClientSize.Height - 150) y = startY; // Ekran sınırlarını aşmamak için
                }
            }
        }

        private void ClearPreviousCards()
        {
            List<Control> toRemove = new List<Control>();

            foreach (Control control in Controls)
            {
                if (control is PictureBox)
                {
                    toRemove.Add(control);
                }
            }

            foreach (var control in toRemove)
            {
                Controls.Remove(control);
                control.Dispose();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde yapılacak işlemler buraya eklenebilir
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
