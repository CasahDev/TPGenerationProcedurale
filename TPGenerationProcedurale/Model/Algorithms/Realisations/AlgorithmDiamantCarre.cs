using System.Collections.Generic;
using TPGenerationProcedurale.Model.Palettes;

namespace TPGenerationProcedurale.Model.Algorithms.Realisations;

public class AlgorithmDiamantCarre : AbstractAlgorithm
{
    
    private int i = 0;
    
    public override List<IPalette> Palettes { get; }
    public override int ClosestValideSize(int size)
    {
        return size;
    }

    public override void NextStep()
    {
        if (i % 2 == 0)
        {
            Carre();
        }
        else
        {
            Diamant();
        }

        i++;
    }

    private void Diamant()
    {
        
    }
    
    private void Carre()
    {
        
    }
}