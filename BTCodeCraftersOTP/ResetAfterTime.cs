namespace BTCodeCraftersOTP
{
    public class ResetAfterTime : BackgroundService
    {
        //Logger:
        private ILogger<ResetAfterTime> _logger;

        public ResetAfterTime(ILogger<ResetAfterTime> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string dataFromFile = "";
            //Write first time:
            StreamWriter writer1 = null;
            try
            {
                writer1 = new StreamWriter("../EncryptedOTP.txt");
                writer1.WriteLine("");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                //Close file:
                writer1.Close();
            }


            while (!stoppingToken.IsCancellationRequested)
            {
                if(dataFromFile.Equals("") != true)
                {
                    //5 seconds:
                    //var howMuchDelay = 5;
                    //await Task.Delay(howMuchDelay * 1000, stoppingToken);

                    Console.WriteLine("OTP: " + dataFromFile + " ;");

                    var howMuchDelay = 30; // 30;
                    await Task.Delay(howMuchDelay * 1000, stoppingToken);

                    //Empty the OTP:
                    //Write:
                    StreamWriter writer2 = null;
                    try
                    {
                        writer2 = new StreamWriter("../EncryptedOTP.txt");
                        writer2.WriteLine("");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        //Close file:
                        writer2.Close();
                    }

                    //Reset also:
                    dataFromFile = "";
                }
                else
                {
                    //Read:
                    StreamReader reader = null;
                    try
                    {
                        reader = new StreamReader("../EncryptedOTP.txt");

                        dataFromFile = reader.ReadLine();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        //Close file:
                        reader.Close();
                    }
                    Console.WriteLine("File data: " + dataFromFile + "is empty;");

                    var howMuchDelay = 1;
                    await Task.Delay(howMuchDelay * 1000, stoppingToken);
                }
            }
        }
    }
}
