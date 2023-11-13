using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGGame
{
    internal class Shop
    {

        // 항상 출력되야하는건 함수로 만들어서 우려먹어야겠는데. 아이템목록 펼치기같은거


        static public InventorySlot[] equipSale;
        static public InventorySlot[] consumSale;


        static int cost;
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

            ItemReset();

        }

        static void ItemReset()
        {
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



        public static void Shop_Epuip()
        {
           // ItemReset(); // 재고추가

            Console.Clear();

            Console.WriteLine("대장장이 : 힘썌고 강한 아침. 크고 아름다운 장비를 팔고있지");
            Console.WriteLine();
            Console.WriteLine("[    판매목록    ]");
            Console.WriteLine();

            for (int i = 0; i < equipSale.Length; i++)
            {
                if (equipSale[i].item != null)
                    Console.WriteLine($"{(equipSale[i].item.part).ToString()} | {equipSale[i].item.name} | {equipSale[i].item.capacity}상승 | {equipSale[i].item.comment} | {equipSale[i].item.price}원 | {equipSale[i].count}개");
                else
                    Console.WriteLine($"-----------------------  품절  -----------------------");
            }

            Console.WriteLine("\n");
            Console.WriteLine("1.장비 구매");
            Console.WriteLine("0.나가기");
            Console.WriteLine();
            Console.Write(">> ");

            switch (GameManager.NextChoice(0, 1))
            {
                case 1:
                    Shop_Epuip_Use();
                    break;
                case 0:
                    GameManager.Map_Village();
                    break;
            }
        }


        public static void Shop_Epuip_Use()
        {
            Console.Clear();

            Console.WriteLine("대장장이 : 어디 원하는걸 골라보슈!");
            Console.WriteLine();
            Console.WriteLine("[    판매목록    ]");
            Console.WriteLine();

            for (int i = 0; i < equipSale.Length; i++)
            {
                if (equipSale[i].item != null)
                    Console.WriteLine($"{i + 1}. {(equipSale[i].item.part).ToString()} | {equipSale[i].item.name} | {equipSale[i].item.capacity}상승 | {equipSale[i].item.comment} | {equipSale[i].item.price}원 | {equipSale[i].count}개");
                else
                    Console.WriteLine($"{i + 1}.-----------------------  품절  -----------------------");
            }

            Console.WriteLine("\n");
            Console.WriteLine("구매할 아이템의 숫자를 적어주세요");
            Console.WriteLine("0.나가기");
            Console.WriteLine();
            Console.Write(">> ");


            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int num))
                {
                    if (--num < 0)
                    {
                        Shop_Epuip();
                        break;
                    }

                    if ((GameManager.character.slotCount >= GameManager.character.maxSlot))
                    {
                        Console.WriteLine("인벤토리공간이 부족합니다");
                        break;
                    }

                    Console.Clear();
                    Console.WriteLine("대장장이 : 어디 원하는걸 골라보슈!");
                    Console.WriteLine();
                    Console.WriteLine("[    판매목록    ]");
                    Console.WriteLine();

                    for (int i = 0; i < equipSale.Length; i++)
                    {
                        if (equipSale[i].item != null)
                            Console.WriteLine($"{i + 1}. {(equipSale[i].item.part).ToString()} | {equipSale[i].item.name} | {equipSale[i].item.capacity}상승 | {equipSale[i].item.comment} | {equipSale[i].item.price}원 | {equipSale[i].count}개");
                        else
                            Console.WriteLine($"{i + 1}.-----------------------  품절  -----------------------");
                    }



                    if (num < equipSale.Length)
                    {
                        if (equipSale[num].item != null)
                        {
                            cost = equipSale[num].item.price;

                            

                            if (GameManager.character.Wallet(-cost))
                            {
                                GameManager.character.AddItem(equipSale[num].item);
                                
                                Console.WriteLine();
                                Console.WriteLine($"{equipSale[num].item.name}이 인벤토리에 추가되었습니다.");
                                Console.WriteLine($"현재 소지금 : {GameManager.character.gold} ( -{cost}원 )");
                                Console.WriteLine();
                                Console.WriteLine(" 감사합니다 손님!!!!");
                                Console.WriteLine();


                                if (--equipSale[num].count <= 0)
                                {
                                    equipSale[num].item = null;
                                }


                                Console.WriteLine("1. 더 구매하기");
                                Console.WriteLine("0.나가기");
                                Console.WriteLine();
                                Console.Write(">> ");

                                switch (GameManager.NextChoice(0, 1))
                                {
                                    case 1:
                                        Shop_Epuip_Use();
                                        break;
                                    case 0:
                                        Shop_Epuip();
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("돈이 부족하잖아 이녀석아!");
                                break;

                            }
                        }
                        else
                        { Console.WriteLine("아이템이 존재하지 않습니다."); }

                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }


            }

            Console.WriteLine();

            Console.WriteLine("0.나가기");
            Console.WriteLine();
            Console.Write(">> ");

            switch (GameManager.NextChoice(0, 0))
            {
                case 0:
                default:
                    Shop_Epuip();
                    break;

            }



        }
    }


}