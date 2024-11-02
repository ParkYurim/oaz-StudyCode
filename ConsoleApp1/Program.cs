using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _13_4.Player;

namespace _13_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            GameMessage gameMessage = new GameMessage();

            gameManager.OnGameStart += () => gameMessage.ShowMessage("게임을 시작합니다.");
            gameManager.OnGamePause += () => gameMessage.ShowMessage("게임을 일시정지합니다.");
            gameManager.OnGameEnd += () => gameMessage.ShowMessage("게임을 종료합니다.");

            EnemyUnit enemy = new EnemyUnit();
            PlayerUnit playerUnit = new PlayerUnit();

            enemy.EnemyAttack += playerUnit.HandleDamage;

            Item item = new Item();
            gameManager.GameStart();

            Console.WriteLine("당신의 이름을 입력하세요.");
            string playerName = (string)Console.ReadLine();
            Player player = new Player(playerName);

            EnemyUnit unit = new EnemyUnit();
            unit.GetRandom(player);

            player.GetUnit(enemy);
            player.PrintUnitList();

            while(true)
            {
                Console.WriteLine("어떤 행동을 하실 건가요?");
                Console.WriteLine("1. 새로운 포켓몬을 잡는다.  2. 정보  3. 상점  4. 게임 끝");
                ConsoleKeyInfo input = Console.ReadKey();
                if (input.Key == ConsoleKey.D1)
                {
                    Console.WriteLine();
                    EnemyUnit enemy1 = new EnemyUnit();
                    enemy.GetRandom(player);

                    Console.WriteLine("{0}이 나타났습니다.", enemy.Name);
                    Console.WriteLine("사용할 아이템의 번호를 입력하세요.");
                    player.PrintItemList();
                    int useIdx = int.Parse(Console.ReadLine());
                    while(useIdx >= player.Items.Count)
                    {
                        Console.WriteLine("소지하고 있는 아이템이 없습니다.");
                        useIdx = int.Parse(Console.ReadLine());
                    }
                    player.GotchaEvent(enemy, player.Items[useIdx]);

                }else if(input.Key == ConsoleKey.D2)
                {
                    player.PlayerInfo();
                }
                else if(input.Key == ConsoleKey.D3)
                {
                    gameManager.GameEnd();
                    break;
                }
            }    
        }
    }

    public delegate void GameEvent();
    public class GameManager
    {
        public event GameEvent OnGameStart;
        public event GameEvent OnGamePause;
        public event GameEvent OnGameEnd;
        public void GameStart()
        {
            OnGameStart?.Invoke();
        }
        public void GamePause()
        {
            OnGamePause?.Invoke();
        }
        public void GameEnd()
        {
            OnGameEnd?.Invoke();
        }
    }

    public class GameMessage
    {
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }
    }

    enum TypeEnum
    {
        Fire,
        Water,
        Grass
    }
    public class Unit
    {
        public string Name;
        public int Level;
        public int Attack;
        public int Defense;    //방어 Defence는 방위
        public int Speed;
        public bool IsFaint;
        public int Health;

        public void PrintStats()
        {
            Console.WriteLine("Unit Name : {0}", Name);
            Console.WriteLine("Unit Level : {0}", Level);
            Console.WriteLine("Unit Attack : {0}", Attack);
            Console.WriteLine("Unit Defense : {0}", Defense);
            Console.WriteLine("Unit Speed : {0}", Speed);
            Console.WriteLine("Unit Health : {0}", Health);
            Console.WriteLine("Unit IsFaint : {0}", IsFaint);
        }

        public void GetRandomUnit(Player player)
        {
            Random random = new Random();
            int num = random.Next(1, 101);
            Name = NameEnum.NoData.ToString();
            int naturalExp;
            if (num == 1)
            {
                Name = NameEnum.Mew.ToString();
                naturalExp = 50;
            }
            else if (num <= 10)
            {
                Name = NameEnum.Pikachu.ToString();
                naturalExp = 20;
            }
            else if (num <= 20)
            {
                Name = NameEnum.Purin.ToString();
                naturalExp = 18;
            }
            else if (num <= 50)
            {
                Name = NameEnum.Tunguri.ToString();
                naturalExp = 14;
            }
            else if (num <= 90)
            {
                Name = NameEnum.Snaki.ToString();
                naturalExp = 10;
            }
            else if (num <= 100)
            {
                Name = NameEnum.Naoha.ToString();
                naturalExp = 20;
            }
            else
            {
                Console.WriteLine("오류 발생");
                naturalExp = 0;
            }

            if (player.Level <= 5)
            {
                Level = random.Next(1, player.Level + 6);
                Attack = random.Next(1, 10 + player.Level);
                Defense = random.Next(1,10 + player.Level);
                Speed = random.Next(1, 10 + player.Level);
                Health = ((Defense + Level) + random.Next(1, player.Level));
                IsFaint = false;
            }
            else
            {
                Level = random.Next(player.Level - 5, player.Level + 6);
                Attack = random.Next(1, 10 + player.Level);
                Defense = random.Next(1, 10 + player.Level);
                Speed = random.Next(1, 10 + player.Level);
                Health = ((Defense + Level) + random.Next(player.Level) + random.Next(10));
                IsFaint = false;
            }
        }
    }

    enum NameEnum
    {
        NoData=0,
        Mew = 1,
        Pikachu = 10,
        Purin = 20,
        Tunguri = 50,
        Snaki=90,
        Naoha=100
    }
    
    //Dictionaty사용해서 확률 계산하기
    // NameEnum[] enums = 

    public delegate void EnemyAttackHandler(float damage);
    public class EnemyUnit : Unit
    {
        public float GotchaPercent {  get; set; }
        public float GiveExp { get; set; }
        public EnemyUnit()
        {
            Name = "UnKnown";
            Health = 0;
            Level = 0;
            Attack = 0;
            Defense = 0;
            Speed = 0;
            GotchaPercent = 0;
            IsFaint = false;
            GiveExp = 0;
        }

        public event EnemyAttackHandler EnemyAttack;
        public void AttackPlayer(float damage)
        {
            EnemyAttack?.Invoke(damage);
        }
        public void GetRandom(Player player)
        {
            {
                /*
                 * NameEnum enum = 
                 * Rendom random = new Random();
                 * int 
                */
                Random random = new Random();
                int num = random.Next(1, 101);
                Name = NameEnum.NoData.ToString();
                int naturalExp;
                if (num == 1)
                {
                    Name = NameEnum.Mew.ToString();
                    GotchaPercent = 0.1f;
                    naturalExp = 50;
                }
                else if (num <= 10)
                {
                    Name = NameEnum.Pikachu.ToString();
                    GotchaPercent = 0.3f;
                    naturalExp = 20;
                }
                else if (num <= 20)
                {
                    Name = NameEnum.Purin.ToString();
                    GotchaPercent = 0.5f;
                    naturalExp = 18;
                }
                else if (num <= 50)
                {
                    Name = NameEnum.Tunguri.ToString();
                    GotchaPercent = 0.5f;
                    naturalExp = 14;
                }
                else if (num <= 90)
                {
                    Name = NameEnum.Snaki.ToString();
                    GotchaPercent = 0.5f;
                    naturalExp = 10;
                }
                else if (num <= 100)
                {
                    Name = NameEnum.Naoha.ToString();
                    GotchaPercent = 0.3f;
                    naturalExp = 20;
                }
                else
                {
                    Console.WriteLine("오류 발생");
                    naturalExp = 0;
                }

                if (player.Level <= 5)
                {
                    Level = random.Next(1, player.Level + 6);
                    Attack = random.Next(1, 10 + player.Level);
                    Defense = random.Next(1, 10 + player.Level);
                    Speed = random.Next(1, 10 + player.Level);
                    Health = ((Defense + Level) + random.Next(1, player.Level));
                    IsFaint = false;
                }
                else
                {
                    Level = random.Next(player.Level - 5, player.Level + 6);
                    Attack = random.Next(1, 10 + player.Level);
                    Defense = random.Next(1, 10 + player.Level);
                    Speed = random.Next(1, 10 + player.Level);
                    Health = ((Defense + Level) + random.Next(player.Level) + random.Next(10));
                    IsFaint = false;
                }
                GiveExp = (float)naturalExp * (1 - (player.Level - Level) / Level);
            }
        }
    }

    public class PlayerUnit : Unit
    {
        public float Experience {  get; set; }
        public float NeedExp {  get; set; }
        public int MaxHealth {  get; set; } //

        public PlayerUnit()
        {
            Random rand = new Random();
            Name = "기본캐릭터";
            Level = 5;
            Attack = rand.Next(1,10);
            Defense = rand.Next(1, 10);
            Speed = rand.Next(1, 10);
            Health = rand.Next(1, 10);
            Experience = rand.Next(1, 10);
        }
        public PlayerUnit(EnemyUnit enemy,string? nickName)
        {
            Name = enemy.Name;
            Level = enemy.Level;
            Attack = enemy.Attack;
            Defense = enemy.Defense;
            Speed = enemy.Speed;
            Health = enemy.Health;
            Experience = 0;
            if (nickName!=null)
            {
                Name = nickName;
            }
            NeedExp = Level * (enemy.GiveExp) / 10+100;
        }
        public void ChangeName()
        {
            Console.WriteLine("바꾸고 싶은 이름을 입력하여 주십시오.");
            string? newName = Console.ReadLine();
            if (newName != null)
            {
                Name = newName;
            }
        }
        public void HandleDamage(float damage)
        {
            Console.WriteLine("{0}은 {1}만큼의 데미지를 입었다!", Name, damage);
        }
        public void LevelUp()
        {
                Attack += (1 + Level / 10);
                Defense += (1 + Level / 10);
                Speed += (1 + Level / 10);
                Health += (1 + Level / 5);
                NeedExp = NeedExp+Level;
        }

    }

    public class Player
    {
        public string Name { get; set; } = "Red";
        public int Level { get; set; } = 1;
        public float Exp {  get; set; }
        public List <PlayerUnit> MyUnits { get; set; }
        public List <Item> Items { get; set; }
        public Player(string? name)
        {
            if (name == null)
            {
                Name = "Red";
            }
            else
            {
                Name = name;
            }
            Level = 1;
            Exp = 0;
            MyUnits = new List<PlayerUnit>();
            Items = new List<Item>();
            Item item = new Item();
            Items.Add(item);
        }

        // 공통되는 기능을 가진 애들을 호출하는 클래스를 따로 만들고, 그걸 player가 가지도록 한다.
        // Item Controller 따로 만들어서 Get Use 그 안에 넣고 Items 포인터로 받기

        public void ItemGet(Item item)
        {
            Items.Add(item);
            item.Get();
        }
        public bool ItemUse(Item item)
        {
            while (Items.Contains(item))
            {
                if (item.UseableLevel > Level)
                {
                    Console.WriteLine("레벨이 낮아 아이템을 사용할 수 없습니다.");
                    break;
                }
                else
                {
                    item.Use();
                    int itemIdx = Items.IndexOf(item);
                    Items.RemoveAt(itemIdx);
                    if (Items.Count == 0)
                    {
                        Item newItem = new Item();
                        Items.Add(newItem);
                    }
                    return true;
                }
            }
            return false;
        }
        public void PrintItemList()
        {
            foreach (Item item in Items)
            {
                Console.Write(Items.IndexOf(item)+". ");
                Console.WriteLine($"{item.Name}");
                Console.WriteLine($"{item.ExplainScript}");
            }
        }
        public void PrintUnitList()
        {
            foreach (PlayerUnit myUnit in MyUnits)
            {
                int indexNum = MyUnits.IndexOf(myUnit);

                Console.WriteLine($"{indexNum} ");
                myUnit.PrintStats();
            }
        }
        public void PrintPlayerList()
        {
            Console.WriteLine($"Player Name : {Name}");
            Console.WriteLine($"Player Level : {Level}");
            Console.WriteLine($"Player Exp : {Exp}/10");
        }
        public void PlayerInfo()
        {
            Console.WriteLine("Player");
            PrintPlayerList();
            Console.WriteLine();
            Console.WriteLine("Units");
            PrintUnitList();
            Console.WriteLine();
            Console.WriteLine("Items");
            PrintItemList();
        }
        public void CheckFightable()
        {
            var FightableUnits = from num in MyUnits
                                 where num.IsFaint == false
                                 select num;
            foreach (var unit in FightableUnits)
            {
                Console.WriteLine(unit.Name);
            }
        }
        private void ExpCounter(EnemyUnit enemy)
        {

            Console.WriteLine("경험치를 {0}만큼 획득하였습니다..", enemy.GiveExp);
            Exp += enemy.GiveExp /10;
            Console.WriteLine("플레이어의 경험치 : {0}/10", Exp);
            foreach (var unit in MyUnits)
            {
                Console.WriteLine(unit.Name);
                unit.Experience += enemy.GiveExp;
                Console.WriteLine("경험치 : {0} / {1}", unit.Experience, unit.NeedExp);
            }
            CheckLevelUp();
        }
        public void CheckLevelUp()
        {
            PlayerLevelUp();
            UnitLevelUp();
        }

        private void PlayerLevelUp()
        {
            int levelPtr = Level;
            while(Exp>10)
            {
                Console.WriteLine("플레이어의 레벨이 올랐습니다!");
                Level++;
                Exp-=10;
                Console.WriteLine("플레이어의 레벨 : {0} >> {1}", levelPtr, Level);
            }
        }
        private void UnitLevelUp()
        {
            foreach (var unit in MyUnits)
            {

                int levelPtr = unit.Level;
                while (unit.Experience > unit.NeedExp)
                {
                    unit.Level++;
                    unit.Experience -= unit.NeedExp;
                    Console.WriteLine($"{unit.Name}의 레벨이 올랐습니다!");
                    unit.LevelUp();
                    Console.WriteLine("{0}의 레벨 : {1} >> {2}", unit.Level, levelPtr, unit.Level);
                }
            }
        }

        private bool? GetYesNo()
        {
            bool? result = null;
            do
            {
                Console.WriteLine("1. Yes   2. No ");
                ConsoleKeyInfo key = Console.ReadKey();

                if (key.Key == ConsoleKey.D1)
                {
                    result = true;
                }
                else if (key.Key == ConsoleKey.D2)
                {
                    result = false;
                }
                else
                {
                    Console.WriteLine("1과 2 사이의 값을 입력해주세요.");
                }
            } while (result == null);

            return result;
        }
        
        public string? NameUnit()
        {
            string? nick=null;
            Console.WriteLine("이름을 지어주시겠습니까?");

            if(GetYesNo()==true)
            {
                Console.WriteLine("이름을 입력해주세요.");
                nick=Console.ReadLine();
            }
            return nick;
        }
        public void GetUnit(EnemyUnit unit)
        {
            Console.WriteLine("{0}을 획득하였습니다!", unit.Name);
            //플레이어 경험치 획득.
            ExpCounter(unit);
            string? nickName = NameUnit();
            PlayerUnit myUnit = new PlayerUnit(unit, nickName);
            MyUnits.Add(myUnit);
        }

        private bool CheckGetOrNo(EnemyUnit enemy, Item item)
        {
            ItemUse(item);
            float calculateNum = enemy.GotchaPercent * item.ItemEffect * 100;
            Random rand = new Random();
            int randomVal = rand.Next(1, 101);
            if (randomVal < calculateNum)
            {
                return true;
            }
            else return false;
        }
        public void GotchaEvent(EnemyUnit enemy, Item item)
        {
            bool result = false;
            do
            {
                result = CheckGetOrNo(enemy, item);
                if (result == false)
                {
                    Console.WriteLine("포획에 실패했습니다.");
                    Console.WriteLine("다시 시도하겠습니까?");
                    if (GetYesNo() == true)
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("포획 실패");
                        break;
                    }
                }
            } while (result == false);

            if(result == true)
            {
                GetUnit(enemy);
            }
        }
        

    }


    public class Item
    {
        
        public string Name { get; set; }
        public int UseableLevel { get; set; }
        public string ExplainScript {  get; set; }
        public float ItemEffect {  get; set; }
        // 1. Item에 타입을 넣어서 아이템을 적용시킬 때 조건문을 나눠서 처리하는 방법
        // 2. 아이템 상속받아서 USE메서드를 오버라이드해서 각 하위 클래스에서 정의해주는 것
        public Item() 
        {
            Name = "Simple Ball";
            UseableLevel = 0;
            ExplainScript = string.Empty;
            ItemEffect = 1;
                
        }
        public Item(string? name, int useLevel,string explain, float effect)
        {
            Name = name;
            UseableLevel = useLevel;
            ExplainScript = explain;
            ItemEffect = effect;
        }
        public void Get()
        { Console.WriteLine("{0}을 획득하였다.", this.Name); }
        public void Use()
        { Console.WriteLine("{0}을 사용하였다.", this.Name); }
        public void Drop() 
        { Console.WriteLine("{0}을 버렸다.", this.Name); }
        
    }
    
}
