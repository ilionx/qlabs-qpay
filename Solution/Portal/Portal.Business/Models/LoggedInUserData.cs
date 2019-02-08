namespace Portal.Business.Models
{
    public static class LoggedInUserData
    {
        public static bool DoesUserExist { get; set; }
        public static decimal Balance { get; set; }
        public static string CardId { get; set; }
        public static int AccountLevel { get; set; }
        public static string AmountTransactions { get; set; }
    }
}