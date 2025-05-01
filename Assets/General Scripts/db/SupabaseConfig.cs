
public class SupabaseConfig
{
    public static string Url = System.Environment.GetEnvironmentVariable("SUPABASE_URL") ?? "";
    public static string ApiKey = System.Environment.GetEnvironmentVariable("SUPABASE_ANON_KEY") ?? "";
    
}