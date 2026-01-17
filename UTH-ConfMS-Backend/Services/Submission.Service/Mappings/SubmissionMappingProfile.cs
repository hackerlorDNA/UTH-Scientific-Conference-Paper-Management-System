using AutoMapper;
using Submission.Service.DTOs.Common;
using Submission.Service.DTOs.Requests;
using Submission.Service.DTOs.Responses;
using Submission.Service.Entities;
using SubmissionEntity = Submission.Service.Entities.Submission;

namespace Submission.Service.Mappings;

public class SubmissionMappingProfile : Profile
{
    public SubmissionMappingProfile()
    {
        // Submission mappings
        CreateMap<SubmissionEntity, SubmissionDto>()
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors));

        CreateMap<SubmissionEntity, SubmissionDetailDto>()
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors))
            .ForMember(dest => dest.Files, opt => opt.MapFrom(src => src.Files));

        CreateMap<CreateSubmissionRequest, SubmissionEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PaperNumber, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "DRAFT"))
            .ForMember(dest => dest.Authors, opt => opt.Ignore())
            .ForMember(dest => dest.Files, opt => opt.Ignore());

        CreateMap<UpdateSubmissionRequest, SubmissionEntity>()
            .ForMember(dest => dest.Authors, opt => opt.Ignore())
            .ForMember(dest => dest.Files, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Author mappings
        CreateMap<Author, AuthorDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AuthorId));
        CreateMap<CreateAuthorRequest, Author>()
            .ForMember(dest => dest.AuthorId, opt => opt.Ignore())
            .ForMember(dest => dest.AuthorOrder, opt => opt.MapFrom(src => src.OrderIndex))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Submission, opt => opt.Ignore())
            .ForMember(dest => dest.SubmissionId, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore());

        // SubmissionFile mappings
        CreateMap<SubmissionFile, SubmissionFileDto>();
        CreateMap<SubmissionFile, FileInfoDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FileId));
        CreateMap<CreateSubmissionFileRequest, SubmissionFile>()
            .ForMember(dest => dest.FileId, opt => opt.Ignore())
            .ForMember(dest => dest.SubmissionId, opt => opt.Ignore())
            .ForMember(dest => dest.Submission, opt => opt.Ignore())
            .ForMember(dest => dest.UploadedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        // SubmissionStatistics mappings
        CreateMap<SubmissionStatistics, SubmissionStatisticsDto>();
    }
}
