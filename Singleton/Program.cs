

using Singleton.Thread_Safe;

class Program
{
    //static MemoryLogger logger;
    private static MemoryLoggerLazyLoading logger;

    static void Main()
    {
        AssignVoucher("faris.abuali@outlook.com", "Faris@123");

        UseVoucher("Faris@123");

        logger.ShowLog();

        Console.ReadKey();
    }

    static void AssignVoucher(string email, string voucher)
    {
        logger = MemoryLoggerLazyLoading.GetLogger;

        // Logic here
        logger.LogInfo($"Voucher '{voucher}' assigned");

        // Another Logic
        logger.LogError($"Unable to send email '{email}'");
    }

    static void UseVoucher(string voucher)
    {
        logger = MemoryLoggerLazyLoading.GetLogger;

        // Logic here
        logger.LogWarning("3 attempts made to validate the voucher");

        // Loguc here
        logger.LogInfo($"'{voucher}' is used");
    }
}