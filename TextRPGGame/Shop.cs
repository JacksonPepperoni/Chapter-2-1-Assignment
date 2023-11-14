namespace TextRPGGame
{
    internal class Shop
    {
        public enum ShopName
        {
            장비상점,
            물약상점
        }
        static public ShopName shop = ShopName.장비상점;
        static InventorySlot[] itemCatalog;


        static public InventorySlot[] equipSale;
        static public InventorySlot[] consumSale;

        static Random random;

        public static void Init()
        {
            equipSale = new InventorySlot[6];

            for (int i = 0; i < equipSale.Length; i++)
            {
                InventorySlot inventorySlot = new InventorySlot();
                equipSale[i] = inventorySlot;
            }

            consumSale = new InventorySlot[5];


            for (int i = 0; i < consumSale.Length; i++)
            {
                InventorySlot inventorySlot = new InventorySlot();
                consumSale[i] = inventorySlot;
            }

            random = new Random();

            equipSale[0].item = Data.itemData[random.Next(0, 4)];
            equipSale[0].count = 2;
            equipSale[1].item = Data.itemData[random.Next(0, 4)];
            equipSale[1].count = 1;

            equipSale[2].item = Data.itemData[random.Next(4, 8)];
            equipSale[2].count = 4;
            equipSale[3].item = Data.itemData[random.Next(4, 8)];
            equipSale[3].count = 1;

            equipSale[4].item = Data.itemData[random.Next(8, 12)];
            equipSale[4].count = 1;
            equipSale[5].item = Data.itemData[random.Next(8, 12)];
            equipSale[5].count = 1;


            consumSale[0].item = Data.itemData[random.Next(12, 15)];
            consumSale[0].count = random.Next(70, 100);
            consumSale[1].item = Data.itemData[random.Next(12, 15)];
            consumSale[1].count = random.Next(70, 100);


            consumSale[2].item = Data.itemData[random.Next(15, 17)];
            consumSale[2].count = random.Next(70, 100);
            consumSale[3].item = Data.itemData[random.Next(15, 17)];
            consumSale[3].count = random.Next(70, 100);

            consumSale[4].item = Data.itemData[random.Next(17, 19)];
            consumSale[4].count = random.Next(20, 30);

        }

        static void Screen()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[           판매목록           ]");
            Console.WriteLine();

            for (int i = 0; i < itemCatalog.Length; i++)
            {
                if (itemCatalog[i].item != null)
                    Console.WriteLine($"{i+1}. {(itemCatalog[i].item.part).ToString()} | {itemCatalog[i].item.name} | {itemCatalog[i].item.capacity}상승 | {itemCatalog[i].item.comment} | {itemCatalog[i].item.price}원 | {itemCatalog[i].count}개");
                else
                    Console.WriteLine($"{i+1}. -----------------------  품절  -----------------------");
            }

            Console.WriteLine("\n");

        }

        static public void Visit(ShopName name)
        {
            switch (name)
            {
                case ShopName.장비상점:
                    itemCatalog = equipSale;
                    // 대사모음 넣어주기
                    break;

                case ShopName.물약상점:
                    itemCatalog = consumSale;
                    // 대사모음 넣어주기
                    break;
            }

            Open();
        }


        /*
         
        선택지 우려먹을 방법이 없나?
         
           
            상점 인터페이스 다 똑같으면 화면은 템만 바꾸는 식으로 우려먹고
            상점 주인 상황별 대사랑 그림만 바꿔서 단순한 구매판매 상점은 우려먹을수 있을듯.

            
            상점이름

            인삿말,

            물건산다 골랐을때 말

            구매했을때 말

            돈이 부족할때 말

            작별인사말

            
         
         */

        static void Open() 
        {
            Console.Clear();
            Console.WriteLine("대장장이 : 힘썌고 강한 아침. 크고 아름다운 장비를 팔고있지");
            Screen();

            Console.WriteLine("1. 구매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write(">> ");

            switch (GameManager.NextChoice(0, 1))
            {
                case 1:
                    BuyLoop();
                    break;
                case 0:
                    GameManager.Map_Village();
                    break;
            }

        }


        static void BuyLoop() // 상점 구매 판매는 어차피 다 똑같으니까 우려먹어도될듯 델리게이트만들어서 함수도 다르게 실행시키자
        {
            Console.Clear();
            Console.WriteLine("대장장이 : 어디 원하는걸 골라보슈!");
            Screen();

            Console.WriteLine("구매할 아이템의 숫자를 적고 엔터를 눌러주세요");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write(">> ");


            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int num))
                {
                    if (--num < 0)
                    {
                        Open();
                        break;
                    }

                    if ((GameManager.character.inventory.slotCount >= GameManager.character.inventory.maxSlot))
                    {
                        Console.WriteLine("인벤토리공간이 부족합니다");
                        break;
                    }


                    if (num < itemCatalog.Length)
                    {
                        if (itemCatalog[num].item != null)
                        {

                            if (GameManager.character.Wallet(-itemCatalog[num].item.price))
                            {
                                GameManager.character.inventory.Add(itemCatalog[num].item);

                                int i = itemCatalog[num].item.id;

                                if (--itemCatalog[num].count <= 0)
                                    itemCatalog[num].item = null;

                                Console.Clear();
                                Console.WriteLine("대장장이 : 감사합니다 손님!!!!");

                                Screen();

                                Console.WriteLine($"※ {Data.itemData[i].name}이 인벤토리에 추가되었습니다.");
                                Console.WriteLine($"현재 소지금 : {GameManager.character.gold} ( -{Data.itemData[i].price}원 )");
                                Console.WriteLine();

                                Console.WriteLine("1. 더 둘러보기");
                                Console.WriteLine("0. 나가기");
                                Console.WriteLine();
                                Console.Write(">> ");

                                switch (GameManager.NextChoice(0, 1))
                                {
                                    case 1:
                                        BuyLoop();
                                        break;
                                    case 0:
                                        Open();
                                        break;
                                }
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("대장장이 : 돈이 부족하잖아 이녀석아!");
                                Screen();
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n");
                            Console.WriteLine("아이템이 존재하지 않습니다.");
                           continue;
                        }

                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        continue;
                    }
                }

            }

            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write(">> ");

            switch (GameManager.NextChoice(0, 0))
            {
                case 0:
                default:
                    Open();
                    break;
            }
        }

    }
}