namespace TextRPGGame
{
    internal class GameManager
    {
        public static Character character; // 현재 캐릭터           

        // 마을 스크립트별로 나눌것, 같은 공간 이름통일

        static void Main()
        {
            Data.Init(); // 게임데이터 추가
            Shop.Init();

            character = new Character();

            Console.Clear();
            Console.WriteLine(" ~~~ 텍스트 RPG 게임 테스트테스트 ~~~ ");
            Console.WriteLine("\n");
           // Thread.Sleep(1000);  // 1초 대기


            Console.WriteLine("1.처음부터");

            if ((File.Exists(SaveManager.path))) // 세이브데이터 있을때 불러오기 활성화
            {
                Console.WriteLine("2.불러오기");
                Console.WriteLine();
                Console.Write(">> ");

                switch (NextChoice(1, 2))
                {
                    case 1:
                        CharacterGenerate();
                        Map_Village();
                        break;
                    case 2:
                        SaveManager.Load();
                        Map_Village();
                        break;
                }
            }
            else
            {
                Console.WriteLine();
                Console.Write(">> ");

                switch (NextChoice(1, 1))
                {
                    case 1:
                        CharacterGenerate();
                        Map_Village();
                        break;
                }
            }
        }

        public static void CharacterGenerate()
        {
            Console.Clear();
            Console.WriteLine("캐릭터의 이름을 정해주세요. ( 10글자 이내 )");
            string name;

            while (true)
            {
                name = Console.ReadLine();

                if (name.Length <= 10)
                    break;

                Console.WriteLine("10글자 이내로 적어주세요.");
            }

            character.name = name;
        }

        public static void Map_Village()
        {
            Console.Clear();

            Console.WriteLine("~~~~ 스파르타 마을 ~~~~");
            Console.WriteLine();
            Console.WriteLine($"{character.name}님! 스파르타 마을에 도착했습니다.");

            Console.WriteLine("\n");
            Console.WriteLine("1.촌장집");
            Console.WriteLine("2.여관 (닉네임만 저장)");
            Console.WriteLine("3.장비상점");
            Console.WriteLine("4.고물상");
            Console.WriteLine("5.식당");

            Console.WriteLine("\n");
            Console.WriteLine("6.상태 보기");
            Console.WriteLine("7.인벤토리");

            Console.WriteLine("\n");
            Console.WriteLine("8.치트 - 돈 치트");
            Console.WriteLine("9.치트 - 템 치트");

            Console.WriteLine();
            Console.Write(">> ");

            switch (NextChoice(1, 9))
            {
                case 1:
                    Map_HeadmanHouse();
                    break;
                case 2:
                    Save();
                    break;
                case 3:
                    Shop.Visit(Shop.ShopName.장비상점);
                    break;
                case 4:  Shop.Sell();
                    //    Shop.Visit(Shop.ShopName.물약상점);
                    break;
                case 5:
                    Map_Restaurant();
                    break;
                case 6:
                    Status();
                    break;
                case 7:
                    Inventory();
                    break;
                case 8:
                    Cheatkey.ShowMeTheMoney();
                    break;
                case 9:
                    Cheatkey.ShowMeTheItem();
                    break;
            }
        }

        static void Status()
        {
            Console.Clear();

            Console.WriteLine("[    캐릭터 정보    ]");
            Console.WriteLine();
            Console.WriteLine($"이 름 : {character.name}");
            Console.WriteLine($"레 벨 : {character.level}");
            Console.WriteLine($"직 업 : {character.job.ToString()}");
            Console.WriteLine($"공격력 : {character.atk} (+{character.atkBuff})");
            Console.WriteLine($"방어력 : {character.def} (+{character.defBuff})");
            Console.WriteLine($"체 력 / 최대치 : {character.hp} / {character.maxHp} (+{character.maxHpfBuff})");
            Console.WriteLine($"마 나 / 최대치 : {character.mp} / {character.maxMp}");
            Console.WriteLine($"경험치 : {character.exp}exp");
            Console.WriteLine();
            Console.WriteLine($"소지금 : {character.gold}원");

            Console.WriteLine("\n");
            Console.WriteLine("0.나가기");
            Console.WriteLine();
            Console.Write(">> ");

            switch (NextChoice(0, 0))
            {
                case 0:
                default:
                    Map_Village();
                    break;
            }
        }
        static void InventoryScreen()
        {
            Console.Clear();
            Console.WriteLine("[    장비창    ]");
            Console.WriteLine();

            for (int i = 0; i < character.equip.Length; i++)
            {
                if (character.equip[i] != null)
                {
                    Console.WriteLine($"{((Data.EquipParts)i).ToString()} : {character.equip[i].name} | 능력치 : {character.equip[i].capacity}");
                }
                else
                    Console.WriteLine($"{((Data.EquipParts)i).ToString()} : ");
            }

            Console.WriteLine("\n");
            Console.WriteLine("[    인벤토리    ]");
            Console.WriteLine();


            for (int i = 0; i < character.inventory.slots.Length; i++)
            {
                if (character.inventory.slots[i].item != null)
                    Console.WriteLine($"{i + 1}. {character.inventory.slots[i].item.name} | 능력치 : {character.inventory.slots[i].item.capacity} | {character.inventory.slots[i].item.comment} | {character.inventory.slots[i].item.price}원 | {character.inventory.slots[i].count}개");
                else
                    Console.WriteLine($"{i + 1}. ");
            }
            Console.WriteLine("\n");
        }
        public static void Inventory()
        {
            InventoryScreen();

            Console.WriteLine("1.아이템 사용 & 장착");
            Console.WriteLine("2.장비 해제");
            Console.WriteLine("0.나가기");
            Console.WriteLine();
            Console.Write(">> ");

            switch (NextChoice(0, 2))
            {
                case 1:
                    Inventory_Use();
                    break;
                case 2:
                    Equip_Use();
                    break;
                case 0:
                    Map_Village();
                    break;
            }
        }

        static void Equip_Use()
        {
            InventoryScreen();

            Console.WriteLine("착용을 해제할 장비 번호를 입력해주세요");
            Console.WriteLine("1.머리 2.몸 3.악세");
            Console.WriteLine("0.나가기");
            Console.WriteLine();

            Console.Write(">> ");
            switch (NextChoice(0, 3))
            {
                case 1:
                    if (character.equip[0] != null)
                        character.equip[0].TakeOff();
                    Equip_Use();
                    break;
                case 2:
                    if (character.equip[1] != null)
                        character.equip[1].TakeOff();
                    Equip_Use();

                    break;
                case 3:
                    if (character.equip[2] != null)
                        character.equip[2].TakeOff();
                    Equip_Use();
                    break;
                case 0:
                    Map_Village();
                    break;
            }

        }

        static void Inventory_Use()
        {
            InventoryScreen();

            Console.WriteLine("사용할 아이템 번호를 입력해주세요");
            Console.WriteLine("0.나가기");
            Console.WriteLine();

            Console.Write(">> ");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int num))
                {
                    if (--num < 0)
                    {
                        Inventory();
                        break;
                    }

                    if (num < character.inventory.slots.Length)
                    {
                        if (character.inventory.Use(num))
                        {
                            Inventory();
                            break;
                        }
                    }

                    Console.WriteLine("아이템이 존재하지 않습니다.");
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }

        static void Map_HeadmanHouse() // 촌장집
        {
            Console.Clear();
            Console.WriteLine("~~~~ 촌장집 ~~~~");
            Console.WriteLine();
            Console.WriteLine("촌장 : 어서오게 젊은이 들어올땐 마음대로지만 나갈때도 마음대로란다.");
            Console.WriteLine("\n");

            Console.WriteLine("0.나가기");
            Console.WriteLine();
            Console.Write(">> ");

            switch (NextChoice(0, 0))
            {
                case 0:
                default:
                    Map_Village();
                    break;
            }
        }

        static void Save()
        {
            SaveManager.Save();
            Console.Clear();
            Console.WriteLine("~~~~ 여관 ~~~~");
            Console.WriteLine();
            Console.WriteLine("저장되었습니다");

            Console.WriteLine("\n");
            Console.WriteLine("0.나가기");
            Console.WriteLine();
            Console.Write(">> ");

            switch (NextChoice(0, 0))
            {
                case 0:
                default:
                    Map_Village();
                    break;
            }
        }

        static void Map_Restaurant() // 식당
        {
            Console.Clear();
            Console.WriteLine("~~~~ 르탄진사갈비 ZEP점 ~~~~");
            Console.WriteLine();
            Console.WriteLine("르탄진사갈비점원 : 200원에 무한리필로 즐길 수 있습니다!");
            Console.WriteLine("\n");
            Console.WriteLine($"현재 소지금 : {character.gold} ");
            Console.WriteLine();


            Console.WriteLine($"1.이용한다 ({-200}원 소모.)");
            Console.WriteLine("0.나가기");
            Console.WriteLine();
            Console.Write(">> ");

            switch (NextChoice(0, 1))
            {
                case 1:
                    Map_Restaurant_Use(character.Wallet(-200));
                    break;
                case 0:
                default:
                    Map_Village();
                    break;
            }
        }

        static void Map_Restaurant_Use(bool ok)
        {
            Console.Clear();

            if (ok)
            {
                character.hp = character.maxHp;
                character.mp = character.maxMp; 
                Console.WriteLine("~~~~ 르탄진사갈비 ZEP점 ~~~~");
                Console.WriteLine();
                Console.WriteLine("르탄진사갈비점원 : 또 방문해주세요!");
                Console.WriteLine();
                Console.WriteLine("체력과 마나가 모두 회복되었습니다.");
                Console.WriteLine($"현재 소지금 : {character.gold} ( {-200}원 )");
               
            }
            else
            {
                Console.WriteLine("~~~~ 르탄진사갈비 ZEP점 ~~~~");
                Console.WriteLine();
                Console.WriteLine("르탄진사갈비점원 : 돈이 부족하시다구요...? 다음에 다시 찾아주세요!!");
            }

            Console.WriteLine();

            Console.WriteLine("0.나가기");
            Console.WriteLine();
            Console.Write(">> ");

            switch (NextChoice(0, 0))
            {
                case 0:
                default:
                    Map_Village();
                    break;
            }
        }

        public static int NextChoice(int min, int max) // 최대값 포함됨
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out var ret))
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}

