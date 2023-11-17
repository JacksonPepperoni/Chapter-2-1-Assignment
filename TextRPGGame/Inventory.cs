using System;

namespace TextRPGGame
{
    public class Inventory
    {
        public InventorySlot[] slots;

        public int slotCount = 0;
        public int maxSlot = 10; // 인벤토리 최대 크기

        public Inventory() //생성자
        {
            slots = new InventorySlot[maxSlot];

            for (int i = 0; i < slots.Length; i++)
            {
                InventorySlot inventorySlot = new InventorySlot();
                slots[i] = inventorySlot;
            }
        }

        //★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★

        public bool Add(Item item) // 인벤에 아이템추가
        {
            if (slotCount < maxSlot)
            {
                if (item.isOverlap)
                {
                    for (int i = 0; i < slots.Length; i++)
                    {
                        if (slots[i].item != null && slots[i].item.id == item.id)
                        {
                            slots[i].count++;
                            return true;
                        }
                    }
                }

                for (int i = 0; i < slots.Length; i++)
                {
                    if (slots[i].item == null)
                    {
                        slots[i].item = item;
                        slots[i].count++;
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

        public bool Delete(Item item) // 인벤에서 같은 id템 삭제
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null && slots[i].item.id == item.id)
                {
                    slots[i].count--;

                    if (slots[i].count <= 0)
                    {
                        slots[i].item = null;
                        slots[i].count = 0;
                        slotCount--;
                    }

                    return true;
                }
            }

            return false;

        }

        public bool Use(int num)
        {
            if (slots[num].item != null)
            {

                if (slots[num].item.type == Item.Type.장비)
                {
                    return slots[num].item.Use();
                }
                else
                {
                    if (slots[num].item.Use())
                    {
                        return Delete(slots[num].item);
                    }
                }
            }

            return false;
        }
    }
}

