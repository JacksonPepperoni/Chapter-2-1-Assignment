using System;
using static TextRPGGame.GameManager;

namespace TextRPGGame
{
    public class Item // 모든 아이템이 상속받아야한다.
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

        public Data.EquipParts part; //나중에 장비클래스로옮기기


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
        public virtual bool Use()
        {
           return false;

        } // 누른 인벤칸번호 보내기
    }

    public class Equipment : Item
    {
        public override bool Use()
        {
            //전투중이면 장비바꾸기불가
            // 착용제한있는 템생기면 여기 전부 바꿔야함

            if (character.equip[(int)part] != null)
            {
                character.inventory.Delete(this);
                character.inventory.Add(character.equip[(int)part]);
                character.equip[(int)part] = this;
            }
            else { 

            character.equip[(int)part] = this;
            character.inventory.Delete(this);

            }
            
            BuffUpdate();
            return true;
        }


        public void TakeOff() // 벗기
        {
            if (character.inventory.Add(this)) // 인벤자리 없으면 못벗어!
            {
                character.equip[(int)part] = null;
                BuffUpdate();
            }
            else
            {
                Console.WriteLine("인벤토리 공간이 부족합니다");
            }
        }

        void BuffUpdate() // 스탯 적용 아예 다른 방법으로 할것 장비창에서 관리하는게 나을지도 지금은 스탯창에 적용된것처럼 보이기만하고 합쳐지진않았다. 풀피채웠을떄 버프로올라간 체력이 안참
        {
            character.atkBuff = 0;
            character.defBuff = 0;
            character.maxHpfBuff = 0;


            for (int i = 0; i < character.equip.Length; i++)
            {
                if (character.equip[i] != null)
                {
                    switch (character.equip[i].part)
                    {
                        case Data.EquipParts.무기:
                            character.atkBuff += character.equip[i].capacity;
                            break;

                        case Data.EquipParts.몸:
                            character.defBuff += character.equip[i].capacity;
                            break;

                        case Data.EquipParts.장신구:
                            character.maxHpfBuff += character.equip[i].capacity;
                            character.hp = (character.maxHp < character.hp) ? character.maxHp : character.hp;
                            break;
                    }
                }



            }
        }


    }
    public class Consumable : Item
    {
        public override bool Use()
        {
            character.hp = ((character.hp += capacity) > character.maxHp) ? character.maxHp : character.hp;
            // 아이템능력 반영시키기 현재 모든 회복템은 hp회복만됨
            return true;
        }
    }

    public class BasicItem : Item //잡템 사용 못함
    {
        public override bool Use()
        {
            return base.Use();
        }
    }
}