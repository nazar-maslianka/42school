using System.Collections.Generic;

namespace ComputorV1
{
    public class PolynominalSolution
    {
        public string ReducedForm { get; private set; }
        public int Degree { get; private set; }
        public List<string> Roots { get; set; }

        public PolynominalSolution(string _reducedForm, int _degree)
        {
            ReducedForm = _reducedForm;
            Degree = _degree;
        }
    }
}
