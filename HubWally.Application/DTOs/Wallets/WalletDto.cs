using FluentValidation;
using HubWally.Application.Services.IServices; 

namespace HubWally.Application.DTOs.Wallets
{
    public class WalletDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string? AccountNumber { get; set; }
        public string AccountScheme { get; set; }
        public string Owner { get; set; }
    }

    //TODO: Move validators to separate files
    public class CreateWalletDtoValidator : AbstractValidator<WalletDto>
    {
        private readonly IWalletService _service;
        public CreateWalletDtoValidator(IWalletService service)
        {
            _service = service;
            RuleFor(w => w.Name).NotNull().NotEmpty();
            RuleFor(w => w.Type).NotEmpty().Must(w => w == "momo" || w == "card")
            .WithMessage("Type must be either 'momo' or 'card'.");
            RuleFor(w => w.AccountNumber).NotNull().NotEmpty().MinimumLength(6).
                WithMessage("AccountNumber must not be less than 6 characters long.");
            RuleFor(w => w.AccountScheme).NotNull().NotEmpty().Must(w => w.ToLower() == "mastercard" || w.ToLower() == "mtn" || w.ToLower() == "vodafone" || w.ToLower() == "visa" || w.ToLower() == "airteltigo").
                WithMessage("AccountScheme must be visa, mastercard, mtn, vodafone, airteltigo");
            RuleFor(x => x.Owner)
            .NotNull()
            .NotEmpty()
            .MinimumLength(6)
            .MustAsync(async (userId, cancellation) => await HasLessThanFiveRecordsAsync(userId))
            .WithMessage("User cannot have more than five wallets.");
        }

        private async Task<bool> HasLessThanFiveRecordsAsync (string owner)
        {
            var walletCount = await _service.GetWalletsCountByOwner(owner);
            return walletCount < 5;
        }
    }
    public class UpdateWalletDtoValidator : AbstractValidator<WalletDto>
    { 
        public UpdateWalletDtoValidator()
        { 
            RuleFor(w => w.Name).NotEmpty();
            RuleFor(w => w.Type).NotEmpty().Must(w => w == "momo" || w == "card")
            .WithMessage("Type must be either 'momo' or 'card'.");
            RuleFor(w => w.AccountNumber).NotEmpty().MinimumLength(6).
                WithMessage("AccountNumber must not be less than 6 characters long.");
            RuleFor(w => w.AccountScheme).NotEmpty().Must(w => w.ToLower() == "mastercard" || w.ToLower() == "mtn" || w.ToLower() == "vodafone" || w.ToLower() == "visa" || w.ToLower() == "airteltigo").
                WithMessage("AccountScheme must be visa, mastercard, mtn, vodafone, airteltigo");
            RuleFor(x => x.Owner)
            .NotEmpty()
            .MinimumLength(6);
        }
    }

}
