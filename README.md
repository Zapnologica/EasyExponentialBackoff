# EasyExponentialBackoff
Easy Exponential Back-off Util for C# threading enviroment

Implementes the exponential backoff algorythim.

Generally used when a thread is polling for a certain outcome.

Use Case:

     private void Main()
        {
            try
            {
                Log.Debug("Starting Worker Thread");
                
                // Exponential Back-off 
                var eb = new ExponentialBackoff();
                
                // run for duration of applicaion
                while (_run)
                {
                    try
                    {
                        // Fetch work item
                        var item = GetWorkItemFromSomeWhere();                        
                        if (item != null)
                        {
                            //Handle the work item
                            // TODO: Do work on item                            
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
