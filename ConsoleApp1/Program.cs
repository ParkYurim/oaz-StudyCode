using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _13_5
{
    internal class Program
    {
        // 인터페이스

        public interface IFaintable
        {
            void Faint();
        }


        static void Main(string[] args)
        {

        }

        public class Unit
        {
            public string Name;
            public int Level;
            public Stat[] StatsArr;
            public bool IsFaint;
            
        }

        Dictionary<int, PokemonID> PokemonDic = new Dictionary<int, PokemonID>()
        {
            { 1, new PokemonID("이상해씨", new int[] { 45, 49, 49, 65, 65, 45 }, "Grass", "Poison") },  //연구소 1
            { 4, new PokemonID("파이리", new int[] { 39, 52, 43, 60, 50, 65 }, "Fire", "None") },       //연구소 2
            { 7, new PokemonID("꼬부기", new int[] { 44, 48, 65, 50, 64, 43 }, "Water", "None") },      //연구소 3
            { 16, new PokemonID("구구", new int[] { 40, 45, 40, 35, 35, 56 }, "Normal", "Flying") },    //먹이 주는 새
            { 19, new PokemonID("꼬렛", new int[] { 30, 56, 35, 25, 35, 72 }, "Normal", "None") },      //그린
            { 25, new PokemonID("피카츄", new int[] { 35, 55, 40, 50, 50, 90 }, "Electric", "None") },  //레드
            { 129, new PokemonID("잉어킹", new int[] { 20, 10, 55, 15, 20, 80 }, "Water", "None") },    //이수재
            { 133, new PokemonID("이브이", new int[] { 55, 55, 50, 45, 50, 55 }, "Normal", "None") },   //이수재
            { 152, new PokemonID("치코리타", new int[] { 45, 49, 65, 49, 65, 45 }, "Grass", "None") },  //실버
            { 155, new PokemonID("브케인", new int[] { 39, 52, 43, 60, 50, 65 }, "Fire", "None") },     //금선
            { 175, new PokemonID("토게피", new int[] { 35, 20, 65, 40, 65, 20 }, "Fairy", "None") },    //주인공
            { 176, new PokemonID("토게틱", new int[] { 55, 40, 85, 80, 105, 40 }, "Fairy", "Flying") }, //주인공2
            { 183, new PokemonID("마릴", new int[] { 70, 20, 50, 20, 50, 40 }, "Water", "Fairy") },     //동생(골드)
            { 184, new PokemonID("마릴리", new int[] { 70, 20, 50, 20, 50, 40 }, "Water", "Fairy") },   //할머니
            { 280, new PokemonID("랄토스", new int[] { 40, 30, 35, 35, 55, 30 }, "Psychic", "Fairy") },  //민진
            { 298, new PokemonID("루리리", new int[] { 50, 20, 40, 20, 40, 20 }, "Water", "Fairy") },   //
            { 374, new PokemonID("메탕", new int[] { 60, 75, 100, 55, 80, 50 }, "Steel", "Psychic") },  //성호
            { 468, new PokemonID("토게키스", new int[] { 85, 50, 95, 120, 115, 80 }, "Fairy", "Flying") }  //할아버지
        };
        
        //포켓몬의 키 리스트를 생성, 임의의 키를 출력
        
        
        
        public class PokemonID  //종족값 - IDStat
        {
            private string IDName{get;}
            private int[] SixStats {get;}    // 0 : Health , 1 : atk , 2 : def, 3 : satk, 4 : sdef, 5 : spd
            private List<string> EggGroup {get;}

            public PokemonID()
            {
                IDName = "Null";
                SixStats = new int[6] {0,0,0,0,0,0};
                EggGroup = new List<string>();
                EggGroup.Add("UnKnown");
            }
            public PokemonID(string name, int[] arr, string? eggGroup1, string? eggGroup2)
            {
                IDName = name;
                SixStats = new int[6];

                for(int i=0; i<arr.Length; i++)
                {
                    SixStats[i] = arr[i];
                }
                EggGroup = new List<string>();
                if (!string.IsNullOrEmpty(eggGroup1)) EggGroup.Add(eggGroup1);
                if (!string.IsNullOrEmpty(eggGroup2)) EggGroup.Add(eggGroup2);
            }
        }

        public class UnitStat   //개체값 - UnitStat
        {
            public int[] SixStats { get; set; }
            public UnitStat()
            {
                SixStats = new int[];
                Random rand = new Random();
                for(int i=0; i<6;i++)
                {
                    SixStats[i]=rand.Next(0,32);
                }
            }
        }

        public class GiveStat
        {
            public int[] SixStats { get; set; }
            public GiveStat()
            {
                SixStats = new int[6] {0,0,0,0,0,0};
            }
            public void GivePoint(int i, int point)
            {
                SixStat[i] += point;
            }
        }

        //포켓몬의 성격 :
        public class Attitude
        {
            string Name{get; set;}
            int Up{get; set;}   // 1이 atk
            int Down{get; set;}


            public Attitude()
            {
                Random rand = new Random();
                int up = random.Next(0,5);  //0이 atk
                int down = random.Next(0,5);
                Name = attitudeNameArr[up, down];
                Up = up+1;
                Down = down+1;

                // byte upByte = 1<<up;
                // byte downByte = 1<<down;

                // //만약 Attitude가 전달하는 변수가 이진수라면 여기에서 잘릴 것.
                // Flag upStat = (Flag)upByte; // upStat = "Attack" in flag
                // UpStat = upStat.ToString(); // UpStat = "Attack" string
                // Flag downStat = (Flag)downByte;
                // DownStat = downStat.ToString();
            }
            public Attitude(int up, int down)   // 1 이 atk
            {
                Name = attitudeNameArr[up-1, down-1];
                Up = up;
                Down = down;
            }
        }

        //2진수로 스탯을 관리하기
        [Flags]
        enum Flag
        {
            None = 0,
            Attack = 1<<0, // 0000 0001 : attack
            Defense = 1<<1, // 0000 0010 : defense
            ScAttack = 1<<2, // 0000 0100 : special attack
            ScDefense = 1<<3,   // 0000 1000 : special defense
            Speed = 1<<4,   // 0001 0000 : speed
            All = int.MaxValue
        }
        
        string[,] attitudeNameArr = new string[5,5] {{"노력", "외로움", "고집", "개구쟁이", "용감"}, {"대담", "온순", "장난꾸러기", "촐랑", "무사태평"}, {"조심", "의젓", "수줍음", "덜렁", "냉정"}, {"차분", "얌전", "신중", "변덕", "건방"}, {"겁쟁이", "성급", "명랑", "천진난만", "성실"}};
        /// Attitude <summary>
        ///  
        ///  attack 인 경우에는 0을 입력받아서 byte 의 이동이 0이 되도록 함 >> attack 상승 표시
        ///  낮아지는 스탯이 defense인 경우 1을 입력받아서 byte의 이동이 1이 되도록 함 >> defense의 하락 표시
        ///  rand 0-4 사이의 값을 입력받고 (상승할 스탯) rand 0-4 사이의 값을 입력받는다(하락할 스탯)
        ///  이름은 2차원 배열로 정리하여 출력, 상승 스탯과 하락 스탯은 한 쌍으로 전달.
        ///  Attack
        ///  Flag attack = (Flag)0000_0001; attack = "Attack"
        ///  attack.statMove = 0; // Flag.Attack = 0b0000_0001;
        ///  attack.
        /// </summary>

        //pokedic의 키 값, 
        public class StatManager //스탯의 계산 담당. 레벨업에 의한 스탯의 상승
        {
            public List<int> listKey {get; set;}    //dictionary의 키들의 리스트.
            public int PokeKey{get; set;}   //리스트의 순서를 가리키는 숫자.
            PokemonID nowID;
            UnitStat unitStat;
            GiveStat giveStat;
            Attitude attitude;

            //필수
            public int[] SixStats{get; set;}

            public StatManager(int level)    //랜덤으로 포켓몬 종류 정해서 이름과스탯 가져오기
            {
                PokeDicKeyToList();
                GetRandomKey();

                GetPokemonID();
                GetPokemonUnitStat();
                GetPokemonGiveStat();
                GetRandomAttitude();

            }

            public void PokeDicKeyToList()
            {
                List<int> listKey = new List<int>();
                //PokemonDic의 key값을 가져와 리스트로 만들기
                foreach(int Key in PokemonDic.Keys)
                {
                    listKey.Add(Key);
                }
            }

            public void GetRandomKey()  //랜덤으로 포켓몬 등장시키기
            {
                int num=-1;
                Random rand = new Random();
                num = rand.Next(0,PokemonDic.Count);
                PokeKey = num;
            }
            public void GetRandomKey(int key) //입력하고싶은 포켓몬의 번호(키 값 아니고 그냥 몇번째인지 할때 그 번호)
            {
                PokeKey = key;
            }
            public void GetPokemonID()
            {
                nowID = new PokemonID();
                int keyVal = listKey[PokeKey];  //pokedic의 키 값을 keyval로 설정한 것.
                nowID = PokemonDic[keyVal];   //nowID에 keyVal키를 가진 PokemonDic의 Value값 넣기
            }
            public void GetPokemonUnitStat()
            {
                unitStat = new UnitStat();
            }
            public void GetPokemonGiveStat()
            {
                giveStat = new GiveStat();
            }
            public void GetRandomAttitude()
            {
                attitude = new Attitude();
            }
            
            }
        }
        
        public class Pokemon
        {
            public int Level{get; set;}
            
            public string Name;
            public int[] SixStats{get; set;}
            public StatManager statManager;

            public Pokemon()
            {
                statManager= new StatManager();
                Name = statManager.nowID.IDName();
                StatCalculate(Level);
            }

            public void StatCalculate(int level)
            {
                SixStats = new int[6];

                int[] arrA = nowID.SixStats();
                int[] arrB = unitStat.SixStats();
                int[] arrC = unitStat.SixStats();

                int upStat = attitude.Up;
                int downStat = attitude.Down;

                SixStats[0] = (2*arrA[0]+arrB[0]+arrC[0]/4+100)*(level/100)+10;

                for(int i=1; i<6; i++)
                {
                    // a 종족값 b 개체값 c노력치 level 유닛의 레벨
                    int a = arrA[i];
                    int b = arrB[i];
                    int c = arrC[i];
                    
                    if(upStat==downStat)
                    {
                        SixStats[i]=((2*a + b + c/4)*(level/100)+5);
                    }
                    else if(i==upStat)
                    {
                        SixStats[i]=((2*a + b + c/4)*(level/100)+5)*1.1;
                    }
                    else if(i==downStat)
                    {
                        SixStats[i]=((2*a + b + c/4)*(level/100)+5)*0.9;
                    }
                    else
                    {
                        SixStats[i]=((2*a + b + c/4)*(level/100)+5);
                    }
                }
        }

        public class BattleStat : Stat
        {
            private int RankChange{get; set;}   //배틀 중 스탯의 변화.
            private int maxRank = 6;
            public Stat(string name, int statValue)   //초기 스탯 입력 Attack, calculateValue
            {
                Name = name;
                StatValue = statValue;
                RankChange = 0;
                ApplyValue = statValue;
            }
            // 배틀 중 스탯 변화
            public Stat(int rank)
            {
                RankChange(rank);
                ApplyRankChange();
            }

            public void RankChange(int rank)
            {
                if(RankChange > maxRank)
                {
                    RankChange = maxRank;
                }
                else if(Rankchange < (-1*maxRank))
                {
                    RankChange = (-1)*maxRank;
                }
                else
                {
                    RankChange += rank;
                }
            }
            public void ApplyRankChange()
            {
                if(RankChange > 0)
                {
                    ApplyValue = statValue * (1 + RankChange * 0.5);
                }
                else if(RankChange < 0)
                {
                    ApplyValue = statValue * (2/(2+RankChange));
                }
                else 
                {
                    ApplyValue = statValue;
                }
            }
        }
        public class HealthStat : Stat
        {
            
        }


        public delegate void BattleEvent();

        public class BattleEventManager
        {
        
        }
        
    }
}