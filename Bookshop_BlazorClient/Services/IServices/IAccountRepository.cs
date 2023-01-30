namespace Bookshop_BlazorClient.Services.IServices;

public interface IAccountRepository
{
    public Task<RegisterationResponseDTO> SignUp(UserRequestDTO userRequest);
    public Task<AuthenticationResponseDTO> SignIn(AuthenticationDTO authentication);
    public Task LogOut();
    public Task<bool> ForgotPassword(ForgotPasswordDTO forgotPassword);
    //public Task<bool> ResetPasswordCodeConfrim(string code);
    public Task<bool> ResetPassword(ResetPasswordDTO resetPassword);
    public Task<bool> ChangePassword(ChangePasswordDTO changePasswordDTO,string token);

}
