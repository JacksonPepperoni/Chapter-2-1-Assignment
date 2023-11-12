namespace TextRPGGame
{
    public class InventorySlot
    {
        public Item item;
        public int count; // 겹쳐진 갯수

        public InventorySlot()
        {
            item = null;
            count = 0;
        }

        public InventorySlot(Item tem, int num) // 저장파일 로드할때는 이거씀
        {
            item = tem;
            count = num;
        }
    }
}
