using AutoMapper;
using Fire.ViewModels.Product;
using Fire.ViewModels.User;


namespace Fire.ViewModels.AutoMapperProfiles
{
    public class FirefighterProfile: Profile
    {
        public FirefighterProfile()
        {
            
            CreateMap<Models.User, InputUserViewModels>().ReverseMap();
            CreateMap<Models.User, DeleteUserViewModels>();
            CreateMap<Models.User, EditUserViewModels>().ReverseMap();
            CreateMap<Models.User, UserViewModels>();
            CreateMap<Models.Person, InputUserViewModels>().ReverseMap();
            CreateMap<Models.Product, InputProductViewModels>().ReverseMap();
            CreateMap<Models.Product, DeleteProductViewModels>();
            CreateMap<Models.Product, EditProductViewModels>().ReverseMap();
            CreateMap<Models.Product, ProductViewModels>();
        }
    }
}
