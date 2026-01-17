using FluentValidation;
using Submission.Service.DTOs.Requests;

namespace Submission.Service.Validators;

public class CreateSubmissionRequestValidator : AbstractValidator<CreateSubmissionRequest>
{
    public CreateSubmissionRequestValidator()
    {
        RuleFor(x => x.ConferenceId)
            .NotEmpty().WithMessage("Conference ID is required");

        // TrackId is optional - some conferences may not have tracks
        RuleFor(x => x.TrackId)
            .NotEmpty().WithMessage("Track ID is required")
            .When(x => x.TrackId.HasValue);

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(500).WithMessage("Title must not exceed 500 characters");

        RuleFor(x => x.Abstract)
            .NotEmpty().WithMessage("Abstract is required")
            .MaximumLength(5000).WithMessage("Abstract must not exceed 5000 characters");

        RuleFor(x => x.Keywords)
            .Must(keywords => keywords == null || keywords.Count <= 10)
            .WithMessage("Cannot have more than 10 keywords");

        RuleFor(x => x.Authors)
            .NotEmpty().WithMessage("At least one author is required")
            .Must(authors => authors != null && authors.Count <= 20)
            .WithMessage("Cannot have more than 20 authors");
    }
}

public class UpdateSubmissionRequestValidator : AbstractValidator<UpdateSubmissionRequest>
{
    public UpdateSubmissionRequestValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(500).WithMessage("Title must not exceed 500 characters")
            .When(x => !string.IsNullOrEmpty(x.Title));

        RuleFor(x => x.Abstract)
            .MaximumLength(5000).WithMessage("Abstract must not exceed 5000 characters")
            .When(x => !string.IsNullOrEmpty(x.Abstract));

        RuleFor(x => x.Keywords)
            .Must(keywords => keywords == null || keywords.Count <= 10)
            .WithMessage("Cannot have more than 10 keywords")
            .When(x => x.Keywords != null);

        RuleFor(x => x.Authors)
            .Must(authors => authors == null || authors.Count <= 20)
            .WithMessage("Cannot have more than 20 authors")
            .When(x => x.Authors != null);
    }
}

public class CreateAuthorRequestValidator : AbstractValidator<CreateAuthorRequest>
{
    public CreateAuthorRequestValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required")
            .MaximumLength(200).WithMessage("Full name must not exceed 200 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format")
            .MaximumLength(255).WithMessage("Email must not exceed 255 characters");

        RuleFor(x => x.Affiliation)
            .MaximumLength(255).WithMessage("Affiliation must not exceed 255 characters");

        RuleFor(x => x.OrderIndex)
            .GreaterThanOrEqualTo(0).WithMessage("Order index must be non-negative");
    }
}

public class UploadFileRequestValidator : AbstractValidator<UploadFileRequest>
{
    private readonly long _maxFileSize = 10 * 1024 * 1024; // 10MB
    private readonly string[] _allowedExtensions = { ".pdf", ".doc", ".docx" };

    public UploadFileRequestValidator()
    {
        RuleFor(x => x.File)
            .NotNull().WithMessage("File is required")
            .Must(file => file.Length <= _maxFileSize)
            .WithMessage($"File size must not exceed {_maxFileSize / 1024 / 1024}MB")
            .Must(file => _allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            .WithMessage($"Only {string.Join(", ", _allowedExtensions)} files are allowed");

        RuleFor(x => x.FileType)
            .NotEmpty().WithMessage("File type is required")
            .Must(type => new[] { "PAPER", "SUPPLEMENTARY", "CAMERA_READY" }.Contains(type))
            .WithMessage("Invalid file type");

        RuleFor(x => x.Version)
            .GreaterThanOrEqualTo(1).WithMessage("Version must be at least 1");
    }
}
