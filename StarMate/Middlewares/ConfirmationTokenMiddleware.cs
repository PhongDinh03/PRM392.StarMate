using Application.IRepository;

namespace StarMate.Middlewares
{
    public class ConfirmationTokenMiddleware
    {
        private readonly RequestDelegate _next;


        public ConfirmationTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // create area service temporary
            using (var scope = context.RequestServices.CreateScope())
            {
                // Get the IUnitOfWork from the temporary service scope
                var repo = scope.ServiceProvider.GetRequiredService<IUserRepo>();

                var token = context.Request.Query["token"];

                if (!string.IsNullOrEmpty(token))
                {
                    var user = await repo.GetUserByConfirmationToken(token);

                    if (user != null && !user.IsConfirmed)
                    {
                        // confirm
                        user.IsConfirmed = true;
                        user.ConfirmationToken = null;
                        await repo.SaveChangeAsync();
                        context.Response.Redirect("https://starmate-g8dkcraeardagdfb.canadacentral-01.azurewebsites.net/");
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}