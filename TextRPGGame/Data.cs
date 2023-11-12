namespace TextRPGGame
{
    public class Data
    {

        // 수정 금지 -------------
        // 전부 get; private set; 할 방법 없나

        public enum Jobs
        {
            전사,
            마법사,
            도적,
            힐러
        }

        public static Dictionary<int, Item> itemData = new Dictionary<int, Item>();

        // csv 파일 읽고 게임 모든 템 딕셔너리에 전부 추가해야함.
        
        public static void Init()
        {
            itemData.Clear();

            // for문으로 계속 추가. id가 키값.

            Equipment item = new Equipment();
            itemData.Add(0, item);
            item.Setting(Item.Type.장비, 0, "종이칼", "종이로만든칼", 100, false);

        }




    }
}
