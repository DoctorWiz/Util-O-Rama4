using SimMetrics.Net.API;
using SimMetrics.Net.Utilities;

namespace FuzzyString
{
    public partial class FuzzyFunctions // SmithWatermanGotoh : SmithWatermanGotohWindowedAffine
    {
        private const int AffineGapWindowSize = 0x7fffffff;

        public SmithWatermanGotoh() : base(new AffineGapRange5To0Multiplier1(), new SubCostRange5ToMinus3(), AffineGapWindowSize)
        {
        }

        public SmithWatermanGotoh(AbstractAffineGapCost gapCostFunction) : base(gapCostFunction, new SubCostRange5ToMinus3(), AffineGapWindowSize)
        {
        }

        public SmithWatermanGotoh(AbstractSubstitutionCost costFunction) : base(new AffineGapRange5To0Multiplier1(), costFunction, AffineGapWindowSize)
        {
        }

        public SmithWatermanGotoh(AbstractAffineGapCost gapCostFunction, AbstractSubstitutionCost costFunction) : base(gapCostFunction, costFunction, AffineGapWindowSize)
        {
        }

    }
}