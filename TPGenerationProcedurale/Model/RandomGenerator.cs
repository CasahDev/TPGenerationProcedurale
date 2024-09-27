using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TPGenerationProcedurale.Model
{
    /// <summary>
    /// Seeded random generator
    /// </summary>
    public class RandomGenerator
    {
        /// <summary>
        /// Instance of the singleton
        /// </summary>
        private static RandomGenerator instance;
        private static RandomGenerator Instance
        {
            get
            {
                if (instance == null) instance = new RandomGenerator();
                return instance;
            }
        }

        /// <summary>
        /// Random generator
        /// </summary>
        private Random random;
        /// <summary>
        /// Seed
        /// </summary>
        private int GlobalSeed;

        private RandomGenerator() { }

        /// <summary>
        /// Set the seed and reset the generator
        /// </summary>
        /// <param name="seed">The new seed</param>
        public static void SetSeed(string seed)
        {
            MD5 md5Hasher = MD5.Create();
            var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(seed));
            Instance.GlobalSeed = BitConverter.ToInt32(hashed, 0);
            Instance.random = new Random(Instance.GlobalSeed);
        }

        /// <summary>
        /// Give a random integer
        /// </summary>
        /// <returns>a random integer</returns>
        public static int Next()
        {
            return Instance.random.Next();
        }

        /// <summary>
        /// Give a random integer between 0 and bound-1
        /// </summary>
        /// <param name="bound">Bound for the requested integer</param>
        /// <returns>A random integer between 0 and bound-1</returns>
        public static int Next(int bound)
        {
            return Instance.random.Next(bound);
        }
    }
}
