using System.Reflection;

namespace Todo.Utils.Extensions
{
    public static class TypeExtensions
    {
        public static IEnumerable<string> GetStaticParamValues(this Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(prop => prop.GetValue(prop)?.ToString())
                .Where(value => value != null)!;
        }
    }
}
