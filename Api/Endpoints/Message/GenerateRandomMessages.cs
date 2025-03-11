using Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Message;

public sealed partial class MessageEndpoint
{
    private async Task<IResult> GenerateRandomMessages(
        [FromRoute] int count,
        TelecamContext context
    )
    {
        return Results.Ok(
            await TestingUtils.GenerateRandomMessagesAsync(
                context,
                chat: (
                    new Guid("0d7b6dfa-41a0-4107-a4a2-9ffb0c082d27"), 
                    new Guid("eab97026-79ac-40e3-aa6a-db91bf99e687")
                ),
                count
            )
        );
    }
}