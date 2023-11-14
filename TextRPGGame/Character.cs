using System.ComponentModel.Design;

namespace TextRPGGame
{
    public class Character
    {
        public Inventory inventory; // 인벤토리

        // hp나 exp같은거 전부 프로퍼티로만들고 자기안에서 최소 최대값 계산. 프로퍼티는 bool 반환못해?
        public Equipment[] equip; //착용중인템 (int)enum으로 자리확인

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

        public int atkBuff; // 장비 버프용
        public int defBuff;
        public int maxHpfBuff;

        public Character()
        {
            name = "";

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

            atkBuff = 0;
            defBuff = 0;
            maxHpfBuff = 0;

            equip = new Equipment[Enum.GetValues(typeof(Data.EquipParts)).Length];

            inventory = new Inventory();

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