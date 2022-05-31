using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;


namespace ConsoleApp4
{ 
    class Program
    {
        public static int players;
        public List<Player> ListPlayers;

        public static string rules = @"В покере на костях возможны 12 комбинаций.
Каждую из них можно использовать по одному разу за игру (т.е. за 12 раундов Вы как раз используете все комбинации).
Покер — пять одинаковых костей, например 1 - 1 - 1 - 1 - 1. За нее дается 50 баллов.
Каре — четыре одинаковых кости, например 6 - 2 - 2 - 2 - 2. За нее дается сумма баллов на всех костях (т.е. за 6 - 2 - 2 - 2 - 2 — 14 баллов). Максимум 29 баллов.
Фул Хауз — три кости одного номинала и две другого, например 6 - 6 - 1 - 1 - 1. За нее дается сумма баллов на всех костях (т.е. за 6 - 6 - 1 - 1 - 1 — 15 баллов). Максимум 28 баллов.
Длинный стрит — пять костей, номиналы которых расположены по порядку, например 1 - 2 - 3 - 4 - 5. За нее дается 30 баллов.
Короткий стрит — четыре кости, номиналы которых расположены по порядку, например 1 - 2 - 3 - 4 - 1. За нее дается 25 баллов.
Любая — 5 костей любого номинала (самая свободная комбинация), например 1 - 3 - 3 - 6 - 4. За нее дается сумма баллов на всех костях (т.е. за 1 - 3 - 3 - 6 - 4 — 17 баллов). Максимум 30 баллов.
Единицы — любое количество костей с единицами, например 1 - 1 - 1 - 2 - 5. За нее дается сумма всех единиц на костях (т.е. за 1 - 1 - 1 - 2 - 5 — 3 балла). Максимум 5 баллов.
Двойки — любое количество костей с двойками, например 2 - 2 - 1 - 3 - 5. За нее дается сумма всех двоек на костях (т.е. за 2 - 2 - 1 - 3 - 5 — 4 балла). Максимум 10 баллов.
Тройки — любое количество костей с тройками, например 3 - 3 - 3 - 3 - 5. За нее дается сумма всех троек на костях (т.е. за 3 - 3 - 3 - 3 - 5 —12 баллов). Максимум 15 баллов.
Четверки — любое количество костей с четверками, например 4 - 4 - 4 - 1 - 5. За нее дается сумма всех четверок на костях (т.е. за 4 - 4 - 4 - 1 - 5 — 12 баллов). Максимум 20 баллов.
Пятерки — любое количество костей с пятерками, например 5 - 5 - 3 - 1 - 5. За нее дается сумма всех пятерок на костях (т.е. за 5 - 5 - 3 - 1 - 5 — 15 баллов). Максимум 25 баллов.
Шестерки — любое количество костей с шестерками, например 6 - 6 - 6 - 2 - 4. За нее дается сумма всех шестерок на костях (т.е. за 6 - 6 - 6 - 2 - 4 — 18 баллов). Максимум 30 баллов.


Нажмите ENTER для продолжения";
        static void CheckInput() // проверка на ввод количеста игроков
        {
            bool ok = false;
            Console.Clear();
            do
            {
                Console.WriteLine("Количество игроков: ");
                ok = Int32.TryParse(Console.ReadLine(), out players);
                if (!ok)
                {
                    Console.WriteLine("Некоректное значение");
                }
                if (players < 2 || players > 5)
                {
                    ok = false;
                    Console.WriteLine("Количество игроков должно быть от 2 до 5");
                }
            } while (!ok);
            Console.Clear();
        }

        static List<Player> createPlayers(int len) // массив с игроками
        {
            List<Player> List = new List<Player>();
            
            for (int i = 0; i < len; i++)
            {
                Console.Write($"Введите имя игрока {i+1}:");
                string name = Console.ReadLine();
                Player p = new Player(name);
                List.Add(p);
            }
            Console.Clear();
            
            return List;
        }

        static void Table(Player x) // таблица для подсчета очков и выводв их для игроков
        {
            
            List<int> points = x.points;
            Console.WriteLine("Индексы     1   2   3   4   5   6          7    8      9         10           11        12");
            Console.WriteLine("Комбинации  1   2   3   4   5   6  Бонус Покер Каре ФулХауз ДлинныйСтрит КороткийСтрит Любая   Итог");
            Console.WriteLine($"            {points[0]}   {points[1]}   {points[2]}   {points[3]}   {points[4]}   {points[5]}   {points[12]}" +
                              $"     {points[6]}     {points[7]}     {points[8]}         {points[9]}            {points[10]}          {points[11]}      {points.Sum()}");
            Console.WriteLine();
            
        }

        static void ChooseCombination(Player x) // выбор комбинации
        {
            bool ok;
            int choise;
            
            if (x.points.GetRange(0, 5).Sum() > 60 && !x.Bonus)
            {
                x.Bonus = true;
                x.points[12] = 30;
            }

            if (x.Points_Exist())
            {
                do
                {
                    Console.WriteLine("Выберите комбинацию которую запишем:\nДля пропуска хода введите -1 ");
                    
                    ok = Int32.TryParse(Console.ReadLine(), out choise);
                    if (!ok)
                    {
                        Console.WriteLine("Некоректное значение");
                        continue;
                    }
                    if (choise == -1) break;
                    if (choise < 1 || choise > 12)
                    {
                       Console.WriteLine("Некоректное значение");
                       continue;
                    }
                    if (!x.combos.Contains(choise))
                    {
                        ok = false;
                        Console.WriteLine("Этой комбинации нет в моем списке :с");
                        continue;
                    }
                    if (x.points[choise - 1] != 0)
                    {
                         ok = false;
                         Console.WriteLine("А здесь уже есть значение с:");
                         continue;
                    }
                    if (1 <= choise && choise <= 6)
                    { 
                        int count = 0;
                        foreach (var i in x.cubes)
                        {
                            if (i == choise) count++;
                        }
                        x.points[choise - 1] = count * choise;
                        ok = true;
                    }

                    if (choise == 7)
                    {
                        x.points[choise - 1] = x.cubes[0] * 5;
                        ok = true;
                    }

                    if (choise == 8)
                    {
                        Dictionary<int, int> p = new Dictionary<int, int>();
                        foreach (var i in x.cubes)
                        {
                            if (p.ContainsKey(i)) p[i] += 1;
                            else p[i] = 1;
                        }
                        foreach (var i in p)
                            if (i.Value == 4)
                                x.points[choise - 1] = i.Key * 5;
                        ok = true;
                    }

                    if (choise == 9)
                    {
                        ok = true;
                        Dictionary<int, int> p = new Dictionary<int, int>();
                        foreach (var i in x.cubes)
                        {
                            if (p.ContainsKey(i)) p[i] += 1;
                            else p[i] = 1;
                        }
                        foreach (var i in p)
                        {
                            if (i.Value == 3)
                                x.points[choise - 1] += i.Key * 3;
                            if (i.Value == 2)
                                x.points[choise - 1] += i.Key * 2;
                        }
                    }

                    if (choise == 10)
                    {
                        ok = true;
                        x.points[choise - 1] = 30;
                    }
                        
                    if (choise == 11)
                    {
                        ok = true;
                        x.points[choise - 1] = 25;
                    }
                    if (choise == 12)
                    {
                        ok = true;
                        x.points[choise - 1] = x.cubes.Sum();
                    }
                } while (!ok);
            }
            else
            {
                Console.WriteLine("Не осталось комбинаций:с");
            }
        }

        static void Menu()
        {
            while (true)
            {   
                Console.Clear();
                Console.WriteLine("1. Начать игру \n2. Посмотреть правила");
                string choise = Console.ReadLine();
                if (choise == "1") return;
                else if (choise == "2")
                {
                    Console.WriteLine(rules);
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
                
            }
            
        }

        static void Results(List<Player> players) // вывод результатов
        {
            int max = 0;
            foreach (var p in players)
            {
                
                if (max < p.points.Sum()) max = p.points.Sum();
                Console.WriteLine(p.Name);
                Table(p);
            }

            foreach (var p in players)
            {
                if (max == p.points.Sum())
                {
                    Console.WriteLine($"Похоже, что победил игрок под именем {p.Name} Поздравим его с победой");
                }
            }

        }

        static void ChacngeCombinations(Player x) // выбор костей для переброса и возможные комбинации
        {
            int trys = 3;
            int tek = 0;
            while (tek < trys)
            {
                Console.WriteLine("Введите номера костей, которые хотите перебросить через пробел (Если хотите вернуться, введите -1)");
                try
                {
                    Random random = new Random();
                    List<int> player_cubes = x.cubes;
                    string[] nums = Console.ReadLine().Split(" ");
                    if (nums[0] == "-1") break;
                    foreach (var i in nums)
                    {
                        player_cubes[Int32.Parse(i) - 1] = random.Next(1, 7);
                    }

                    tek++;
                    Console.Write("Ваш бросок: ");
                    foreach (var i in x.cubes)
                        Console.Write($"{i} ");
                    
                    x.CheckForCombinations();
                    Console.Write("Возможные комбинации находятся под номерами: ");
                    foreach (var i in x.combos)
                        Console.Write($"{i} ");
                    Console.WriteLine();
                    if (tek < 3)
                    {
                        Console.WriteLine("\n1.Перебросить еще раз\n.Enter - Продолжить");
                        if (Console.ReadLine() != "1") return;
                    }
                }
                catch
                {
                    Console.WriteLine("Некорректное значение");
                }
            }
            
        }
        static void Main(string[] args) 
        {
            
            Menu();
            CheckInput();
            List<Player> PlayersList = createPlayers(players);
            for (int i = 0; i < 12; i++)
            {
                bool check = true;
                
                
                foreach (var p in PlayersList)
                {
                    if (p.Points_Exist()) check = false;
                }
                if (check) break;
                foreach (var p in PlayersList)
                {
                    Console.WriteLine($"Ход номер {i+1}");
                    p.GenerateCombination();
                    p.CheckForCombinations();
                    Console.WriteLine($"Ход делает игрок под именем {p.Name}");
                    Console.Write("Ваш бросок: ");
                    foreach (var x in p.cubes)
                        Console.Write($"{x} ");
                    Console.WriteLine();Console.WriteLine();
                    Table(p);
                    Console.Write("Возможные комбинации находятся под номерами: ");
                    foreach (var x in p.combos)
                        Console.Write($"{x} ");
                    Console.WriteLine();
                    Console.WriteLine("1.Перебросить кости \n2.Пропустить ход\n Enter.Продолжить  ");
                    string ch = Console.ReadLine();
                    if (ch == "1")
                    {
                        ChacngeCombinations(p);
                    }
                    else if (ch == "2")
                    {
                        continue;
                    }
                    ChooseCombination(p);
                    Table(p);
                    Console.WriteLine("Для следующего хода нажмите Enter \nЧтобы посмотреть правила введите 1");
                    if (Console.ReadLine() == "1")
                    {
                        Console.WriteLine(rules);
                        Console.ReadKey();
                    }
                }
            }

            Results(PlayersList);
        }
    }
}