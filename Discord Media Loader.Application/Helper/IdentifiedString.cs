namespace DML.Application.Helper
{
    internal class IdentifiedString<T>
    {
        internal IdentifiedString(T id, string caption)
        {
            Id = id;
            Caption = caption;
        }

        internal T Id { get; set; }
        internal string Caption { get; set; }

        public override string ToString()
        {
            return Caption;
        }
    }
}