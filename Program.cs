using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Prob_HW12
{
    class Prob
    { 
        static Random rand = new Random((int)DateTime.Now.Ticks);
        public static void PrintUsage()
        {
            var sb = new StringBuilder();
            sb.AppendLine("PROBABILITY AND STATISTICS :: HW12");
            sb.AppendLine("B635248 Won Dong Hyun");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(sb.ToString());

            sb.Clear();
            sb.AppendLine();
            sb.AppendLine("Usage : Type Proper Input to Simulate ");
            sb.AppendLine("\t1a : Question 1 (a) Simulation");
            sb.AppendLine("\t1b : Question 1 (b) Simulation");
            sb.AppendLine("\t1c : Question 1 (c) Simulation");
            sb.AppendLine("\t2a : Question 2 (a) Simulation");
            sb.AppendLine("\t2b : Question 2 (b) Simulation");
            sb.AppendLine("\t2c : Question 2 (c) Simulation");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(sb.ToString());

            Console.ForegroundColor = ConsoleColor.White;
        }

        public static string GetInput()
        {
            Console.Write("Input: ");
            return Console.ReadLine();
        }

        public static int[] Dice()
        {
            int[] events = { 0, 0, 0, 0, 0, 0 };
            for(int i = 0; i < 1000; i++)
            {
                double uniformOne = UniformOne();
                int dice = (int)(uniformOne * 6 + 1) % 6;
                events[dice]++;
            }

            

            return events;
        }

        private static double UniformOne() 
            => rand.NextDouble();

        public static double Exponential(double lambda) 
            => lambda * Math.Pow(Math.E, -lambda * UniformOne()); // λ * e^{-λx}

        public static int Poll()
        {
            double[] prob = { 0.4108, 0.2403, 0.2141, 0.0676, 0.0617, 0.0055 };
            List<double> cum = new List<double>();

            for(int i = 0 ; i < prob.Length; i++)
            {
                double c = 0.0;
                for (int j = 0; j <= i; j++)
                {
                    c += prob[j];
                }
                cum.Add(c);
            }

            double uniformOne = UniformOne();
            for (int i = 0; i < cum.Count; i++)
            {
                if (uniformOne < cum[i])
                    return i + 1;
            }

            return cum.Count;

        }

        public static void GenerateUniformPointInRect(out double x, out double y)
        {
            double uOneX = UniformOne();
            double uOneY = UniformOne();

            x = uOneX * 2 - 1;
            y = uOneY * 2 - 1; 
        }

        public static bool CheckIfIsInCircle(double rad, double x, double y)
            => Math.Sqrt(x * x + y * y) <= rad;

        public static double MonteCarloMethod()
        {
            int tryNum = 1000000;
            int inCircleCnt = 0;
            for(int i = 0; i < tryNum; i++)
            {
                double x, y;
                GenerateUniformPointInRect(out x, out y);
                if (CheckIfIsInCircle(1.0, x, y))
                    inCircleCnt++;
            }

            return 4 * (inCircleCnt / (double)tryNum);
        }

        public static bool InputDigitCheck(string input)
        {
            int n;
            return input.Length == 2 && int.TryParse(input[0].ToString(), out n) && (n == 1 || n == 2);
        }

        public static bool InputAlphaCheck(string input)
            => input[1] == 'a' || input[1] == 'b' || input[1] == 'c';


        public static void ProcessInput(int num, char alpha)
        {
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkBlue;


            switch (num)
            {
                case 1:
                    switch (alpha)
                    {
                        case 'a':
                            {
                                int[] dice = Prob.Dice();
                                Console.WriteLine(String.Format("|1:{0}|2:{1}|3:{2}|4:{3}|5:{4}|6:{5}|",
                                    dice[0], dice[1], dice[2], dice[3], dice[4], dice[5]));
                                break;

                            }

                        case 'b':
                            {
                                Console.WriteLine("Exponential Random Variable");
                                Console.Write("Type Lambda: ");
                                double lambda;
                                double.TryParse(Console.ReadLine(), out lambda);
                                for (int i = 0; i < 10; i++)
                                    Console.WriteLine(String.Format("{0}th try: {1}", i + 1, Prob.Exponential(lambda)));

                                break;

                            }

                        case 'c':
                            {
                                Console.WriteLine("With Probability : ");
                                Console.WriteLine("1: 0.4108 /  2: 0.2403 / 3: 0.2141 / 4: 0.0676 / 5:0.0617 / 6: 0.0055");

                                for (int i = 0; i < 10; i++)
                                    Console.WriteLine(String.Format("{0}th try: {1}", i + 1, Prob.Poll()));

                                break;
                            }
                    }
                    break;

                case 2:
                    switch (alpha)
                    {
                        case 'a':
                            {
                                double x, y;
                                Prob.GenerateUniformPointInRect(out x, out y);
                                Console.WriteLine("Generating Uniform Probability (X, Y) : (" + x + ", " + y + ")");
                                break;
                            }


                        case 'b':
                            {
                                double x, y;
                                Prob.GenerateUniformPointInRect(out x, out y);
                                Console.WriteLine("Generating Uniform Probability (X, Y) : (" + x + ", " + y + ")");
                                Console.WriteLine("Is In the Circle ? : " + Prob.CheckIfIsInCircle(1.0, x, y));
                                break;
                            }

                        case 'c':
                            {
                                double pi = Prob.MonteCarloMethod();
                                Console.WriteLine("π Approximation : " + pi);
                                break;
                            }


                    }
                    break;
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }


    }


    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Prob.PrintUsage();
                string input = Prob.GetInput();

                int num;
                char alpha;
                if (input.Length == 2 && Prob.InputDigitCheck(input) && Prob.InputAlphaCheck(input))
                {
                    num = int.Parse(input[0].ToString());
                    alpha = input[1];
                    Prob.ProcessInput(num, alpha);

                    Console.WriteLine("\n\nType Any Key");
                    Console.ReadKey();
                }

                else
                {
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong Input!");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(2000);
                }

                Console.Clear();
            }
            
        }
    }
}
