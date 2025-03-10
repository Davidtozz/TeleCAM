using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenCleanupTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION delete_old_refresh_tokens_fn()
                RETURNS TRIGGER AS $$
                BEGIN
                    -- Delete all previous refresh tokens for this user, except the newly inserted one
                    DELETE FROM ""RefreshTokens"" 
                    WHERE ""Id"" != NEW.""Id"";                    
                    RETURN NEW;
                END;
                $$ LANGUAGE plpgsql;
            ");

            migrationBuilder.Sql(@"
                CREATE OR REPLACE TRIGGER delete_old_refresh_tokens_trigger
                AFTER INSERT ON ""RefreshTokens""
                FOR EACH ROW
                EXECUTE FUNCTION delete_old_refresh_tokens_fn();
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS delete_old_refresh_tokens_trigger ON ""RefreshTokens"";");    
            migrationBuilder.Sql(@"DROP FUNCTION IF EXISTS delete_old_refresh_tokens_fn();");        
        }
    }
}
