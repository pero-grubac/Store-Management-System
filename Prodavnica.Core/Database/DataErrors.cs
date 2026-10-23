using MySqlConnector;

namespace Prodavnica.Database
{
    // Database failures the user can act on, translated from MySQL error numbers and SIGNAL messages.
    public class StoreDataException : Exception
    {
        public StoreDataException(string code, Exception? inner = null) : base(code, inner)
        {
            Code = code;
        }

        // A stable key (also a translation key in the desktop app), e.g. "INSUFFICIENT_STOCK".
        public string Code { get; }
    }

    public sealed class DuplicateValueException : StoreDataException
    {
        public DuplicateValueException(Exception inner) : base("DUPLICATE_VALUE", inner) { }
    }

    public sealed class InUseException : StoreDataException
    {
        public InUseException(Exception inner) : base("IN_USE", inner) { }
    }

    public sealed class InsufficientStockException : StoreDataException
    {
        public InsufficientStockException(Exception inner) : base("INSUFFICIENT_STOCK", inner) { }
    }

    public static class DataErrors
    {
        private const int DuplicateKey = 1062;
        private const int RowIsReferenced = 1451;
        private const int RowIsReferencedOld = 1217;
        private const int Signal = 1644;
        private const int CheckViolated = 3819;

        public static Exception Translate(MySqlException exception)
        {
            return exception.Number switch
            {
                DuplicateKey => new DuplicateValueException(exception),
                RowIsReferenced or RowIsReferencedOld => new InUseException(exception),
                Signal when exception.Message == "INSUFFICIENT_STOCK" => new InsufficientStockException(exception),
                Signal => new StoreDataException(exception.Message, exception),
                CheckViolated => new StoreDataException("INVALID_VALUE", exception),
                _ => exception
            };
        }
    }
}
