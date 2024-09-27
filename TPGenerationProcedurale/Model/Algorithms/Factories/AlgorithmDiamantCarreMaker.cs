using TPGenerationProcedurale.Model.Algorithms.Realisations;

namespace TPGenerationProcedurale.Model.Algorithms.Factories;

public class AlgorithmDiamantCarreMaker : IAlgorithmMaker
{
    public IAlgorithm Create()
    {
        return new AlgorithmDiamantCarre();
    }
}