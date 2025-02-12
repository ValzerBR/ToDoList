using System.Runtime.CompilerServices;

namespace ToDo.Util
{
    public static class Methods
    {
        public static bool IsNull(this object? source)
        {
            return source == null;
        }

        public static bool IsEmpty(this object source)
        {
            return source == "";
        }

        //sobrecarga
        public static string ToDateBR(this DateTime? data)
        {
            return data.HasValue ? data.Value.ToString("dd/MM/yyyy") : string.Empty;
        }

        public static string ToDateBR(this DateTime data)
        {
            return data.ToString("dd/MM/yyyy");
        }

        // Método de extensão genérico que formata o nome de um valor enum
        public static string GetEnumName<TEnum>(this TEnum enumValue) where TEnum : Enum
        {
            return Enum.GetName(typeof(TEnum), enumValue);
        }
    }
}
