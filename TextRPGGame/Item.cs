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

        public int capacity; // 회복량 뎀증 수치 임시

        public Character.EquipParts part; //나중에 장비클래스로옮기기


        public void Setting(int idd, Item.Type typee, string namee, string comee, int pricc, bool over, int caaaaa)
        {
            id = idd;
            type = typee;
            name = namee;
            comment = comee;
            price = pricc;
            isOverlap = over;
            capacity = caaaaa;
        }
        public abstract bool Use(int num); // 누른 인벤칸번호 보내기
    }


    public class Equipment : Item
    {

        public override bool Use(int num)
        {
            // 전투중이 아닐때만 가능하게

            if (GameManager.character.equip[(int)part] == null)
            {
                GameManager.character.equip[(int)part] = this;
                GameManager.character.DeleteItem(num);


                switch (part)
                {
                    case Character.EquipParts.무기:
                        GameManager.character.atkBuff += capacity;
                        GameManager.character.atk += capacity;
                        break;

                    case Character.EquipParts.몸:
                        GameManager.character.defBuff += capacity;
                        GameManager.character.def += capacity;
                        break;

                    case Character.EquipParts.장신구:
                        GameManager.character.maxHpfBuff += capacity;
                        GameManager.character.maxHp += capacity;
                        break;
                }

            }
            else
            {
                switch (part)
                {
                    case Character.EquipParts.무기:
                        GameManager.character.atkBuff += capacity;
                        GameManager.character.atk += capacity;
                        break;

                    case Character.EquipParts.몸:
                        GameManager.character.defBuff += capacity;
                        GameManager.character.def += capacity;
                        break;

                    case Character.EquipParts.장신구:
                        GameManager.character.maxHpfBuff += capacity;
                        GameManager.character.maxHp += capacity;
                        break;
                }

                switch (GameManager.character.equip[(int)part].part) // 능력치 업다운
                {
                    case Character.EquipParts.무기:
                        GameManager.character.atkBuff -= GameManager.character.equip[(int)part].capacity;
                        GameManager.character.atk -= GameManager.character.equip[(int)part].capacity;
                        break;

                    case Character.EquipParts.몸:
                        GameManager.character.defBuff -= GameManager.character.equip[(int)part].capacity;
                        GameManager.character.def -= GameManager.character.equip[(int)part].capacity;
                        break;

                    case Character.EquipParts.장신구:
                        GameManager.character.maxHpfBuff -= GameManager.character.equip[(int)part].capacity;
                        GameManager.character.maxHp -= GameManager.character.equip[(int)part].capacity;

                        if (GameManager.character.maxHp < GameManager.character.hp)
                        { GameManager.character.hp = GameManager.character.maxHp; }
                        break;
                }


                GameManager.character.DeleteItem(num);
                GameManager.character.AddItem(GameManager.character.equip[(int)part]);
                GameManager.character.equip[(int)part] = this;
            }

            return true;
        }
    }


    public class Consumable : Item
    {
        public override bool Use(int num)
        {
            GameManager.character.hp = ((GameManager.character.hp += capacity) > GameManager.character.maxHp) ? GameManager.character.maxHp : GameManager.character.hp;

            // 아이템능력 반영시키기 현재 모든 회복템은 hp회복만됨
            return GameManager.character.DeleteItem(num);
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

