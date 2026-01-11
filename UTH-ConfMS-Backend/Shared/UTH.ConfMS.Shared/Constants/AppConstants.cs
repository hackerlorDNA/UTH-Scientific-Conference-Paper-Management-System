namespace UTH.ConfMS.Shared.Constants;

public static class Roles
{
    public const string SystemAdmin = "SYSTEM_ADMIN";
    public const string ConferenceChair = "CONFERENCE_CHAIR";
    public const string TrackChair = "TRACK_CHAIR";
    public const string PCMember = "PC_MEMBER";
    public const string Reviewer = "REVIEWER";
    public const string Author = "AUTHOR";
    public const string Guest = "GUEST";
}

public static class Permissions
{
    // Conference permissions
    public const string ConferenceCreate = "conference.create";
    public const string ConferenceRead = "conference.read";
    public const string ConferenceUpdate = "conference.update";
    public const string ConferenceDelete = "conference.delete";
    public const string ConferenceManage = "conference.manage";

    // Submission permissions
    public const string SubmissionCreate = "submission.create";
    public const string SubmissionRead = "submission.read";
    public const string SubmissionUpdate = "submission.update";
    public const string SubmissionDelete = "submission.delete";
    public const string SubmissionManage = "submission.manage";

    // Review permissions
    public const string ReviewCreate = "review.create";
    public const string ReviewRead = "review.read";
    public const string ReviewUpdate = "review.update";
    public const string ReviewDelete = "review.delete";
    public const string ReviewManage = "review.manage";

    // User permissions
    public const string UserCreate = "user.create";
    public const string UserRead = "user.read";
    public const string UserUpdate = "user.update";
    public const string UserDelete = "user.delete";
    public const string UserManage = "user.manage";

    // System permissions
    public const string SystemAudit = "system.audit";
    public const string SystemSettings = "system.settings";
    public const string SystemBackup = "system.backup";
}

public static class SubmissionStatus
{
    public const string Draft = "DRAFT";
    public const string Submitted = "SUBMITTED";
    public const string UnderReview = "UNDER_REVIEW";
    public const string Reviewed = "REVIEWED";
    public const string Accepted = "ACCEPTED";
    public const string Rejected = "REJECTED";
    public const string Withdrawn = "WITHDRAWN";
}

public static class ReviewStatus
{
    public const string Pending = "PENDING";
    public const string Accepted = "ACCEPTED";
    public const string Declined = "DECLINED";
    public const string Completed = "COMPLETED";
    public const string Overdue = "OVERDUE";
}

public static class DecisionTypes
{
    public const string Accept = "ACCEPT";
    public const string Reject = "REJECT";
    public const string MajorRevision = "MAJOR_REVISION";
    public const string MinorRevision = "MINOR_REVISION";
    public const string ConditionalAccept = "CONDITIONAL_ACCEPT";
}

public static class ConferenceStatus
{
    public const string Draft = "DRAFT";
    public const string Published = "PUBLISHED";
    public const string Active = "ACTIVE";
    public const string Completed = "COMPLETED";
    public const string Cancelled = "CANCELLED";
}
