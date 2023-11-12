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

        public Type type;
        public int id;
        public string name;
        public string comment;
        public int price;
        public bool isOverlap;

        public void Setting(int idd, Item.Type typee, string namee, string comee, int pricc, bool over)
        {
            id = idd;
            type = typee;
            name = namee;
            comment = comee;
            price = pricc;
            isOverlap = over;

        }

        public abstract bool Use(int num); // 누른 인벤칸번호 보내기

    }


    public class Equipment : Item
    {
        public Character.EquipParts part { get; private set; }

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

    public class BasicItem : Item //잡템 사용 못함
    {
        public override bool Use(int num)
        {
            // 사용하깅벗는 잡템
            return true;
        }
    }



}


/*
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

        public void Setting(int idd, Type typee,string namee, string comee, int money, bool overlap) // 아이템딕셔너리만들때 사용
        {
           
            id = idd;
            type = typee;
            name = namee;
            comment = comee;
            price = money;
            isOverlap = overlap;
        }

        public abstract bool Use(int num); // 누른 인벤칸번호 보내기

    }


    public class Equipment : Item
    {
        public Character.EquipParts part { get; private set; }

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

    public class BasicItem : Item //잡템 사용 못함
    {
        public override bool Use(int num)
        {
            // 사용하깅벗는 잡템
            return true;
        }
    }



}

 
 
 */