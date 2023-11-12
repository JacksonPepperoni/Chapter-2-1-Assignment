using System.Reflection.Emit;
using System.Xml.Linq;

namespace TextRPGGame
{
    public class SaveManager
    {

        /*
            static string[] path = new string[] {
        "C:\\Users\\nabida\\Documents\\save1.csv",
        "C:\\Users\\nabida\\Documents\\save2.csv"
         };
        
        세이브슬록 여러개 만드려면 배열로만들고 슬롯넘버넣을것
        path[slotNumber]
         
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        내문서위치

        파일명, (경로있으면 경로포함)


        세이브파일 변조됐을때 어떻게 체크?

         */

        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\save.csv";

        public static void Save()
        {
            using (StreamWriter sw = new StreamWriter(new FileStream(path, FileMode.Create)))
            {
                sw.WriteLine(GameManager.character.name);
                /*  sw.WriteLine("캐릭터정보 전체");
                  sw.WriteLine("착용장비");
                  sw.WriteLine("인벤토리 템 id+소지갯수");
  
                 */
            }
        }

        public static void Load()
        {
            if (File.Exists(path)) // 파일이 있으면 true
            {
                using (StreamReader sr = new StreamReader(new FileStream(path, FileMode.Open)))
                {

                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] data = line.Split(',');

                        for (int i = 0; i < data.Length; i++)
                        {
                            GameManager.character.DefaultSetting(); // 임시. 이름만 저장됨
                            GameManager.character.name = data[i];

                        }

                        // Program.character.name = data[i];

                        // Program.character.equip[i] = ; 해당 장비 id 딕셔너리에서 찾고 장착부위맞춰서 착용시키기 능력치 적용되어야함

                        //  Program.character.inventory[i].item = dd;
                        //  Program.character.inventory[i].count = dd;

                        // 세이브 정보 다 넣고 생성해야함
                        // 아이템갯수로 슬롯카운트 숫자도 바꿔놓기



                    }
                }

            }
        }

    }

}
