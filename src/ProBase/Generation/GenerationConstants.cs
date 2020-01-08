namespace ProBase.Generation
{
    /// <summary>
    /// Represents application-wide code generation constants.
    /// </summary>
    internal class GenerationConstants
    {
        /// <summary>
        /// The name of the procedure mapper used to map procedures to their result type.
        /// </summary>
        public const string ProcedureMapperFieldName = "procedureMapper";

        /// <summary>
        /// The name of the <see cref="System.Data.Common.DbProviderFactory"/> used for ADO.NET object creation.
        /// </summary>
        public const string ProviderFactoryFieldName = "providerFactory";
    }
}
