using AutoMapper;
using AutomobileInsuranceSystem.Models;
using AutomobileInsuranceSystem.Models.DTOs;
using AutomobileInsuranceSystem.Mapping;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AutomobileInsuranceSystem.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, RegisterDTO>().ReverseMap();
            CreateMap<User, LoginDTO>().ReverseMap();
            CreateMap<Proposal, ProposalDTO>().ReverseMap();
            CreateMap<Policy, PolicyDTO>().ReverseMap();
            CreateMap<Quote, QuoteDTO>().ReverseMap();
            CreateMap<Payment, PaymentDTO>().ReverseMap();
            CreateMap<Review, ReviewDTO>().ReverseMap();
            CreateMap<Document, DocumentDTO>().ReverseMap();
        }
    }

}
