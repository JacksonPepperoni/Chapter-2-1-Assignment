using System;

namespace TextRPGGame
{
    public abstract class Item // 모든 아이템이 상속받아야한다.
    {
        public enum Type
        {
            장비,
            소모품,
            잡템
        }

        public Type type { get; private set; }
        public int id { get; private set; }
        public string name { get; private set; }
        public string comment { get; private set; }
        public int price { get; private set; }
        public bool isOverlap { get; private set; } 

        public void Setting(Type typee, int num, string namee, string come, int money, bool over) // 아이템딕셔너리만들때 사용
        {
            type = typee;
            id = num;
            name = namee;
            comment = come;
            price = money;
            isOverlap = over;
        }

        public abstract bool Use(int num); // 누른 인벤칸번호 보내기

    }


    public class Equipment : Item
    {
        public Character.Parts part { get; private set; }

        public override bool Use(int num)
        {
            // 전투중이 아닐때만 가능하게

            if (Program.character.equip[(int)part] == null)
            {
                Program.character.equip[(int)part] = this;
            }
            else
            {
                Program.character.DeleteItem(num);
                Program.character.AddItem(Program.character.equip[(int)part]);
                Program.character.equip[(int)part] = this;
            }

            // 능력치 업다운

             return true;
        }
    }


    public class Consumable : Item
    {
        public override bool Use(int num)
        {
            // 아이템능력 반영시키기

            return Program.character.DeleteItem(num);
        }
    }

    public class Useless : Item
    {
        public override bool Use(int num)
        {
            // 사용하깅벗는 잡템
            return true;
        }
    }



}
