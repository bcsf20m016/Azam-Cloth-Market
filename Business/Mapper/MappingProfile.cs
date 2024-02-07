using AutoMapper;
using DataAccess.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Store, StoreDTO>().ReverseMap();
            CreateMap<StoreAttributes, StoreAttributesDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<CategoryAttributes, CategoryAttributesDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<ProductAttributes, ProductAttributesDTO>().ReverseMap();
            CreateMap<ProductImage, ProductImageDTO>().ReverseMap();
            CreateMap<IPAddress, UserIpDTO>().ReverseMap();
            CreateMap<StoreImage, StoreImageDTO>().ReverseMap();
            CreateMap<TagType, TagTypeDTO>().ReverseMap();
            CreateMap<Tag, TagDTO>().ReverseMap();
            CreateMap<StoreTags, StoreTagDTO>().ReverseMap();
            CreateMap<CountDetails, CountDetailsDTO>().ReverseMap();
        }
    }
}
