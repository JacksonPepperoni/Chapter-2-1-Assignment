using System.ComponentModel.Design;

namespace TextRPGGame
{
    internal class Program
    {
        public static Character character; // 현재 내캐릭터

        static int cost; // 돈 거래할때끄는 변수. 선택지 여러개일땐 안좋은듯. 상점같은곳. 변수를 쓰긴 써야한다 오타방지로. 배열로쓸까?

        static void Main()
        {
            Data.Init();
            character = new Character();

            Console.Clear();
            Console.WriteLine("텍스트 RPG 게임 테스트테스트");

            Console.WriteLine("\n");
            Console.WriteLine("1.처음부터");
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
                    GameManager.Load();
                    Map_Village();
                    break;
            }


            // 불러오기 저장데이터없으면 사용불가

            //  Equipment item = new Equipment();
            //   item.Setting(3, "", "", 3, true);

            // 마을 스크립트별로 나눌것, 같은 공간 이름통일

        }

        public static void CharacterGenerate()
        {
            Console.WriteLine("캐릭터의 이름을 정해주세요. ( 10글자 이내 )");

            string name;

            while (true)
            {
                name = Console.ReadLine();

                if (name.Length <= 10)
                    break;

                Console.WriteLine("10글자 이내로 적어주세요.");
            }

            Program.character.name = name;
        }





        static void Map_Village()
        {
            Console.Clear();

            Console.WriteLine("~ 스파르타 마을 ~");
            Console.WriteLine($"{character.name}님! 스파르타 마을에 오신 것을 환영합니다.");

            Console.WriteLine("\n");
            Console.WriteLine("1.촌장집");
            Console.WriteLine("2.여관 (게임저장)");
            Console.WriteLine("3.상점1");
            Console.WriteLine("4.상점2");
            Console.WriteLine("5.식당 (체력회복)");

            Console.WriteLine("\n");
            Console.WriteLine("6.상태 보기");
            Console.WriteLine("7.인벤토리");



            Console.WriteLine();
            Console.Write(">> ");

            switch (NextChoice(1, 7))
            {
                case 6:
                    Status();
                    break;
                case 7:
                    Inventory();
                    break;
            }
        }

        static void Status()
        {
            Console.Clear();

            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine($"레 벨 : {character.level}");
            Console.WriteLine($"직 업 : {character.job.ToString()}");
            Console.WriteLine($"공격력 : {character.atk}");
            Console.WriteLine($"방어력 : {character.def}");
            Console.WriteLine($"체 력 : {character.hp} / {character.maxHp}");
            Console.WriteLine($"마 나 : {character.mp} / {character.maxMp}");
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
        static void Inventory()
        {
            Console.Clear();

            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");


            //for문으로ㅓ    character.inventory[i]


            Console.WriteLine("- [E]무쇠갑옷 | 방어력 + 5 | 무쇠로 만들어져 튼튼한 갑옷입니다.");
            Console.WriteLine("- 낡은 검 | 공격력 + 2 | 쉽게 볼 수 있는 낡은 검 입니다.");
            Console.WriteLine("\n");
            Console.WriteLine("1.아이템 사용 & 장착");
            Console.WriteLine("0.나가기");
            Console.WriteLine();
            Console.Write(">> ");

            switch (NextChoice(0, 1))
            {
                case 1:
                    ///////
                    break;
                case 0:
                    Map_Village();
                    break;
            }
        }


        static void Map_HeadmanHouse() // 촌장집
        {
            Console.Clear();

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



        static void Map_Restaurant() // 식당
        {
            Console.Clear();

            Console.WriteLine("르탄진사갈비점원 : 200원에 무한리필로 즐길 수 있습니다!");
            Console.WriteLine("\n");

            cost = -200;

            Console.WriteLine($"1.이용한다 ({cost}원 소모)");
            Console.WriteLine("0.나가기");
            Console.WriteLine();
            Console.Write(">> ");

            switch (NextChoice(0, 1))
            {
                case 1:
                    Map_Restaurant_Use(character.Wallet(cost));
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
                Console.Clear();

                character.hp = character.maxHp;
                character.mp = character.maxMp;

                Console.WriteLine("체력과 마나가 모두 회복되었습니다.");
                Console.WriteLine($"현재 소지금 : {character.gold} ( {cost}원 )");
                Console.WriteLine();
                Console.WriteLine("르탄진사갈비점원 : 또 방문해주세요!");
            }
            else
            {
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


        static int NextChoice(int min, int max) // 최대값 포함됨
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

