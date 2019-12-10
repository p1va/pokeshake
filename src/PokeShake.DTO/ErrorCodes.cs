namespace PokeShake.Dto
{
    /// <summary>
    /// The error codes class
    /// </summary>
    public static class ErrorCodes
    {
        /// <summary>
        /// The not found generic error code
        /// </summary>
        public const string NotFound = "not_found";

        /// <summary>
        /// The invalid name generic error code
        /// </summary>
        public const string InvalidName = "invalid_name";

        /// <summary>
        /// The internal server error code
        /// </summary>
        public const string InternalServerError = "internal_server_error";

        /// <summary>
        /// The pokemon error codes class
        /// </summary>
        public static class Pokemon
        {
            /// <summary>
            /// The pokemon specific error code prefix
            /// </summary>
            public const string Prefix = "pokemon_";

            /// <summary>
            /// The pokemon not found error code
            /// </summary>
            public const string NotFound = Prefix + ErrorCodes.NotFound;

            /// <summary>
            /// The pokemon invalid name error code
            /// </summary>
            public const string InvalidName = Prefix + ErrorCodes.InvalidName;

            /// <summary>
            /// The internal server error code
            /// </summary>
            public const string InternalServerError = Prefix + ErrorCodes.InternalServerError;
        }
    }
}
