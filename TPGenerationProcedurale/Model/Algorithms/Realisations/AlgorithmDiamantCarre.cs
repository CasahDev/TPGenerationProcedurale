using System.Collections.Generic;
using TPGenerationProcedurale.Model.Palettes;
using TPGenerationProcedurale.Model.Palettes.Realisations;

namespace TPGenerationProcedurale.Model.Algorithms.Realisations;

public class AlgorithmDiamantCarre : AbstractAlgorithm
{
    private int taille;

    public override List<IPalette> Palettes
    {
        get => new List<IPalette>() { new GrayPalette() };
    }

    public override int ClosestValideSize(int size)
    {
        return size;
    }

    public override void NextStep()
    {
        if (taille > 1)
        {
            Carre();
            Diamant();
            taille /= 2;
        }
    }

    private void Diamant()
    {
    }

    private void Carre()
    {
        int colonne = taille/2;
        int ligne = taille/2;


        while (ligne < Image.Height)
        {
            while (colonne < Image.Width)
            {
                Image.GetPixels(ligne, colonne).Nuance = 255;
                colonne += 2 * (taille / 2);
            }

            ligne += 2 * (taille / 2);
            colonne = taille / 2;
        }
    }

    /// <summary>
    /// Initialisation of the algorithm 
    /// </summary>
    public override void Start()
    {
        base.Start();
        for (int line = 0; line < Image.Height; line++)
        for (int column = 0; column < Image.Width; column++)
            Image.GetPixels(line, column).Nuance = 0;
        RandomGenerator.SetSeed(Seed);
        taille = base.Image.Width;
        Image.GetPixels(0, 0).Nuance = 255;
        Image.GetPixels(taille-1, 0).Nuance = 255;
        Image.GetPixels(0, taille-1).Nuance = 255;
        Image.GetPixels(taille-1, taille-1).Nuance = 255;
    }
}