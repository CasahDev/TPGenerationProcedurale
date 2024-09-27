using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPGenerationProcedurale.Model.Images;

namespace TPGenerationProcedurale.Model.Algorithms
{
    /// <summary>
    /// Abstract Factory for the IAlgorithm
    /// </summary>
    public static class AlgorithmFactory
    {
        /// <summary>
        /// List of the constructors
        /// </summary>
        private static Dictionary<string, IAlgorithmMaker> constructors = new Dictionary<string, IAlgorithmMaker>();

        /// <summary>
        /// Register a new algorithm
        /// </summary>
        /// <param name="AlgorithmName">Name of the algorithm</param>
        /// <param name="maker">Maker of the algorithm</param>
        public static void Register(string AlgorithmName, IAlgorithmMaker maker)
        {
            constructors[AlgorithmName] = maker;
        }

        /// <summary>
        /// List of the types in the factory
        /// </summary>
        public static List<String> Types => constructors.Keys.ToList();

        /// <summary>
        /// Create the algorithm
        /// </summary>
        /// <param name="algorithmName">Name of the algorithm</param>
        /// <param name="image">Image for the algorithm</param>
        /// <returns>The new algorithm</returns>
        /// <exception cref="Exception"></exception>
        public static IAlgorithm Create(string algorithmName)
        {
            if (!constructors.ContainsKey(algorithmName)) throw new Exception("Algorithm " + algorithmName + " doesn't exist !");
            return constructors[algorithmName].Create();
        }
    }
}
