using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp93
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game gameTable = new Game();
            gameTable.Play();
        }
    }

    class Game
    {
        public void Play()
        {
            const string SelectCardCommand = "1";
            const string ExitProgrammCommand = "2";

            string userInput;
            bool isOpen = true;

            while (isOpen == true)
            {
                Console.WriteLine("Игра колода карт");
                Console.WriteLine($"{SelectCardCommand} - раздать карты");
                Console.WriteLine($"{ExitProgrammCommand} - выход из игры");
                Console.WriteLine("Выберите команду");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case SelectCardCommand:
                        DealCards();
                        break;
                    case ExitProgrammCommand:
                        isOpen = false;
                        break;
                    default:
                        Console.WriteLine("Введена неверная команда. Повторите попытку.");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                }

            }
        }

        public void DealCards()
        {
            CardDeck cardDeck = new CardDeck();

            Player playerDeck = new Player();

            int numberOfCards = cardDeck.CardsCount;
            int desiredNumberCard = ChooseNumberOfCards(numberOfCards);

            for (int i = 0; i < desiredNumberCard; i++)
            {
                playerDeck.TakeCard(cardDeck.GiveCard());
            }

            SelectDisplayOption(playerDeck, cardDeck);
        }

        private int ChooseNumberOfCards(int numberOfCards)
        {
            string userInput;

            int desiredNumberCard = 0;
            int minNumberCard = 0;
            bool isCorrectInput = false;

            while (isCorrectInput == false)
            {
                Console.Clear();
                Console.Write($"\nСколько карт от {minNumberCard} до {numberOfCards} вы хотите взять:");
                userInput = Console.ReadLine();

                if (int.TryParse(userInput, out desiredNumberCard) && desiredNumberCard > 0 && desiredNumberCard <= numberOfCards)
                {
                    isCorrectInput = true;
                }
                else
                {
                    Console.WriteLine("Ошибка. Повторите ввод");
                    Console.ReadKey();
                }
            }

            return desiredNumberCard;
        }

        private void SelectDisplayOption(Player playerDeck, CardDeck cardDeck)
        {
            const string ShowSelectCardsCommand = "1";
            const string ShowRemainingCardCommand = "2";
            const string ExitMenuProgramm = "3";

            string userInput;
            bool isOpen = true;

            while (isOpen)
            {
                Console.Clear();
                Console.WriteLine($"{ShowSelectCardsCommand} - Показать выданные карты");
                Console.WriteLine($"{ShowRemainingCardCommand} - Показать оставшийся карты");
                Console.WriteLine($"{ExitMenuProgramm} - Выйти из меню");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case ShowSelectCardsCommand:
                        playerDeck.ShowDealtCards();                      
                        break;
                    case ShowRemainingCardCommand:
                        cardDeck.ShowRemainingCards();
                        break;
                    case ExitMenuProgramm:
                        isOpen = false;
                        break;
                    default:
                        Console.WriteLine("Введена неверная команда. Повторите попытку");
                        Console.ReadKey();
                        break;
                }
            }

        }

        class CardDeck
        {
            private List<GameCard> _cardes = new List<GameCard>();
            private Random _random = new Random();

            public CardDeck()
            {
                CreateCardsList();
            }

            public int CardsCount => _cardes.Count;

            public GameCard GiveCard()
            {
                int index = _random.Next(0, _cardes.Count);
                GameCard card = _cardes[index];
                _cardes.Remove(card);
                return card;
            }

            public void ShowRemainingCards()
            {
                foreach (var card in _cardes)
                {
                    Console.WriteLine($"{card.Name} {card.Suit}");
                }

                Console.ReadKey();
            }

            public void CreateCardsList()
            {
                List<string> names = new List<string>() { "6", "7", "8", "9", "10", "Валет", "Дама", "Король", " Туз " };
                List<string> suits = new List<string>() { "Пики", "Крести", "Буби", "Черви" };

                for (int i = 0; i < names.Count; i++)
                {
                    for (int j = 0; j < suits.Count; j++)
                    {
                        _cardes.Add(new GameCard(names[i], suits[j]));
                    }
                }
            }
        }

        class GameCard
        {
            public GameCard(string name, string suit)
            {
                Name = name;
                Suit = suit;
            }

            public string Name { get; private set; }
            public string Suit { get; private set; }
        }

        class Player
        {
            private List<GameCard> _cards = new List<GameCard>();

            public void ShowDealtCards()
            {
                Console.WriteLine($"\nИгрок вытянул следующие карты:");

                foreach (var card in _cards)
                {
                    Console.WriteLine($"{card.Name} {card.Suit}");
                }

                Console.ReadKey();
            }

            public void TakeCard(GameCard card)
            {
                _cards.Add(card);
            }
        }   
    }
}
