



namespace TextGame
{

    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Gold { get; set; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
    }

    public class Item
    {
        public string Name { get; }
        public string Description { get; }
        public int Type { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Price { get; }

        public bool IsEquipped { get; set; }
        public bool GetItem {  get; set; }

        public Item(string name, string description, int type, int atk, int def, int hp, int price, bool isEquipped = false, bool getItem = false)
        {
            Name = name;
            Description = description;
            Type = type;
            Atk = atk;
            Def = def;
            Hp = hp;
            Price = price;
            IsEquipped = isEquipped;
            GetItem = getItem; 
        }


        public void PrintitemStatDescription(bool withNumber = false, int idx = 0)
        {
            Console.Write("-");
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("{0} ", idx);
                Console.ResetColor();
            }
            if (IsEquipped)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("E");
                Console.ResetColor();
                Console.Write("]");
                Console.Write(PadRightForMixedText(Name, 9));
            }
            else Console.Write(PadRightForMixedText(Name, 12));
            Console.Write(" | ");

            if (Atk != 0) Console.Write($"Atk {(Atk >= 0 ? "+ " + Atk : "")}");
            if (Def != 0) Console.Write($"Def {(Def >= 0 ? "+ " + Def: "")}");
            if (Hp != 0) Console.Write($"Hp {(Hp >= 0 ? "+ " + Hp : "")}");

            Console.Write(" | ");
            Console.WriteLine(Description);
        }

        public void SoldOutItem(bool withNumber = false, int idx =0)
        {
            Console.Write("-");
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("{0} ", idx);
                Console.ResetColor();
            }
            if (GetItem)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("S");
                Console.ResetColor();
                Console.Write("]");
                Console.Write(PadRightForMixedText(Name, 9));
            }
            else Console.Write(PadRightForMixedText(Name, 20));
            Console.Write(" | ");

            if (Atk != 0) Console.Write($"Atk {(Atk >= 0 ? "+ " + Atk : "")}");
            if (Def != 0) Console.Write($"Def {(Def >= 0 ? "+ " + Def : "")}");
            if (Hp != 0) Console.Write($"Hp {(Hp >= 0 ? "+ " + Hp : "")}");

            Console.Write(" | ");
            Console.Write(Price);
            Console.Write(" | ");
            Console.WriteLine(Description);
        }

        public static int GetPrinttableLength(string str)
        {
            int length = 0;
            foreach (char c in str)
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    length += 2;
                }
                else
                {
                    length += 1;
                }
            }
            return length;
        }

        public static string PadRightForMixedText(string str, int totalLength)
        {
            int currentLength = GetPrinttableLength(str);
            int padding = totalLength - currentLength;
            return str.PadRight(str.Length + padding);
        }
       
    }

    internal class Program
    {
        static string playerName;
        static Character _player;
        static List<Item> myItem = new List<Item>();
        static List<Item> shopItem = new List<Item>();

        static void Main(string[] args)
        {
            GameDataSetting();
            PrintStartLogo();
            StartMenu();
        }

        static void StartMenu()
        {
            Console.Clear();
            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
            Console.WriteLine("");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("");

            switch (CheckValidinput(1, 3))
            {
                case 1:
                    StatusMenu();
                    break;
                case 2:
                    InventoryMenu();
                    break;
                case 3:
                    ShopMenu();
                    break;
            }
        }

        private static void StatusMenu()
        {
            Console.Clear();

            ShowHighlightedText("■ 상태 보기 ■");
            Console.WriteLine("캐릭터의 정보가 표기됩니다.");

            PrintTextWithHighiights("LV. ", _player.Level.ToString("00"));  // 01, 07;
            Console.WriteLine("");
            Console.WriteLine("{0}, {1}", _player.Name, _player.Job);

            int bonusAtk = getSumBonusAtr();
            int bonusDef = getSumBonusDef();
            int bonusHp = getSumBonusHp();
            PrintTextWithHighiights("공격력 : ", (_player.Atk + bonusAtk).ToString(), bonusAtk > 0 ? string.Format(" (+{0})", bonusAtk) : "");
            PrintTextWithHighiights("방어력 : ", (_player.Atk + bonusDef).ToString(), bonusDef > 0 ? string.Format(" (+{0})", bonusDef) : "");
            PrintTextWithHighiights("체  력 : ", (_player.Atk + bonusHp).ToString(), bonusHp > 0 ? string.Format(" (+{0})", bonusHp) : "");
            PrintTextWithHighiights("골  드 : ", _player.Gold.ToString());
            Console.WriteLine("");
            Console.WriteLine("0. 뒤로가기");
            Console.WriteLine("");
            switch (CheckValidinput(0, 0))
            {
                case 0:
                    StartMenu();
                    break;
            }

        }

        private static int getSumBonusAtr()
        {
            int sum = 0;
            for (int i = 0; i < myItem.Count; i++)
            {
                if (myItem[i].IsEquipped) sum += myItem[i].Atk;
            }
            return sum;
        }

        private static int getSumBonusDef()
        {
            int sum = 0;
            for (int i = 0; i < myItem.Count; i++)
            {
                if (myItem[i].IsEquipped) sum += myItem[i].Def;
            }
            return sum;
        }

        private static int getSumBonusHp()
        {
            int sum = 0;
            for (int i = 0; i < myItem.Count; i++)
            {
                if (myItem[i].IsEquipped) sum += myItem[i].Hp;
            }
            return sum;
        }

        private static void InventoryMenu()
        {
            Console.Clear();

            ShowHighlightedText("■ 인벤토리 ■");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < myItem.Count; i++)
            {
                myItem[i].PrintitemStatDescription();
            }
            Console.WriteLine(" ");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 장착관리");
            Console.WriteLine("");
            switch (CheckValidinput(0, 1))
            {
                case 0:
                    StartMenu();
                    break;
                case 1:
                    EquiipMenu();
                    break;
            }
        }

        private static void EquiipMenu()
        {
            Console.Clear();

            ShowHighlightedText("■ 인벤토리 ■");
            Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < myItem.Count; i++)
            {
                myItem[i].PrintitemStatDescription(true, i + 1);
            }
            Console.WriteLine(" ");
            Console.WriteLine("0. 나가기");

            int keyinput = CheckValidinput(0, myItem.Count);

            switch (keyinput)
            {
                case 0:
                    InventoryMenu();
                    break;
                default:
                    ToggleEquipStatus(keyinput - 1);
                    EquiipMenu();
                    break;
            }
        }

        private static void ShopMenu()
        {
            Console.Clear();

            ShowHighlightedText("■ 상점 ■");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[보유골드]");
            Console.WriteLine($"{_player.Gold} G");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < shopItem.Count; i++)
            {
                shopItem[i].SoldOutItem();
            }
            Console.WriteLine(" ");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 구매하기");

            int keyinput = CheckValidinput(0, myItem.Count);

            switch (keyinput)
            {
                case 0:
                    StartMenu();
                    break;
                case 1:
                    ShopBuyMenu();
                    break;
            }

        }

        private static void ShopBuyMenu()
        {
            Console.Clear();

            ShowHighlightedText("■ 상점 ■");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[보유골드]");
            Console.WriteLine($"{_player.Gold} G");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < shopItem.Count; i++)
            {
                shopItem[i].SoldOutItem(true, i + 1);
            }
            Console.WriteLine(" ");
            Console.WriteLine("0. 나가기");

            int keyinput = CheckValidinput(0, myItem.Count);

            switch (keyinput)
            {
                case 0:
                    StartMenu();
                    break;
                default:
                    BuyItem(keyinput - 1);
                    ShopBuyMenu();
                    break;
            }

        }

        private static void BuyItem(int idx)
        {
            if (_player.Gold > shopItem[idx].Price)
            {
                myItem.Add(shopItem[idx]);
                shopItem[idx].GetItem = true;
                _player.Gold -= shopItem[idx].Price;
            }

            else
            {
                Console.WriteLine("Gold가 부족합니다.");
            }
        }

        
        private static void ToggleEquipStatus(int idx)
        {
            myItem[idx].IsEquipped = !myItem[idx].IsEquipped;
        }

        private static void ShowHighlightedText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private static void PrintTextWithHighiights(string s1, string s2, string s3 = "")
        {
            Console.Write(s1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(s2);
            Console.ResetColor();
            Console.WriteLine(s3);
        }

        private static int CheckValidinput(int min, int max)
        {
            int keyinput;
            bool resuit;
            do
            {
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                resuit = int.TryParse(Console.ReadLine(), out keyinput);
            }
            while (resuit == false || CheckIfValid(keyinput, min, max) == false);

            return keyinput;
        }

        private static bool CheckIfValid(int keyinput, int min, int max)
        {
            if (min <= keyinput && max >= keyinput) return true;
            return false;
        }

        private static void PrintStartLogo()
        {
            Console.Write("사용할 닉네임을 입력해주세요 : ");
            playerName = Console.ReadLine();
        }

        private static void GameDataSetting()
        {
            _player = new Character(playerName, "전사", 1, 5, 10, 100, 1500);
            myItem.Add(new Item("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 0, 9, 0, 1500, false, true));
            myItem.Add(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 1, 2, 0, 0, 600, false, true));

            shopItem.Add(new Item("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 0, 0, 5, 0, 1000));
            shopItem.Add(new Item("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 0, 9, 0, 1500));
            shopItem.Add(new Item("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 0, 0, 15, 0, 3500));
            shopItem.Add(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", 1, 2, 0, 0, 600));
            shopItem.Add(new Item("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", 1, 5, 0, 0, 1500));
            shopItem.Add(new Item("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 1, 7, 0, 0, 3000));

        }
    }
}