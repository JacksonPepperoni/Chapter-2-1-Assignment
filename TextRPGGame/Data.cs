using System;
using System.IO;

namespace TextRPGGame
{
    public static class Data
    {
        public enum EquipParts
        {
            무기,
            몸,
            장신구
        }
        public enum Jobs
        {
            전사,
            마법사,
            도적,
            힐러
        }

        public static Dictionary<int, Item> itemData;

        // csv 파일 읽고 게임 모든 템 딕셔너리에 전부 추가해야함.


        public static string path = "ItemData.csv";


        public static void Init()
        {
            itemData = new Dictionary<int, Item>();
            itemData.Clear();

            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(new FileStream(path, FileMode.Open)))
                {

                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] data = line.Split(',');

                        if (data.Length == 0) { continue; } // 헤더 넘기기

                        switch (data[1])
                        {
                            case "장비":
                                Equipment item1 = new Equipment();
                                item1.Setting(int.Parse(data[0]), Item.Type.장비, data[3], data[4], int.Parse(data[5]), data[6] == "TRUE" ? true : false, int.Parse(data[7]));
                                item1.part = (EquipParts)int.Parse(data[2]);
                                itemData.Add(item1.id, item1);
                                break;

                            case "소모품":
                                Consumable item2 = new Consumable();
                                item2.Setting(int.Parse(data[0]), Item.Type.소모품, data[3], data[4], int.Parse(data[5]), data[6] == "TRUE" ? true : false, int.Parse(data[7]));
                                itemData.Add(item2.id, item2);
                                break;

                            case "잡템":
                                BasicItem item3 = new BasicItem();
                                item3.Setting(int.Parse(data[0]), Item.Type.잡템, data[3], data[4], int.Parse(data[5]), data[6] == "TRUE" ? true : false, int.Parse(data[7]));
                                itemData.Add(item3.id, item3);
                                break;
                        }

                        // for문으로 계속 추가. id가 키값.
                        //    data[2] = 세부파츠 있는거 없는거 체크
                        // 카테고리 enum도 숫자로 적어야할듯
                    }

                }
            }

        }
    }

}