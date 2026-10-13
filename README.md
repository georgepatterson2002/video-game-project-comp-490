

# Local integration setup

This branch includes an optional Supabase integration. Set `SUPABASE_URL` and
`SUPABASE_ANON_KEY` in the environment of the Unity Editor or desktop game
process before using sign-up or login. `SUPABASE_ANON_KEY` must be a public
client key, never an administrator/service-role key. Configure database access
policies in your own Supabase project. No shared project configuration is included.
