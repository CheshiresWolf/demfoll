using UnityEngine;

using Generator;
using System.Collections.Generic;

namespace AbstractButterflyClass {
    public abstract class ButterflyEffect : MonoBehaviour  {
        public abstract void step(int ticks_in_day);
        public abstract void day_step(int ticks_in_day);
        public abstract void month_step(int ticks_in_day);
    }

	class ButterflyEffectController {
	    private List<ButterflyEffect> queue = new List<ButterflyEffect>();

	    public void step(int ticks_in_day) {
	        foreach (ButterflyEffect script in queue) {
	            script.step(ticks_in_day);
	        }

	        dayCheck(ticks_in_day);
	    }

	    public void day_step(int ticks_in_day) {
	        foreach (ButterflyEffect script in queue) {
	            script.day_step(ticks_in_day);
	        }

	        monthCheck(ticks_in_day);
	    }

	    public void month_step(int ticks_in_day) {
	        foreach (ButterflyEffect script in queue) {
	            script.month_step(ticks_in_day);
	        }
	    }

	    public void add(ButterflyEffect script) {
	        queue.Add(script);
	    }

	    public void remove(ButterflyEffect script) {
	        queue.Remove(script);
	    }

	    private int local_ticks_in_day = 100; // 10 ticks in second, 10 seconds in day
	    private int stepsCount = 0;

		private void dayCheck(int ticks_in_day) {
			if (local_ticks_in_day != ticks_in_day) {
	            float flowMultiplier = (float) ticks_in_day / local_ticks_in_day;
	            // loosing steps at this moment, need to be checked (e.g. 91 * 0.33 = 30.03 to int 30)
	            stepsCount = (int) (stepsCount * flowMultiplier);

	            local_ticks_in_day = ticks_in_day;
	        }

	        if (stepsCount < local_ticks_in_day) {
	            stepsCount += 1;
	        } else {
	            stepsCount = 0;
	            
	            day_step(ticks_in_day);
	        }
	    }

	    private void monthCheck(int ticks_in_day) {

	    }
	}
}