using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public class Player
    {
        private string name;
        private bool bonus;
        private List<int> Combinations;
        private List<int> PossibleVariants;
        private List<int> Points = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public Player(string name)
        {
            this.name = name;
            Combinations = new List<int>();
            bonus = false;
        }
        public bool Bonus
        {
            get { return bonus; }
            set { bonus = value; }
        }
        public List<int> points
        {
            get { return Points; }
            set { Points = value; }
        }
        public List<int> combos
        {
            get { return PossibleVariants; }
        }

        public string Name
        {
            get { return name; }
        }
        public List<int> cubes
        {
            get { return Combinations; }
            set { Combinations = value; }
        }
        public void GenerateCombination() // генератор комбинаций
        {
            List<int> RandomCombinations = new List<int>();
            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                RandomCombinations.Add(random.Next(1, 7));
            }

            Combinations = RandomCombinations;
        }

        public void CheckForCombinations() // проверка комбинаций
        {
            List<int> items = new List<int>();
            if (Combinations.Contains(1))
            {
                items.Add(1);
            }
            if (Combinations.Contains(2))
            {
                items.Add(2);
            }
            if (Combinations.Contains(3))
            {
                items.Add(3);
            }
            if (Combinations.Contains(4))
            {
                items.Add(4);
            }
            if (Combinations.Contains(5))
            {
                items.Add(5);
            }
            if (Combinations.Contains(6))
            {
                items.Add(6);
            }

            if ((Combinations.Contains(1) && Combinations.Contains(2) && Combinations.Contains(3) &&
                Combinations.Contains(4))
                || (Combinations.Contains(2) && Combinations.Contains(3) && Combinations.Contains(4) &&
                Combinations.Contains(5)) || (Combinations.Contains(3) && Combinations.Contains(4) && Combinations.Contains(5) &&
                Combinations.Contains(6)))
            {
                items.Add(11);
            }

            if ((Combinations.Contains(1) && Combinations.Contains(2) && Combinations.Contains(3) &&
                Combinations.Contains(4) && Combinations.Contains(5))
                || (Combinations.Contains(2) && Combinations.Contains(3) && Combinations.Contains(4) &&
                Combinations.Contains(5) && Combinations.Contains(6)))
            {
                items.Add(10);
            }

            foreach (var x in Combinations)
            {
                Dictionary<int, int> dict = new Dictionary<int, int>();
                foreach (var i in Combinations)
                    if (dict.Keys.Contains(i))
                        dict[i] += 1;
                    else dict[i] = 1;
                foreach (var i in dict)
                {
                    if (i.Value == 4) items.Add(8);
                    if (i.Value == 5) items.Add(7);
                }
                if (dict.Count > 1)
                    if (dict.ContainsValue(3) && dict.ContainsValue(2)) items.Add(9);

            }

            PossibleVariants = new List<int>();
            foreach (var x in items)
            {
                if (!PossibleVariants.Contains(x) && Points[x - 1] == 0)
                {
                    PossibleVariants.Add(x);
                }
            }
            if (points[11] == 0)
                PossibleVariants.Add(12);
        }

        public bool Points_Exist()
        {
            bool ok = false;
            foreach (var x in Points)
            {
                if (x == 0) ok = true;
            }

            return ok;
        }
    }
}
