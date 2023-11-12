using System.ComponentModel.Design;

namespace TextRPGGame
{
    public class Character
    {
        // hp나 exp같은거 전부 프로퍼티로만들고 자기안에서 최소 최대값 계산. 프로퍼티는 bool 반환못해?

        public enum EquipParts
        {
            무기,
            몸,
            장신구
        }
        public Item[] equip; //착용중인템 (int)enum으로 자리확인
        public InventorySlot[] inventory;

        public string name;
        public int level;
        public int atk;
        public int def;

        public int maxHp;
        public int hp;

        public int maxMp;
        public int mp;

        public int gold;
        public float exp; // 경험치가 몇일떄 필요 경험치 -하고 레벨업 시켜야함. 프로퍼티로 만들어서 계산하면될듯

        public Data.Jobs job;

        public int slotCount;
        public int maxSlot; // 인벤토리 크기


        public void DefaultSetting()
        {
            job = Data.Jobs.전사; // 직업마다 기본값 달라지도록

            level = 1;
            atk = 10;
            def = 5;

            maxHp = 100;
            hp = 100;

            maxMp = 100;
            mp = 100;

            gold = 1500;
            exp = 0;

            slotCount = 0;
            maxSlot = 10;

            inventory = new InventorySlot[maxSlot];

            for (int i = 0; i < inventory.Length; i++)
            {
                InventorySlot inventorySlot = new InventorySlot();
                inventory[i] = inventorySlot;
            }
        }



        public bool AddItem(Item item) //인벤토리에 추가
        {
            if (slotCount < maxSlot)
            {
                if (item.isOverlap)
                {
                    for (int i = 0; i < inventory.Length; i++)
                    {
                        if (inventory[i].item != null && inventory[i].item.id == item.id && item.isOverlap)
                        {
                            inventory[i].count++;
                            return true;
                        }
                    }
                }

                for (int i = 0; i < inventory.Length; i++)
                {
                    if (inventory[i].item == null)
                    {
                        inventory[i].item = item;
                        inventory[i].count++;
                        slotCount++;
                        break;
                    }
                }

                return true;
            }
            else // 인벤 공간부족
            {
                return false;
            }
        }

        public bool DeleteItem(int num) //인벤토리에서 삭제
        {
            if (inventory[num].item != null)
            {
                inventory[num].count--;

                if (inventory[num].count <= 0)
                {
                    inventory[num].item = null;
                    inventory[num].count = 0;
                    slotCount--;
                }

                return true;
            }

            return false;

        }

        public bool UseItem(int num) // 인벤토리에서 사용
        {
            if (inventory[num].item != null)
            {
                if (inventory[num].item.Use(num))
                    return DeleteItem(num);
                else
                    return false;
            }

            return false;
        }



        public bool Wallet(int money)  // 매개변수 : +금액 or -금액   얻는돈 소비한돈 다 Wallet이 관리. 프로퍼티로 만들까...?
        {
            if ((gold + money) >= 0) // +면 그냥 더해지는거, -면 - 했을때 0보다 작아지면 false 돈부족
            {
                gold += money;
                return true;
            }
            else
            {
                return false;
            }
        }




    }
}