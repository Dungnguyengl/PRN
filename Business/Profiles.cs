using AutoMapper;
using Model.Entities;
using ViewModel.Dtos;

namespace ViewModel
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            _ = CreateMap<Order, OrderDto>()
                .ForMember(des => des.Member, opt => opt.Ignore())
                .ForMember(des => des.Details, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(des => des.MemberId, opt => opt.MapFrom(src => src.Member.Id));
            _ = CreateMap<OrderDetail, OrderDetailDto>()
                .ReverseMap()
                .ForMember(des => des.OrderId, opt => opt.Ignore());

            _ = CreateMap<Member, MemberDto>()
                .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Email));
            _ = CreateMap<Product, ProductDto>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.ProductName, opt => opt.MapFrom(src => src.ProductName));
            _ = CreateMap<Product, OrderDetailDto>()
                .ForMember(des => des.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(des => des.Quantity, opt => opt.Ignore())
                .ForMember(des => des.Discount, opt => opt.Ignore());
            _ = CreateMap<ViewGetOrder, OrderDto>()
                .ForMember(des => des.Details, opt => opt.Ignore())
                .ForMember(des => des.Member, opt => opt.MapFrom(src => new MemberDto { Id = src.MemberId, Name = src.Email }))
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.OrderId));
            _ = CreateMap<ViewGetOrderDetail, OrderDetailDto>();
        }
    }
}
