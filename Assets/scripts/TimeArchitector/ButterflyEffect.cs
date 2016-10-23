using UnityEngine;

namespace AbstractButterflyClass {
    public abstract class ButterflyEffect : MonoBehaviour  {
        public abstract void step(int ticks_in_day);
    }
}