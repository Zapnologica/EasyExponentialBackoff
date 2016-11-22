# EasyExponentialBackoff
Easy Exponential Back-off Utils for C# threading enviroment

Use Case:

 private void Main()
        {
            try
            {
                Log.Debug("Starting Worker Thread");
             
                // Exponential Back-off 
                var eb = new ExponentialBackoff();
                while (_run)
                {
                    try
                    {
                        //Fetch Latest Item
                        QueueRetryHolder<byte[]> input;
                        if (IncomingQueue.TryDequeue(out input))
                        {
                            //Handle the logItem
                            ProcessItem(input, handler);
                            Interlocked.Increment(ref ProcessedInLastSec);
                            //Reset the exponential back off counter
                            eb.reset();
                        }
                        else
                        {
                            //Exponential back off thread sleep
                            eb.sleep();
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Exception occurred in while true loop in thread:", ex);
                    }
                }
                Log.Debug("Exiting Worker Thread: ");
            }
            catch (Exception ex)
            {
                Log.Error("Exception occurred in thread:", ex);
            }
        }
