using Generator;

namespace Utils {
    public class RandomUtils {

        public string getRandomFromCollection(Collection coll) {
            if (coll.probs.Length > 0) {
                return getRandomFromArrays(coll.names, coll.probs);
            } else {
                return getRandomFromArray(coll.names);
            }
        }

        public string getRandomFromArrays(string[] names, int[] probabilities) {
            int probSum = 0;

            for (int i = 0; i < probabilities.Length; i++) {
                probSum += probabilities[i];
            }

            double normalizationFactor = probSum / 100;

            double probabilityValue = getRandomBetween(0, 100) * normalizationFactor;

            for (int i = probabilities.Length - 1; i >= 0; i--) {
                if (probSum > probabilityValue && probabilityValue > (probSum - probabilities[i])) {
                    return names[i];
                } else {
                    probSum -= probabilities[i];
                }
            }

            return names[0]; // return something if nothing helps
        }

        public string getRandomFromArray(string[] names) {
            return names[getRandomBetween(0, names.Length)];
        }

        public int getRandomBetween(int min, int max) {
            return UnityEngine.Random.Range(min, max); // strange Unity method, need to check its normal distribution
        }

    }
}