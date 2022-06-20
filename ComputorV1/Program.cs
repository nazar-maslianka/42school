using System;
using System.Collections.Generic;
using System.Linq;

namespace ComputorV1
{
    class Program
    {
        #region Members
        private static readonly string xDegreePattern = @"\*[Xx]\^\d";
        #endregion

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Add polinominal equation: ");

                //==Sample==
                //3*x^0-4*x^1
                var polynominalEquation = Console.ReadLine();

                var polynominalResolvedSolution = ResolvePolynominalEquation(polynominalEquation);

                if (polynominalResolvedSolution == null)
                {
                    Console.WriteLine("Polinominal equation is wrong!");
                    return;
                }

                Console.WriteLine("Reduced form: {0}=0", polynominalResolvedSolution.ReducedForm);
                Console.WriteLine("Polynominal degree: {0}", polynominalResolvedSolution.Degree);

                if (polynominalResolvedSolution.Degree > 2 || polynominalResolvedSolution.Roots == null)
                {
                    Console.WriteLine("The polynominal degree > 2. I can't solve.");
                    return;
                }

                Console.WriteLine("The solution is:");
                polynominalResolvedSolution.Roots.ForEach(itm => Console.WriteLine("{0}", itm));
            }
        }

        #region Internal methods
        private static PolynominalSolution ResolvePolynominalEquation(string polynominalEquation)
        {
            PolynominalSolution polynominalEquationSolution = null;

            if (string.IsNullOrWhiteSpace(polynominalEquation)) return polynominalEquationSolution; 

            var polynominalSplitedByEqual = polynominalEquation.Split('=');
            var polynominalPartsCollection = new string[2][]
            {
                polynominalSplitedByEqual[0].RegexSplit(xDegreePattern),
                polynominalSplitedByEqual[1].RegexSplit(xDegreePattern)
            };

            if (polynominalPartsCollection[0] != null || polynominalPartsCollection[1] != null)
            {
                var leftPartOfPolynominalLeng = polynominalPartsCollection[0]?.Length ?? 0;
                var rightPartOfPolynominalLeng = polynominalPartsCollection[1]?.Length ?? 0;

                var polynominalDegree = leftPartOfPolynominalLeng > rightPartOfPolynominalLeng ? leftPartOfPolynominalLeng : rightPartOfPolynominalLeng;
                var reducedPolynominalPartsCollection = new double[polynominalDegree];

                for (int i = 0; i < polynominalDegree; i++)
                {
                    var left = leftPartOfPolynominalLeng >= i ? double.Parse(polynominalPartsCollection[0][i]) : 0;
                    var right = rightPartOfPolynominalLeng >= i ? double.Parse(polynominalPartsCollection[1][i]) : 0;

                    reducedPolynominalPartsCollection[i] = left - right;
                }

                var reducedForm = string.Concat(reducedPolynominalPartsCollection
                    .Select((num, i) => i > 0 && num > 0 ? $"+{num}*X^{i}" : $"{num}*X^{i}"));

                polynominalEquationSolution = new PolynominalSolution(reducedForm, --polynominalDegree);

                if (polynominalDegree > 2) return polynominalEquationSolution;

                polynominalEquationSolution.Roots = GetRoots(reducedPolynominalPartsCollection);
            }

            return polynominalEquationSolution;
        }

        private static List<string> GetRoots(double[] polynominalParts)
        {
            List<string> roots = null;
            if (polynominalParts == null || polynominalParts.Length == 0) return roots;

            roots = new List<string>();
            switch (polynominalParts.Length)
            {
                case 1:
                    var firstPolynominalPart = polynominalParts[0];
                    roots.Add(firstPolynominalPart == 0 ? "all real numbers" : "0");
                    break;
                case 2:
                    var root1 =  polynominalParts[0] / -polynominalParts[1];
                    roots.Add(root1.ToString());
                    break;
                case 3:
                    var discriminantResult = polynominalParts[1] * polynominalParts[1] - 4 * polynominalParts[0] * polynominalParts[2];
                    roots = discriminantResult.GetRootsByDiscriminant(polynominalParts);
                    break;
            }
            return roots;
        }
        #endregion
    }
}
