using System;
using System.Threading;

public class Program
{
    static event Action<Exception> ErrorOccurred;
    public static void Main(string[] args)
    {
        ErrorOccurred += HandleError;
        // Create a new thread
        Thread thread = new Thread(BackGroundThread);
        //start the thread
        thread.Start();
        Console.WriteLine("Main thread continues...");

        //Work in the main thread
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"Main thread working... {i}");
            Thread.Sleep(1000);
        }

        // Wait for the second thread to complete
        thread.Join();

        ErrorOccurred -= HandleError;
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    public static void BackGroundThread()
    {
        try
        {
            //Work in the BackGround thread
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"BackGround thread working... {i}");
                Thread.Sleep(500);

                //Exception Handle
                if (i == 4)
                    throw new Exception("Something went wrong in the BackGround thread!");
            }
        }
        catch (Exception ex)
        {
            //Console.WriteLine($"Exception caught in the BackGround thread: {ex.Message}");
            // Log the exception or perform any necessary actions
            ErrorOccurred?.Invoke(ex);
        }

    }
        public static void HandleError(Exception ex)
        {
            // Handle or report the error in the main thread or user interface
            Console.WriteLine($"Exception caught in the BackGround thread: {ex.Message}");
        }
    
}

