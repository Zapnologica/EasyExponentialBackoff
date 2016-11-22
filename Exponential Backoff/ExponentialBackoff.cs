using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zapnologica.Utils.ExponentialBackoff
{
    /// <summary>
    /// Class used to do exponential back off timing for threads, Implements a floor and a roof 
    /// </summary>
    public class ExponentialBackoff
    {
        # region Class Variables
        private readonly int _minInterval;
        private int _interval;
        private readonly int _exponent;
        private readonly int _maxInterval;
        # endregion Class Variables

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ExponentialBackoff()
        {
            _minInterval = 1;
            _interval = _minInterval;
            _exponent = 2;
            _maxInterval = 1000;
        }

        /// <summary>
        ///  Constructor taking in min and max parameters
        /// </summary>
        /// <param name="min">the minimum time that the thread should block for in milliseconds. Bigger than ZERO</param>
        /// <param name="max">the maximum time that the thread should block for in milliseconds</param>
        /// <param name="exponent">the exponent to apply to the waiting. Default is 2</param>
        public ExponentialBackoff(int min, int max, int exponent = 2)
        {
            if (min < 1)
            {
                throw new ArgumentOutOfRangeException(@"min time has to be bigger than zero.");
            }
            _minInterval = min;
            _interval = _minInterval;
            _exponent = exponent;
            _maxInterval = max;
        }

        # region main working space

        /// <summary>
        /// Called to reset the Exponential Backoff Counter
        /// </summary>
        public void Reset()
        {
            _interval = _minInterval;
        }

        /// <summary>
        /// Call this to block for a calculated period of time
        /// </summary>
        public void Sleep()
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(_interval));
            _interval = GetInterval();
        }

        /// <summary>
        /// Call this to wait async for a calculated period of time
        /// </summary>
        public async Task SleepAsync()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(_interval));
            _interval = GetInterval();
        }
        # endregion main working space


        /// <summary>
        /// Get the next interval time
        /// </summary>
        /// <returns></returns>
        private int GetInterval()
        {
            return Math.Min(_maxInterval, _interval * _exponent);
        }
    }
}
