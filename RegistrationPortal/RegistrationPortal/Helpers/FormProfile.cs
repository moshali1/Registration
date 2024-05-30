using AutoMapper;

namespace RegistrationPortal.Helpers;

public class FormProfile : Profile
{
    public FormProfile()
    {
        CreateMap<Form, FormDto>();
        CreateMap<FormDto, Form>();

        CreateMap<PersonalInfo, PersonalInfoDto>();
        CreateMap<PersonalInfoDto, PersonalInfo>();

        CreateMap<AddressInfo, AddressInfoDto>();
        CreateMap<AddressInfoDto, AddressInfo>();

        CreateMap<CompetitionInfo, CompetitionInfoDto>();
        CreateMap<CompetitionInfoDto, CompetitionInfo>();

        CreateMap<ParentInfo, ParentInfoDto>();
        CreateMap<ParentInfoDto, ParentInfo>();

        CreateMap<TeacherInfo, TeacherInfoDto>();
        CreateMap<TeacherInfoDto, TeacherInfo>();

        CreateMap<StatusInfo, StatusInfoDto>();
        CreateMap<StatusInfoDto, StatusInfo>();

        CreateMap<FileUploadInfo, FileUploadInfoDto>();
        CreateMap<FileUploadInfoDto, FileUploadInfo>();

        CreateMap<Tracking, TrackingDto>();
        CreateMap<TrackingDto, Tracking>();

        CreateMap<EventScheduleInfo, EventScheduleInfoDto>();
        CreateMap<EventScheduleInfoDto, EventScheduleInfo>();
    }
}
