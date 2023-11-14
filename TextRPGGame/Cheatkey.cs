namespace TextRPGGame
{
    public class Cheatkey
    {
        public static void ShowMeTheMoney()
        {
            Console.Clear();
            Console.WriteLine("돈을 얼마로 바꿀지 숫자만 적으세용");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int num))
                {
                    GameManager.character.gold = num;
                    break;
                }
                else
                {
                    Console.WriteLine("숫자만 입력!");
                }
            }

            GameManager.Map_Village();

        }
        public static void ShowMeTheItem()
        {
            Console.Clear();
            Console.WriteLine("추가할 템 id입력하세욤 있는템이면 인벤에 추가됨");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int num))
                {
                    if (Data.itemData.ContainsKey(num))
                    {
                        GameManager.character.inventory.Add(Data.itemData[num]);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("id 제대로입력!");

                    }
                }
                else
                {
                    Console.WriteLine("숫자만 입력!");
                }
            }

            GameManager.Map_Village();

        }
    }
}
