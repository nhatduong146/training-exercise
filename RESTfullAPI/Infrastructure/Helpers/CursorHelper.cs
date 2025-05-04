using System.Text;

namespace RESTfullAPI.Infrastructure.Helpers;

public static class CursorHelper
{
    public static string Encode(DateTime createdOn, Guid id)
    {
        var raw = $"{createdOn:O}|{id}";
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(raw));
    }

    public static (DateTime? createdOn, Guid? id) Decode(string cursor)
    {
        try
        {
            var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(cursor));
            var parts = decoded.Split('|');
            return (DateTime.Parse(parts[0], null, System.Globalization.DateTimeStyles.RoundtripKind), Guid.Parse(parts[1]));
        }
        catch (Exception)
        {
            return (null, null);
        }
    }
}
