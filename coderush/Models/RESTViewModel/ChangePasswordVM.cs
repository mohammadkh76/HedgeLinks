namespace HedgeLinks.Models
{
    public class ChangePasswordVM
    {
        public string SelectedId { get; set; }
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
